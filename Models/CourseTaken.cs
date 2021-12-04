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

        [Required]
        [Range(0, 4.0)]
        public double GPA { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [DisplayName("Scheduled Time")]
        public DateTime ScheduledTime { get; set; }

        [Required]
        public int Section { get; set; }

        [ForeignKey("StudentId")]
        public virtual Student Student { get; set; }
        [ForeignKey("FacultyId")]
        public virtual Faculty Faculty { get; set; }
        [ForeignKey("CourseId")]
        public virtual Course Course { get; set; }
    }
}
