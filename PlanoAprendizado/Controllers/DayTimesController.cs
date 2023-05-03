using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PlanoAprendizado.Data;
using PlanoAprendizado.Models;

namespace PlanoAprendizado.Controllers
{
    [Authorize]
    public class DayTimesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DayTimesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DayTimes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.DayTimes
                 .Include(d => d.ActualState);

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: DayTimes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dayTime = await _context.DayTimes
                .Include(d => d.ActualState)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dayTime == null)
            {
                return NotFound();
            }

            return View(dayTime);
        }

        // GET: DayTimes/Create
        public IActionResult Create()
        {
            ViewData["ActualStateId"] = new SelectList(_context.ActualStates, "Id", "Description");
            return View();
        }

        // POST: DayTimes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ActualStateId,Date,Hours,Delivered")] DayTime dayTime)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dayTime);
                await _context.SaveChangesAsync();

                var actualState = await _context.ActualStates
                    .Where(x => x.Id == dayTime.ActualStateId)
                    .Include(X => X.Project)
                    .Include(X => X.TypeConsultor)
                    .Include(X => X.DayTimes)
                    .FirstOrDefaultAsync();

                actualState.AttCalculos();

                _context.Update(dayTime);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ActualStateId"] = new SelectList(_context.ActualStates, "Id", "Description", dayTime.ActualStateId);
            return View(dayTime);
        }

        // GET: DayTimes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dayTime = await _context.DayTimes.FindAsync(id);
            if (dayTime == null)
            {
                return NotFound();
            }
            ViewData["ActualStateId"] = new SelectList(_context.ActualStates, "Id", "Description", dayTime.ActualStateId);
            return View(dayTime);
        }

        // POST: DayTimes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ActualStateId,Date,Hours,Delivered")] DayTime dayTime)
        {
            if (id != dayTime.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var oldDayTime = await _context.DayTimes
                        .AsNoTracking()
                        .Where(x => x.Id == id)
                        .FirstOrDefaultAsync();

                    _context.Update(dayTime);
                    await _context.SaveChangesAsync();

                    var actualState = await _context.ActualStates
                        .Where(x => x.Id == oldDayTime.ActualStateId)
                        .Include(X => X.Project)
                        .Include(X => X.TypeConsultor)
                        .Include(X => X.DayTimes)
                        .FirstOrDefaultAsync();

                    actualState.AttCalculos();

                    _context.Update(actualState);
                    await _context.SaveChangesAsync();

                    _context.Update(dayTime);
                    await _context.SaveChangesAsync();

                    actualState = await _context.ActualStates
                   .Where(x => x.Id == dayTime.ActualStateId)
                   .Include(X => X.Project)
                   .Include(X => X.TypeConsultor)
                   .Include(X => X.DayTimes)
                   .FirstOrDefaultAsync();

                    actualState.AttCalculos();

                    _context.Update(actualState);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DayTimeExists(dayTime.Id))
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
            ViewData["ActualStateId"] = new SelectList(_context.ActualStates, "Id", "Description", dayTime.ActualStateId);
            return View(dayTime);
        }

        // GET: DayTimes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dayTime = await _context.DayTimes
                .Include(d => d.ActualState)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dayTime == null)
            {
                return NotFound();
            }

            return View(dayTime);
        }

        // POST: DayTimes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dayTime = await _context.DayTimes.FindAsync(id);
            _context.DayTimes.Remove(dayTime);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DayTimeExists(int id)
        {
            return _context.DayTimes.Any(e => e.Id == id);
        }
    }
}
