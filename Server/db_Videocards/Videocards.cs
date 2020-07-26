namespace Server
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Videocards : DbContext
    {
        public Videocards()
            : base("name=Videocards")
        {
        }

        public virtual DbSet<Architecture> Architecture { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<Manufacturer> Manufacturer { get; set; }
        public virtual DbSet<Series> Series { get; set; }
        public virtual DbSet<Series_Arch> Series_Arch { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Videocard> Videocard { get; set; }
        public virtual DbSet<Video_Company> Video_Company { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Architecture>()
                .HasMany(e => e.Series_Arch)
                .WithRequired(e => e.Architecture)
                .HasForeignKey(e => e.id_Architect)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Architecture>()
                .HasMany(e => e.Videocard)
                .WithRequired(e => e.Architecture)
                .HasForeignKey(e => e.id_Architect)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Company>()
                .HasMany(e => e.Video_Company)
                .WithRequired(e => e.Company)
                .HasForeignKey(e => e.id_Company)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Manufacturer>()
                .HasMany(e => e.Architecture)
                .WithRequired(e => e.Manufacturer)
                .HasForeignKey(e => e.id_Manufact)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Manufacturer>()
                .HasMany(e => e.Series)
                .WithRequired(e => e.Manufacturer)
                .HasForeignKey(e => e.id_Manufact)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Manufacturer>()
                .HasMany(e => e.Videocard)
                .WithRequired(e => e.Manufacturer)
                .HasForeignKey(e => e.id_Manufact)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Series>()
                .HasMany(e => e.Series_Arch)
                .WithRequired(e => e.Series)
                .HasForeignKey(e => e.id_Series)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Series>()
                .HasMany(e => e.Videocard)
                .WithRequired(e => e.Series)
                .HasForeignKey(e => e.id_Series)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Videocard>()
                .HasMany(e => e.Video_Company)
                .WithRequired(e => e.Videocard)
                .HasForeignKey(e => e.id_Videocard)
                .WillCascadeOnDelete(false);
        }
    }
}
