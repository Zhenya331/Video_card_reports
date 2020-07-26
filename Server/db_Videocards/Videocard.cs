namespace Server
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Videocard")]
    public partial class Videocard
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Videocard()
        {
            Video_Company = new HashSet<Video_Company>();
        }

        public long id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public int Energy_use { get; set; }

        public int Year { get; set; }

        public int Memory_size { get; set; }

        public long id_Manufact { get; set; }

        public long id_Architect { get; set; }

        public long id_Series { get; set; }

        public virtual Architecture Architecture { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }

        public virtual Series Series { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Video_Company> Video_Company { get; set; }
    }
}
