using StudentManagementWithAI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagementWithAI.Utilities {
    public static class DataParser {
        public static List<object> GpaAndCredit(IEnumerable<CourseTaken> data) {
            List<object> parsedData = new List<object>();
            List<double> GPAs = new List<double>();
            List<int> credits = new List<int>();

            foreach (var obj in data) {
                GPAs.Add(obj.GPA.Value);
                credits.Add(obj.CoursesOffered.Course.CreditHours);
            }
            parsedData.Add(GPAs);
            parsedData.Add(credits);

            return parsedData;
        }
    }

    public struct StudentDetails {
        public string courseCode;
        public string courseTitle;
        public int creditHours;
        public double gradePoint;
        public double CGPA;
    }
}
