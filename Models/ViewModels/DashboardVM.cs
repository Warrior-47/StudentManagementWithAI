using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagementWithAI.Models.ViewModels {
    public class DashboardVM {
        public Dictionary<string, KeyValuePair<string, string>> SchedulesData { get; set; }

        public DashboardVM() {
            SchedulesData = new Dictionary<string, KeyValuePair<string, string>>();
        }

        public void CreateData(IEnumerable<CourseTaken> coursesTaken) {
            foreach (var obj in coursesTaken) {
                var offered = obj.CoursesOffered;
                var course = obj.CoursesOffered.Course;

                string time = offered.ScheduledTime.ToString("hh:mm tt");
                KeyValuePair<string, string> dayAndTime = new KeyValuePair<string, string>(offered.WeekDays, time);
                SchedulesData[course.CourseCode] = dayAndTime;
            }
        }
    }
}
