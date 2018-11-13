namespace Epione.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("epione.appointment")]
    public partial class Appointment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Appointment()
        {
            evaluations = new HashSet<evaluation>();
            notifications = new HashSet<notification>();
            reports = new HashSet<report>();
        }

        public int id { get; set; }

        [Column(TypeName = "date")]
        public DateTime? date_appointment { get; set; }

        [StringLength(255)]
        public string endHour { get; set; }

        [StringLength(255)]
        public string message { get; set; }

        public float note { get; set; }

        [Column("object")]
        [StringLength(255)]
        public string _object { get; set; }

        [StringLength(255)]
        public string start_hour { get; set; }

        public int? state { get; set; }

        [StringLength(255)]
        public string title { get; set; }

        public int? id_doctor { get; set; }

        public int? id_patient { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<evaluation> evaluations { get; set; }

        public virtual user user { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<notification> notifications { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<report> reports { get; set; }

        public virtual user user1 { get; set; }
    }
}
