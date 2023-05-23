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
    public class GradesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GradesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Grades
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Grades.Include(g => g.Course).Include(g => g.Student);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Grades/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grade = await _context.Grades
                .Include(g => g.Course)
                .Include(g => g.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (grade == null)
            {
                return NotFound();
            }

            return View(grade);
        }

        // GET: Grades/Create
        public IActionResult Create()
        {
            ViewData["CourseID"] = new SelectList(_context.Courses, "CourseID", "CourseID");
            ViewData["StudentID"] = new SelectList(_context.Students, "StudentID", "StudentID");
            return View();
        }

        // POST: Grades/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Year,Semester,StudentID,CourseID,Score")] Grade grade)
        {
            if (ModelState.IsValid)
            {
                _context.Add(grade);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseID"] = new SelectList(_context.Courses, "CourseID", "CourseID", grade.CourseID);
            ViewData["StudentID"] = new SelectList(_context.Students, "StudentID", "StudentID", grade.StudentID);
            return View(grade);
        }

        // GET: Grades/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grade = await _context.Grades.FindAsync(id);
            if (grade == null)
            {
                return NotFound();
            }
            ViewData["CourseID"] = new SelectList(_context.Courses, "CourseID", "CourseID", grade.CourseID);
            ViewData["StudentID"] = new SelectList(_context.Students, "StudentID", "StudentID", grade.StudentID);
            return View(grade);
        }

        // POST: Grades/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Year,Semester,StudentID,CourseID,Score")] Grade grade)
        {
            if (id != grade.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(grade);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GradeExists(grade.Id))
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
            ViewData["CourseID"] = new SelectList(_context.Courses, "CourseID", "CourseID", grade.CourseID);
            ViewData["StudentID"] = new SelectList(_context.Students, "StudentID", "StudentID", grade.StudentID);
            return View(grade);
        }

        // GET: Grades/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grade = await _context.Grades
                .Include(g => g.Course)
                .Include(g => g.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (grade == null)
            {
                return NotFound();
            }

            return View(grade);
        }

        // POST: Grades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var grade = await _context.Grades.FindAsync(id);
            _context.Grades.Remove(grade);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GradeExists(int id)
        {
            return _context.Grades.Any(e => e.Id == id);
        }
    }
}
