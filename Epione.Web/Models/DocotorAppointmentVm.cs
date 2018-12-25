using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static Epione.Domain.user;

namespace Epione.Web.Models
{
    public class DocotorAppointmentVm
    {
        public int id { get; set; }

        
        public Adresse adress { get; set; }

        
        public DateTime? birthDate { get; set; }

         public string email { get; set; }

        
        public string firstName { get; set; }

       
        public DateTime? lastLogin { get; set; }

        
        public string lastName { get; set; }
        public string phone { get; set; }

        public string motif_s { get; set; }

        
        public string speciality_s { get; set; }
        public DateTime? creationDate { get; set; }
        public int days { get; set; }
        public int month { get; set; }
        public int year { get; set; }

        public void fill(DocotorAppointmentVm doc)
        {
            id = doc.id;

        }


    }
}