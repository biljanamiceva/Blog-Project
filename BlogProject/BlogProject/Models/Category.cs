using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BlogProject.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [DisplayName("Category Name")]
        [Required]
        public String? CategoryName { get; set; }

        [DisplayName("Category Description")]
        [Required]
        public String? CategoryDescription { get; set; }

        public ICollection<Post>? Posts { get; set; }
    }
}
