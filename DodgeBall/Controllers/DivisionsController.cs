using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DodgeBall.Models;

namespace DodgeBall.Controllers
{
    public class DivisionsController : Controller
    {
        private readonly DodgeBallDbContext _context;

        public DivisionsController(DodgeBallDbContext context)
        {
            _context = context;    
        }

        // GET: Divisions
        public async Task<IActionResult> Index()
        {
            return View(await _context.Divisions.ToListAsync());
        }

        // GET: Divisions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thisDivision = await _context.Divisions.Include(division => division.Teams)
                .SingleOrDefaultAsync(m => m.DivisionId == id);
            if (thisDivision == null)
            {
                return NotFound();
            }

            return View(thisDivision);
        }

        // GET: Divisions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Divisions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DivisionId,Name,SkillLevel")] Division division)
        {
            if (ModelState.IsValid)
            {
                _context.Add(division);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(division);
        }

        // GET: Divisions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var division = await _context.Divisions.SingleOrDefaultAsync(m => m.DivisionId == id);
            if (division == null)
            {
                return NotFound();
            }
            return View(division);
        }

        // POST: Divisions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DivisionId,Name,SkillLevel")] Division division)
        {
            if (id != division.DivisionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(division);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DivisionExists(division.DivisionId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(division);
        }

        // GET: Divisions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var division = await _context.Divisions
                .SingleOrDefaultAsync(m => m.DivisionId == id);
            if (division == null)
            {
                return NotFound();
            }

            return View(division);
        }

        // POST: Divisions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var division = await _context.Divisions.SingleOrDefaultAsync(m => m.DivisionId == id);
            _context.Divisions.Remove(division);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool DivisionExists(int id)
        {
            return _context.Divisions.Any(e => e.DivisionId == id);
        }
    }
}
