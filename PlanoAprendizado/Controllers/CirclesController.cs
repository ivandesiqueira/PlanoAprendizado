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
    public class CirclesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CirclesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Circles
        public async Task<IActionResult> Index(/*int pageNumber = 1, int pageSize = 10*/)
        {
           /* var data =*/ 

            //    int totalCount = data.Count();
            //    int correctNumber = (pageNumber - 1) * pageSize;
            //    data = data 
            //        .Skip(correctNumber)
            //        .Take(pageSize)
            //        .ToList();

            //    var viewModel = new PaginationViewModel<Circle>
            //    {
            //        Items = data,
            //        PageNumber = pageNumber,
            //        PageSize = pageSize,
            //        TotalCount = totalCount
            //    };
            return View(/*viewModel*/await _context.Circles.ToListAsync());
        }

        // GET: Circles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var circle = await _context.Circles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (circle == null)
            {
                return NotFound();
            }

            return View(circle);
        }

        // GET: Circles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Circles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Circle circle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(circle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(circle);
        }

        // GET: Circles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var circle = await _context.Circles.FindAsync(id);
            if (circle == null)
            {
                return NotFound();
            }
            return View(circle);
        }

        // POST: Circles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Circle circle)
        {
            if (id != circle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(circle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CircleExists(circle.Id))
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
            return View(circle);
        }

        // GET: Circles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var circle = await _context.Circles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (circle == null)
            {
                return NotFound();
            }

            return View(circle);
        }

        // POST: Circles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var circle = await _context.Circles.FindAsync(id);
            _context.Circles.Remove(circle);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: Circles/Delete/5
        [HttpPost, ActionName("MultipleDelete")]
        public async Task<IActionResult> MultipleDeleteConfirmed(List <int> ids)
        {
            try
            {
                foreach (int id in ids)
                {
                    var circle = await _context.Circles.FindAsync(id);
                    _context.Circles.Remove(circle);
                }
            }
            catch(Exception)
            { 
                return BadRequest();    
            }
            await _context.SaveChangesAsync();
            return Ok();
        }

        private bool CircleExists(int id)
        {
            return _context.Circles.Any(e => e.Id == id);
        }
    }
}
