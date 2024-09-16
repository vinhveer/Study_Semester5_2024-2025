using BaiTap3_64132989.Models;
using System.Web.Mvc;
using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System;
using System.Web;

namespace BaiTap3_64132989.Controllers
{
    public class NhanVien_64132989Controller : Controller
    {
        // Path to the CSV file
        private const string CsvFilePath = "~/App_Data/employees.csv";

        // GET: NhanVien_64132989/Index
        [HttpGet]
        public ActionResult Index()
        {
            return View(new EmployeeModel());
        }

        // POST: NhanVien_64132989/Index
        [HttpPost]
        public ActionResult Index(EmployeeModel model, HttpPostedFileBase avatar)
        {
            try
            {
                string avatarPath = System.IO.Path.GetFileName(avatar.FileName);

                // Save the uploaded avatar to the server
                if (avatar != null && avatar.ContentLength > 0)
                {
                    var avatarSavePath = Server.MapPath("~/Content/avatars/" + avatarPath);
                    avatar.SaveAs(avatarSavePath);
                }

                model.avatar = avatarPath;
                

                return RedirectToAction("ListEmployee");
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Có lỗi xảy ra: " + ex.Message;
                return View(model);
            }
        }

        // Save employee information to the CSV file
        private void SaveToCsv(EmployeeModel model)
        {
            var filePath = Server.MapPath(CsvFilePath);
            using (var writer = new StreamWriter(filePath, true))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecord(model);
                writer.WriteLine();
            }
        }

        // GET: NhanVien_64132989/ListEmployee
        public ActionResult ListEmployee()
        {
            var employees = new List<EmployeeModel>();

            // Read employee data from the CSV file
            var filePath = Server.MapPath(CsvFilePath);
            if (System.IO.File.Exists(filePath))
            {
                using (var reader = new StreamReader(filePath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    employees = csv.GetRecords<EmployeeModel>().ToList();
                }
            }

            return View(employees);
        }
    }
}
