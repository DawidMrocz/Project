using Aplikacja.DTOS.RaportDto;
using Aplikacja.Entities.RaportModels;

namespace Aplikacja.Repositories.RaportRepository
{
    public interface IRaportRepository
    {

        public Task<RaportDTO> GetRaport(int raportId);
        public Task<List<Raport>> GetRaports();
        public Task<UserRaport> CreateRaport(int userId, double hours,int inboxItemId);
        public Task<bool> DeleteRaport(int userId, double hours, int inboxItemId);

    }
}
