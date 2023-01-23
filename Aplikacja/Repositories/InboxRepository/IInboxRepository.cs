
using Aplikacja.DTOS.InboxDtos;
using Aplikacja.DTOS.UserDtos;
using Aplikacja.Entities.InboxModel;

namespace InboxMicroservice.Repositories
{
    public interface IInboxRepository
    {

        public Task<UserDto> GetMyInbox(Guid userId);
        public Task<InboxItemDTO> GetMyInboxItem(Guid inboxItemId);

        public Task<bool> DeleteInboxItem(Guid userId, Guid inboxItemId);
        public Task<InboxItemDTO> UpdateInboxItem(UpdateInboxItemDto updateInboxItemDto,Guid inboxItemId);


 

    }
}