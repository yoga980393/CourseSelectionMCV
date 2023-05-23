using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace CourseSelectionMCV.Models
{
    [Table("grades")]
    public class Grade
    {
        [Key]
        [Display(Name = "編號")]
        public int Id { get; set; }

        [Display(Name = "年份")]
        public int Year { get; set; }

        [Display(Name = "學期")]
        public string Semester { get; set; }

        [Display(Name = "學號")]
        [ForeignKey("Student")]
        public string StudentID { get; set; }

        [Display(Name = "課程編號")]
        [ForeignKey("Course")]
        public string CourseID { get; set; }

        [Display(Name = "分數")]
        public int Score { get; set; }

        public Student Student { get; set; }
        public Course Course { get; set; }
    }
}
