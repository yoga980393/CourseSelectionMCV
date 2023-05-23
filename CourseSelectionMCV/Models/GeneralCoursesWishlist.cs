using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace CourseSelectionMCV.Models
{
    [Table("general_courses_wishlist")]
    public class GeneralCoursesWishlist
    {
        [Key]
        [Display(Name = "編號")]
        public int Id { get; set; }

        [Display(Name = "學號")]
        [ForeignKey("Student")]
        public string StudentID { get; set; }

        [ForeignKey("Course1")]
        [Display(Name = "選擇一")]
        public string Choice_1 { get; set; }

        [ForeignKey("Course2")]
        [Display(Name = "選擇二")]
        public string Choice_2 { get; set; }

        [ForeignKey("Course3")]
        [Display(Name = "選擇三")]
        public string Choice_3 { get; set; }

        [ForeignKey("Course4")]
        [Display(Name = "選擇四")]
        public string Choice_4 { get; set; }

        [ForeignKey("Course5")]
        [Display(Name = "選擇五")]
        public string Choice_5 { get; set; }

        [ForeignKey("Course6")]
        [Display(Name = "選擇六")]
        public string Choice_6 { get; set; }

        [ForeignKey("Course7")]
        [Display(Name = "選擇七")]
        public string Choice_7 { get; set; }

        [ForeignKey("Course8")]
        [Display(Name = "選擇八")]
        public string Choice_8 { get; set; }

        [ForeignKey("Course9")]
        [Display(Name = "選擇九")]
        public string Choice_9 { get; set; }

        [ForeignKey("Course10")]
        [Display(Name = "選擇十")]
        public string Choice_10 { get; set; }

        public Student Student { get; set; }
        public Course Course1 { get; set; }
        public Course Course2 { get; set; }
        public Course Course3 { get; set; }
        public Course Course4 { get; set; }
        public Course Course5 { get; set; }
        public Course Course6 { get; set; }
        public Course Course7 { get; set; }
        public Course Course8 { get; set; }
        public Course Course9 { get; set; }
        public Course Course10 { get; set; }
    }

    public class GeneralCoursesWishlistInput
    {
        public int Id { get; set; }
        public string StudentID { get; set; }
        public string[] Choices { get; set; }
    }
}
