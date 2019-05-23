using System;
using System.ComponentModel.DataAnnotations;
using LazZiya.ExpressLocalization.Messages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ExpressLocalizationSampleProject.Pages
{
    public class SampleFormModel : PageModel
    {
        public class TestModel
        {
            [Required(ErrorMessage = DataAnnotationsErrorMessages.RequiredAttribute_ValidationError)]
            [Display(Name = "ID")]
            public int ID { get; set; }

            [Required(ErrorMessage = DataAnnotationsErrorMessages.RequiredAttribute_ValidationError)]
            [Display(Name = "Name")]
            public string Name { get; set; }

            [Required(ErrorMessage = DataAnnotationsErrorMessages.RequiredAttribute_ValidationError)]
            [Display(Name = "Price")]
            public decimal Price { get; set; }

            [Required(ErrorMessage = DataAnnotationsErrorMessages.RequiredAttribute_ValidationError)]
            [Display(Name = "Date")]
            public DateTime? Date { get; set; }
        }

        [BindProperty]
        public TestModel Test { get; set; }

        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            return RedirectToPage("Privacy");
        }
    }
}