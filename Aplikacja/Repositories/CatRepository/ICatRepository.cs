using Aplikacja.DTOS.CatDto;
using Aplikacja.Entities.CatModels;

namespace Aplikacja.Repositories.CatRepository
{
    public interface ICatRepository
    {

        public Task<CatDTO> GetCat(int catId);
        public Task<List<Cat>> GetCats();

        public Task<double> CreateCat(int userId, int inboxItemId, DateTime date);

        public Task<double> DeleteCat(int inboxItemId, DateTime date);
    }
}
