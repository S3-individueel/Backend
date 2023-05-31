namespace VolksmondAPI.Models
{
    public class Citizen
    {
        public int Id { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public byte[]? Photo { get; set; }
    }
}
