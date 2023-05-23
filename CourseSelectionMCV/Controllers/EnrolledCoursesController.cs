using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CourseSelectionMCV.Data;
using CourseSelectionMCV.Models;

namespace CourseSelectionMCV.Controllers
{
    public class EnrolledCoursesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EnrolledCoursesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EnrolledCourses
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.EnrolledCourses.Include(e => e.Course).Include(e => e.Student);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: EnrolledCourses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrolledCourse = await _context.EnrolledCourses
                .Include(e => e.Course)
                .Include(e => e.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (enrolledCourse == null)
            {
                return NotFound();
            }

            return View(enrolledCourse);
        }

        // GET: EnrolledCourses/Create
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseID", "CourseID");
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentID", "StudentID");
            return View();
        }

        // POST: EnrolledCourses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StudentId,CourseId")] EnrolledCourse enrolledCourse)
        {
            if (ModelState.IsValid)
            {
                _context.Add(enrolledCourse);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseID", "CourseID", enrolledCourse.CourseId);
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentID", "StudentID", enrolledCourse.StudentId);
            return View(enrolledCourse);
        }

        // GET: EnrolledCourses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrolledCourse = await _context.EnrolledCourses.FindAsync(id);
            if (enrolledCourse == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseID", "CourseID", enrolledCourse.CourseId);
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentID", "StudentID", enrolledCourse.StudentId);
            return View(enrolledCourse);
        }

        // POST: EnrolledCourses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StudentId,CourseId")] EnrolledCourse enrolledCourse)
        {
            if (id != enrolledCourse.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(enrolledCourse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnrolledCourseExists(enrolledCourse.Id))
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
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseID", "CourseID", enrolledCourse.CourseId);
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentID", "StudentID", enrolledCourse.StudentId);
            return View(enrolledCourse);
        }

        // GET: EnrolledCourses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrolledCourse = await _context.EnrolledCourses
                .Include(e => e.Course)
                .Include(e => e.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (enrolledCourse == null)
            {
                return NotFound();
            }

            return View(enrolledCourse);
        }

        // POST: EnrolledCourses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var enrolledCourse = await _context.EnrolledCourses.FindAsync(id);
            _context.EnrolledCourses.Remove(enrolledCourse);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnrolledCourseExists(int id)
        {
            return _context.EnrolledCourses.Any(e => e.Id == id);
        }
    }
}
