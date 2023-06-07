using System.ComponentModel.DataAnnotations.Schema;

namespace VolksmondAPI.Models
{
    public class ReplyVote
    {
        public int Id { get; set; }
        [ForeignKey(nameof(Reply))]
        public int ReplyId { get; set; }
        public int CitizenId { get; set; }
        public int Vote { get; set; } // -1, 0, 1
    }
}
