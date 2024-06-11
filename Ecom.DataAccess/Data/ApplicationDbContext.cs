using System;
using Ecom.Models.Category;
using Microsoft.EntityFrameworkCore;

namespace Ecom.DataAccess.Data
{
	public class ApplicationDbContext: DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
			
		}

		public DbSet<Category> Category { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId=1, DisplayOrder = 1, Name = "Action" },
                new Category { CategoryId=2, DisplayOrder = 2, Name = "SciFi" });
        }
    }
}

