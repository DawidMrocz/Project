using Aplikacja.DTOS.InboxDtos;
using Aplikacja.DTOS.UserDtos;
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
        public async Task<UserDto> GetMyInbox(Guid userId)
        {

            UserDto? myInbox = await _cache.GetRecordAsync<UserDto>($"Profile_{userId}");
            if (myInbox is null)
            {
                myInbox = await _context.Users
                .Include(i => i.InboxItems)
                .ThenInclude(j => j.Job)
                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                .AsNoTracking()
                .SingleAsync(i => i.UserId == userId);
   
                if (myInbox is null) throw new BadHttpRequestException("Inbox don't exist");
                await _cache.SetRecordAsync($"Profile_{userId}", myInbox);
            }
            return myInbox;
        }

        public async Task<InboxItemDTO> GetMyInboxItem(Guid inboxItemId)
        {
            var myInboxItem = await _context.InboxItems
                .Include(j => j.Job)
                .ProjectTo<InboxItemDTO>(_mapper.ConfigurationProvider)
                .AsNoTracking()
                .SingleAsync(i => i.InboxItemId == inboxItemId);
            if (myInboxItem is null) throw new BadHttpRequestException("Inbox don't exist");
            return myInboxItem;
        }

        public async Task<InboxItemDTO> UpdateInboxItem(UpdateInboxItemDto updateInboxItemDto, Guid inboxItemId)
        {
            var inboxItem = await _context.InboxItems.Include(j => j.Job).SingleAsync(i => i.InboxItemId == inboxItemId);
            if (inboxItem is null) throw new BadHttpRequestException("Item not found!");

            inboxItem.Components = updateInboxItemDto.Components;
            inboxItem.DrawingsComponents = updateInboxItemDto.DrawingsComponents;
            inboxItem.DrawingsAssembly = updateInboxItemDto.DrawingsAssembly;
            inboxItem.Job.Started = updateInboxItemDto.Started;
            inboxItem.Job.Finished = updateInboxItemDto.Finished;
            inboxItem.Hours = updateInboxItemDto.Hours;
            inboxItem.Job.Status = updateInboxItemDto.Status is not null ? updateInboxItemDto.Status : inboxItem.Job.Status;
            await _context.SaveChangesAsync();
            return _mapper.Map<InboxItemDTO>(inboxItem);
        }

        public async Task<bool> DeleteInboxItem(Guid userId, Guid inboxItemId)
        {
            var deletedResults = await _context.InboxItems.Where(i => i.InboxItemId == inboxItemId).ExecuteDeleteAsync();
            if (deletedResults != 1) throw new BadHttpRequestException("Request did not succeed");
            return true;
        }  
    }
}