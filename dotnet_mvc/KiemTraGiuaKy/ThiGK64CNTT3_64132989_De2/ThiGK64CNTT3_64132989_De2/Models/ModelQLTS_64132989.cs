using System.Data.Entity;

namespace ThiGK64CNTT3_64132989_De2.Models
{
    public partial class ModelQLTS_64132989 : DbContext
    {
        public ModelQLTS_64132989()
            : base("name=ModelQLTS_64132989")
        {
        }

        public virtual DbSet<LoaiTaiSan> LoaiTaiSans { get; set; }
        public virtual DbSet<TaiSan> TaiSans { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LoaiTaiSan>()
                .Property(e => e.MaLTS)
                .IsUnicode(false);

            modelBuilder.Entity<TaiSan>()
                .Property(e => e.MaTS)
                .IsUnicode(false);

            modelBuilder.Entity<TaiSan>()
                .Property(e => e.MaLTS)
                .IsUnicode(false);
        }
    }
}
