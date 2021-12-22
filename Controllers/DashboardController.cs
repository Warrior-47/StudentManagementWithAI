using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StudentManagementWithAI.Data;
using StudentManagementWithAI.Models;
using StudentManagementWithAI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagementWithAI.Controllers {
    [Authorize]
    public class DashboardController : Controller {
        private readonly ILogger<DashboardController> _logger;
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public DashboardController(UserManager<ApplicationUser> userManager, ILogger<DashboardController> logger, ApplicationDbContext db) {
            _userManager = userManager;
            _logger = logger;
            _db = db;
        }

        public IActionResult Index() {
            if (User.IsInRole(Constants.StudentRole)) {
                var userId = _userManager.GetUserId(User);
                Student currentStudent = _db.Student.Where(u => u.UserId == userId).FirstOrDefault();

                IEnumerable<CourseTaken> coursesTakenByUser = _db.CourseTaken.Where(
                    u => u.StudentId == currentStudent.Id && u.GPA == null
                ).Include(u => u.CoursesOffered).Include(u => u.CoursesOffered.Course);

                DashboardVM dashboardVM = new DashboardVM();
                dashboardVM.CreateData(coursesTakenByUser);

                return View(dashboardVM);
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
