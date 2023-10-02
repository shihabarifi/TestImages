using System.ComponentModel.DataAnnotations;

namespace TestImages.Models
{
    public class Images
    {
        [Key]
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Descrption { get; set; }
        public string? ImageUrl { get; set; }

    }
}
