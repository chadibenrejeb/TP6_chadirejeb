using System.ComponentModel.DataAnnotations;

namespace SchoolWebAppClient.Models
{
    public class SchoolClient
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!; 
        public string Sections { get; set; } = null!;
        public string Director { get; set; } = null!;
        public double Rating { get; set; }
        public string WebSite { get; set; } = null!; 
    }
}
