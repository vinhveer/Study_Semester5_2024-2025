namespace BaiTap6_64132989.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NhanVien")]
    public partial class NhanVien
    {
        [Key]
        [Display(Name = "#")]
        public int MaNV { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Họ")]
        public string HoNV { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Tên")]
        public string TenNV { get; set; }

        [Display(Name = "Giới tính")]
        public bool GioiTinh { get; set; }

        [Column(TypeName = "date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Ngày sinh")]
        public DateTime? NgaySinh { get; set; }

        [Display(Name = "Lương")]
        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        public decimal? Luong { get; set; }

        [StringLength(255)]
        [Display(Name = "Ảnh nhân viên")]
        public string AnhNV { get; set; }

        [StringLength(255)]
        [Display(Name = "Địa chỉ")]
        public string DiaChi { get; set; }

        [Display(Name = "Phòng ban")]
        public int? MaPhongBan { get; set; }

        public virtual PhongBan PhongBan { get; set; }
    }
}
