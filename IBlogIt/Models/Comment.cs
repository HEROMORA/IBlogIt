using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IBlogIt.Models
{
    public class Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Body { get; set; }
        
        [Required]
        [DataType(DataType.Text)]
        public string Author { get; set; }
        
        [Required]
        public virtual Post Post { get; set; }

        [Required]
        public long PostId { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime PostedTime { get; set; }
    }
}
