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
                new Category { CategoryId = 1, categoryName = "Home" },
                new Category { CategoryId = 2, categoryName = "School" },
                new Category { CategoryId = 3, categoryName = "Work" },
                new Category { CategoryId = 4, categoryName = "Church" }


                );
            mb.Entity<Task>().HasData(
                new Task 
                { 
                    TaskId = 1, 
                    CategoryId = 1,

                    task = "Apply for internships",
                    dueDate = "2023-01-01",
                    quadrant = 3,
                    completed = false
                },

                new Task
                {
                    TaskId = 2,
                    CategoryId = 2,

                    task = "Finish Mission 8",
                    dueDate = "2023-02-24",
                    quadrant = 2,
                    completed = false
                }
                );
        }
    }
}s
