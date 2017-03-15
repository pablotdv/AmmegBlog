using Ammeg.Blog.Controllers;
using Ammeg.Blog.Data;
using Ammeg.Blog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Ammeg.Blog.xTests.Controllers
{
    public class PostsControllerUnitTest
    {
        [Fact]
        public async Task Index_ResturnsAViewResult_WithAListOfPosts()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                await context.Posts.AddAsync(new Post
                {
                    PostId = Guid.NewGuid(),
                    Usuario = new ApplicationUser(),
                    Conteudo = "Conteúdo",
                    Titulo = "Título"
                });
                await context.Posts.AddAsync(new Post
                {
                    PostId = Guid.NewGuid(),
                    Usuario = new ApplicationUser(),
                    Conteudo = "Conteúdo",
                    Titulo = "Título"
                });
                await context.SaveChangesAsync();
            }

            var controller = new PostsController(new ApplicationDbContext(options));

            var result = await controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Post>>(viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public void Criar_RetornoUmViewResult_DeUmPost()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var controller = new PostsController(new ApplicationDbContext(options));
            var result = controller.Criar();
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Post>(viewResult.ViewData.Model);
            Assert.True(model != null);
            Assert.IsType<Post>(model);
        }

        [Fact]
        public async Task CriarPost_RetornaUmBadRequestResult_QuandoModelStateNaoValido()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var controller = new PostsController(new ApplicationDbContext(options));
            controller.ModelState.AddModelError("PostTitulo", "Required");

            var post = new Post
            {
                PostId = Guid.NewGuid()
            };

            var result = await controller.Criar(post);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<SerializableError>(badRequestResult.Value);
        }

        [Fact]
        public async Task CriarPost_RetornaUmRedirectEAdicionaPost_QuantoModelEhValido()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var controller = new PostsController(new ApplicationDbContext(options));
            var post = new Post
            {
                PostId = Guid.NewGuid(),
                Usuario = new ApplicationUser(),
                Conteudo = "Conteúdo",
                Titulo = "Título"
            };

            var result = await controller.Criar(post);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }
    }
}
