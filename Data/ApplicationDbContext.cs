using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace project001.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<project001.Models.Cart> Cart { get; set; }
        public DbSet<project001.Models.Category> Category { get; set; }
        public DbSet<project001.Models.Comments> Comments { get; set; }
        public DbSet<project001.Models.Features> Features { get; set; }
        public DbSet<project001.Models.Film> Item { get; set; }
    }
}
