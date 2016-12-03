using System.ComponentModel.DataAnnotations;

namespace InstamediaApi.Models
{
    public class Picture
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public User User { get; set; }

        [Required]
        public int UserId { get; set; }

        public byte[] Image { get; set; }
    }
}