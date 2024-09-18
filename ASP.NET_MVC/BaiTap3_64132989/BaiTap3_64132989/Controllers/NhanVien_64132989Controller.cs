using BaiTap3_64132989.Models;
using System.Web.Mvc;
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
                if (avatar != null && avatar.ContentLength > 0)
                {
                    // Get the file name of the uploaded file
                    string avatarFileName = Path.GetFileName(avatar.FileName);

                    // Combine the path to the Content folder and the file name
                    var avatarSavePath = Server.MapPath("~/Content/" + avatarFileName);

                    // Debugging: Log the file path
                    System.Diagnostics.Debug.WriteLine($"Saving avatar to: {avatarSavePath}");

                    // Save the uploaded file to the server
                    avatar.SaveAs(avatarSavePath);

                    // Check if the file was saved
                    if (System.IO.File.Exists(avatarSavePath))
                    {
                        // Store the relative path of the saved avatar in the model
                        model.avatar = "/Content/" + avatarFileName;
                    }
                    else
                    {
                        throw new Exception("Avatar file was not saved.");
                    }
                }
                else
                {
                    throw new Exception("No avatar file uploaded.");
                }

                SaveToCsv(model);

                return RedirectToAction("ListEmployee");
            }
            catch (Exception ex)
            {
                // Debugging: Log the error message
                System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");

                ViewBag.Message = "An error occurred: " + ex.Message;
                return View(model);
            }
        }


        // Save employee information to the CSV file
        private void SaveToCsv(EmployeeModel model)
        {
            var filePath = Server.MapPath(CsvFilePath);
            using (var writer = new StreamWriter(filePath, true))
            {
                var line = $"{model.id},{model.fullName},{model.dateOfBirth.ToString("MM/dd/yyyy")},{model.email},{model.password},{model.department},{model.position},{model.avatar}";
                writer.WriteLine(line);
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
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var fields = line.Split(',');
                        if (fields.Length >= 8)
                        {
                            var employee = new EmployeeModel
                            {
                                id = fields[0],
                                fullName = fields[1],
                                dateOfBirth = DateTime.Parse(fields[2]), // Adjust the format if needed
                                email = fields[3],
                                password = fields[4],
                                department = fields[5],
                                position = fields[6],
                                avatar = fields[7]
                            };
                            employees.Add(employee);
                        }
                    }
                }
            }

            return View(employees); // Make sure to create a view that handles a list of employees
        }
    }
}
