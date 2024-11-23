namespace KT0720Vinh_64132989.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Lop")]
    public partial class Lop
    {
        public Lop()
        {
            SinhViens = new HashSet<SinhVien>();
        }

        [Key]
        [StringLength(5)]
        public string MaLop { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Tên lớp")]
        public string TenLop { get; set; }

        public virtual ICollection<SinhVien> SinhViens { get; set; }
    }
}
