using System.ComponentModel.DataAnnotations;

namespace DT191G_Moment2.Models
{
    public class NoteModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Rubrik �r obligatoriskt!")]
        [StringLength(50, ErrorMessage = "Rubriken f�r vara max 50 tecken l�ng!")]
        public required string Title { get; set; }

        [Required(ErrorMessage = "Anteckningen f�r inte vara tom!")]
        [StringLength(1000, ErrorMessage = "Anteckningen f�r vara max 1000 tecken l�ng.")]
        public required string Notes { get; set; }

        [Required(ErrorMessage = "V�lj en kategori!")]
        public required string Category { get; set; }

        public bool IsImportant { get; set; }
    }
}