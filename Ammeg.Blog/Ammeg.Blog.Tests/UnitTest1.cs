using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using Ammeg.Blog.Data;
using Ammeg.Blog.Models;
using System.Linq;

namespace Ammeg.Blog.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "Add_writes_to_database")
               .Options;

            var post = new Post()
            {
                PostId = Guid.NewGuid(),
                Conteudo = "Conteúdo",
                Titulo = "Título",
                Usuario = new ApplicationUser() { }
            };

            using (var context = new ApplicationDbContext(options))
            {               
                context.Posts.Add(post);
                context.SaveChanges();
            }
                        
            using (var context = new ApplicationDbContext(options))
            {
                Assert.AreEqual(1, context.Users.Count());
                Assert.AreEqual(1, context.Posts.Count());

                var postInMemory = context.Posts.Single();
                Assert.AreNotEqual(null, postInMemory);
                Assert.AreEqual(post.PostId, postInMemory.PostId);
                Assert.AreEqual(post.Conteudo, postInMemory.Conteudo);
                Assert.AreEqual(post.Titulo, postInMemory.Titulo);
            }
        }
    }
}
