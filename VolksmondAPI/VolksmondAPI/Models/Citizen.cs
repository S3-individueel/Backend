namespace VolksmondAPI.Models
{
    public class Citizen
    {
        public int Id { get; set; } = 1;
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? Photo { get; set; }
    }
}
