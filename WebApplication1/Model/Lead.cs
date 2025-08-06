using Microsoft.AspNetCore.SignalR;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace WebApplication1.Model

{
    public class Lead
    {

        [Key]
        public long LEADGENID { get; set; }
        public string FORMID { get; set; }
        public string DATECREATED { get; set; }

        public string UNIVERSITYNAME { get; set; }
        public string LOCATION { get; set; }
        public string FIRSTNAME { get; set; }
        public string SECONDNAME { get; set; }
        public string EMAIL { get; set; }
        public string FULLNAME { get; set; }
        public string PHONENUMBER { get; set; }


    }

    
}
