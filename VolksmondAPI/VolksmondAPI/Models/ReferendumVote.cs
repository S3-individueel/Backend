namespace VolksmondAPI.Models
{
    public class ReferendumVote
    {
        public int Id { get; set; }
        public int ReferendumId { get; set; }
        public int SolutionId { get; set; }
        public int CitizenId { get; set; }
    }
}
