using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace CourseSelectionMCV.Models
{
    [Table("credit_requirements")]
    public class CreditRequirements
    {
        [Key]
        [Display(Name = "編號")]
        public int Id { get; set; }

        [Display(Name = "學號")]
        [ForeignKey("Student")]
        public string StudentID { get; set; }

        [Display(Name = "必修學分")]
        [Column("required_credits")]
        public int RequiredCredits { get; set; }

        [Display(Name = "選修學分（主修）")]
        [Column("elective_major")]
        public int ElectiveMajor { get; set; }

        [Display(Name = "選修學分（輔修）")]
        [Column("elective_minor")]
        public int ElectiveMinor { get; set; }

        [Display(Name = "通識學分（人文學科）")]
        [Column("general_humanities")]
        public int GeneralHumanities { get; set; }

        [Display(Name = "通識學分（社會科學）")]
        [Column("general_social_sciences")]
        public int GeneralSocialSciences { get; set; }

        [Display(Name = "通識學分（藝術）")]
        [Column("general_arts")]
        public int GeneralArts { get; set; }

        [Display(Name = "通識學分（自然科學）")]
        [Column("general_sciences")]
        public int GeneralSciences { get; set; }

        [Display(Name = "通識學分（自我探索）")]
        [Column("general_self")]
        public int GeneralSelf { get; set; }

        [Display(Name = "通識學分（生物醫學）")]
        [Column("general_biomedical")]
        public int GeneralBiomedical { get; set; }

        [Display(Name = "通識學分（多元進階）")]
        [Column("general_diversity")]
        public int GeneralDiversity { get; set; }

        [Display(Name = "通識總學分")]
        [Column("general_total")]
        public int GeneralTotal { get; set; }

        [Display(Name = "英語學分")]
        [Column("english_credits")]
        public int EnglishCredits { get; set; }

        [Display(Name = "體育總學分")]
        [Column("physical_total")]
        public int PhysicalTotal { get; set; }

        [Display(Name = "體育必修學分")]
        [Column("physical_required")]
        public int PhysicalRequired { get; set; }

        [Display(Name = "體適能")]
        [Column("physical_fitness")]
        public bool PhysicalFitness { get; set; }

        [Display(Name = "游泳")]
        [Column("physical_swimming")]
        public bool PhysicalSwimming { get; set; }

        [Display(Name = "軍訓學分")]
        [Column("military_training")]
        public int MilitaryTraining { get; set; }

        [Display(Name = "總學分")]
        [Column("total_credits")]
        public int TotalCredits { get; set; }

        public Student Student { get; set; }
    }

}
