using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace ThiGK64CNTT3_64132989_De1.Models
{
    public partial class Model_64132989 : DbContext
    {
        public Model_64132989()
            : base("name=Model_64132989")
        {
        }

        public virtual DbSet<ThanhVien> ThanhViens { get; set; }
        public virtual DbSet<Tinh> Tinhs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
