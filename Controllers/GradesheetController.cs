using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementWithAI.Data;
using StudentManagementWithAI.Models;
using StudentManagementWithAI.Models.ViewModels;
using StudentManagementWithAI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagementWithAI.Controllers {
    [Authorize(Roles = Constants.StudentRole)]
    public class GradesheetController : Controller {

        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public GradesheetController(ApplicationDbContext db, UserManager<ApplicationUser> userManager) {
            _db = db;
            _userManager = userManager;
        }

        public IActionResult Index() {
            Student currentStudent = _db.Student.Where(u => u.UserId == _userManager.GetUserId(User)).FirstOrDefault();
            IEnumerable<CourseTaken> coursesDone = _db.CourseTaken.Where(u => u.StudentId == currentStudent.Id && u.GPA != null)
                .Include(u => u.CoursesOffered).Include(u => u.CoursesOffered.Course);

            var parsedData = DataParser.GpaAndCredit(coursesDone);
            double CGPA = CGPACalculator.Calculate(parsedData[0] as List<double>, parsedData[1] as List<int>);

            IEnumerable<StudentDetails> studentDetails = coursesDone.Select(u => new StudentDetails {
                courseCode = u.CoursesOffered.Course.CourseCode,
                courseTitle = u.CoursesOffered.Course.Title,
                creditHours = u.CoursesOffered.Course.CreditHours,
                gradePoint = u.GPA.Value
            });

            GradesheetVM gradesheetVM = new GradesheetVM() { StudentDetails = studentDetails, CGPA = CGPA };

            return View(gradesheetVM);
        }
    }
}
