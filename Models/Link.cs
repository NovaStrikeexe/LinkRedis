using System.ComponentModel.DataAnnotations;
namespace LinkApi.Models
{
    public class Link
    {
        [Key]
        [MaxLength(16)]
        public string guid { get; set; } = $"{Guid.NewGuid().ToString()}";
        [Required]
        public string stringLink { get; set; } = String.Empty;
        [Required]
        public DateTime dateOfCreation { get; set; } = DateTime.Now;
    }
}