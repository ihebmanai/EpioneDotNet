namespace Epione.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("epione.evaluation")]
    public partial class evaluation
    {
        public int id { get; set; }

        [StringLength(255)]
        public string message { get; set; }

        public float note { get; set; }

        public int? appointment_id { get; set; }

        public int? id_patient { get; set; }

        public virtual Appointment appointment { get; set; }

        public virtual user user { get; set; }
    }
}
