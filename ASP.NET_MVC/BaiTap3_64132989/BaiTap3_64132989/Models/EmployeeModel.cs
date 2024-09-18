using CsvHelper.Configuration.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace BaiTap3_64132989.Models
{
    public class EmployeeModel
    {
        [Display(Name = "ID")]
        public string id { get; set; }

        [Display(Name = "Họ và tên")]
        public string fullName { get; set; }

        [Display(Name = "Ngày sinh")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime dateOfBirth { get; set; }

        [Display(Name = "Email")]
        public string email { get; set; }

        [Display(Name = "Mật khẩu")]
        public string password { get; set; }

        [Display(Name = "Phòng ban")]
        public string department { get; set; }

        [Display(Name = "Vị trí")]
        public string position { get; set; }

        [Display(Name = "Ảnh đại diện")]
        public string avatar { get; set; }
    }
}
