using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagementWithAI.Models {
    public class Course {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(6, ErrorMessage = "Course Code cannot be more than 6 characters")]
        [DisplayName("Course Code")]
        public string CourseCode { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [Range(3,4, ErrorMessage = "Credit hours can only be 3 or 4")]
        public int CreditHours { get; set; }

        [DisplayName("Prerequisite")]
        public int PreReqId { get; set; }
        public int DepartmentId { get; set; }

        [ForeignKey("PreReqId")]
        public virtual Course PreReqCourse { get; set; }
        [ForeignKey("DepartmentId")]
        public virtual Department Department { get; set; }
    }
}
