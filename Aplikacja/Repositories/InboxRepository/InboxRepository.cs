

using Aplikacja.DTOS.InboxDtos;
using Aplikacja.Entities.InboxModel;
using Aplikacja.Entities.JobModel;
using Aplikacja.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace InboxMicroservice.Repositories
{
    public class InboxRepository : IInboxRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IDistributedCache _cache;
        public InboxRepository(ApplicationDbContext context, IDistributedCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task<Inbox> GetMyInbox(int userId)
        {
            var myInbox = await _context.Inboxs
                .Include(u => u.User)
                .Include(i => i.InboxItems)
                .ThenInclude(j => j.Job)
                .AsNoTracking()
                .SingleAsync(i => i.UserId == userId);
            if (myInbox is null) throw new BadHttpRequestException("Inbox don't exist");
            return myInbox;
        }

        public async Task<InboxItem> GetMyInboxItem(int inboxItemId)
        {
            var myInboxItem = await _context.InboxItems
                .Include(j => j.Job)
                .AsNoTracking()
                .SingleAsync(i => i.InboxItemId == inboxItemId);
            if (myInboxItem is null) throw new BadHttpRequestException("Inbox don't exist");
            return myInboxItem;
        }

        public async Task<InboxItem> UpdateInboxItem(UpdateInboxItemDto updateInboxItemDto, int inboxItemId)
        {
            var inboxItem = await _context.InboxItems.Include(j => j.Job).SingleAsync(i => i.InboxItemId == inboxItemId);
            if (inboxItem is null) throw new BadHttpRequestException("Item not found!");
            inboxItem.Components = updateInboxItemDto.Components;
            inboxItem.DrawingsComponents = updateInboxItemDto.DrawingsComponents;
            inboxItem.DrawingsAssembly = updateInboxItemDto.DrawingsAssembly;
            inboxItem.WhenComplete = updateInboxItemDto.WhenComplete;
            inboxItem.Job.Started = updateInboxItemDto.Started;
            inboxItem.Job.Finished = updateInboxItemDto.Finished;
            inboxItem.Hours = updateInboxItemDto.Hours;
            inboxItem.Job.Status = updateInboxItemDto.Status;
            await _context.SaveChangesAsync();
            return inboxItem;
        }

        public async Task<bool> DeleteInboxItem(int userId,int jobId)
        {
            InboxItem inboxItem = await _context.InboxItems.SingleAsync(i => i.InboxItemId == jobId);
            _context.InboxItems.Remove(inboxItem);
            await _context.SaveChangesAsync();
            return true;
        }

        
    }
}