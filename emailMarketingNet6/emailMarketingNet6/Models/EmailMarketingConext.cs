using Microsoft.EntityFrameworkCore;

namespace emailMarketingNet6.Models
{
    public class EmailMarketingContext : DbContext
    {
        public DbSet<EmailModel> Emails { get; set; }
        public DbSet<TemplateModel> Templates { get; set; }
        public DbSet<ContactListModel> ContactLists { get; set; }
        public DbSet<ContactModel> Contacts { get; set; }
        public DbSet<CampaignModel> Campaigns { get; set; }

        public string DbPath { get; }

        public EmailMarketingContext()
        {
            var path = Path.Join(Directory.GetCurrentDirectory(), "wwwroot/database");
            DbPath = Path.Join(path, "marketingEmail.db");
           
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }
}
