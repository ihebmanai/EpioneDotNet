namespace Epione.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("epione.availibility")]
    public partial class availibility
    {
        public int id { get; set; }

        public DateTime? date { get; set; }

        public int idDoc { get; set; }
    }
}
