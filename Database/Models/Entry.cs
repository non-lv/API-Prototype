using System.ComponentModel.DataAnnotations;

namespace API_Prototype.Database.Models
{
    public class Entry
    {
        [Key]
        public int Id { get; set; }
        public EntryType Type { get; set; }
        public DateTime TimeStamp { get; set; }
        public bool Edited { get; set; }
        public required string Message { get; set; }
    }

    public enum EntryType { 
        Regular,
        Uneditable
    }
}
