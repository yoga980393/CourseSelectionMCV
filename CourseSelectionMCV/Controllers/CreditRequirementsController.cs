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
    public class CreditRequirementsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CreditRequirementsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CreditRequirements
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CreditRequirements.Include(c => c.Student);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CreditRequirements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var creditRequirements = await _context.CreditRequirements
                .Include(c => c.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (creditRequirements == null)
            {
                return NotFound();
            }

            return View(creditRequirements);
        }

        // GET: CreditRequirements/Create
        public IActionResult Create()
        {
            ViewData["StudentID"] = new SelectList(_context.Students, "StudentID", "StudentID");
            return View();
        }

        // POST: CreditRequirements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StudentID,RequiredCredits,ElectiveMajor,ElectiveMinor,GeneralHumanities,GeneralSocialSciences,GeneralArts,GeneralSciences,GeneralSelf,GeneralBiomedical,GeneralDiversity,GeneralTotal,EnglishCredits,PhysicalTotal,PhysicalRequired,PhysicalFitness,PhysicalSwimming,MilitaryTraining,TotalCredits")] CreditRequirements creditRequirements)
        {
            if (ModelState.IsValid)
            {
                _context.Add(creditRequirements);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StudentID"] = new SelectList(_context.Students, "StudentID", "StudentID", creditRequirements.StudentID);
            return View(creditRequirements);
        }

        // GET: CreditRequirements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var creditRequirements = await _context.CreditRequirements.FindAsync(id);
            if (creditRequirements == null)
            {
                return NotFound();
            }
            ViewData["StudentID"] = new SelectList(_context.Students, "StudentID", "StudentID", creditRequirements.StudentID);
            return View(creditRequirements);
        }

        // POST: CreditRequirements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StudentID,RequiredCredits,ElectiveMajor,ElectiveMinor,GeneralHumanities,GeneralSocialSciences,GeneralArts,GeneralSciences,GeneralSelf,GeneralBiomedical,GeneralDiversity,GeneralTotal,EnglishCredits,PhysicalTotal,PhysicalRequired,PhysicalFitness,PhysicalSwimming,MilitaryTraining,TotalCredits")] CreditRequirements creditRequirements)
        {
            if (id != creditRequirements.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(creditRequirements);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CreditRequirementsExists(creditRequirements.Id))
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
            ViewData["StudentID"] = new SelectList(_context.Students, "StudentID", "StudentID", creditRequirements.StudentID);
            return View(creditRequirements);
        }

        // GET: CreditRequirements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var creditRequirements = await _context.CreditRequirements
                .Include(c => c.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (creditRequirements == null)
            {
                return NotFound();
            }

            return View(creditRequirements);
        }

        // POST: CreditRequirements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var creditRequirements = await _context.CreditRequirements.FindAsync(id);
            _context.CreditRequirements.Remove(creditRequirements);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CreditRequirementsExists(int id)
        {
            return _context.CreditRequirements.Any(e => e.Id == id);
        }
    }
}
