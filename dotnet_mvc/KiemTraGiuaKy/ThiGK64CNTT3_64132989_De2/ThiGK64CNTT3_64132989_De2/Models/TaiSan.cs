namespace ThiGK64CNTT3_64132989_De2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TaiSan")]
    public partial class TaiSan
    {
        [Key]
        [StringLength(10, ErrorMessage = "Mã tài sản không được quá 10 ký tự.")]
        [RegularExpression(@"^[A-Z0-9]+$", ErrorMessage = "Mã tài sản chỉ được chứa chữ hoa và số.")]
        [Display(Name = "Mã tài sản")]
        public string MaTS { get; set; }

        [Required(ErrorMessage = "Tên tài sản là bắt buộc.")]
        [StringLength(100, ErrorMessage = "Tên tài sản không được quá 100 ký tự.")]
        [RegularExpression(@"^[a-zA-Z0-9 ]+$", ErrorMessage = "Tên tài sản chỉ được chứa chữ cái, số và khoảng trắng.")]
        [Display(Name = "Tên tài sản")]
        public string TenTS { get; set; }

        [Required(ErrorMessage = "Đơn vị tính là bắt buộc.")]
        [StringLength(10, ErrorMessage = "Đơn vị tính không được quá 10 ký tự.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Đơn vị tính chỉ được chứa chữ cái.")]
        [Display(Name = "Đơn vị tính")]
        public string DVT { get; set; }


        [Required(ErrorMessage = "Xuất xứ là bắt buộc.")]
        [Display(Name = "Xuất xứ")]
        public bool XuatXu { get; set; }

        [Required(ErrorMessage = "Số lượng là bắt buộc.")]
        [Display(Name = "Đơn giá")]
        [Range(0.01, 99999999.99, ErrorMessage = "Đơn giá phải nằm trong khoảng từ 0.01 đến 99999999.99.")]
        [DisplayFormat(DataFormatString = "{0:#,##0.##}")]
        public decimal DonGia { get; set; }

        [StringLength(255, ErrorMessage = "Tên tệp ảnh không được quá 255 ký tự.")]
        [Display(Name = "Ảnh minh hoạ")]
        public string AnhMH { get; set; }

        [StringLength(10, ErrorMessage = "Mã loại tài sản không được quá 10 ký tự.")]
        [Display(Name = "Mã loại tài sản")]
        public string MaLTS { get; set; }

        [StringLength(255, ErrorMessage = "Ghi chú không được quá 255 ký tự.")]
        [Display(Name = "Ghi chú")]
        public string GhiChu { get; set; }

        public virtual LoaiTaiSan LoaiTaiSan { get; set; }
    }
}
