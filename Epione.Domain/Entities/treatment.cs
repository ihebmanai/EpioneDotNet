namespace Epione.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("epione.treatments")]
    public partial class treatment
    {
        public int id { get; set; }

        [StringLength(255)]
        public string dateTreatments { get; set; }

        [StringLength(255)]
        public string doctorForRecommandation { get; set; }

        [StringLength(255)]
        public string justification { get; set; }

        [StringLength(255)]
        public string nameTreatment { get; set; }

        public int valider { get; set; }

        public int? report_id { get; set; }

        public virtual report report { get; set; }
    }
}
