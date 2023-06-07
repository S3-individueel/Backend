namespace VolksmondAPI.Models
{
    public class Referendum
    {
        public int Id { get; set; }
        public int ProblemId { get; set; }
        public DateTime VotingStart { get; set; }
        public DateTime VotingEnd { get; set; }

        public virtual ICollection<ReferendumVote>? Votes { get; set; }
    }
}
