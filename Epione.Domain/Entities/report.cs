namespace Epione.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("epione.report")]
    public partial class report
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public report()
        {
            treatments = new HashSet<treatment>();
        }

        public int id { get; set; }

        public DateTime? date { get; set; }

        [StringLength(255)]
        public string description { get; set; }

        [StringLength(255)]
        public string desease { get; set; }

        [StringLength(255)]
        public string objet { get; set; }

        public int? appointment_id { get; set; }

        public int? id_course { get; set; }

        public virtual Appointment appointment { get; set; }

        public virtual course course { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<treatment> treatments { get; set; }
    }
}
