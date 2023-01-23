using Aplikacja.DTOS.RaportDto;
using Aplikacja.Entities.RaportModels;
using Aplikacja.Extensions;
using Aplikacja.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace Aplikacja.Repositories.RaportRepository
{
    public class RaportRepository : IRaportRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IDistributedCache _cache;
        private readonly IMapper _mapper;
        public RaportRepository(ApplicationDbContext context, IDistributedCache cache, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<RaportDTO> GetRaport(Guid raportId)
        {
            RaportDTO? raport = await _cache.GetRecordAsync<RaportDTO>($"Raport_{raportId}");
            if (raport is null)
            {

                raport = _mapper.Map<RaportDTO>(await _context.Raports
                    .Include(r => r.UserRaportRecords)
                    .ThenInclude(u => u.User)
                    .ThenInclude(j => j.InboxItems)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(r => r.RaportId == raportId));

                if (raport is null) throw new BadHttpRequestException("Bad request");
                await _cache.SetRecordAsync($"Raport_{raportId}", raport);
            }
            return raport;
        }

        public async Task<List<RaportDTO>> GetRaports()
        {
            List<RaportDTO>? raports = await _cache.GetRecordAsync<List<RaportDTO>>($"Raports");
            if (raports is null)
            {
                raports = await _context.Raports
                    .AsNoTracking()
                    .OrderBy(y => y.Year)
                    .ThenBy(m => m.Month)
                    .ProjectTo<RaportDTO>(_mapper.ConfigurationProvider)
                    .ToListAsync();
                if (raports is null) throw new BadHttpRequestException("Bad request");
                await _cache.SetRecordAsync($"Raports", raports);
            }
            return raports;
        }

        public async Task<Raport> CreateRaport(Guid userId, double hours, Guid inboxItemId,DateTime entryDate)
        {
            Raport? raport = await _context.Raports.FirstOrDefaultAsync(r => r.Year == entryDate.Year && r.Month == entryDate.Month);

            if (raport is null)
            {
                raport = new Raport()
                {
                    Month = entryDate.Month,
                    Year = entryDate.Year,
                };
                await _context.Raports.AddAsync(raport);
                await _context.SaveChangesAsync();
            }

            //UserRaport? myUserRaport = await _context.UserRaports.FirstOrDefaultAsync(uR => uR.RaportId == raport.RaportId && uR.UserId == userId);
            
            //if (myUserRaport is null)
            //{
            //    myUserRaport = new UserRaport()
            //    {
            //        RaportId = raport.RaportId,
            //        UserId = userId,
            //        UserAllHours = hours
            //    };
            //    await _context.UserRaports.AddAsync(myUserRaport);
            //    await _context.SaveChangesAsync();
            //}

            UserRaportRecord? userRaportRecord = await _context.UserRaportRecords.FirstOrDefaultAsync(
                    record => record.InboxItemId == inboxItemId);

            if (userRaportRecord is not null)
            {
                userRaportRecord.InboxItemId = inboxItemId;
                userRaportRecord.TaskHours = hours;
            }
            else
            {
                raport.UserRaportRecords = new List<UserRaportRecord>()
                {
                    new UserRaportRecord()
                    {
                        InboxItemId = inboxItemId,
                        TaskHours = hours,
                        RaportId =  raport.RaportId
                    }
                };

            }
            await _context.SaveChangesAsync();

            //myUserRaport.UserAllHours = await _context.UserRaportRecords
            //    .Where(i => i.UserRaportId == myUserRaport.UserRaportId)
            //    .SumAsync(h => h.TaskHours);

            //await _context.SaveChangesAsync();

            //raport.TotalHours = await _context.UserRaports
            //    .Where(d => d.RaportId == raport.RaportId)
            //    .SumAsync(h => h.UserAllHours);

            await _context.SaveChangesAsync();
            return raport;
        }

        public async Task<bool> DeleteRaport(Guid userId,double hours, Guid inboxItemId, DateTime entryDate)
        {
            Raport? raport = await _context.Raports.FirstOrDefaultAsync(d => d.Year == entryDate.Year && d.Month == entryDate.Month);

            if (raport is null) throw new BadHttpRequestException("Raport not found");

            UserRaportRecord? recordToDelete = await _context.UserRaportRecords.FirstOrDefaultAsync(r => r.InboxItemId == inboxItemId);

            if (recordToDelete is null) throw new BadHttpRequestException("Record not found");

            if (hours == 0)
            {
                _context.UserRaportRecords.Remove(recordToDelete);
                await _context.SaveChangesAsync();
            }
            else
            {
                recordToDelete.TaskHours = hours;
                await _context.SaveChangesAsync();
            }
            //userRaport.UserAllHours = await _context.UserRaportRecords
            //    .Where(i => i.UserRaportId == userRaport.UserRaportId)
            //    .SumAsync(h => h.TaskHours);

            //await _context.SaveChangesAsync();

            //raport.TotalHours = await _context.UserRaports
            //    .Where(d => d.RaportId == raport.RaportId)
            //    .SumAsync(h => h.UserAllHours);

            await _context.SaveChangesAsync();

            return true;
        }

    }
}
