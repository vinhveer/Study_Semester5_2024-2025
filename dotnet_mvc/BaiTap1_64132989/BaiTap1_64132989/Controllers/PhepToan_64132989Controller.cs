using System.Web.Mvc;
using BaiTap1_64132989.Models;
namespace BaiTap1_64132989.Controllers
{
    public class PhepToan_64132989Controller : Controller
    {
        // Cách 1. Sử dụng Modal

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(CalculateModels model)
        {
            double result = 0;

            if (model == null)
            {
                return View();
            }

            switch (model.operation)
            {
                case "+":
                    result = model.a + model.b;
                    break;
                case "-":
                    result = model.a - model.b;
                    break;
                case "*":
                    result = model.a * model.b;
                    break;
                case "/":
                    if (model.b == 0)
                    {
                        ViewBag.result = "Không thể chia cho 0";
                        return View();
                    }
                    else
                    {
                        result = model.a / model.b;
                    }
                    break;
            }

            ViewBag.a = model.a;
            ViewBag.b = model.b;
            ViewBag.operation = model.operation;
            ViewBag.result = result;
            return View();
        }

        // Cách 2. Sử dụng FormCollection

        public ActionResult UseFormCollection()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UseFormCollection(FormCollection form)
        {
            double a = double.Parse(form["a"]);
            double b = double.Parse(form["b"]);
            string operation = form["operation"];
            double result = 0;

            switch (operation)
            {
                case "+":
                    result = a + b;
                    break;
                case "-":
                    result = a - b;
                    break;
                case "*":
                    result = a * b;
                    break;
                case "/":
                    if (b == 0)
                    {
                        ViewBag.result = "Không thể chia cho 0";
                        return View();
                    }
                    else
                    {
                        result = a / b;
                    }
                    break;
            }

            ViewBag.a = a;
            ViewBag.b = b;
            ViewBag.operation = operation;
            ViewBag.result = result;
            return View();
        }

        // Cách 3: Sử dụng đối số của Action
        public ActionResult UseArguments()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UseArguments(double a, double b, string operation = "+")
        {
            double result = 0;

            switch (operation)
            {
                case "+":
                    result = a + b;
                    break;
                case "-":
                    result = a - b;
                    break;
                case "*":
                    result = a * b;
                    break;
                case "/":
                    if (b == 0)
                    {
                        ViewBag.result = "Không thể chia cho 0";
                        return View();
                    }
                    else
                    {
                        result = a / b;
                    }
                    break;
            }

            ViewBag.a = a;
            ViewBag.b = b;
            ViewBag.operation = operation;
            ViewBag.result = result;
            return View();
        }

        // Cách 4: Sử dụng Request

        public ActionResult UseRequest()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UseRequest(string operation)
        {
            double a = double.Parse(Request["a"]);
            double b = double.Parse(Request["b"]);
            operation = Request["operation"];
            double result = 0;

            switch (operation)
            {
                case "+":
                    result = a + b;
                    break;
                case "-":
                    result = a - b;
                    break;
                case "*":
                    result = a * b;
                    break;
                case "/":
                    if (b == 0)
                    {
                        ViewBag.result = "Không thể chia cho 0";
                        return View();
                    }
                    else
                    {
                        result = a / b;
                    }
                    break;
            }

            ViewBag.a = a;
            ViewBag.b = b;
            ViewBag.operation = operation;
            ViewBag.result = result;
            return View();
        }
    }
}