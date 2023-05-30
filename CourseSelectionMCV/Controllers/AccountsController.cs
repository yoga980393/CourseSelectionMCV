using CourseSelectionMCV.Data;
using CourseSelectionMCV.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace CourseSelectionMCV.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AccountsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ngrok http 34858 --host-header="localhost:34858"

        // POST: api/Accounts/Login
        [HttpPost("Login")]
        public async Task<ActionResult<Student>> Login(Student student)
        {
            var user = await _context.Students.FirstOrDefaultAsync(x => x.StudentID == student.StudentID && x.Password == student.Password);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpGet("GetAllCourseData")]
        public IActionResult GetAllCourseData()
        {
            var courses = _context.Courses.Include(c => c.Schedules).Include(c => c.CourseTeachers).ThenInclude(ct => ct.Teacher).ToList();

            var result = courses.Select(c => new
            {
                id = c.CourseID,
                name = c.Name,
                shortName = c.ShortName,
                department = c.Department,
                introduction = c.Introduction,
                language = c.Language,
                type = c.Type,
                credits = c.Credits,
                hour = c.Hour,
                place = c.Place,
                numberOfPeople = c.NumberOfPeople,
                maxOfPeople = c.MaxOfPeople,
                image = c.Image,
                schedule = c.Schedules.Select(s => s.ScheduleTime).ToList(),
                teacher = c.CourseTeachers.Select(ct => ct.Teacher.TeacherName).ToList()
            }).ToList();


            return Ok(result);
        }

        //已加選成功的課程
        [HttpGet("GetEnrolledCourses/{studentId}")]
        public IActionResult GetEnrolledCourses(string studentId)
        {
            var enrolledCourses = _context.EnrolledCourses
                .Include(ec => ec.Course)
                    .ThenInclude(c => c.Schedules)
                .Include(ec => ec.Course)
                    .ThenInclude(c => c.CourseTeachers)
                        .ThenInclude(ct => ct.Teacher)
                .Where(ec => ec.StudentId == studentId)
                .ToList();

            var result = enrolledCourses.Select(ec => new
            {
                id = ec.Course.CourseID,
                name = ec.Course.Name,
                shortName = ec.Course.ShortName,
                department = ec.Course.Department,
                introduction = ec.Course.Introduction,
                language = ec.Course.Language,
                type = ec.Course.Type,
                credits = ec.Course.Credits,
                hour = ec.Course.Hour,
                place = ec.Course.Place,
                numberOfPeople = ec.Course.NumberOfPeople,
                maxOfPeople = ec.Course.MaxOfPeople,
                image = ec.Course.Image,
                schedule = ec.Course.Schedules.Select(s => s.ScheduleTime).ToList(),
                teacher = ec.Course.CourseTeachers.Select(ct => ct.Teacher.TeacherName).ToList()
            });

            return Ok(result);
        }

        [HttpDelete("RemoveEnrolledCourse/{studentId}")]
        public async Task<IActionResult> RemoveEnrolledCourse(string studentId, [FromBody] Course course)
        {
            try
            {
                // 尋找符合學生ID和課程ID的選課實體
                var courseToRemove = await _context.EnrolledCourses
                    .FirstOrDefaultAsync(ec => ec.StudentId == studentId && ec.CourseId == course.CourseID);

                if (courseToRemove == null)
                {
                    return NotFound(); // 如果找不到該選課實體，返回404 Not Found
                }

                // 移除該選課實體
                _context.EnrolledCourses.Remove(courseToRemove);

                // 將變更儲存到資料庫
                await _context.SaveChangesAsync();

                return NoContent(); // 如果成功，返回204 No Content
            }
            catch (Exception ex)
            {
                // 如果有錯誤，返回500 Internal Server Error並附帶錯誤訊息
                return StatusCode(500, ex.Message);
            }
        }

        //已進入抽選的課程
        [HttpGet("GetLotteryCourses/{studentId}")]
        public IActionResult GetLotteryCourses(string studentId)
        {
            var lotteryCourses = _context.LotteryCourses
                .Include(ec => ec.Course)
                    .ThenInclude(c => c.Schedules)
                .Include(ec => ec.Course)
                    .ThenInclude(c => c.CourseTeachers)
                        .ThenInclude(ct => ct.Teacher)
                .Where(ec => ec.StudentId == studentId)
                .ToList();

            var result = lotteryCourses.Select(ec => new
            {
                id = ec.Course.CourseID,
                name = ec.Course.Name,
                shortName = ec.Course.ShortName,
                department = ec.Course.Department,
                introduction = ec.Course.Introduction,
                language = ec.Course.Language,
                type = ec.Course.Type,
                credits = ec.Course.Credits,
                hour = ec.Course.Hour,
                place = ec.Course.Place,
                numberOfPeople = ec.Course.NumberOfPeople,
                maxOfPeople = ec.Course.MaxOfPeople,
                image = ec.Course.Image,
                schedule = ec.Course.Schedules.Select(s => s.ScheduleTime).ToList(),
                teacher = ec.Course.CourseTeachers.Select(ct => ct.Teacher.TeacherName).ToList()
            });

            return Ok(result);
        }

        [HttpPost("AddCourse/{studentId}")]
        public async Task<IActionResult> AddCourse(string studentId, [FromBody] Course course)
        {
            var user = await _context.Students.FindAsync(studentId);

            if (user == null)
            {
                return NotFound();
            }

            var lotteryCourse = new LotteryCourse
            {
                StudentId = user.StudentID,
                CourseId = course.CourseID,
            };

            await _context.LotteryCourses.AddAsync(lotteryCourse);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("RemoveCourse/{studentId}")]
        public async Task<IActionResult> RemoveCourse(string studentId, [FromBody] Course course)
        {
            try
            {
                // 尋找符合學生ID和課程ID的選課實體
                var courseToRemove = await _context.LotteryCourses
                    .FirstOrDefaultAsync(ec => ec.StudentId == studentId && ec.CourseId == course.CourseID);

                if (courseToRemove == null)
                {
                    return NotFound(); // 如果找不到該選課實體，返回404 Not Found
                }

                // 移除該選課實體
                _context.LotteryCourses.Remove(courseToRemove);

                // 將變更儲存到資料庫
                await _context.SaveChangesAsync();

                return NoContent(); // 如果成功，返回204 No Content
            }
            catch (Exception ex)
            {
                // 如果有錯誤，返回500 Internal Server Error並附帶錯誤訊息
                return StatusCode(500, ex.Message);
            }
        }

        //已加入收藏夾的課程
        [HttpGet("GetFavoriteCourses/{studentId}")]
        public IActionResult GetFavoriteCourses(string studentId)
        {
            var favoriteCourses = _context.FavoriteCourses
                .Include(ec => ec.Course)
                    .ThenInclude(c => c.Schedules)
                .Include(ec => ec.Course)
                    .ThenInclude(c => c.CourseTeachers)
                        .ThenInclude(ct => ct.Teacher)
                .Where(ec => ec.StudentId == studentId)
                .ToList();

            var result = favoriteCourses.Select(ec => new
            {
                id = ec.Course.CourseID,
                name = ec.Course.Name,
                shortName = ec.Course.ShortName,
                department = ec.Course.Department,
                introduction = ec.Course.Introduction,
                language = ec.Course.Language,
                type = ec.Course.Type,
                credits = ec.Course.Credits,
                hour = ec.Course.Hour,
                place = ec.Course.Place,
                numberOfPeople = ec.Course.NumberOfPeople,
                maxOfPeople = ec.Course.MaxOfPeople,
                image = ec.Course.Image,
                schedule = ec.Course.Schedules.Select(s => s.ScheduleTime).ToList(),
                teacher = ec.Course.CourseTeachers.Select(ct => ct.Teacher.TeacherName).ToList()
            });

            return Ok(result);
        }



        [HttpPost("AddFavoriteCourse/{studentId}")]
        public async Task<IActionResult> AddFavoriteCourse(string studentId, [FromBody] Course course)
        {
            var user = await _context.Students.FindAsync(studentId);

            if (user == null)
            {
                return NotFound();
            }

            var favoriteCourse = new FavoriteCourse
            {
                StudentId = user.StudentID,
                CourseId = course.CourseID,
            };

            await _context.FavoriteCourses.AddAsync(favoriteCourse);
            await _context.SaveChangesAsync();

            return Ok();
        }



        [HttpDelete("RemoveFavoriteCourse/{studentId}")]
        public async Task<IActionResult> RemoveFavoriteCourse(string studentId, [FromBody] Course course)
        {
            try
            {
                // 尋找符合學生ID和課程ID的選課實體
                var courseToRemove = await _context.FavoriteCourses
                    .FirstOrDefaultAsync(ec => ec.StudentId == studentId && ec.CourseId == course.CourseID);

                if (courseToRemove == null)
                {
                    return NotFound(); // 如果找不到該選課實體，返回404 Not Found
                }

                // 移除該選課實體
                _context.FavoriteCourses.Remove(courseToRemove);

                // 將變更儲存到資料庫
                await _context.SaveChangesAsync();

                return NoContent(); // 如果成功，返回204 No Content
            }
            catch (Exception ex)
            {
                // 如果有錯誤，返回500 Internal Server Error並附帶錯誤訊息
                return StatusCode(500, ex.Message);
            }
        }

        //通識志願

        [HttpGet("GetGeneralCoursesWishlist/{studentId}")]
        public IActionResult GetGeneralCoursesWishlist(string studentId)
        {
            var wishlist = _context.GeneralCoursesWishlist
                .Where(w => w.StudentID == studentId)
                .FirstOrDefault();

            if (wishlist == null)
            {
                return NotFound();
            }

            var result = new
            {
                id = wishlist.Id,
                studentId = wishlist.StudentID,
                choices = new string[] { wishlist.Choice_1, wishlist.Choice_2, wishlist.Choice_3, wishlist.Choice_4, wishlist.Choice_5,
            wishlist.Choice_6, wishlist.Choice_7, wishlist.Choice_8, wishlist.Choice_9, wishlist.Choice_10 }
            };

            return Ok(result);
        }

        [HttpPost("UpdateGeneralCoursesWishlist/{studentId}")]
        public IActionResult UpdateGeneralCoursesWishlist(string studentId, [FromBody] GeneralCoursesWishlistInput input)
        {
            var wishlist = _context.GeneralCoursesWishlist
                .Where(w => w.StudentID == studentId)
                .FirstOrDefault();

            if (wishlist == null)
            {
                return NotFound();
            }

            // Update the choices in the wishlist
            wishlist.Choice_1 = input.Choices.Length > 0 ? input.Choices[0] : null;
            wishlist.Choice_2 = input.Choices.Length > 1 ? input.Choices[1] : null;
            wishlist.Choice_3 = input.Choices.Length > 2 ? input.Choices[2] : null;
            wishlist.Choice_4 = input.Choices.Length > 3 ? input.Choices[3] : null;
            wishlist.Choice_5 = input.Choices.Length > 4 ? input.Choices[4] : null;
            wishlist.Choice_6 = input.Choices.Length > 5 ? input.Choices[5] : null;
            wishlist.Choice_7 = input.Choices.Length > 6 ? input.Choices[6] : null;
            wishlist.Choice_8 = input.Choices.Length > 7 ? input.Choices[7] : null;
            wishlist.Choice_9 = input.Choices.Length > 8 ? input.Choices[8] : null;
            wishlist.Choice_10 = input.Choices.Length > 9 ? input.Choices[9] : null;

            _context.SaveChanges();

            return Ok(wishlist);
        }

        //學分
        [HttpGet("GetCreditRequirements/{studentId}")]
        public IActionResult GetCreditRequirements(string studentId)
        {
            var creditRequirements = _context.CreditRequirements
                .Where(c => c.StudentID == studentId)
                .FirstOrDefault();

            if (creditRequirements == null)
            {
                return NotFound();
            }

            var result = new
            {
                required_credits = creditRequirements.RequiredCredits,
                elective_major = creditRequirements.ElectiveMajor,
                elective_minor = creditRequirements.ElectiveMinor,
                general_education = new
                {
                    humanities = creditRequirements.GeneralHumanities,
                    social_sciences = creditRequirements.GeneralSocialSciences,
                    arts = creditRequirements.GeneralArts,
                    sciences = creditRequirements.GeneralSciences,
                    self = creditRequirements.GeneralSelf,
                    biomedical = creditRequirements.GeneralBiomedical,
                    diversity = creditRequirements.GeneralDiversity,
                    total = creditRequirements.GeneralTotal,
                },
                english_credits = creditRequirements.EnglishCredits,
                physical_education = new
                {
                    total = creditRequirements.PhysicalTotal,
                    required = creditRequirements.PhysicalRequired,
                    fitness = creditRequirements.PhysicalFitness,
                    swimming = creditRequirements.PhysicalSwimming,
                },
                military_training = creditRequirements.MilitaryTraining,
                total_credits = creditRequirements.TotalCredits
            };

            return Ok(result);
        }

        //成績
        [HttpGet("GetGrades/{studentId}")]
        public IActionResult GetGrades(string studentId)
        {
            var grades = _context.Grades
                        .Where(g => g.StudentID == studentId)
                        .Include(g => g.Course)
                        .Select(g => new
                        {
                            year = g.Year,
                            semester = g.Semester,
                            number = g.CourseID,
                            department = g.Course.Department,
                            name = g.Course.Name,
                            type = g.Course.Type,
                            credits = g.Course.Credits,
                            score = g.Score
                        })
                        .ToList();

            if (!grades.Any())
            {
                return NotFound();
            }

            return Ok(grades);
        }

    }
}
