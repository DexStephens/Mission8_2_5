using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mission8_2_5.Models
{
    public class TaskSubmissionContext : DbContext
    {

        //Constructor
        public TaskSubmissionContext(DbContextOptions<TaskSubmissionContext> options) : base(options)
        {
        }

        public DbSet<Task> tasks { get; set; }
        public DbSet<Category> categories { get; set; }


        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<Category>().HasData(
                new Category { CategoryId = 1, categoryName = "Urgent, Important"},
                new Category { CategoryId = 2, categoryName = "Not Urgent, Important"},
                new Category { CategoryId = 3, categoryName = "Urgent, Not Important" },
                new Category { CategoryId = 4, categoryName = "Not Urgent, Not Important"}
                
                );
        }
    }
}
