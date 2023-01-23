using Aplikacja.DTOS.CatDto;
using Aplikacja.DTOS.UserDtos;
using Aplikacja.Entities.CatModels;
using Aplikacja.Entities.InboxModel;
using Aplikacja.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;


namespace Aplikacja.Repositories.CatRepository
{
    public class CatRepository:ICatRepository
    {

            private readonly ApplicationDbContext _context;
            private readonly IDistributedCache _cache;
            private readonly IMapper _mapper;

            public CatRepository(ApplicationDbContext context, IDistributedCache cache, IMapper mapper)
            {
                _context = context ?? throw new ArgumentNullException(nameof(context));
                _cache = cache ?? throw new ArgumentNullException(nameof(cache));
                _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

            public async Task<UserDto> GetCat(Guid userId)
            {
                var cat = await _context.Users
                    .Include(r => r.CatRecords)
                    .ThenInclude(h => h.CatRecordHours)
                    .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                    .AsNoTracking()
                    .SingleAsync(c => c.UserId == userId);

                return cat;
            }

        public async Task<List<CatRecord>> GetCats()
        {
            return await _context.CatRecords.AsNoTracking().OrderBy(y => y.Year).ThenBy(m => m.Month).ToListAsync();
        }

        public async Task<double> CreateCat(Guid userId, Guid inboxItemId,DateTime entryDate)
        {
            InboxItem? inboxItem = await _context.InboxItems.Include(j => j.Job).FirstOrDefaultAsync(r => r.InboxItemId == inboxItemId);
            
            CatRecord? userCatRecord = await _context.CatRecords.FirstOrDefaultAsync(u => u.UserId == userId && u.Year == entryDate.Year && u.Month == entryDate.Month);
  
            if (userCatRecord is null)
            {
                userCatRecord = new CatRecord()
                {
                    Year = entryDate.Year,
                    Month = entryDate.Month,
                    UserId = userId,
                    InboxItemId = inboxItemId,
                    SapText = $"{inboxItem.Job.Region}_{inboxItem.Job.ProjectNumber}_{inboxItem.Job.Client}_{inboxItem.Job.ProjectName}",
                };

                switch (userCatRecord.InboxItem.Job.Region)
                {
                    case "NA":
                        userCatRecord.Receiver = "RECIVE FOR NA";
                        break;

                    case "CN":
                        userCatRecord.Receiver = "RECIVE FOR CN";
                        break;

                    case "IN":
                        userCatRecord.Receiver = "RECIVE FOR IN";
                        break;

                    default:
                        userCatRecord.Receiver = "RECIVE FOR RYB";
                        break;
                }

                await _context.CatRecords.AddAsync(userCatRecord);
                await _context.SaveChangesAsync();
            }

            CatRecordHours? catRecordHour = await _context
                .CatRecordHourss
                .FirstOrDefaultAsync(c => c.CatRecordId == userCatRecord.CatRecordId
                    && c.Day == entryDate.Day);


            if (catRecordHour is not null)
            {
                catRecordHour.Hours = inboxItem.Hours;
            }
            else
            {
                catRecordHour = new CatRecordHours()
                {
                    CatRecordId = userCatRecord.CatRecordId,
                    Hours = inboxItem.Hours,
                    Day = entryDate.Day
                };
                await _context.CatRecordHourss.AddAsync(catRecordHour);
            }
            await _context.SaveChangesAsync();
            double sumOfHours = await _context.CatRecordHourss
                .Where(r => r.CatRecordId == userCatRecord.CatRecordId)
                .SumAsync(h => h.Hours);
            return sumOfHours;
        }

        public async Task<double> DeleteCat(Guid inboxItemId, DateTime entryDate)
        {
            CatRecord? catRecord = await _context.CatRecords.FirstOrDefaultAsync(r => r.InboxItemId == inboxItemId);

            if (catRecord is null) throw new BadHttpRequestException("Cat not found");

            CatRecordHours? catRecordHour = await _context.CatRecordHourss
                .FirstOrDefaultAsync(h => h.CatRecordId == catRecord.CatRecordId &&
                h.Day == entryDate.Day);
            _context.CatRecordHourss.Remove(catRecordHour);
            await _context.SaveChangesAsync();

            double sumOfHours = await _context.CatRecordHourss
                .Where(r => r.CatRecordId == catRecord.CatRecordId)
                .SumAsync(h => h.Hours);
            return sumOfHours;
        }
    }   
}
