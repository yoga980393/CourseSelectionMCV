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
    public class GeneralCoursesWishlistsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GeneralCoursesWishlistsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: GeneralCoursesWishlists
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.GeneralCoursesWishlist.Include(g => g.Course1).Include(g => g.Course10).Include(g => g.Course2).Include(g => g.Course3).Include(g => g.Course4).Include(g => g.Course5).Include(g => g.Course6).Include(g => g.Course7).Include(g => g.Course8).Include(g => g.Course9).Include(g => g.Student);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: GeneralCoursesWishlists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var generalCoursesWishlist = await _context.GeneralCoursesWishlist
                .Include(g => g.Course1)
                .Include(g => g.Course10)
                .Include(g => g.Course2)
                .Include(g => g.Course3)
                .Include(g => g.Course4)
                .Include(g => g.Course5)
                .Include(g => g.Course6)
                .Include(g => g.Course7)
                .Include(g => g.Course8)
                .Include(g => g.Course9)
                .Include(g => g.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (generalCoursesWishlist == null)
            {
                return NotFound();
            }

            return View(generalCoursesWishlist);
        }

        // GET: GeneralCoursesWishlists/Create
        public IActionResult Create()
        {
            ViewData["Choice_1"] = new SelectList(_context.Courses, "CourseID", "CourseID");
            ViewData["Choice_10"] = new SelectList(_context.Courses, "CourseID", "CourseID");
            ViewData["Choice_2"] = new SelectList(_context.Courses, "CourseID", "CourseID");
            ViewData["Choice_3"] = new SelectList(_context.Courses, "CourseID", "CourseID");
            ViewData["Choice_4"] = new SelectList(_context.Courses, "CourseID", "CourseID");
            ViewData["Choice_5"] = new SelectList(_context.Courses, "CourseID", "CourseID");
            ViewData["Choice_6"] = new SelectList(_context.Courses, "CourseID", "CourseID");
            ViewData["Choice_7"] = new SelectList(_context.Courses, "CourseID", "CourseID");
            ViewData["Choice_8"] = new SelectList(_context.Courses, "CourseID", "CourseID");
            ViewData["Choice_9"] = new SelectList(_context.Courses, "CourseID", "CourseID");
            ViewData["StudentID"] = new SelectList(_context.Students, "StudentID", "StudentID");
            return View();
        }

        // POST: GeneralCoursesWishlists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StudentID,Choice_1,Choice_2,Choice_3,Choice_4,Choice_5,Choice_6,Choice_7,Choice_8,Choice_9,Choice_10")] GeneralCoursesWishlist generalCoursesWishlist)
        {
            if (ModelState.IsValid)
            {
                _context.Add(generalCoursesWishlist);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Choice_1"] = new SelectList(_context.Courses, "CourseID", "CourseID", generalCoursesWishlist.Choice_1);
            ViewData["Choice_10"] = new SelectList(_context.Courses, "CourseID", "CourseID", generalCoursesWishlist.Choice_10);
            ViewData["Choice_2"] = new SelectList(_context.Courses, "CourseID", "CourseID", generalCoursesWishlist.Choice_2);
            ViewData["Choice_3"] = new SelectList(_context.Courses, "CourseID", "CourseID", generalCoursesWishlist.Choice_3);
            ViewData["Choice_4"] = new SelectList(_context.Courses, "CourseID", "CourseID", generalCoursesWishlist.Choice_4);
            ViewData["Choice_5"] = new SelectList(_context.Courses, "CourseID", "CourseID", generalCoursesWishlist.Choice_5);
            ViewData["Choice_6"] = new SelectList(_context.Courses, "CourseID", "CourseID", generalCoursesWishlist.Choice_6);
            ViewData["Choice_7"] = new SelectList(_context.Courses, "CourseID", "CourseID", generalCoursesWishlist.Choice_7);
            ViewData["Choice_8"] = new SelectList(_context.Courses, "CourseID", "CourseID", generalCoursesWishlist.Choice_8);
            ViewData["Choice_9"] = new SelectList(_context.Courses, "CourseID", "CourseID", generalCoursesWishlist.Choice_9);
            ViewData["StudentID"] = new SelectList(_context.Students, "StudentID", "StudentID", generalCoursesWishlist.StudentID);
            return View(generalCoursesWishlist);
        }

        // GET: GeneralCoursesWishlists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var generalCoursesWishlist = await _context.GeneralCoursesWishlist.FindAsync(id);
            if (generalCoursesWishlist == null)
            {
                return NotFound();
            }
            ViewData["Choice_1"] = new SelectList(_context.Courses, "CourseID", "CourseID", generalCoursesWishlist.Choice_1);
            ViewData["Choice_10"] = new SelectList(_context.Courses, "CourseID", "CourseID", generalCoursesWishlist.Choice_10);
            ViewData["Choice_2"] = new SelectList(_context.Courses, "CourseID", "CourseID", generalCoursesWishlist.Choice_2);
            ViewData["Choice_3"] = new SelectList(_context.Courses, "CourseID", "CourseID", generalCoursesWishlist.Choice_3);
            ViewData["Choice_4"] = new SelectList(_context.Courses, "CourseID", "CourseID", generalCoursesWishlist.Choice_4);
            ViewData["Choice_5"] = new SelectList(_context.Courses, "CourseID", "CourseID", generalCoursesWishlist.Choice_5);
            ViewData["Choice_6"] = new SelectList(_context.Courses, "CourseID", "CourseID", generalCoursesWishlist.Choice_6);
            ViewData["Choice_7"] = new SelectList(_context.Courses, "CourseID", "CourseID", generalCoursesWishlist.Choice_7);
            ViewData["Choice_8"] = new SelectList(_context.Courses, "CourseID", "CourseID", generalCoursesWishlist.Choice_8);
            ViewData["Choice_9"] = new SelectList(_context.Courses, "CourseID", "CourseID", generalCoursesWishlist.Choice_9);
            ViewData["StudentID"] = new SelectList(_context.Students, "StudentID", "StudentID", generalCoursesWishlist.StudentID);
            return View(generalCoursesWishlist);
        }

        // POST: GeneralCoursesWishlists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StudentID,Choice_1,Choice_2,Choice_3,Choice_4,Choice_5,Choice_6,Choice_7,Choice_8,Choice_9,Choice_10")] GeneralCoursesWishlist generalCoursesWishlist)
        {
            if (id != generalCoursesWishlist.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(generalCoursesWishlist);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GeneralCoursesWishlistExists(generalCoursesWishlist.Id))
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
            ViewData["Choice_1"] = new SelectList(_context.Courses, "CourseID", "CourseID", generalCoursesWishlist.Choice_1);
            ViewData["Choice_10"] = new SelectList(_context.Courses, "CourseID", "CourseID", generalCoursesWishlist.Choice_10);
            ViewData["Choice_2"] = new SelectList(_context.Courses, "CourseID", "CourseID", generalCoursesWishlist.Choice_2);
            ViewData["Choice_3"] = new SelectList(_context.Courses, "CourseID", "CourseID", generalCoursesWishlist.Choice_3);
            ViewData["Choice_4"] = new SelectList(_context.Courses, "CourseID", "CourseID", generalCoursesWishlist.Choice_4);
            ViewData["Choice_5"] = new SelectList(_context.Courses, "CourseID", "CourseID", generalCoursesWishlist.Choice_5);
            ViewData["Choice_6"] = new SelectList(_context.Courses, "CourseID", "CourseID", generalCoursesWishlist.Choice_6);
            ViewData["Choice_7"] = new SelectList(_context.Courses, "CourseID", "CourseID", generalCoursesWishlist.Choice_7);
            ViewData["Choice_8"] = new SelectList(_context.Courses, "CourseID", "CourseID", generalCoursesWishlist.Choice_8);
            ViewData["Choice_9"] = new SelectList(_context.Courses, "CourseID", "CourseID", generalCoursesWishlist.Choice_9);
            ViewData["StudentID"] = new SelectList(_context.Students, "StudentID", "StudentID", generalCoursesWishlist.StudentID);
            return View(generalCoursesWishlist);
        }

        // GET: GeneralCoursesWishlists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var generalCoursesWishlist = await _context.GeneralCoursesWishlist
                .Include(g => g.Course1)
                .Include(g => g.Course10)
                .Include(g => g.Course2)
                .Include(g => g.Course3)
                .Include(g => g.Course4)
                .Include(g => g.Course5)
                .Include(g => g.Course6)
                .Include(g => g.Course7)
                .Include(g => g.Course8)
                .Include(g => g.Course9)
                .Include(g => g.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (generalCoursesWishlist == null)
            {
                return NotFound();
            }

            return View(generalCoursesWishlist);
        }

        // POST: GeneralCoursesWishlists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var generalCoursesWishlist = await _context.GeneralCoursesWishlist.FindAsync(id);
            _context.GeneralCoursesWishlist.Remove(generalCoursesWishlist);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GeneralCoursesWishlistExists(int id)
        {
            return _context.GeneralCoursesWishlist.Any(e => e.Id == id);
        }
    }
}
