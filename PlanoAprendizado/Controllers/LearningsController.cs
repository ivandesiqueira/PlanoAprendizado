using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
    public class LearningsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LearningsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Learnings
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Learnings
                .Include(l => l.Circle)
                .Include(l => l.Theme)
                .Include(l => l.PeopleLearns)
                .ThenInclude(l => l.Person);
   
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Learnings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var learning = await _context.Learnings
                .Include(l => l.Circle)
                .Include(l => l.Theme)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (learning == null)
            {
                return NotFound();
            }

            return View(learning);
        }

        // GET: Learnings/Create
        public IActionResult Create()
        {
            ViewData["CircleId"] = new SelectList(_context.Circles, "Id", "Name");
            ViewData["ThemeId"] = new SelectList(_context.Themes, "Id", "Name");
            ViewData["MentorId"] = new SelectList(_context.People.Where(x => x.Type == TypePerson.Mentor), "Id", "Name");
            ViewData["MentoradoId"] = new SelectList(_context.People.Where(x => x.Type == TypePerson.Mentorado), "Id", "Name");
            return View();
        }

        // POST: Learnings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,CircleId,ThemeId,OportunityLearning,LearningAction,MeasurementDate," +
            "MeasurementForm,Result,Comment,Status,MentorId,MentoradoId")] Learning learning)
        {
            if (ModelState.IsValid)
            {
                _context.Add(learning);
                await _context.SaveChangesAsync();

                PersonLearn mentor = new PersonLearn();
                mentor.Type = TypePerson.Mentor;
                mentor.LearningId = learning.Id;
                mentor.PersonId = learning.MentorId;

                PersonLearn mentorado = new PersonLearn();
                mentorado.Type = TypePerson.Mentorado;
                mentorado.LearningId = learning.Id;
                mentorado.PersonId = learning.MentoradoId;

                _context.Add(mentor);
                _context.Add(mentorado);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CircleId"] = new SelectList(_context.Circles, "Id", "Name", learning.CircleId);
            ViewData["ThemeId"] = new SelectList(_context.Themes, "Id", "Name", learning.ThemeId);
            ViewData["MentorId"] = new SelectList(_context.People.Where(x => x.Type == TypePerson.Mentor), "Id", "Name");
            ViewData["MentoradoId"] = new SelectList(_context.People.Where(x => x.Type == TypePerson.Mentorado), "Id", "Name");
            return View(learning);
        }

        // GET: Learnings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var learning = await _context.Learnings.FindAsync(id);
            if (learning == null)
            {
                return NotFound();
            }
            ViewData["CircleId"] = new SelectList(_context.Circles, "Id", "Name", learning.CircleId);
            ViewData["ThemeId"] = new SelectList(_context.Themes, "Id", "Name", learning.ThemeId);
            ViewData["MentorId"] = new SelectList(_context.People.Where(x => x.Type == TypePerson.Mentor), "Id", "Name");
            ViewData["MentoradoId"] = new SelectList(_context.People.Where(x => x.Type == TypePerson.Mentorado), "Id", "Name");
            return View(learning);
        }

        // POST: Learnings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,CircleId,ThemeId,OportunityLearning,LearningAction,MeasurementDate," +
            "MeasurementForm,Result,Comment,Status,MentorId,MentoradoId")] Learning learning)
        {
            if (id != learning.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var aprendizados = await _context.PeopleLearns
                        .Where(x => x.LearningId == learning.Id)
                        .ToListAsync();

                    foreach (var i in aprendizados)
                    {
                        if (i.Type == TypePerson.Mentor)
                        {
                            i.PersonId = learning.MentorId;
                        }
                        else
                        {
                            i.PersonId = learning.MentoradoId;
                        }

                    }

                    _context.Update(learning);
                    _context.UpdateRange(aprendizados);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LearningExists(learning.Id))
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
            ViewData["CircleId"] = new SelectList(_context.Circles, "Id", "Name", learning.CircleId);
            ViewData["ThemeId"] = new SelectList(_context.Themes, "Id", "Name", learning.ThemeId);
            ViewData["MentorId"] = new SelectList(_context.People.Where(x => x.Type == TypePerson.Mentor), "Id", "Name");
            ViewData["MentoradoId"] = new SelectList(_context.People.Where(x => x.Type == TypePerson.Mentorado), "Id", "Name");
            return View(learning);
        }

        // GET: Learnings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var learning = await _context.Learnings
                .Include(l => l.Circle)
                .Include(l => l.Theme)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (learning == null)
            {
                return NotFound();
            }

            return View(learning);
        }

        // POST: Learnings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var learning = await _context.Learnings.FindAsync(id);
            _context.Learnings.Remove(learning);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LearningExists(int id)
        {
            return _context.Learnings.Any(e => e.Id == id);
        }
    }
}
