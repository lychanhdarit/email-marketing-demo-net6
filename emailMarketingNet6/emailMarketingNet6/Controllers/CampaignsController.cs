using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using emailMarketingNet6.Models;
using emailMarketingNet6.Services;

namespace emailMarketingNet6.Controllers
{
    public class CampaignsController : Controller
    {
        private readonly EmailMarketingContext _context;

        public CampaignsController(EmailMarketingContext context)
        {
            _context = context;
        }

        // GET: Campaigns
        public async Task<IActionResult> Index()
        {
            var emailMarketingContext = _context.Campaigns.Include(c => c.ContactList).Include(c => c.Template).Include(c => c.EmailSend);
            return View(await emailMarketingContext.ToListAsync());
        }

        // GET: Campaigns/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Campaigns == null)
            {
                return NotFound();
            }

            var campaignModel = await _context.Campaigns
                .Include(c => c.ContactList)
                .Include(c => c.Template).Include(c => c.EmailSend)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (campaignModel == null)
            {
                return NotFound();
            }

            return View(campaignModel);
        }

        // GET: Campaigns/Create
        public IActionResult Create()
        {
            ViewData["ContactListId"] = new SelectList(_context.ContactLists, "Id", "Name");
            ViewData["TemplateId"] = new SelectList(_context.Templates, "Id", "Name");
            ViewData["EmailSendId"] = new SelectList(_context.Emails, "Id", "Email");
            return View();
        }

        // POST: Campaigns/Create
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
            ViewData["ContactListId"] = new SelectList(_context.ContactLists, "Id", "Name", campaignModel.ContactListId);
            ViewData["TemplateId"] = new SelectList(_context.Templates, "Id", "Name", campaignModel.TemplateId);
            ViewData["EmailSendId"] = new SelectList(_context.Emails, "Id", "Email", campaignModel.EmailSendId);
            return View(campaignModel);
        }

        // GET: Campaigns/Edit/5
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
            ViewData["ContactListId"] = new SelectList(_context.ContactLists, "Id", "Name", campaignModel.ContactListId);
            ViewData["TemplateId"] = new SelectList(_context.Templates, "Id", "Name", campaignModel.TemplateId);
            ViewData["EmailSendId"] = new SelectList(_context.Emails, "Id", "Email", campaignModel.EmailSendId);
            return View(campaignModel);
        }

        // POST: Campaigns/Edit/5
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
            ViewData["ContactListId"] = new SelectList(_context.ContactLists, "Id", "Name", campaignModel.ContactListId);
            ViewData["TemplateId"] = new SelectList(_context.Templates, "Id", "Name", campaignModel.TemplateId);
            ViewData["EmailSendId"] = new SelectList(_context.Emails, "Id", "Email", campaignModel.EmailSendId);
            return View(campaignModel);
        }

        // GET: Campaigns/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Campaigns == null)
            {
                return NotFound();
            }

            var campaignModel = await _context.Campaigns
                .Include(c => c.ContactList)
                .Include(c => c.Template).Include(c => c.EmailSend)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (campaignModel == null)
            {
                return NotFound();
            }

            return View(campaignModel);
        }

        // POST: Campaigns/Delete/5
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
        [HttpPost]
        public async Task<IActionResult> SendCampaign(int id)
        {
            try
            {
                string masterTempalte = "<!DOCTYPE html> <html> <head> <meta name=\"viewport\" content=\"width=device-width\" />   <link rel=\"preconnect\" href=\"https://fonts.googleapis.com\"> <link rel=\"preconnect\" href=\"https://fonts.gstatic.com\" crossorigin>  <link href=\"https://fonts.googleapis.com/css2?family=Roboto&display=swap\" rel=\"stylesheet\"><title>@Name</title> </head> <body> @Content </body> </html>";

                var campaign = await _context.Campaigns.Include(c => c.ContactList).Include(c => c.ContactList.Contacts)
                .Include(c => c.Template).Include(c => c.EmailSend).FirstOrDefaultAsync(m => m.Id == id);
                if (campaign != null)
                {
                    var template = campaign.Template;
                    var listEmail = campaign.ContactList.Contacts;
                    if (template != null)
                    {
                        if (listEmail != null)
                        {
                            var mail = campaign.EmailSend;
                            if (campaign != null)
                            {
                                MailServices mailServices = new MailServices(mail.Email, mail.PassLogin, mail.Name);
                                foreach (var item in listEmail)
                                {
                                    string content = template.Content;
                                    if (!string.IsNullOrEmpty(content))
                                    {
                                        content = content.Replace("[Email]", item.Email);
                                    }
                                    mailServices.SendEmail(item.Email, campaign.Name, masterTempalte.Replace("@Name", campaign.Name).Replace("@Content", content));
                                }

                            }
                        }
                    }
                }
                return Json(new { msg = "Thành công", code = 1 });
            }
            catch (Exception e)
            {
                return Json(new { msg = e.Message, code = -1 });
            }
        }

    }
}
