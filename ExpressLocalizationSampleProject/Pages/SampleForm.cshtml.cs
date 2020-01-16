using System;
using System.ComponentModel.DataAnnotations;
using LazZiya.ExpressLocalization.DataAnnotations;
using LazZiya.ExpressLocalization.Messages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ExpressLocalizationSampleProject.Pages
{
    public class SampleFormModel : PageModel
    {
        public class TestModel
        {
            [ExRequired]
            [Display(Name = "ID")]
            public int ID { get; set; }

            [ExRequired]
            [Display(Name = "Name")]
            public string Name { get; set; }

            [ExRequired]
            [Display(Name = "Price")]
            public decimal Price { get; set; }

            [ExRequired]
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