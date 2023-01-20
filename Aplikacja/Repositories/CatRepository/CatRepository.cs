using Aplikacja.DTOS.CatDto;
using Aplikacja.Entities.CatModels;
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

            public async Task<CatDTO> GetCat(int catId)
            {
                var cat = await _context.Cats
                    .Include(r => r.User)
                    .Include(r => r.CatRecords)
                    .ThenInclude(h => h.CatRecordHours)
                    .ProjectTo<CatDTO>(_mapper.ConfigurationProvider)
                    .AsNoTracking()
                    .SingleAsync(c => c.CatId == catId);

                return cat;
            }

        public async Task<List<Cat>> GetCats()
        {
            return await _context.Cats.AsNoTracking().OrderBy(d => d.CatCreated).ToListAsync();
        }

        public async Task<double> CreateCat(int userId, int inboxItemId,DateTime entryDate)
        {
            Cat? userCat;
            var catUser = await _context.Users.SingleAsync(u => u.UserId == userId);
            var catExist = await _context.Cats.AnyAsync(u => u.UserId == catUser.UserId && u.CatCreated == entryDate.ToString("yyyy MM"));
            if (catExist)
            {
                userCat = await _context.Cats.SingleAsync(u => u.UserId == catUser.UserId && u.CatCreated == entryDate.ToString("yyyy MM"));
            }
            else
            {
                userCat = new Cat()
                {
                    CatCreated = entryDate.ToString("yyyy MM"),
                    UserId = catUser.UserId,
                };
                await _context.Cats.AddAsync(userCat);
                await _context.SaveChangesAsync();
            }

            var inboxItem = await _context.InboxItems.Include(j => j.Job).SingleAsync(r => r.InboxItemId == inboxItemId);

            CatRecord? catRecord;
            bool recordExist = await _context.CatRecords.AnyAsync(r => r.InboxItemId == inboxItemId);
            if (recordExist)
            {
                catRecord = await _context.CatRecords.SingleAsync(r => r.InboxItemId == inboxItemId);
            }
            else
            {
                catRecord = new CatRecord()
                {
                    CatId = userCat.CatId,
                    InboxItemId = inboxItemId,
                    Receiver = "RECEIVER DEPEND",
                    SapText = $"NA_{inboxItem.Job.ProjectNumber}_{inboxItem.Job.Client}_{inboxItem.Job.ProjectName}",
                };
                await _context.CatRecords.AddAsync(catRecord);
                await _context.SaveChangesAsync();

            }

            CatRecordHours? catRecordHour;
            bool hoursExits = await _context
                .CatRecordHourss
                .AnyAsync(c => c.CatRecordId == catRecord.CatRecordId
                && c.Day == entryDate.Day);

            if (hoursExits)
            {
                catRecordHour = await _context
                .CatRecordHourss
                .SingleAsync(c => c.CatRecordId == catRecord.CatRecordId
                && c.Day == entryDate.Day);
                catRecordHour.Hours = inboxItem.Hours;
            }
            else
            {
                catRecordHour = new CatRecordHours()
                {
                    CatRecordId = catRecord.CatRecordId,
                    Hours = inboxItem.Hours,
                    Day = entryDate.Day
                };
                await _context.CatRecordHourss.AddAsync(catRecordHour);
            }
            await _context.SaveChangesAsync();
            double sumOfHours = await _context.CatRecordHourss
                .Where(r => r.CatRecordId == catRecord.CatRecordId)
                .SumAsync(h => h.Hours);
            return sumOfHours;
        }

        public async Task<double> DeleteCat(int inboxItemId, DateTime entryDate)
        {
            CatRecord catRecord = await _context.CatRecords.SingleAsync(r => r.InboxItemId == inboxItemId);

            if (catRecord is null) throw new BadHttpRequestException("Cat not found");

            CatRecordHours catRecordHour = await _context.CatRecordHourss
                .SingleAsync(h => h.CatRecordId == catRecord.CatRecordId &&
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
