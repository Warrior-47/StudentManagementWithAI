using Newtonsoft.Json;
using StudentManagementWithAI.Models;
using StudentManagementWithAI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagementWithAI.Utilities {

    public struct CombinedTime {
        public string TimeSlot;
        public string Weekdays;
    }
    
    public class PreAdvisorAI {
        private List<Solution> _varAssignments;
        private Dictionary<string, Dictionary<string, object>> _varDomains;

        public PreAdvisorAI(PreAdvisorVM userPrefs, IEnumerable<CoursesOffered> coursesOffered) {
            _varAssignments = new List<Solution>(new Solution[userPrefs.Courses.Count]);
            for (int i = 0; i < _varAssignments.Count; i++) {
                _varAssignments[i] = new Solution();

                _varAssignments[i].CourseCode = userPrefs.Courses[i].ToUpper();
                _varAssignments[i].FacultyInitial = userPrefs.Faculties[i] != null ? userPrefs.Faculties[i].ToUpper() : userPrefs.Faculties[i];
            }
            _varDomains = VariableDomains.GetDomains(coursesOffered, _varAssignments);
        }

        public List<Solution> BacktrackingSearch() {
            if(RecursiveBacktrackingSearch()) {
                return _varAssignments;
            }
            return new List<Solution>();
        }

        private bool RecursiveBacktrackingSearch() {
            if(CompletenessCheck()) {
                return true;
            }
            Solution selectedVar = SelectUnassignedVariable();
            for (int i = 0; i < (_varDomains[selectedVar.CourseCode]["Faculty"] as List<string>).Count(); i++) {
                var fDomain = _varDomains[selectedVar.CourseCode]["Faculty"] as List<string>;
                var tDomain = _varDomains[selectedVar.CourseCode]["Timeslot"] as List<CombinedTime>;

                if (ConsistencyCheck(selectedVar, fDomain[i], tDomain[i])) {
                    selectedVar.FacultyInitial = fDomain[i];
                    selectedVar.TimeSlot = tDomain[i].TimeSlot;
                    selectedVar.WeekDays = tDomain[i].Weekdays;

                    if (RecursiveBacktrackingSearch()) {
                        return true;
                    }
                    selectedVar.FacultyInitial = null;
                    selectedVar.TimeSlot = null;
                    selectedVar.WeekDays = null;
                }
            }
            return false;
        }

        private Solution SelectUnassignedVariable() {
            var unassignedVariables = _varAssignments.Where(u => u.TimeSlot == null).ToList();
            Solution chosenVar = unassignedVariables[0];
            int count = (_varDomains[chosenVar.CourseCode]["Faculty"] as List<string>).Count();

            for(int i=1; i < unassignedVariables.Count(); i++) {
                int currCount = (_varDomains[unassignedVariables[i].CourseCode]["Faculty"] as List<string>).Count();
                if (count > currCount) {
                    chosenVar = unassignedVariables[i];
                    count = currCount;
                }
            }
            return chosenVar;
        }

        private bool ConsistencyCheck(Solution selectedVar, string initial, CombinedTime time) {
            var assignmentsCopy = copyList(_varAssignments);

            int index = _varAssignments.IndexOf(selectedVar);
            assignmentsCopy[index].FacultyInitial = initial;
            assignmentsCopy[index].TimeSlot = time.TimeSlot;
            assignmentsCopy[index].WeekDays = time.Weekdays;

            if(!(_varDomains[assignmentsCopy[index].CourseCode]["Faculty"] as List<string>).Contains(assignmentsCopy[index].FacultyInitial)) {
                return false;
            }

            var notNullAssignmentsCopy = assignmentsCopy.Where(u => u.TimeSlot != null);
            if (notNullAssignmentsCopy.Select(u => new { u.TimeSlot, u.WeekDays }).Distinct().Count() != notNullAssignmentsCopy.Count()) {
                return false;
            }

            return true;
        }

        private bool CompletenessCheck() {
            if (_varAssignments.Select(u => u.TimeSlot).Contains(null)) {
                return false;
            }
            return true;
        }

        private List<Solution> copyList(List<Solution> oldList) {
            List<Solution> newList = new List<Solution>();
            foreach (var obj in oldList) {
                newList.Add(obj.copy());
            }
            return newList;
        }

    }

    public class Solution {
        public string CourseCode { get; set; }
        public string FacultyInitial { get; set; }
        public string TimeSlot { get; set; }
        public string WeekDays { get; set; }

        public Solution copy() {
            Solution newSol = new Solution() {
                CourseCode = this.CourseCode,
                FacultyInitial = this.FacultyInitial,
                TimeSlot = this.TimeSlot,
                WeekDays = this.WeekDays
            };

            return newSol;
        }
    }

    public static class VariableDomains {
        public static Dictionary<string, Dictionary<string, object>> GetDomains(IEnumerable<CoursesOffered> coursesOffered, List<Solution> varAssignments) {
            Dictionary<string, Dictionary<string, object>> data = new Dictionary<string, Dictionary<string, object>>();
            var groupedCourses = coursesOffered.GroupBy(u => u.CourseId).ToList();

            foreach (var course in groupedCourses) {
                var courseCode = course.ElementAt(0).Course.CourseCode;
                data[courseCode] = new Dictionary<string, object>();

                if(varAssignments.Select(u => u.CourseCode).Contains(courseCode) && varAssignments.Where(u => u.CourseCode == courseCode).FirstOrDefault().FacultyInitial != null) {
                    var index = varAssignments.Select(u => u.CourseCode).ToList().IndexOf(courseCode);

                    data[courseCode]["Faculty"] = course.Where(u => u.Faculty.Initial == varAssignments[index].FacultyInitial)
                                                                             .Select(u => u.Faculty.Initial).ToList();

                    data[courseCode]["Timeslot"] = course.Where(u => u.Faculty.Initial == varAssignments[index].FacultyInitial)
                                                                              .Select(u => new CombinedTime {
                                                                                  TimeSlot = u.ScheduledTime.ToString("hh:mm tt"),
                                                                                  Weekdays = u.WeekDays
                                                                              }).ToList();
                } else {
                    data[courseCode]["Faculty"] = course.Select(u => u.Faculty.Initial).ToList();
                    data[courseCode]["Timeslot"] = course.Select(u => new CombinedTime {
                        TimeSlot = u.ScheduledTime.ToString("hh:mm tt"),
                        Weekdays = u.WeekDays
                    }).ToList();
                }
            }

            return data;
        }

    }

}
