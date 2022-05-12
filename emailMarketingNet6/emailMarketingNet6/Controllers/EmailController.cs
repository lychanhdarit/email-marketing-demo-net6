using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using emailMarketingNet6.Models;

namespace emailMarketingNet6.Controllers
{
    public class EmailController : Controller
    {
        private readonly EmailMarketingContext _context;

        public EmailController(EmailMarketingContext context)
        {
            _context = context;
        }

        // GET: Email
        public async Task<IActionResult> Index()
        {
              return _context.Emails != null ? 
                          View(await _context.Emails.ToListAsync()) :
                          Problem("Entity set 'EmailMarketingContext.Emails'  is null.");
        }

        // GET: Email/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Emails == null)
            {
                return NotFound();
            }

            var emailModel = await _context.Emails
                .FirstOrDefaultAsync(m => m.Id == id);
            if (emailModel == null)
            {
                return NotFound();
            }

            return View(emailModel);
        }

        // GET: Email/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Email/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Email,PassLogin,Name")] EmailModel emailModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(emailModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(emailModel);
        }

        // GET: Email/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Emails == null)
            {
                return NotFound();
            }

            var emailModel = await _context.Emails.FindAsync(id);
            if (emailModel == null)
            {
                return NotFound();
            }
            return View(emailModel);
        }

        // POST: Email/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Email,PassLogin,Name")] EmailModel emailModel)
        {
            if (id != emailModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(emailModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmailModelExists(emailModel.Id))
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
            return View(emailModel);
        }

        // GET: Email/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Emails == null)
            {
                return NotFound();
            }

            var emailModel = await _context.Emails
                .FirstOrDefaultAsync(m => m.Id == id);
            if (emailModel == null)
            {
                return NotFound();
            }

            return View(emailModel);
        }

        // POST: Email/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Emails == null)
            {
                return Problem("Entity set 'EmailMarketingContext.Emails'  is null.");
            }
            var emailModel = await _context.Emails.FindAsync(id);
            if (emailModel != null)
            {
                _context.Emails.Remove(emailModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmailModelExists(int id)
        {
          return (_context.Emails?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
