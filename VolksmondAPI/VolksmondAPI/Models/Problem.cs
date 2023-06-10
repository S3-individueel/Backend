namespace VolksmondAPI.Models
{
    public class Problem
    {
        public int Id { get; set; }
        public int CitizenId { get; set; }
        public string? Title { get; set; }
        public DateTime PostDate { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<Referendum>? Referendums { get; set;}
        public virtual ICollection<Solution>? Solutions { get; set; }
        public virtual Citizen? Citizen { get; set;}
    }
}
