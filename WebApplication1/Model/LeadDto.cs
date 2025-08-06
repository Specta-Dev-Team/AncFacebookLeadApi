using System;
namespace WebApplication1.Model
{
    public class LeadDto
    {

        public long leadgenId { get; set; }
        public string formId { get; set; }
        public Boolean isOrganic { get; set; }
        public string dateCreated { get; set; }

     

        public Datadto data { get; set; }
    }
}
