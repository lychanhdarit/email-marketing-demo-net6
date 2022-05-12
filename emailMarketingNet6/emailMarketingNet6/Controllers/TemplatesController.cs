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
    public class TemplatesController : Controller
    {
        private readonly EmailMarketingContext _context;

        public TemplatesController(EmailMarketingContext context)
        {
            _context = new EmailMarketingContext();
            _context = context;
            //try { _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking; } catch { }

        }

        // GET: Templates
        public async Task<IActionResult> Index()
        {
            return _context.Templates != null ?
                        View(await _context.Templates.AsNoTracking().ToListAsync()) :
                        Problem("Entity set 'EmailMarketingContext.Templates'  is null.");
        }

        // GET: Templates/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Templates == null)
            {
                return NotFound();
            }

            var templateModel = await _context.Templates.AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (templateModel == null)
            {
                return NotFound();
            }

            return View(templateModel);
        }

        // GET: Templates/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Templates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Code,Name,Content,Active")] TemplateModel templateModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(templateModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(templateModel);
        }

        // GET: Templates/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Templates == null)
            {
                return NotFound();
            }

            var templateModel = await _context.Templates.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
            if (templateModel == null)
            {
                return NotFound();
            }
            return View(templateModel);
        }

        // POST: Templates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Code,Name,Content,Active")] TemplateModel templateModel)
        {
            //_context.ChangeTracker.DetectChanges();
            if (id != templateModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                { 
                    _context.Update(templateModel);  
                    await _context.SaveChangesAsync(); 
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TemplateModelExists(templateModel.Id))
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
            return View(templateModel);
        }

        // GET: Templates/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Templates == null)
            {
                return NotFound();
            }

            var templateModel = await _context.Templates
                .FirstOrDefaultAsync(m => m.Id == id);
            if (templateModel == null)
            {
                return NotFound();
            }

            return View(templateModel);
        }

        // POST: Templates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Templates == null)
            {
                return Problem("Entity set 'EmailMarketingContext.Templates'  is null.");
            }
            var templateModel = await _context.Templates.FindAsync(id);
            if (templateModel != null)
            {
                _context.Templates.Remove(templateModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TemplateModelExists(int id)
        {
            return (_context.Templates?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
