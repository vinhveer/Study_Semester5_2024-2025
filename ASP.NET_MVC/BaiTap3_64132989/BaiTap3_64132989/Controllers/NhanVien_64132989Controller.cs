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
        private const string CsvFilePath = "~/App_Data/employees.csv";

        // GET: NhanVien_64132989/Index
        [HttpGet]
        public ActionResult Index() => View(new EmployeeModel());

        // POST: NhanVien_64132989/Index
        [HttpPost]
        public ActionResult Index(EmployeeModel model, HttpPostedFileBase avatar)
        {
            try
            {
                if (avatar != null && avatar.ContentLength > 0)
                {
                    string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
                    string extension = Path.GetExtension(avatar.FileName).ToLower();

                    if (!allowedExtensions.Contains(extension))
                        throw new Exception("Invalid file format. Only images are allowed.");

                    string avatarFileName = Path.GetFileName(avatar.FileName);
                    string avatarSavePath = Path.Combine(Server.MapPath("~/Images/"), avatarFileName);

                    avatar.SaveAs(avatarSavePath);

                    if (System.IO.File.Exists(avatarSavePath))
                        model.avatar = "/Images/" + avatarFileName;
                    else
                        throw new Exception("Avatar file was not saved.");
                }

                if (ModelState.IsValid)
                {
                    SaveToCsv(model);
                    return RedirectToAction("ListEmployee");
                }
                throw new Exception("Model is not valid.");
            }
            catch (Exception ex)
            {
                ViewBag.Message = "An error occurred: " + ex.Message;
                return View(model);
            }
        }

        // GET: NhanVien_64132989/Details/{id}
        [HttpGet]
        public ActionResult Details(string id)
        {
            var employee = GetEmployeeById(id);
            return employee != null ? View(employee) : (ActionResult)HttpNotFound();
        }

        // GET: NhanVien_64132989/Edit/{id}
        [HttpGet]
        public ActionResult Edit(string id)
        {
            var employee = GetEmployeeById(id);
            return employee != null ? View(employee) : (ActionResult)HttpNotFound();
        }

        // POST: NhanVien_64132989/Edit/{id}
        [HttpPost]
        public ActionResult Edit(string id, EmployeeModel model, HttpPostedFileBase avatar)
        {
            var employees = GetAllEmployees();
            var index = employees.FindIndex(e => e.id == id);

            if (index == -1)
                return HttpNotFound();

            var existingEmployee = employees[index];

            // Update password if provided
            if (!string.IsNullOrEmpty(model.password))
            {
                existingEmployee.password = model.password;
            }

            // Update avatar if provided
            if (avatar != null && avatar.ContentLength > 0)
            {
                string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
                string extension = Path.GetExtension(avatar.FileName).ToLower();

                if (!allowedExtensions.Contains(extension))
                    throw new Exception("Invalid file format. Only images are allowed.");

                string avatarFileName = Path.GetFileName(avatar.FileName);
                string avatarSavePath = Path.Combine(Server.MapPath("~/Images/"), avatarFileName);

                avatar.SaveAs(avatarSavePath);
                existingEmployee.avatar = "/Images/" + avatarFileName;
            }

            // Update other fields from the model
            existingEmployee.fullName = model.fullName;
            existingEmployee.dateOfBirth = model.dateOfBirth;
            existingEmployee.email = model.email;
            existingEmployee.department = model.department;
            existingEmployee.position = model.position;

            // Save the updated list back to the CSV file
            SaveAllToCsv(employees);

            return RedirectToAction("ListEmployee");
        }


        // GET: NhanVien_64132989/Delete/{id}
        [HttpGet]
        public ActionResult Delete(string id)
        {
            var employee = GetEmployeeById(id);
            return employee != null ? View(employee) : (ActionResult)HttpNotFound();
        }

        // POST: NhanVien_64132989/Delete/{id}
        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(string id)
        {
            var employees = GetAllEmployees();
            var employee = employees.FirstOrDefault(e => e.id == id);

            if (employee == null)
                return HttpNotFound();

            employees.Remove(employee);
            SaveAllToCsv(employees);

            return RedirectToAction("ListEmployee");
        }

        // List all employees
        public ActionResult ListEmployee() => View(GetAllEmployees());

        // Helper methods
        private List<EmployeeModel> GetAllEmployees()
        {
            var employees = new List<EmployeeModel>();
            var filePath = Server.MapPath(CsvFilePath);

            if (System.IO.File.Exists(filePath))
            {
                foreach (var line in System.IO.File.ReadAllLines(filePath))
                {
                    var fields = line.Split(',');
                    if (fields.Length >= 8 && DateTime.TryParseExact(fields[2], "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dob))
                    {
                        employees.Add(new EmployeeModel
                        {
                            id = fields[0],
                            fullName = fields[1],
                            dateOfBirth = dob,
                            email = fields[3],
                            password = fields[4],
                            department = fields[5],
                            position = fields[6],
                            avatar = fields[7]
                        });
                    }
                }
            }
            return employees;
        }

        private EmployeeModel GetEmployeeById(string id) => GetAllEmployees().FirstOrDefault(e => e.id == id);

        private void SaveToCsv(EmployeeModel model)
        {
            var filePath = Server.MapPath(CsvFilePath);
            var line = $"{model.id},{model.fullName},{model.dateOfBirth:MM/dd/yyyy},{model.email},{model.password},{model.department},{model.position},{model.avatar}";
            System.IO.File.AppendAllText(filePath, line + Environment.NewLine);
        }

        private void SaveAllToCsv(List<EmployeeModel> employees)
        {
            var filePath = Server.MapPath(CsvFilePath);
            var lines = employees.Select(e => $"{e.id},{e.fullName},{e.dateOfBirth:MM/dd/yyyy},{e.email},{e.password},{e.department},{e.position},{e.avatar}");
            System.IO.File.WriteAllLines(filePath, lines);
        }
    }
}
