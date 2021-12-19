using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagementWithAI.Models {
    public class CourseTaken {
        public int StudentId { get; set; }

        public int FacultyId { get; set; }

        public int CourseId { get; set; }

        public int Section { get; set; }

        [Range(0, 4.0, ErrorMessage = "GPA needs to a non-negative number less than 4")]
        public double? GPA { get; set; }

        [ForeignKey("StudentId")]
        public virtual Student Student { get; set; }

        [ForeignKey("FacultyId,CourseId,Section")]
        public virtual CoursesOffered CoursesOffered { get;set; }
    }
}
