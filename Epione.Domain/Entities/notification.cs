namespace Epione.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("epione.notification")]
    public partial class notification
    {
        public int id { get; set; }

        [StringLength(255)]
        public string checkAppointmentType { get; set; }

        public DateTime? date { get; set; }

        [Column("object")]
        [StringLength(255)]
        public string _object { get; set; }

        public int seen { get; set; }

        [StringLength(255)]
        public string title { get; set; }

        public int? appointment_id { get; set; }

        public int? doctor_id { get; set; }

        public int? patient_id { get; set; }

        public virtual Appointment appointment { get; set; }

        public virtual user user { get; set; }

        public virtual user user1 { get; set; }
    }
}
