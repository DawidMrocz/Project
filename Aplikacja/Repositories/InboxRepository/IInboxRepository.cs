
using Aplikacja.DTOS.InboxDtos;
using Aplikacja.Entities.InboxModel;

namespace InboxMicroservice.Repositories
{
    public interface IInboxRepository
    {

        public Task<Inbox> GetMyInbox(int userId);
        public Task<InboxItem> GetMyInboxItem(int inboxItemId);

        public Task<bool> DeleteInboxItem(int userId, int inboxItemId);
        public Task<InboxItem> UpdateInboxItem(UpdateInboxItemDto updateInboxItemDto,int inboxItemId);


 

    }
}