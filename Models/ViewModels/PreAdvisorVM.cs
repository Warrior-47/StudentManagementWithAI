using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagementWithAI.Models.ViewModels {
    public class PreAdvisorVM {
        public List<string> Courses { get; set; }
        public List<string> Faculties { get; set; }
    }
}
