using System.ComponentModel.DataAnnotations;

namespace SchoolAPI.Models
{
    public class School
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        public string Sections { get; set; } = null!;

        public string Director { get; set; } = null!;

        [Range(0, 5, ErrorMessage = "Le rating doit être compris entre 0 et 5")]
        public double Rating { get; set; }

        [Url(ErrorMessage = "Le format de l'URL n'est pas valide")]
        public string? WebSite { get; set; } = null!; 
    }
}
