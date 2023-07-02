using System.ComponentModel.DataAnnotations;

namespace SecendWebAppi.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        public string? Text { get; set; }
    }
}
