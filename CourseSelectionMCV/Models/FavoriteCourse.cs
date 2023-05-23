using System.ComponentModel.DataAnnotations.Schema;

namespace CourseSelectionMCV.Models
{
    [Table("favorite_courses")]
    public class FavoriteCourse
    {
        public int Id { get; set; }
        public string StudentId { get; set; }
        public string CourseId { get; set; }

        // Navigation properties
        public Student Student { get; set; }
        public Course Course { get; set; }
    }
}
