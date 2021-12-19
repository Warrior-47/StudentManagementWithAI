using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagementWithAI.Models {
    public class CoursesOffered {
        public int FacultyId { get; set; }
        public int CourseId { get; set; }

        [Required]
        [DisplayName("Max Students")]
        public int MaxStudents { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [DisplayName("Scheduled Time")]
        public DateTime ScheduledTime { get; set; }

        [Required]
        [MaxLength(2, ErrorMessage = "Cannot have more than 2 characters!")]
        public string WeekDays { get; set; }

        [Required]
        public int Section { get; set; }

        [ForeignKey("FacultyId")]
        public virtual Faculty Faculty { get; set; }
        [ForeignKey("CourseId")]
        public virtual Course Course { get; set; }

    }
}
