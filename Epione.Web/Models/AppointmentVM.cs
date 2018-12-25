using Epione.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Epione.Web.Models
{
    public class AppointmentVM
    {
        public int id { get; set; }
        [Required(ErrorMessage = "date required")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        [DataType(DataType.Date)]
        public DateTime? date_appointment { get; set; }

       
        public String end_Hour { get; set; }
        [Required(ErrorMessage = "Start date required")]
        public String start_Hour { get; set; }
        
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage ="message required")]
        public String message { get; set; }
        [Required(ErrorMessage = "Subject required")]
        public String object_appointment { get; set; }
        [Required(ErrorMessage = "Title required")]
        public String title { get; set; }

        public String state { get; set; }
        public int? note { get; set; }
        public int? id_doctor { get; set; }
        public int? id_patient { get; set; }
        
        public virtual user doctor { get; set; }

        public virtual user patient { get; set; }

        public void fill(Appointment appointment)
        {
            id = appointment.id;

        }


    }
}
