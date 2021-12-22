using StudentManagementWithAI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagementWithAI.Utilities {
    public static class CGPACalculator {
        public static double Calculate(List<double> GPAs, List<int> credits) {
            double CGPA = 0;

            for(int i = 0; i < GPAs.Count; i++) {
                CGPA += GPAs[i] * credits[i];
            }
            CGPA = CGPA / credits.Sum();
            CGPA = Math.Round(CGPA, 2);

            return CGPA;
        }
    }
}
