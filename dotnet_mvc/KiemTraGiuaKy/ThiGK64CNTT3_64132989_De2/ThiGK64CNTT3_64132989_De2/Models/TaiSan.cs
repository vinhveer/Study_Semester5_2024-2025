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
        [StringLength(10)]
        [Display(Name = "Mã tài sản")]
        public string MaTS { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Tên tài sản")]
        public string TenTS { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Đơn vị tính")]
        public string DVT { get; set; }

        [Display(Name = "Xuất xứ")]
        public bool XuatXu { get; set; }

        [Display(Name = "Đơn giá")]
        [DisplayFormat(DataFormatString = "{0:#,##0.##}")]
        public decimal DonGia { get; set; }

        [Display(Name = "Ảnh minh hoạ")]
        [StringLength(255)]
        public string AnhMH { get; set; }

        [StringLength(10)]
        [Display(Name = "Mã loại tài sản")]
        public string MaLTS { get; set; }

        [StringLength(255)]
        [Display(Name = "Ghi chú")]
        public string GhiChu { get; set; }

        public virtual LoaiTaiSan LoaiTaiSan { get; set; }
    }
}
