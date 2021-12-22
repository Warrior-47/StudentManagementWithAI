using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagementWithAI.Models.ViewModels;
using StudentManagementWithAI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagementWithAI.Controllers {
    [Authorize]
    public class CGPACalculatorController : Controller {
        public IActionResult Index(List<int>? credits, List<double>? gpas) {
            if (credits != null && gpas != null) {
                double CGPA = CGPACalculator.Calculate(gpas, credits);
                CalculatorVM calculatorVM = new CalculatorVM() { CGPA = CGPA };

                return View(calculatorVM);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Calculate() {
            List<int> credits = Request.Form["credit"].ToList().Select(int.Parse).ToList();
            List<double> gpas = Request.Form["gpa"].ToList().Select(double.Parse).ToList();

            return RedirectToAction("Index", new { credits = credits, gpas = gpas });
        }
    }
}
