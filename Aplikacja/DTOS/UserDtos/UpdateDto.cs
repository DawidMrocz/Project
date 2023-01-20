namespace Aplikacja.DTOS.UserDtos
{
    public class UpdateDto
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? CCtr { get; set; }
        public string? ActTyp { get; set; }
        public IFormFile? ProfilePhoto { get; set; }
    }
}
