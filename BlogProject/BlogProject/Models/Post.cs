using Microsoft.VisualBasic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace BlogProject.Models
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }


        [DisplayName("Post Title")]
        [Required]
        public String? PostTitle { get; set; }

        [DisplayName("Post Description")]
        [Required]
        public String? PostDescription { get; set; }

        [DisplayName("Post Author")]
        [Required]
        public String? PostAuthor { get; set; }

        [DisplayName("Post Date")]
       
        public DateTime PostDate { get; set; } = DateTime.Now;

        [DisplayName("Post Image")]
        [Required]
        public String? PostImg { get; set; }

        public int CurrentCategoryId { get; set; }
        public Category? Category { get; set; }













    }
}
