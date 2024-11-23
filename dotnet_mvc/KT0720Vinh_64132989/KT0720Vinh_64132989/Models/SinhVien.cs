namespace KT0720Vinh_64132989.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SinhVien")]
    public partial class SinhVien
    {
        [Key]
        [StringLength(5)]
        [Display(Name = "Mã sinh viên")]
        [Required(ErrorMessage = "Mã Sinh Viên là bắt buộc.")]
        public string MaSV { get; set; }

        [Required(ErrorMessage = "Họ sinh viên là bắt buộc.")]
        [StringLength(30, ErrorMessage = "Họ sinh viên không vượt quá 30 ký tự.")]
        [Display(Name = "Họ sinh siên")]
        public string HoSV { get; set; }

        [Required(ErrorMessage = "Tên sinh viên là bắt buộc.")]
        [StringLength(30, ErrorMessage = "Tên sinh viên không vượt quá 30 ký tự.")]
        [Display(Name = "Tên sinh viên")]
        public string TenSV { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Ngày sinh")]
        [Required(ErrorMessage = "Ngày sinh là bắt buộc.")]
        public DateTime NgaySinh { get; set; }

        [Display(Name = "Giới tính")]
        [Required(ErrorMessage = "Giới tính là bắt buộc.")]
        public bool GioiTinh { get; set; }

        [StringLength(50, ErrorMessage = "Ảnh sinh viên không vượt quá 50 ký tự.")]
        [Display(Name = "Ảnh sinh viên")]
        public string AnhSV { get; set; }

        [StringLength(100, ErrorMessage = "Địa chỉ không vượt quá 100 ký tự.")]
        [Display(Name = "Địa chỉ")]
        public string DiaChi { get; set; }

        [StringLength(5)]
        [Display(Name = "Mã Lớp")]
        public string MaLop { get; set; }

        public virtual Lop Lop { get; set; }
    }
}
