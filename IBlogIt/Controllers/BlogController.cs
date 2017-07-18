using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IBlogIt.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IBlogIt.Controllers
{
    public class BlogController : Controller
    {

        private readonly PostsDbContext _db;
        public BlogController(PostsDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var posts = _db.Posts.ToList();
            return View(posts);
        }

        [HttpGet]
        public IActionResult Details(long id)
        {
            var post = _db.Posts.Where(x => x.Id == id).FirstOrDefault();
            return View(post);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Add()
        {
            ViewBag.checkgot = "Please enter your post data";
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(Post post)
        {
            post.PostedTime = DateTime.Now;
            post.Author = User.Identity.Name;
            try
            {
                _db.Posts.Add(post);
                _db.SaveChanges();
                return RedirectToAction("Details", new { id = post.Id });
            }
            catch
            {
                ViewBag.checkgot = "Something wrong happened";
            }
            return View();
        }

        [HttpGet]
        [Route("{Username}")]
        public IActionResult Profile(string username)
        {
            var UserPosts = _db.Posts.Where(x => x.Author == username).ToList();
            if (UserPosts == null || UserPosts.Count() == 0)
            {
                ViewBag.Empty = "Either the user doesn't exist or he hasn't posted yet!";
                return View();
            }                
            return View(UserPosts);
        }
    }
}
