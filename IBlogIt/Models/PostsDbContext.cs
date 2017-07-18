using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace IBlogIt.Models
{
    public class PostsDbContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public PostsDbContext(DbContextOptions<PostsDbContext> options) : base (options)
        {
            Database.EnsureCreated();
        }
    }
}
