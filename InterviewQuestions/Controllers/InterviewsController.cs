using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InterviewQuestions.Data;
using InterviewQuestions.Models;
using Microsoft.AspNetCore.Authorization;

namespace InterviewQuestions.Controllers
{
    public class InterviewsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InterviewsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Interviews
        public async Task<IActionResult> Index()
        {
            return View(await _context.Interview.ToListAsync());
        }
        public async Task<IActionResult> ShowSearchForm()
        {
            return View();
        }
        public async Task<IActionResult> ShowSearchResults(String SearchPhrase)
        {
            return View("Index",await _context.Interview.Where( j => j.InterviewQuestion.Contains(SearchPhrase)).ToListAsync());
        }
        // GET: Interviews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var interview = await _context.Interview
                .FirstOrDefaultAsync(m => m.Id == id);
            if (interview == null)
            {
                return NotFound();
            }

            return View(interview);
        }
        [Authorize]
        // GET: Interviews/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Interviews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,InterviewQuestion,InterviewAnswer")] Interview interview)
        {
            if (ModelState.IsValid)
            {
                _context.Add(interview);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(interview);
        }

        // GET: Interviews/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var interview = await _context.Interview.FindAsync(id);
            if (interview == null)
            {
                return NotFound();
            }
            return View(interview);
        }

        // POST: Interviews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,InterviewQuestion,InterviewAnswer")] Interview interview)
        {
            if (id != interview.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(interview);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InterviewExists(interview.Id))
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
            return View(interview);
        }

        // GET: Interviews/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var interview = await _context.Interview
                .FirstOrDefaultAsync(m => m.Id == id);
            if (interview == null)
            {
                return NotFound();
            }

            return View(interview);
        }

        // POST: Interviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var interview = await _context.Interview.FindAsync(id);
            _context.Interview.Remove(interview);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InterviewExists(int id)
        {
            return _context.Interview.Any(e => e.Id == id);
        }
    }
}
