using System.ComponentModel.DataAnnotations;

namespace SecendWebAppi.ViewModels
{
    public class TruckViewModel
    {
        [Required]
        [MaxLength(50)]
        public string? Title { get; set; }

        [MaxLength(50)]
        public string? Barcode { get; set; }

        public int Capacity { get; set; } = 0;

        //98
        [MaxLength(4)]
        public string? PelakPart1 { get; set; }

        // ع
        [MaxLength(3)]
        public string? PelakPart2 { get; set; }

        // 185
        [MaxLength(6)]
        public string? PelakPart3 { get; set; }

        // 14
        [MaxLength(4)]
        public string? PelakPart4 { get; set; }

        [MaxLength(50)]
        public string? SmartCode { get; set; }

        [MaxLength(50)]
        public string? ShasiNumber { get; set; }

        [MaxLength(50)]
        public string? IRCode { get; set; }

        [MaxLength(100)]
        public string? OwnerName { get; set; }

        [MaxLength(50)]
        public string? OwnerMobile { get; set; }

        [MaxLength(50)]
        public string? OwnerMelliCode { get; set; }

        public bool BlackListFlag { get; set; } = false;

        [MaxLength(400)]

        public string? BlackListDescr { get; set; }
    }
}
