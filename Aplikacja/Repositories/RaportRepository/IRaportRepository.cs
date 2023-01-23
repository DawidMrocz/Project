using Aplikacja.DTOS.RaportDto;
using Aplikacja.Entities.RaportModels;

namespace Aplikacja.Repositories.RaportRepository
{
    public interface IRaportRepository
    {

        public Task<RaportDTO> GetRaport(Guid raportId);
        public Task<List<RaportDTO>> GetRaports();
        public Task<Raport> CreateRaport(Guid userId, double hours,Guid inboxItemId,DateTime entryDate);
        public Task<bool> DeleteRaport(Guid userId, double hours, Guid inboxItemId, DateTime entryDate);

    }
}
