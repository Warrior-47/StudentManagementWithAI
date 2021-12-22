using StudentManagementWithAI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagementWithAI.Models.ViewModels {
    public class GradesheetVM {
        public IEnumerable<StudentDetails> StudentDetails { get; set; }
        public double CGPA { get; set; }
    }
}
