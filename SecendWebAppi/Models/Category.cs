using System.ComponentModel.DataAnnotations;

namespace SecendWebAppi.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Title { get; set; }
    }
}
