﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KT0720NguyenQuangVinh_64132989.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class KT0720_64132989Entities : DbContext
    {
        public KT0720_64132989Entities()
            : base("name=KT0720_64132989Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Lop> Lops { get; set; }
        public virtual DbSet<SinhVien> SinhViens { get; set; }
    
        public virtual ObjectResult<TimKiemSinhVien_Result> TimKiemSinhVien(string maSV, string hoTen)
        {
            var maSVParameter = maSV != null ?
                new ObjectParameter("MaSV", maSV) :
                new ObjectParameter("MaSV", typeof(string));
    
            var hoTenParameter = hoTen != null ?
                new ObjectParameter("HoTen", hoTen) :
                new ObjectParameter("HoTen", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<TimKiemSinhVien_Result>("TimKiemSinhVien", maSVParameter, hoTenParameter);
        }
    }
}
