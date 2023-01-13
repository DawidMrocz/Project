using Aplikacja.Entities.CatModels;
using Aplikacja.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;


namespace Aplikacja.Repositories.CatRepository
{
    public class CatRepository:ICatRepository
    {

            private readonly ApplicationDbContext _context;
            private readonly IDistributedCache _cache;
            public CatRepository(ApplicationDbContext context, IDistributedCache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<Cat> GetCat(int catId)
            {
                var cat = await _context.Cats
                    .Include(r => r.User)
                    .Include(r => r.CatRecords)
                    .ThenInclude(h => h.CatRecordHours)
                    .AsNoTracking()
                    .SingleAsync(c => c.CatId == catId);

                return cat;
            }

        public async Task<List<Cat>> GetCats()
        {
            return await _context.Cats.ToListAsync();
        }

        public async Task<double> CreateCat(int userId, int inboxItemId,int entryDate)
        {
            Cat? userCat;
            string currentDate = DateTime.Now.ToString("yyyy MM");
            var catUser = await _context.Users.SingleAsync(u => u.UserId == userId);
            var catExist = await _context.Cats.AnyAsync(u => u.UserId == catUser.UserId && u.CatCreated == currentDate);
            if (catExist)
            {
                userCat = await _context.Cats.SingleAsync(u => u.UserId == catUser.UserId && u.CatCreated == currentDate);
            }
            else
            {
                userCat = new Cat()
                {
                    CatCreated = currentDate,
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
                && c.Day == entryDate);

            if (hoursExits)
            {
                catRecordHour = await _context
                .CatRecordHourss
                .SingleAsync(c => c.CatRecordId == catRecord.CatRecordId
                && c.Day == entryDate);
                catRecordHour.Hours = inboxItem.Hours;
            }
            else
            {
                catRecordHour = new CatRecordHours()
                {
                    CatRecordId = catRecord.CatRecordId,
                    Hours = inboxItem.Hours,
                    Day = entryDate
                };
                await _context.CatRecordHourss.AddAsync(catRecordHour);
            }
            await _context.SaveChangesAsync();
            double sumOfHours = await _context.CatRecordHourss
                .Where(r => r.CatRecordId == catRecord.CatRecordId)
                .SumAsync(h => h.Hours);
            return sumOfHours;
        }

        public async Task<double> DeleteCat(int inboxItemId, int entryDate)
        {
            CatRecord catRecord = await _context.CatRecords.SingleAsync(r => r.InboxItemId == inboxItemId);

            if (catRecord is null) throw new BadHttpRequestException("Cat not found");

            CatRecordHours catRecordHour = await _context.CatRecordHourss
                .SingleAsync(h => h.CatRecordId == catRecord.CatRecordId &&
                h.Day == entryDate);
            _context.CatRecordHourss.Remove(catRecordHour);
            await _context.SaveChangesAsync();

            double sumOfHours = await _context.CatRecordHourss
                .Where(r => r.CatRecordId == catRecord.CatRecordId)
                .SumAsync(h => h.Hours);
            return sumOfHours;
        }




    }
    
}
