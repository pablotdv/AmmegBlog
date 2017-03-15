using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ammeg.Blog.Data;
using Microsoft.EntityFrameworkCore;
using Ammeg.Blog.Models;

namespace Ammeg.Blog.Controllers
{
    public class PostsController : Controller
    {
        public readonly ApplicationDbContext context;

        public PostsController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            var posts = await context.Posts.ToListAsync();

            return View(posts);
        }

        public IActionResult Criar()
        {
            return View(new Post());
        }

        [HttpPost]        
        public async Task<IActionResult> Criar(Post post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await context.Posts.AddAsync(post);
            await context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}