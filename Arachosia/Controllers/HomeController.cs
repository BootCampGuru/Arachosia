using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Arachosia.Models;
using System.IO;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Arachosia.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }
        [HttpGet]
        public IActionResult SignUp()
        {
            SignUp signup = new SignUp();
            return View(signup);
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUp signup)
        {
            //Send Grid Email
            var apiKey = "feKr3k83R3GGluPOn5-gdA";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("ranjanbiswasgwu@gmail.com", "Arachosia User");
            var subject = "New Signup";
            var to = new EmailAddress("biswasrk@gwu.edu", "Arachosia User");
            var plainTextContent = "Please sign up user:" + signup.Email;
            var htmlContent = "<strong>Please sign up user:/strong>" + signup.Email;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
            return View("Confirmation");
        }

        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }

        public ActionResult LaunchMenu()
        {

            var fileStream = new FileStream("PDF/" + "OnePageMenu.pdf",
                                    FileMode.Open,
                                    FileAccess.Read
                                  );
            var fsResult = new FileStreamResult(fileStream, "application/pdf");
            return fsResult;

        }

        public IActionResult Location()

        {
            return View();
        }

        public IActionResult Catering()
        {
            return View();
        }

        public  IActionResult Menu()
        {
            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
