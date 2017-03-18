using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ammeg.Blog.Models
{
    [Table("Posts")]
    public class Post
    {
        [Key]
        public Guid PostId { get; set; }

        [Required]
        public string Titulo { get; set; }

        [Required]
        public string Conteudo { get; set; }

        [Required]
        public string AutorId { get; set; }

        [ForeignKey(nameof(AutorId))]
        public virtual ApplicationUser Usuario {get;set;}        
    }
}
