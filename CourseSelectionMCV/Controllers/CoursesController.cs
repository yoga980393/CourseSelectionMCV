using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CourseSelectionMCV.Data;
using CourseSelectionMCV.Models;
using Microsoft.AspNetCore.Http;

namespace CourseSelectionMCV.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CoursesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Courses
        public async Task<IActionResult> Index()
        {
            return View(await _context.Courses
                .Include(c => c.Schedules)
                .Include(c => c.CourseTeachers)
                    .ThenInclude(ct => ct.Teacher)
                .ToListAsync());
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.Schedules)
                .Include(c => c.CourseTeachers)
                    .ThenInclude(ct => ct.Teacher)
                .FirstOrDefaultAsync(m => m.CourseID == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            ViewData["Teachers"] = new SelectList(_context.Teachers, "TeacherID", "TeacherName");
            return View();
        }


        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CourseID,Name,ShortName,Department,Introduction,Language,Type,Credits,Hour,Place,NumberOfPeople,MaxOfPeople,Image")] Course course, List<string> scheduleTimes, List<string> teacherIds)
        {
            if (ModelState.IsValid)
            {
                _context.Add(course);
                await _context.SaveChangesAsync();

                foreach (var time in scheduleTimes)
                {
                    if (!string.IsNullOrEmpty(time))
                    {
                        var schedule = new Schedule
                        {
                            CourseId = course.CourseID,
                            ScheduleTime = Convert.ToInt32(time)
                        };
                        _context.Add(schedule);
                    }
                }

                foreach (var teacherId in teacherIds)
                {
                    if (!string.IsNullOrEmpty(teacherId))
                    {
                        var courseTeacher = new CourseTeacher
                        {
                            CourseId = course.CourseID,
                            TeacherId = teacherId
                        };
                        _context.Add(courseTeacher);
                    }
                }

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }


        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.Schedules)
                .Include(c => c.CourseTeachers)
                    .ThenInclude(ct => ct.Teacher)
                .FirstOrDefaultAsync(m => m.CourseID == id);
            if (course == null)
            {
                return NotFound();
            }
            ViewData["Teachers"] = new SelectList(_context.Teachers, "TeacherID", "TeacherName");
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CourseID,Name,ShortName,Department,Introduction,Language,Type,Credits,Hour,Place,NumberOfPeople,MaxOfPeople,Image")] Course course, List<int> scheduleTimes, List<string> teacherIds)
        {
            if (id != course.CourseID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(course);

                    // Remove old schedules
                    var oldSchedules = await _context.Schedules.Where(s => s.CourseId == id).ToListAsync();
                    _context.Schedules.RemoveRange(oldSchedules);

                    // Add new schedules
                    foreach (var scheduleTime in scheduleTimes)
                    {
                        var newSchedule = new Schedule { CourseId = id, ScheduleTime = scheduleTime };
                        _context.Schedules.Add(newSchedule);
                    }

                    var existingCourseTeachers = await _context.CourseTeachers.Where(ct => ct.CourseId == course.CourseID).ToListAsync();
                    _context.CourseTeachers.RemoveRange(existingCourseTeachers);
                    await _context.SaveChangesAsync();

                    foreach (var teacherId in teacherIds)
                    {
                        if (!string.IsNullOrEmpty(teacherId))
                        {
                            var courseTeacher = new CourseTeacher
                            {
                                CourseId = course.CourseID,
                                TeacherId = teacherId
                            };
                            _context.Add(courseTeacher);
                        }
                    }

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.CourseID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        private bool CourseExists(string id)
        {
            return _context.Courses.Any(e => e.CourseID == id);
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.Schedules)
                .Include(c => c.CourseTeachers)
                    .ThenInclude(ct => ct.Teacher)
                .FirstOrDefaultAsync(m => m.CourseID == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var course = await _context.Courses
                .Include(c => c.Schedules)
                .Include(c => c.CourseTeachers)
                .FirstOrDefaultAsync(m => m.CourseID == id);

            if (course != null)
            {
                // 刪除對應的 Schedules
                _context.Schedules.RemoveRange(course.Schedules);

                // 刪除對應的 CourseTeachers
                _context.CourseTeachers.RemoveRange(course.CourseTeachers);

                // 刪除 Course
                _context.Courses.Remove(course);

                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
