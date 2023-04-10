﻿using System;
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
    public class TypeConsultorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TypeConsultorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TypeConsultors
        public async Task<IActionResult> Index()
        {
            return View(await _context.TypeConsultors.ToListAsync());
        }

        // GET: TypeConsultors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeConsultor = await _context.TypeConsultors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (typeConsultor == null)
            {
                return NotFound();
            }

            return View(typeConsultor);
        }

        // GET: TypeConsultors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TypeConsultors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Fee")] TypeConsultor typeConsultor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(typeConsultor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(typeConsultor);
        }

        // GET: TypeConsultors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeConsultor = await _context.TypeConsultors.FindAsync(id);
            if (typeConsultor == null)
            {
                return NotFound();
            }
            return View(typeConsultor);
        }

        // POST: TypeConsultors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Fee")] TypeConsultor typeConsultor)
        {
            if (id != typeConsultor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(typeConsultor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TypeConsultorExists(typeConsultor.Id))
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
            return View(typeConsultor);
        }

        // GET: TypeConsultors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeConsultor = await _context.TypeConsultors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (typeConsultor == null)
            {
                return NotFound();
            }

            return View(typeConsultor);
        }

        // POST: TypeConsultors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var typeConsultor = await _context.TypeConsultors.FindAsync(id);
            _context.TypeConsultors.Remove(typeConsultor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TypeConsultorExists(int id)
        {
            return _context.TypeConsultors.Any(e => e.Id == id);
        }
    }
}
