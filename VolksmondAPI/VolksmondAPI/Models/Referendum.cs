using System.ComponentModel.DataAnnotations.Schema;

namespace VolksmondAPI.Models
{
    public class Referendum
    {
        public int Id { get; set; }
        public int ProblemId { get; set; }

        public DateTime VotingStart { get; set; }
        public DateTime VotingEnd { get; set; }

        public virtual ICollection<ReferendumVote>? Votes { get; set; }
        //public virtual Problem? Problem { get; set; }

        [NotMapped]
        public bool Active
        {
            get
            {
                var currentDateTime = DateTime.Now;
                return currentDateTime >= VotingStart && currentDateTime <= VotingEnd;
            }
        }

        [NotMapped]
        public bool Ended
        {
            get
            {
                var currentDateTime = DateTime.Now;
                return currentDateTime > VotingEnd;
            }
        }

        [NotMapped]
        public Solution? WinningSolution { get; set; }
    }
}
