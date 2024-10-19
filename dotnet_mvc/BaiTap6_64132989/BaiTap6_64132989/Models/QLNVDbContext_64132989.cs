using System.Data.Entity;

namespace BaiTap6_64132989.Models
{
    public partial class QLNVDbContext_64132989 : DbContext
    {
        public QLNVDbContext_64132989()
            : base("name=QLNVDbContext_64132989")
        {
        }

        public virtual DbSet<NhanVien> NhanViens { get; set; }
        public virtual DbSet<PhongBan> PhongBans { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
