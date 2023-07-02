using System.ComponentModel.DataAnnotations;

namespace SecendWebAppi.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public Guid uid { get; set; }

        [Required]
        [MaxLength(40)]
        
        public string  UserName { get; set; }

        [Required]
        [MaxLength(100)]
        public string PasswordHash { get; set; }

        [MaxLength(170)]
        public string FullName { get; set; }

        [MaxLength(40)]
        public string Role { get; set; }

        public bool IsActive { get; set; }
    }
}
