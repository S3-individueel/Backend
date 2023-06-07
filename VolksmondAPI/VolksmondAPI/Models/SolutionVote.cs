namespace VolksmondAPI.Models
{
    public class SolutionVote
    {
        public int Id { get; set; }
        public int SolutionId { get; set; }
        public int CitizenId { get; set; }
        public int Vote { get; set; } // -1, 0, 1
    }
}
