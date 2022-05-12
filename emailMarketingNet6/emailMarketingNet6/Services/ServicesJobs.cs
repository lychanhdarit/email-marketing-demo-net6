using emailMarketingNet6.Models;
using Microsoft.EntityFrameworkCore;
namespace emailMarketingNet6.Services
{
    public class ServicesJobs
    {  
       
        public static void SendCampaigns()
        {
            EmailMarketingContext _context = new EmailMarketingContext();
            string masterTempalte = "<!DOCTYPE html> <html> <head> <meta name=\"viewport\" content=\"width=device-width\" />   <link rel=\"preconnect\" href=\"https://fonts.googleapis.com\"> <link rel=\"preconnect\" href=\"https://fonts.gstatic.com\" crossorigin>  <link href=\"https://fonts.googleapis.com/css2?family=Roboto&display=swap\" rel=\"stylesheet\"><title>@Name</title> </head> <body> @Content </body> </html>";

           
            var now = DateTime.Now; 
            var campaigns = _context.Campaigns.Include(c => c.ContactList)
                .Include(c => c.Template).Include(c => c.EmailSend).Where(m=>m.Actived==true && m.Schedule == true && m.Day.Value.Day == now.Day && m.Day.Value.Hour == now.Hour && m.Day.Value.Minute <= now.Minute);
            foreach(var campaign in campaigns)
            {
                if (campaign != null)
                {
                    var template = campaign.Template;
                    var listEmail = _context.Contacts.Where(m => m.ContactListId == campaign.ContactListId).ToList();
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

                campaign.Actived = false;
                _context.Campaigns.Update(campaign);
                _context.SaveChangesAsync();
            }

        }

    }
}
