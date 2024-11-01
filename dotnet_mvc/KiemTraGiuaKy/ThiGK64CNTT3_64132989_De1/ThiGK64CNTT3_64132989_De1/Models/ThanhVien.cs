namespace ThiGK64CNTT3_64132989_De1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ThanhVien")]
    public partial class ThanhVien
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(5, ErrorMessage = "Mã thành viên không được quá 5 ký tự.")]
        [RegularExpression(@"^[A-Za-z0-9]+$", ErrorMessage = "Mã thành viên chỉ được chứa chữ cái và số.")]
        [Display(Name = "Mã TV")]
        public string MaTV { get; set; }

        [Required(ErrorMessage = "Họ thành viên là bắt buộc.")]
        [StringLength(50, ErrorMessage = "Họ thành viên không được quá 50 ký tự.")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Họ thành viên chỉ được chứa chữ cái.")]
        [Display(Name = "Họ")]
        public string HoTV { get; set; }

        [Required(ErrorMessage = "Tên thành viên là bắt buộc.")]
        [StringLength(50, ErrorMessage = "Tên thành viên không được quá 50 ký tự.")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Tên thành viên chỉ được chứa chữ cái.")]
        [Display(Name = "Tên")]
        public string TenTV { get; set; }

        [StringLength(50)]
        [Display(Name = "Ảnh đại diện")]
        public string AnhDaiDien { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Ngày sinh")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date, ErrorMessage = "Ngày sinh không hợp lệ.")]
        public DateTime NgaySinh { get; set; }

        [Display(Name = "Giới tính")]
        public bool GioiTinh { get; set; }

        [Required(ErrorMessage = "Email là bắt buộc.")]
        [StringLength(100, ErrorMessage = "Email không được quá 100 ký tự.")]
        [EmailAddress(ErrorMessage = "Định dạng email không hợp lệ.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [StringLength(100, ErrorMessage = "Địa chỉ không được quá 100 ký tự.")]
        [Display(Name = "Địa chỉ")]
        public string DiaChi { get; set; }

        [Display(Name = "Mã tỉnh")]
        public int? MaTinh { get; set; }

        public virtual Tinh Tinh { get; set; }
    }
}
