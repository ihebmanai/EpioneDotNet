namespace Epione.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("epione.soins")]
    public partial class soin
    {
        public int id { get; set; }

        [StringLength(255)]
        public string typeSoins { get; set; }

        public int? id_doctor { get; set; }

        public virtual user user { get; set; }
    }
}
