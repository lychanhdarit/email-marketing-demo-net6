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
    public class CampaignModelsController : Controller
    {
        private readonly EmailMarketingContext _context;

        public CampaignModelsController(EmailMarketingContext context)
        {
            _context = context;
        }

        // GET: CampaignModels
        public async Task<IActionResult> Index()
        {
            var emailMarketingContext = _context.Campaigns.Include(c => c.ContactList).Include(c => c.Template);
            return View(await emailMarketingContext.ToListAsync());
        }

        // GET: CampaignModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Campaigns == null)
            {
                return NotFound();
            }

            var campaignModel = await _context.Campaigns
                .Include(c => c.ContactList)
                .Include(c => c.Template)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (campaignModel == null)
            {
                return NotFound();
            }

            return View(campaignModel);
        }

        // GET: CampaignModels/Create
        public IActionResult Create()
        {
            ViewData["ContactListId"] = new SelectList(_context.ContactLists, "Id", "Id");
            ViewData["TemplateId"] = new SelectList(_context.Templates, "Id", "Id");
            return View();
        }

        // POST: CampaignModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Schedule,Day,Hour,Minute,Actived,TemplateId,ContactListId,EmailSendId")] CampaignModel campaignModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(campaignModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ContactListId"] = new SelectList(_context.ContactLists, "Id", "Id", campaignModel.ContactListId);
            ViewData["TemplateId"] = new SelectList(_context.Templates, "Id", "Id", campaignModel.TemplateId);
            return View(campaignModel);
        }

        // GET: CampaignModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Campaigns == null)
            {
                return NotFound();
            }

            var campaignModel = await _context.Campaigns.FindAsync(id);
            if (campaignModel == null)
            {
                return NotFound();
            }
            ViewData["ContactListId"] = new SelectList(_context.ContactLists, "Id", "Id", campaignModel.ContactListId);
            ViewData["TemplateId"] = new SelectList(_context.Templates, "Id", "Id", campaignModel.TemplateId);
            return View(campaignModel);
        }

        // POST: CampaignModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Schedule,Day,Hour,Minute,Actived,TemplateId,ContactListId,EmailSendId")] CampaignModel campaignModel)
        {
            if (id != campaignModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(campaignModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CampaignModelExists(campaignModel.Id))
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
            ViewData["ContactListId"] = new SelectList(_context.ContactLists, "Id", "Id", campaignModel.ContactListId);
            ViewData["TemplateId"] = new SelectList(_context.Templates, "Id", "Id", campaignModel.TemplateId);
            return View(campaignModel);
        }

        // GET: CampaignModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Campaigns == null)
            {
                return NotFound();
            }

            var campaignModel = await _context.Campaigns
                .Include(c => c.ContactList)
                .Include(c => c.Template)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (campaignModel == null)
            {
                return NotFound();
            }

            return View(campaignModel);
        }

        // POST: CampaignModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Campaigns == null)
            {
                return Problem("Entity set 'EmailMarketingContext.Campaigns'  is null.");
            }
            var campaignModel = await _context.Campaigns.FindAsync(id);
            if (campaignModel != null)
            {
                _context.Campaigns.Remove(campaignModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CampaignModelExists(int id)
        {
          return (_context.Campaigns?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
