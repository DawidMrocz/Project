using Aplikacja.Entities.CatModels;
using Aplikacja.Entities.InboxModel;

namespace Aplikacja.Entities.RaportModels
{
    public class Raport
    {
        public int RaportId { get; set; }
        public double TotalHours { get; set; } = 0;
        public string Created { get; set; } = DateTime.Now.ToString();
        public List<UserRaport> UserRaports { get; set; }
        public Raport()
        {
            this.UserRaports = new List<UserRaport>();
        }

    }
}
