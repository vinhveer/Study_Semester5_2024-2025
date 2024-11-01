namespace ThiGK64CNTT3_64132989_De2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LoaiTaiSan")]
    public partial class LoaiTaiSan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LoaiTaiSan()
        {
            TaiSans = new HashSet<TaiSan>();
        }

        [Key]
        [StringLength(10)]
        [Display(Name = "Mã loại tài sản")]
        public string MaLTS { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Tên loại tài sản")]
        public string TenLTS { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TaiSan> TaiSans { get; set; }
    }
}
