using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace IBlogIt.Models.Account
{
    public class AccountsDbContext : IdentityDbContext
    {
        public AccountsDbContext(DbContextOptions<AccountsDbContext> options) : base (options)
        {
            Database.EnsureCreated();
        }
    }
}
