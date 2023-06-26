using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace moovdAPI.Models
{
    public class GPS
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? DeviceId { get; set; }
        public string? DeviceType { get; set; }
        public DateTime? Timestamp { get; set; }
        public string? Location { get; set; }
    }
}
