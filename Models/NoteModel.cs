using System.ComponentModel.DataAnnotations;

namespace DT191G_Moment2.Models
{
    public class NoteModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Rubrik är obligatoriskt!")]
        [StringLength(50, ErrorMessage = "Rubriken får vara max 50 tecken lång!")]
        public required string Title { get; set; }

        [Required(ErrorMessage = "Anteckningen får inte vara tom!")]
        [StringLength(1000, ErrorMessage = "Anteckningen får vara max 1000 tecken lång.")]
        public required string Notes { get; set; }

        [Required(ErrorMessage = "Välj en kategori!")]
        public required string Category { get; set; }

        public bool IsImportant { get; set; }
    }
}