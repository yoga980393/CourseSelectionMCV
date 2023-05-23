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
    public class LotteryCoursesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LotteryCoursesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LotteryCourses
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.LotteryCourses.Include(l => l.Course).Include(l => l.Student);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: LotteryCourses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lotteryCourse = await _context.LotteryCourses
                .Include(l => l.Course)
                .Include(l => l.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lotteryCourse == null)
            {
                return NotFound();
            }

            return View(lotteryCourse);
        }

        // GET: LotteryCourses/Create
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseID", "CourseID");
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentID", "StudentID");
            return View();
        }

        // POST: LotteryCourses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StudentId,CourseId")] LotteryCourse lotteryCourse)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lotteryCourse);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseID", "CourseID", lotteryCourse.CourseId);
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentID", "StudentID", lotteryCourse.StudentId);
            return View(lotteryCourse);
        }

        // GET: LotteryCourses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lotteryCourse = await _context.LotteryCourses.FindAsync(id);
            if (lotteryCourse == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseID", "CourseID", lotteryCourse.CourseId);
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentID", "StudentID", lotteryCourse.StudentId);
            return View(lotteryCourse);
        }

        // POST: LotteryCourses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StudentId,CourseId")] LotteryCourse lotteryCourse)
        {
            if (id != lotteryCourse.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lotteryCourse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LotteryCourseExists(lotteryCourse.Id))
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
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseID", "CourseID", lotteryCourse.CourseId);
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentID", "StudentID", lotteryCourse.StudentId);
            return View(lotteryCourse);
        }

        // GET: LotteryCourses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lotteryCourse = await _context.LotteryCourses
                .Include(l => l.Course)
                .Include(l => l.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lotteryCourse == null)
            {
                return NotFound();
            }

            return View(lotteryCourse);
        }

        // POST: LotteryCourses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lotteryCourse = await _context.LotteryCourses.FindAsync(id);
            _context.LotteryCourses.Remove(lotteryCourse);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LotteryCourseExists(int id)
        {
            return _context.LotteryCourses.Any(e => e.Id == id);
        }
    }
}
