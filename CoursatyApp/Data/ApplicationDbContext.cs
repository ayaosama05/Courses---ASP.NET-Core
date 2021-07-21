using CoursatyApp.Entitities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CoursatyApp.Data
{
    public class ApplicationUser : IdentityUser
    {
        [StringLength(250)] //if we don't write it  , It'll be stored as nvarchar(MAX) in DB which takes 2 GB
        public string FirstName { get; set; }
        [StringLength(250)]
        public string LasttName { get; set; }
        [StringLength(250)]
        public string MobilePhone { get; set; }

        public int Country { get; set; }
        [ForeignKey("UserId")]
        public virtual ICollection<UserCategory> UserCategories { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryItem> CategoryItems { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<UserCategory> UserCategories { get; set; }
        public DbSet<MediaType> MediaTypes { get; set; }

    }
}
