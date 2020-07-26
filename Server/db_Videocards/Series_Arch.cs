namespace Server
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Series_Arch
    {
        public long id { get; set; }

        public long id_Series { get; set; }

        public long id_Architect { get; set; }

        public virtual Architecture Architecture { get; set; }

        public virtual Series Series { get; set; }
    }
}
