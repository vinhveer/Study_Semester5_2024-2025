namespace ThiGK_64132989.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TinTuc")]
    public partial class TinTuc
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Mã tin tức")]
        [Required(ErrorMessage = "Mã tin tức không được để trống")]
        [StringLength(5, ErrorMessage = "Mã tin tức không được vượt quá 5 ký tự")]
        public string MaTT { get; set; }

        [Required(ErrorMessage = "Tiêu đề không được để trống")]
        [StringLength(200, ErrorMessage = "Tiêu đề không được vượt quá 200 ký tự")]
        [Display(Name = "Tiêu đề")]
        public string TieuDe { get; set; }

        [Required(ErrorMessage = "Ngôn ngữ không được để trống")]
        [Display(Name = "Ngôn ngữ")]
        public bool NgonNgu { get; set; }

        [Required(ErrorMessage = "Người đăng không được để trống")]
        [StringLength(100, ErrorMessage = "Tên người đăng không được vượt quá 100 ký tự")]
        [Display(Name = "Người đăng")]
        public string NguoiDang { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Ngày đăng")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Ngày đăng không được để trống")]
        public DateTime NgayDang { get; set; }

        [StringLength(100, ErrorMessage = "Tên ảnh đại diện không được vượt quá 100 ký tự")]
        [Display(Name = "Ảnh đại diện")]
        public string AnhDaiDien { get; set; }

        [Display(Name = "Mã loại tin tức")]
        public int? MaLTT { get; set; }

        [StringLength(255, ErrorMessage = "Ghi chú không được vượt quá 255 ký tự")]
        [Display(Name = "Ghi chú")]
        public string GhiChu { get; set; }

        public virtual LoaiTinTuc LoaiTinTuc { get; set; }
    }
}
