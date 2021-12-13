using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagementWithAI.Models {
    public class Student {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Address { get; set; }

        [Required]
        public char Gender { get; set; }

        [Range(0, 4.0, ErrorMessage = "CGPA needs to be a non-negative number less than 4")]
        [DefaultValue(0.0)]
        public double CGPA { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Date of Birth")]
        public DateTime DOB { get; set; }

        [DefaultValue(0)]
        [DisplayName("Total Credits")]
        public int TotalCredits { get; set; }
        public string UserId { get; set; }
        public int DepartmentId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
        [ForeignKey("DepartmentId")]
        public virtual Department Department { get; set; }

    }
}
