using Microsoft.AspNetCore.Mvc;
using MyMvcProject.Models;
using MyMvcProject.Services;
using System.IO;
using System.Linq;

namespace MyMvcProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly DocumentConversionService _docService;
        private readonly IWebHostEnvironment _env;

        public HomeController(DocumentConversionService docService, IWebHostEnvironment env)
        {
            _docService = docService;
            _env = env;
        }

        // Action to list available forms
        public IActionResult SelectForm()
        {
            // Build the path to your forms folder
            string formsDirectory = Path.Combine(_env.ContentRootPath, "App_Data", "Forms");

            // Get all DOCX files in the directory
            var formFiles = Directory.GetFiles(formsDirectory, "*.docx");

            // Extract the form names (without extension)
            var formNames = formFiles.Select(Path.GetFileNameWithoutExtension).ToList();

            // Return the view with the list of form names
            return View(formNames);
        }

        // Other actions like RenderForm and Submit go here...
    

        // Converts the selected DOCX form to HTML and renders it.
        public IActionResult RenderForm(string formName)
        {
            if (string.IsNullOrEmpty(formName))
                return RedirectToAction("SelectForm");

            string filePath = Path.Combine(_env.ContentRootPath, "App_Data", "Forms", formName + ".docx");

            if (!System.IO.File.Exists(filePath))
                return NotFound("Form not found.");

            var htmlContent = _docService.ConvertDocxToHtml(filePath);
            ViewBag.HtmlContent = htmlContent;
            ViewBag.FormName = formName;
            return View();
        }

        // Handles the form submission with the signature.
        [HttpPost]
        public IActionResult Submit(FormModel model)
        {
            if (ModelState.IsValid)
            {
                // Process the submitted data (save to DB, convert back to PDF, etc.)
                return RedirectToAction("Success");
            }
            // Re-render the form if validation fails.
            return View("RenderForm", model);
        }

        public IActionResult Success()
        {
            return View();
        }
    }
}
