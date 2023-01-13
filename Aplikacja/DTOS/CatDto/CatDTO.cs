using Aplikacja.Entities.CatModels;
using Aplikacja.Entities.UserModel;

namespace Aplikacja.DTOS.CatDto
{
    public class CatDTO
    {
        public int CatId { get; set; }
        public string? CatCreated { get; set; }
        public string? Name { get; set; }
        public string? CCtr { get; set; }
        public string? ActTyp { get; set; }
        public string? Photo { get; set; }
        public List<CatRecordDTO>? CatRecords { get; set; }
    }
}
