using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace IBlogIt.Models
{
    public class Post
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Post Title")]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Author { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name ="Post Body")]
        public string Body { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime PostedTime { get; set; }

    }
}
