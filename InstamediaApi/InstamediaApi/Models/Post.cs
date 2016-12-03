using System.ComponentModel.DataAnnotations;

namespace InstamediaApi.Models
{
    public class Post
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        public string Description { get; set; }

        public byte[] Image { get; set; }
    }
}