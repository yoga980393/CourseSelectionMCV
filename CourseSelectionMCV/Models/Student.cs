using System.ComponentModel.DataAnnotations;

namespace CourseSelectionMCV.Models
{
    public class Student
    {
        [Display(Name = "學號")]
        public string StudentID { get; set; }

        [Display(Name = "密碼")]
        public string Password { get; set; }

        [Display(Name = "姓名")]
        public string Name { get; set; }

        [Display(Name = "電子郵件")]
        public string Email { get; set; }

        [Display(Name = "系所")]
        public string Department { get; set; }

        [Display(Name = "年級")]
        public int Grade { get; set; }
    }

}
