using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace BaiTap6_64132989.Models
{
    public partial class Employee : DbContext
    {
        public Employee()
            : base("name=Employee")
        {
        }

        public virtual DbSet<NhanVien> NhanViens { get; set; }
        public virtual DbSet<PhongBan> PhongBans { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
