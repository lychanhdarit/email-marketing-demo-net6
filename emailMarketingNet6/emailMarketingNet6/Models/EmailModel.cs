using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace emailMarketingNet6.Models
{
    public class EmailModel
    {
       
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? PassLogin { get; set; }
        public string? Name { get; set; }
        public List<CampaignModel> Campaigns { get; } = new();
    }
    public class CampaignModel
    {
      
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; } 
        [Display(Name ="Hẹn giờ")]
        public bool Schedule { get; set; }
        public DateTime? Day { get; set; }
        public int? Hour { get; set; }
        public int? Minute { get; set; } 
        public bool Actived { get; set; }

        public int TemplateId { get; set; }
        public TemplateModel? Template { get; set; } 
        public int ContactListId { get; set; }
        public ContactListModel? ContactList { get; set; }
        public int EmailSendId { get; set; }
        public EmailModel? EmailSend { get; set; }
    }
    public class TemplateModel
    {
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        [DataType(DataType.MultilineText)]
        public string? Content { get; set; }
        public Nullable<bool> Active { get; set; }
        public List<CampaignModel> Campaigns { get; } = new();
    }


    public class ContactListModel
    {
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ContactModel> Contacts { get; } = new();
        public List<CampaignModel> Campaigns { get; } = new();
    }
    public class ContactModel
    { 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int ContactListId { get; set; }
        public ContactListModel? ContactList { get; set; }
    }
}
