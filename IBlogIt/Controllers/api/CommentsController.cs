using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IBlogIt.Models;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IBlogIt.Controllers.api
{
    [Route("api/posts/{PostId}/comments")]
    public class CommentsController : Controller
    {
        private readonly PostsDbContext _db;

        public CommentsController(PostsDbContext db)
        {
            _db = db;
        }
       
        [HttpGet]
        public IQueryable<Comment> Get(long PostId)
        {
            return _db.Comments.Where(x => x.Post.Id == PostId);
        }

        // POST api/values
        [HttpPost]
        [Authorize]
        public Comment Post(long PostId, [FromBody]Comment comment)
        {
            var post = _db.Posts.Where(x => x.Id == PostId).First();

            if (post == null)
                return null;
            comment.Post = post;
            comment.PostedTime = DateTime.Now;
            comment.Author = User.Identity.Name;
            _db.Comments.Add(comment);
            _db.SaveChanges();

            return comment;

        }
    }
}
