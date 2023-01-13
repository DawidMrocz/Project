using Aplikacja.DTOS.RaportDto;
using Aplikacja.Entities.RaportModels;
using Aplikacja.Extensions;
using Aplikacja.Models;
using AutoMapper;
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

        public async Task<RaportDTO> GetRaport(int raportId)
        {
            RaportDTO? raport = await _cache.GetRecordAsync<RaportDTO>($"Raport_{raportId}");
            if (raport is null)
            {
                raport = _mapper.Map<RaportDTO>(await _context.Raports
                    .Include(a => a.UserRaports)
                    .ThenInclude(b => b.UserRaportRecords)
                    .ThenInclude(c => c.InboxItem)
                    .ThenInclude(d => d.Job)
                    .AsNoTracking()
                    .SingleAsync(r => r.RaportId == raportId));
                if (raport is null) throw new BadHttpRequestException("Bad request");
                await _cache.SetRecordAsync($"Raport_{raportId}", raport);
            }
            return raport;
        }

        public async Task<List<Raport>> GetRaports()
        {
            List<Raport>? raports = await _cache.GetRecordAsync<List<Raport>>($"Raports");
            if (raports is null)
            {
                raports = await _context.Raports.AsNoTracking().OrderBy(date => date.Created).ToListAsync();
                if (raports is null) throw new BadHttpRequestException("Bad request");
                await _cache.SetRecordAsync($"Raports", raports);
            }
            return raports;
        }

        public async Task<UserRaport> CreateRaport(int userId, double hours, int inboxItemId)
        {
            string currentDate = DateTime.Now.ToString("yyyy MM");
            Raport? raport;
            bool raportExist = await _context.Raports.AnyAsync<Raport>(r => r.Created == currentDate);
            if (raportExist)
            {
                raport = await _context.Raports.SingleAsync<Raport>(r => r.Created == currentDate);
            }
            else
            {
                raport = new Raport()
                {
                    TotalHours = 0,
                    Created = DateTime.Now.ToString("yyyy MM"),
                };
                await _context.Raports.AddAsync(raport);
                await _context.SaveChangesAsync();
            }
            UserRaport? myUserRaport;
            bool myUserRaportExist = await _context.UserRaports.AnyAsync(uR => uR.RaportId == raport.RaportId && uR.UserId == userId);
            if (myUserRaportExist)
            {
                myUserRaport = await _context.UserRaports.SingleAsync(uR => uR.RaportId == raport.RaportId && uR.UserId == userId);
            }
            else
            {
                myUserRaport = new UserRaport()
                {
                    RaportId = raport.RaportId,
                    UserId = userId,
                    UserAllHours = hours
                };
                await _context.UserRaports.AddAsync(myUserRaport);
                await _context.SaveChangesAsync();
            }
            UserRaportRecord? userRaportRecord;
            bool userRaportRecordExist = await _context.UserRaportRecords.AnyAsync(
                    record => record.InboxItemId == inboxItemId);

            if (userRaportRecordExist)
            {
                userRaportRecord = await _context.UserRaportRecords.SingleAsync(
                    record => record.InboxItemId == inboxItemId);

                userRaportRecord.InboxItemId = inboxItemId;
                userRaportRecord.TaskHours = hours;
            }
            else
            {
                myUserRaport.UserRaportRecords = new List<UserRaportRecord>()
                {
                    new UserRaportRecord()
                    {
                        InboxItemId = inboxItemId,
                        TaskHours = hours,
                        UserRaportId =  myUserRaport.RaportId
                    }
                };

            }
            await _context.SaveChangesAsync();

            myUserRaport.UserAllHours = await _context.UserRaportRecords
                .Where(i => i.UserRaportId == myUserRaport.UserRaportId)
                .SumAsync(h => h.TaskHours);

            await _context.SaveChangesAsync();

            raport.TotalHours = await _context.UserRaports
                .Where(d => d.RaportId == raport.RaportId)
                .SumAsync(h => h.UserAllHours);

            await _context.SaveChangesAsync();
            return myUserRaport;
        }

        public async Task<bool> DeleteRaport(int userId,double hours, int inboxItemId)
        {
            string currentRaport = DateTime.Now.ToString("yyyy MM");
            Raport raport = await _context.Raports.SingleAsync(d => d.Created == currentRaport);

            UserRaport userRaport = await _context.UserRaports.SingleAsync(r => r.RaportId == raport.RaportId && r.UserId == userId);

            UserRaportRecord recordToDelete = await _context.UserRaportRecords.SingleAsync(r => r.InboxItemId == inboxItemId);
            if (recordToDelete == null) throw new BadHttpRequestException("Bad request");
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
            userRaport.UserAllHours = await _context.UserRaportRecords
                .Where(i => i.UserRaportId == userRaport.UserRaportId)
                .SumAsync(h => h.TaskHours);

            await _context.SaveChangesAsync();

            raport.TotalHours = await _context.UserRaports
                .Where(d => d.RaportId == raport.RaportId)
                .SumAsync(h => h.UserAllHours);

            await _context.SaveChangesAsync();

            return true;
        }

    }
}
