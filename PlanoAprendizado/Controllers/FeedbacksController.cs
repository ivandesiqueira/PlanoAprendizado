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
    public class FeedbacksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FeedbacksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Feedbacks
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Feedbacks
                .Include(f => f.Circle)
                .Include(f => f.Theme)
                .Include(f => f.PeopleFeedbacks)
                .ThenInclude(f => f.Person);

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Feedbacks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feedback = await _context.Feedbacks
                .Include(f => f.Circle)
                .Include(f => f.Theme)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (feedback == null)
            {
                return NotFound();
            }

            return View(feedback);
        }

        // GET: Feedbacks/Create
        public IActionResult Create()
        {
            ViewData["CircleId"] = new SelectList(_context.Circles, "Id", "Name");
            ViewData["ThemeId"] = new SelectList(_context.Themes, "Id", "Name");
            ViewData["MentorId"] = new SelectList(_context.People.Where(x => x.Type == TypePerson.Mentor), "Id", "Name");
            ViewData["MentoradoId"] = new SelectList(_context.People.Where(x => x.Type == TypePerson.Mentorado), "Id", "Name");
            return View();
        }

        // POST: Feedbacks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,CircleId,ThemeId,OportunityLearning,Note,Comment,MentorId,MentoradoId")] Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                _context.Add(feedback);
                await _context.SaveChangesAsync();

                PersonFeedback mentor = new PersonFeedback();
                mentor.Type = TypePerson.Mentor;
                mentor.FeedbackId = feedback.Id;
                mentor.PersonId = feedback.MentorId;

                PersonFeedback mentorado = new PersonFeedback();
                mentor.Type = TypePerson.Mentorado;
                mentorado.FeedbackId = feedback.Id;
                mentorado.PersonId = feedback.MentoradoId;

                _context.Add(mentor);
                _context.Add(mentorado);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CircleId"] = new SelectList(_context.Circles, "Id", "Name", feedback.CircleId);
            ViewData["ThemeId"] = new SelectList(_context.Themes, "Id", "Name", feedback.ThemeId);
            ViewData["MentorId"] = new SelectList(_context.People.Where(x => x.Type == TypePerson.Mentor), "Id", "Name");
            ViewData["MentoradoId"] = new SelectList(_context.People.Where(x => x.Type == TypePerson.Mentorado), "Id", "Name");
            return View(feedback);
        }

        // GET: Feedbacks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feedback = await _context.Feedbacks.FindAsync(id);
            if (feedback == null)
            {
                return NotFound();
            }
            ViewData["CircleId"] = new SelectList(_context.Circles, "Id", "Name", feedback.CircleId);
            ViewData["ThemeId"] = new SelectList(_context.Themes, "Id", "Name", feedback.ThemeId);
            ViewData["MentorId"] = new SelectList(_context.People.Where(x => x.Type == TypePerson.Mentor), "Id", "Name");
            ViewData["MentoradoId"] = new SelectList(_context.People.Where(x => x.Type == TypePerson.Mentorado), "Id", "Name");
            return View(feedback);
        }

        // POST: Feedbacks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,CircleId,ThemeId,OportunityLearning,Note,Comment,MentorId,MentoradoId")] Feedback feedback)
        {
            if (id != feedback.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var feedbacks = await _context.PeopleFeedbacks
                        .Where(x => x.FeedbackId == feedback.Id)
                        .ToListAsync();

                    foreach (var i in feedbacks)
                    {
                        if (i.Type == TypePerson.Mentor)
                        {
                            i.PersonId = feedback.MentorId;
                        }
                        else
                        {
                            i.PersonId = feedback.MentoradoId;
                        }

                    }

                    _context.Update(feedback);
                    _context.UpdateRange(feedbacks);

                    _context.Update(feedback);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FeedbackExists(feedback.Id))
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
            ViewData["CircleId"] = new SelectList(_context.Circles, "Id", "Name", feedback.CircleId);
            ViewData["ThemeId"] = new SelectList(_context.Themes, "Id", "Name", feedback.ThemeId);
            ViewData["MentorId"] = new SelectList(_context.People.Where(x => x.Type == TypePerson.Mentor), "Id", "Name");
            ViewData["MentoradoId"] = new SelectList(_context.People.Where(x => x.Type == TypePerson.Mentorado), "Id", "Name");
            return View(feedback);
        }

        // GET: Feedbacks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feedback = await _context.Feedbacks
                .Include(f => f.Circle)
                .Include(f => f.Theme)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (feedback == null)
            {
                return NotFound();
            }

            return View(feedback);
        }

        // POST: Feedbacks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var feedback = await _context.Feedbacks.FindAsync(id);
            _context.Feedbacks.Remove(feedback);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FeedbackExists(int id)
        {
            return _context.Feedbacks.Any(e => e.Id == id);
        }
    }
}
