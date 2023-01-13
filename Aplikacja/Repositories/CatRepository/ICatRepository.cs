using Aplikacja.Entities.CatModels;

namespace Aplikacja.Repositories.CatRepository
{
    public interface ICatRepository
    {

        public Task<Cat> GetCat(int catId);
        public Task<List<Cat>> GetCats();

        public Task<double> CreateCat(int userId, int inboxItemId, int entryDate);

        public Task<double> DeleteCat(int inboxItemId, int entryDate);


        ////CatRecord
        //public Task<bool> DeleteCatRecord(DeleteCatRecordCommand command);
        //public Task<CatRecord> UpdateCatRecord(UpdateCatRecordCommand command);

        ////CatRecordHours
        //public Task<bool> DeleteCatRecordHours(DeleteCatRecordHoursCommand command);
        //public Task<CatRecordHours> UpdateCatRecordHours(UpdateCatRecordHoursCommand command);
    }
}
