using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CourseSelectionMCV.Models
{
    public class Course
    {
        [Key]
        [Display(Name = "課程編號")]
        public string CourseID { get; set; }
        [Display(Name = "課程名稱")]
        public string Name { get; set; }
        [Display(Name = "簡稱")]
        public string ShortName { get; set; }
        [Display(Name = "開課系所")]
        public string Department { get; set; }
        [Display(Name = "課程介紹")]
        public string Introduction { get; set; }
        [Display(Name = "授課語言")]
        public string Language { get; set; }
        [Display(Name = "課程類型")]
        public string Type { get; set; }
        [Display(Name = "學分數")]
        public int Credits { get; set; }
        [Display(Name = "上課時數")]
        public int Hour { get; set; }
        [Display(Name = "上課時間")]
        public ICollection<Schedule> Schedules { get; set; }
        [Display(Name = "授課老師")]
        public ICollection<CourseTeacher> CourseTeachers { get; set; }
        [Display(Name = "上課地點")]
        public string Place { get; set; }
        [Display(Name = "已選人數")]
        public int NumberOfPeople { get; set; }
        [Display(Name = "人數上限")]
        public int MaxOfPeople { get; set; }
        [Display(Name = "課程圖片")]
        public string Image { get; set; }
    }

    public class Schedule
    {
        public int Id { get; set; }
        public string CourseId { get; set; }
        public int ScheduleTime { get; set; }
        public Course Course { get; set; }
    }

    [Table("course_Teacher")]
    public class CourseTeacher
    {
        public int Id { get; set; }
        public string CourseId { get; set; }
        public string TeacherId { get; set; }
        public Course Course { get; set; }
        public Teacher Teacher { get; set; }
    }
}

