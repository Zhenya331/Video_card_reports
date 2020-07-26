namespace Server
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Users
    {
        [Key]
        [Column(Order = 0)]
        public long id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string Username { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(50)]
        public string Password { get; set; }
    }
}
