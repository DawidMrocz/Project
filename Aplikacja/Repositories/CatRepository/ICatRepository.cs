using Aplikacja.DTOS.CatDto;
using Aplikacja.DTOS.UserDtos;
using Aplikacja.Entities.CatModels;

namespace Aplikacja.Repositories.CatRepository
{
    public interface ICatRepository
    {

        public Task<UserDto> GetCat(Guid catId);
        public Task<List<CatRecord>> GetCats();
        public Task<double> CreateCat(Guid userId, Guid inboxItemId, DateTime date);
        public Task<double> DeleteCat(Guid inboxItemId, DateTime date);
    }
}
