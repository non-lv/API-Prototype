using System.ComponentModel.DataAnnotations;

namespace API_Prototype.Database.Models
{
    public class MessageThread
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public required string Description { get; set; }
        public required virtual List<Entry> Entries { get; set; }

    }
}
