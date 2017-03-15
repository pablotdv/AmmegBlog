using Ammeg.Blog.Data;
using Ammeg.Blog.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Xunit;

namespace Ammeg.Blog.xTests.Models
{
    public class PostUnistTest
    {
        [Fact]
        public void Add_Writes_to_database()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var post = new Post()
            {
                PostId = Guid.NewGuid(),
                Usuario = new ApplicationUser(),
                Conteudo = "Conte�do",
                Titulo = "T�tulo"
            };

            using (var context = new ApplicationDbContext(options))
            {                
                context.Posts.Add(post);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(options))
            {
                var postInMemory = context.Posts.Single();
                Assert.True(context.Posts.Count() == 1);
                Assert.True(postInMemory.PostId == post.PostId);
                Assert.True(postInMemory.Conteudo == post.Conteudo);
                Assert.True(postInMemory.Titulo == post.Titulo);
            }
        }

        [Fact]
        public void Find_Searches_conteudo()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.Posts.Add(new Post()
                {
                    PostId = Guid.NewGuid(),
                    Usuario = new ApplicationUser(),
                    Conteudo = "Cats",
                    Titulo = "T�tulo"
                });
                context.Posts.Add(new Post()
                {
                    PostId = Guid.NewGuid(),
                    Usuario = new ApplicationUser(),
                    Conteudo = "Dogs",
                    Titulo = "T�tulo"
                });
                context.Posts.Add(new Post()
                {
                    PostId = Guid.NewGuid(),
                    Usuario = new ApplicationUser(),
                    Conteudo = "Fish and Dogs",
                    Titulo = "T�tulo"
                });
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(options))
            {
                var posts = context.Posts.Where(a => a.Conteudo.Contains("Dogs"));

                Assert.True(posts.Count() == 2);
            }
        }
    }
}
