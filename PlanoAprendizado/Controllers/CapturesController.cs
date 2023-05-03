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
    public class CapturesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CapturesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Captures
        public async Task<IActionResult> Index()
        {
            return View(await _context.Captures.ToListAsync());
        }

        // GET: Captures/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var capture = await _context.Captures
                .FirstOrDefaultAsync(m => m.Id == id);
            if (capture == null)
            {
                return NotFound();
            }

            return View(capture);
        }

        // GET: Captures/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Captures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Capture capture)
        {
            if (ModelState.IsValid)
            {
                _context.Add(capture);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(capture);
        }

        // GET: Captures/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var capture = await _context.Captures.FindAsync(id);
            if (capture == null)
            {
                return NotFound();
            }
            return View(capture);
        }

        // POST: Captures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Capture capture)
        {
            if (id != capture.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(capture);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CaptureExists(capture.Id))
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
            return View(capture);
        }

        // GET: Captures/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var capture = await _context.Captures
                .FirstOrDefaultAsync(m => m.Id == id);
            if (capture == null)
            {
                return NotFound();
            }

            return View(capture);
        }

        // POST: Captures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var capture = await _context.Captures.FindAsync(id);
            _context.Captures.Remove(capture);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CaptureExists(int id)
        {
            return _context.Captures.Any(e => e.Id == id);
        }
    }
}
