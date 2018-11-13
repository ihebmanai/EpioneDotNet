namespace Epione.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("epione.discussion")]
    public partial class discussion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public discussion()
        {
            messages = new HashSet<message>();
        }

        public int id { get; set; }

        [Column(TypeName = "bit")]
        public bool doctorDeleted { get; set; }

        public DateTime? lastUpdated { get; set; }

        [Column(TypeName = "bit")]
        public bool patientDeleted { get; set; }

        public int? doctor_id { get; set; }

        public int? patient_id { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<message> messages { get; set; }

        public virtual user doctor { get; set; }

        public virtual user patient { get; set; }
    }
}
