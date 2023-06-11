using System.ComponentModel.DataAnnotations.Schema;

namespace VolksmondAPI.Models
{
    public class Reply
    {
        public int Id { get; set; }
        public int CitizenId { get; set; }
        public int SolutionId { get; set; }
        public int? ReplyId { get; set; }

        public string? Text { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsPinned { get; set; }
        public DateTime? PostDate { get; set; }

        public virtual ICollection<ReplyVote>? Votes { get; set; }
        public virtual ICollection<Reply>? Replies { get; set; }

        [NotMapped]
        public virtual Citizen? Citizen { get; set; }

        [NotMapped]
        public virtual int? Score { get; set; }
    }
}
