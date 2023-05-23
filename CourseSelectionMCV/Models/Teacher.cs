using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace CourseSelectionMCV.Models
{
    public class Teacher
    {
        [Display(Name = "工號")]
        public string TeacherID { get; set; }
        [Display(Name = "姓名")]
        public string TeacherName { get; set; }
        public ICollection<CourseTeacher> CourseTeachers { get; set; }
    }
}
