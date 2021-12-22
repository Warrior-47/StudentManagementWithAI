using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagementWithAI.Models.ViewModels {
    public class CalculatorVM {
        public double CGPA { get; set; }

        public bool Any() {
            if (double.IsNaN(CGPA)) 
                return false;

            return true;
        }
    }
}
