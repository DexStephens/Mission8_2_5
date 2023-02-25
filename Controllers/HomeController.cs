using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mission8_2_5.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Mission8_2_5.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private TaskSubmissionContext _taskSubmissionContext;
        public HomeController(ILogger<HomeController> logger, TaskSubmissionContext taskSubmissionContext)
        {
            _logger = logger;
            _taskSubmissionContext = taskSubmissionContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Quadrants()
        {
            var allTasks = _taskSubmissionContext.tasks.Include(x => x.Category);
            return View(allTasks);
        }

        [HttpGet]
        public IActionResult AddEdit(string? taskName)
        {
            ViewBag.Categories = _taskSubmissionContext.categories.ToList();
            var task = new Task();
            if (taskName != null)
            {
                task = _taskSubmissionContext.tasks.Single(x => x.task == taskName);
            }
            return View(task);
        }

        [HttpPost]
        public IActionResult AddEdit(Task task)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Task oldTask = _taskSubmissionContext.tasks.Single(x => x.TaskId == task.TaskId);
                    _taskSubmissionContext.Update(task);
                }
                catch
                {
                    _taskSubmissionContext.Add(task);
                }
                finally
                {
                    _taskSubmissionContext.SaveChanges();
                }
                return RedirectToAction("Quadrants");
            }
            else
            {
                ViewBag.Categories = _taskSubmissionContext.tasks.ToList();
                return View("AddEdit", task);
            }
        }

        [HttpGet]
        public IActionResult DeleteTask(string taskName)
        {
            Task taskToDelete = _taskSubmissionContext.tasks.Single(x => x.task == taskName);
            return View(taskToDelete);
        }

        [HttpPost]
        public IActionResult DeleteTask(int taskId)
        {
            try
            {
                Task taskToDelete = _taskSubmissionContext.tasks.Single(x => x.TaskId == taskId);
                _taskSubmissionContext.tasks.Remove(taskToDelete);
                _taskSubmissionContext.SaveChanges();

                return RedirectToAction("Quadrants");
            }
            catch
            {
                return RedirectToAction("Quadrants");
            }
        }
    }
}
