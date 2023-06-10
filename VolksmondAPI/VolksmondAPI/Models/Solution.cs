using System.ComponentModel.DataAnnotations.Schema;

namespace VolksmondAPI.Models
{
    public class Solution
    {
        public int Id { get; set; }
        public int ProblemId { get; set; }
        public int CitizenId { get; set; }
        public string? Title { get; set; }
        public string? Text { get; set; }
        [NotMapped]
        public int? Score { get; set; }

        public virtual ICollection<SolutionVote>? Votes { get; set; }
        public virtual ICollection<Reply>? Replies { get; set; }

        public virtual Citizen? Citizen { get; set; }
        //{
        //    get { return Citizen; }
        //    set
        //    {
        //        Citizen.Firstname = value.Firstname;
        //        Citizen.Lastname = value.Lastname;
        //        Citizen.Photo = value.Photo;
        //        Citizen.DateOfBirth = value.DateOfBirth;
        //    }
        //}

        public Solution()
        {
            Votes = new List<SolutionVote>();
            Score = Votes.Sum(v => v.Vote);
        }
    }
}
