﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BaiTap6_64132989_NoAuth.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class NhanVien
    {
        [Display(Name = "Mã nhân viên")]
        public int MaNV { get; set; }

        [Display(Name = "Họ nhân viên")]
        public string HoNV { get; set; }

        [Display(Name = "Tên nhân viên")]
        public string TenNV { get; set; }

        [Display(Name = "Giới tính")]
        public bool GioiTinh { get; set; }

        [Display(Name = "Ngày sinh")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> NgaySinh { get; set; }

        [Display(Name = "Lương")]
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public Nullable<decimal> Luong { get; set; }

        [Display(Name = "Ảnh nhân viên")]
        public string AnhNV { get; set; }

        [Display(Name = "Địa chỉ")]
        public string DiaChi { get; set; }

        [Display(Name = "Mã phòng ban")]
        public Nullable<int> MaPhongBan { get; set; }

        public virtual PhongBan PhongBan { get; set; }
    }
}