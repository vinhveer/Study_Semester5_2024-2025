using System;
using System.Web;

namespace BaiTap3_64132989.Models
{
    public class EmployeeModel
    {
        public string id { get; set; }
        public string fullName { get; set; }
        public DateTime dateOfBirth { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string department { get; set; }
        public string position { get; set; }  // Add position field
        public string avatar { get; set; }
    }
}
