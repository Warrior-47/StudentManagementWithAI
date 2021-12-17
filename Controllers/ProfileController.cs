using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementWithAI.Data;
using StudentManagementWithAI.Models;
using StudentManagementWithAI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace StudentManagementWithAI.Controllers {
    [Authorize]
    public class ProfileController : Controller {

        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProfileController(UserManager<ApplicationUser> userManager, ApplicationDbContext db) {
            _db = db;
            _userManager = userManager;
        }

        public IActionResult Index() {
            var userId = _userManager.GetUserId(User);

            ProfileVM profileVM;
            if (User.IsInRole(Constants.StudentRole)) {
                profileVM = new ProfileVM() {
                    Student = _db.Student.Include(u => u.ApplicationUser).Where(u => u.UserId == userId).FirstOrDefault()
                };
            } else {
                profileVM = new ProfileVM() {
                    Faculty = _db.Faculty.Include(u => u.ApplicationUser).Where(u => u.UserId == userId).FirstOrDefault()
                };
            }

            return View(profileVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Constants.FacultyRole)]
        public async Task<IActionResult> Update(ProfileVM profileVM) {
            if (ModelState.IsValid) {
                var userToUpdate = await _userManager.FindByIdAsync(profileVM.Faculty.UserId);
                var facultyToUpdate = await _db.Faculty.FindAsync(profileVM.Faculty.Id);

                userToUpdate.PhoneNumber = profileVM.Faculty.ApplicationUser.PhoneNumber;
                userToUpdate.Email = profileVM.Faculty.ApplicationUser.Email;

                facultyToUpdate.Name = profileVM.Faculty.Name;
                facultyToUpdate.Address = profileVM.Faculty.Address;

                var result = await _userManager.UpdateAsync(userToUpdate);
                if (result.Succeeded) {
                    _db.Faculty.Update(facultyToUpdate);
                    _db.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }
    }
}
