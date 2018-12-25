namespace Epione.Domain
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("epione.appointment")]
    public partial class Appointment
    {
        public enum State
        {
            accepeted,
            running,
            refused,
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Appointment()
        {
            evaluations = new HashSet<evaluation>();
            notifications = new HashSet<notification>();
            reports = new HashSet<report>();
        }

        public int id { get; set; }

        [Column(TypeName = "date")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime? date_appointment { get; set; }

        [StringLength(255)]
        public string endHour { get; set; }

        [StringLength(255)]
        public string message { get; set; }

        public float note { get; set; }

        [Column("object")]
        [JsonProperty("object")]
        [StringLength(255)]
        public string _object { get; set; }

        [StringLength(255)]
        public string start_hour { get; set; }

       
        public string state { get; set; }

        [StringLength(255)]
        public string title { get; set; }



        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<evaluation> evaluations { get; set; }

        public virtual user doctor { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<notification> notifications { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<report> reports { get; set; }

        public virtual user patient { get; set; }

        public bool ShouldSerializeid_doctor()
        {
            return false;
        }

        public bool ShouldSerializeid_patient()
        {
            return false;
        }

        public bool ShouldSerializenotifications()
        {
            return false;
        }
        public bool ShouldSerializeevaluations()
        {
            return false;
        }
        public bool ShouldSerializereports()
        {
            return false;
        }
       

    }
}
