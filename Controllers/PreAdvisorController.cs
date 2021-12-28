using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using StudentManagementWithAI.Data;
using StudentManagementWithAI.Models;
using StudentManagementWithAI.Models.ViewModels;
using StudentManagementWithAI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagementWithAI.Controllers {
    [Authorize]
    public class PreAdvisorController : Controller {

        private readonly ApplicationDbContext _db;

        public PreAdvisorController(ApplicationDbContext db) {
            _db = db;
        }

        public IActionResult Index() {
            PreAdvisorVM preAdvisorVM = new PreAdvisorVM();
            return View(preAdvisorVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ShowResult(PreAdvisorVM preAdvisorVM) {
            if (preAdvisorVM.Courses.Contains(null)) {
                Response.StatusCode = 400;
                return Content("<div><h3>Course Field(s) cannot be Empty<h3></div>", "text/html");
            }
            var coursesOffered = _db.CoursesOffered.Include(u => u.Course).Include(u => u.Faculty);

            PreAdvisorAI AI = new PreAdvisorAI(preAdvisorVM, coursesOffered);

            try {
                var result = AI.BacktrackingSearch();
                return View(result);
            }
            catch(KeyNotFoundException e) {
                string courseCode = e.Message.Split('\'')[1];
                return Content("<div><h3>"+ courseCode +" does not exist<h3></div>", "text/html");
            }
        }
    }
}
