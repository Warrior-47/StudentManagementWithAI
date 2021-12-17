using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using StudentManagementWithAI.Data;
using StudentManagementWithAI.Models;

namespace StudentManagementWithAI.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly ApplicationDbContext _db;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            IWebHostEnvironment webHostEnvironment,
        ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            ApplicationDbContext db)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IEnumerable<SelectListItem> DepartmentDropDown;

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Required]
            public string Name { get; set; }
            
            public string Address { get; set; }

            [Required]
            [Display(Name = "Phone Number")]
            public string PhoneNumber { get; set; }

            [Required]
            public char Gender { get; set; }

            [DataType(DataType.Date)]
            [Display(Name = "Date of Birth")]
            public DateTime DOB { get; set; }

            [Required]
            public int DepartmentId { get; set; }

            [Display(Name = "Register Student")]
            public bool RegisterStudent { get; set; }

            /* Only Faculty Fields */
            [StringLength(4, MinimumLength = 3, ErrorMessage = "Initial must be at least 3 characters.")]
            public string? Initial { get; set; }

            /* Only Student Fields */
            [Display(Name = "Total Credits")]
            [Range(1, int.MaxValue, ErrorMessage = "Value must be positive.")]
            public int? TotalCredits { get; set; }


        }

        public async Task OnGetAsync(string returnUrl = null) {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            DepartmentDropDown = _db.Department.Select(i => new SelectListItem {
                Text = i.Name,
                Value = i.Id.ToString()
            });
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null) {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid) {
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;
                string fileName = "";

                if (files.Count() > 0) {
                    string upload = webRootPath + Constants.UserImagePath;
                    fileName = Input.Email + Path.GetExtension(files[0].FileName);
                    using (var fileStream = new FileStream(Path.Combine(upload, fileName), FileMode.Create)) {
                        files[0].CopyTo(fileStream);
                    }
                }

                var user = new ApplicationUser { UserName = Input.Email, Email = Input.Email, PhotoName = fileName, PhoneNumber = Input.PhoneNumber };
                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded) {
                    await _userManager.AddClaimAsync(user, new Claim("PhotoName", user.PhotoName));

                    if (Input.RegisterStudent) {
                        await _userManager.AddToRoleAsync(user, Constants.StudentRole);

                        var obj = new Student {
                            Name = Input.Name,
                            Address = Input.Address,
                            Gender = Input.Gender,
                            TotalCredits = Input.TotalCredits.Value,
                            DOB = Input.DOB,
                            UserId = user.Id,
                            DepartmentId = Input.DepartmentId
                        };
                        await _db.Student.AddAsync(obj);

                    } else {
                        await _userManager.AddToRoleAsync(user, Constants.FacultyRole);

                        var obj = new Faculty {
                            Name = Input.Name,
                            Initial = Input.Initial.ToUpper(),
                            Address = Input.Address,
                            Gender = Input.Gender,
                            DOB = Input.DOB,
                            UserId = user.Id,
                            DepartmentId = Input.DepartmentId
                        };
                        await _db.Faculty.AddAsync(obj);
                    }
                    await _db.SaveChangesAsync();

                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount) {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    } else {
                        //await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect("/Identity/Account/Login");
                    }
                }
                foreach (var error in result.Errors) {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            DepartmentDropDown = _db.Department.Select(i => new SelectListItem {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
