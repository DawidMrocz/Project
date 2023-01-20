
using Aplikacja.DTOS.InboxDtos;
using Aplikacja.Entities.InboxModel;

namespace InboxMicroservice.Repositories
{
    public interface IInboxRepository
    {

        public Task<InboxDTO> GetMyInbox(int userId);
        public Task<InboxItemDTO> GetMyInboxItem(int inboxItemId);

        public Task<bool> DeleteInboxItem(int userId, int inboxItemId);
        public Task<InboxItemDTO> UpdateInboxItem(UpdateInboxItemDto updateInboxItemDto,int inboxItemId);


 

    }
}