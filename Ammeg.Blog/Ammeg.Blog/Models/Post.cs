using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ammeg.Blog.Models
{
    [Table("Posts")]
    public class Post
    {
        public Guid PostId { get; set; }

        public string Titulo { get; set; }

        public string Conteudo { get; set; }


    }
}
