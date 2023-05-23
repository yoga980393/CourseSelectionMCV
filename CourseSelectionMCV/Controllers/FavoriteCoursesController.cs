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
    public class FavoriteCoursesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FavoriteCoursesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: FavoriteCourses
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.FavoriteCourses.Include(f => f.Course).Include(f => f.Student);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: FavoriteCourses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var favoriteCourse = await _context.FavoriteCourses
                .Include(f => f.Course)
                .Include(f => f.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (favoriteCourse == null)
            {
                return NotFound();
            }

            return View(favoriteCourse);
        }

        // GET: FavoriteCourses/Create
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseID", "CourseID");
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentID", "StudentID");
            return View();
        }

        // POST: FavoriteCourses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StudentId,CourseId")] FavoriteCourse favoriteCourse)
        {
            if (ModelState.IsValid)
            {
                _context.Add(favoriteCourse);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseID", "CourseID", favoriteCourse.CourseId);
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentID", "StudentID", favoriteCourse.StudentId);
            return View(favoriteCourse);
        }

        // GET: FavoriteCourses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var favoriteCourse = await _context.FavoriteCourses.FindAsync(id);
            if (favoriteCourse == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseID", "CourseID", favoriteCourse.CourseId);
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentID", "StudentID", favoriteCourse.StudentId);
            return View(favoriteCourse);
        }

        // POST: FavoriteCourses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StudentId,CourseId")] FavoriteCourse favoriteCourse)
        {
            if (id != favoriteCourse.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(favoriteCourse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FavoriteCourseExists(favoriteCourse.Id))
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
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseID", "CourseID", favoriteCourse.CourseId);
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentID", "StudentID", favoriteCourse.StudentId);
            return View(favoriteCourse);
        }

        // GET: FavoriteCourses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var favoriteCourse = await _context.FavoriteCourses
                .Include(f => f.Course)
                .Include(f => f.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (favoriteCourse == null)
            {
                return NotFound();
            }

            return View(favoriteCourse);
        }

        // POST: FavoriteCourses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var favoriteCourse = await _context.FavoriteCourses.FindAsync(id);
            _context.FavoriteCourses.Remove(favoriteCourse);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FavoriteCourseExists(int id)
        {
            return _context.FavoriteCourses.Any(e => e.Id == id);
        }
    }
}
