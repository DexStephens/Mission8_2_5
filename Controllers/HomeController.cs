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
        public IActionResult AddEdit(int? taskId)
        {
            ViewBag.Categories = _taskSubmissionContext.categories.ToList();
            var task = new Task();
            if (taskId != null)
            {
                task = _taskSubmissionContext.tasks.Single(x => x.TaskId == taskId);
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
                    if (task.TaskId == 0)
                    {
                        _taskSubmissionContext.Add(task);
                    }
                    else
                    {
                        _taskSubmissionContext.Update(task);
                    }
                    _taskSubmissionContext.SaveChanges();
                    return RedirectToAction("Quadrants");
                }
                catch
                {
                    ViewBag.Categories = _taskSubmissionContext.tasks.ToList();
                    return View("AddEdit", task);
                }
            }
            else
            {
                ViewBag.Categories = _taskSubmissionContext.tasks.ToList();
                return View("AddEdit", task);
            }
        }

        [HttpGet]
        public IActionResult Delete(int taskId)
        {
            Task taskToDelete = _taskSubmissionContext.tasks.Single(x => x.TaskId == taskId);
            return View(taskToDelete);
        }

        [HttpPost]
        public IActionResult Delete(Task taskToDelete)
        {
            try
            {
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
