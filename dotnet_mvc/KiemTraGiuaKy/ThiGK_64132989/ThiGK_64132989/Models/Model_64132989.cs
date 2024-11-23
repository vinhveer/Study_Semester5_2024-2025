using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace ThiGK_64132989.Models
{
    public partial class Model_64132989 : DbContext
    {
        public Model_64132989()
            : base("name=Model_64132989")
        {
        }

        public virtual DbSet<LoaiTinTuc> LoaiTinTucs { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<TinTuc> TinTucs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
