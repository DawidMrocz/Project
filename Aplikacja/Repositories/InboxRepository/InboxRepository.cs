using Aplikacja.DTOS.InboxDtos;
using Aplikacja.Extensions;
using Aplikacja.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BenchmarkDotNet.Attributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace InboxMicroservice.Repositories
{
    public class InboxRepository : IInboxRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IDistributedCache _cache;
        private readonly IMapper _mapper;
        public InboxRepository(ApplicationDbContext context, IDistributedCache cache, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [Benchmark]
        public async Task<InboxDTO> GetMyInbox(int userId)
        {

            InboxDTO? myInbox = await _cache.GetRecordAsync<InboxDTO>($"Inbox_{userId}");
            if (myInbox is null)
            {
                myInbox = await _context.Inboxs
                .Include(u => u.User)
                .Include(i => i.InboxItems)
                .ThenInclude(j => j.Job)
                .ProjectTo<InboxDTO>(_mapper.ConfigurationProvider)
                .AsNoTracking()
                .SingleAsync(i => i.UserId == userId);
   
                if (myInbox is null) throw new BadHttpRequestException("Inbox don't exist");
                await _cache.SetRecordAsync($"Inbox_{userId}", myInbox);
            }
            return myInbox;
        }

        public async Task<InboxItemDTO> GetMyInboxItem(int inboxItemId)
        {
            var myInboxItem = await _context.InboxItems
                .Include(j => j.Job)
                .ProjectTo<InboxItemDTO>(_mapper.ConfigurationProvider)
                .AsNoTracking()
                .SingleAsync(i => i.InboxItemId == inboxItemId);
            if (myInboxItem is null) throw new BadHttpRequestException("Inbox don't exist");
            return myInboxItem;
        }

        public async Task<InboxItemDTO> UpdateInboxItem(UpdateInboxItemDto updateInboxItemDto, int inboxItemId)
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
            return _mapper.Map<InboxItemDTO>(inboxItem);
        }

        public async Task<bool> DeleteInboxItem(int userId,int jobId)
        {
            var deletedResults = await _context.InboxItems.Include(j => j.Job).Where(i => i.JobId == jobId).ExecuteDeleteAsync();
            if (deletedResults != 1) throw new BadHttpRequestException("Request did not succeed");
            return true;
        }  
    }
}