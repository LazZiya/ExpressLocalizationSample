using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using LazZiya.Localization.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SampleProject.Pages
{
    public class IndexModel : PageModel
    {
        public class TestModel
        {
            [Required(ErrorMessage = DataAnnotationMessage.RequiredErrorMessage)]
            [Display(Name = "ID")]
            public int? ID { get; set; }

            [Required(ErrorMessage = DataAnnotationMessage.RequiredErrorMessage)]
            [Display(Name = "Name")]
            public string Name { get; set; }

            [Required(ErrorMessage = DataAnnotationMessage.RequiredErrorMessage)]
            [Display(Name = "Price")]
            public decimal Price { get; set; }

            [Required(ErrorMessage = DataAnnotationMessage.RequiredErrorMessage)]
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
