using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using LazZiya.ExpressLocalization;
using LazZiya.ExpressLocalization.Messages;
using LazZiya.TagHelpers.Alerts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ExpressLocalizationSampleProject.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ResetPasswordModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SharedCultureLocalizer _loc;

        public ResetPasswordModel(UserManager<IdentityUser> userManager, SharedCultureLocalizer loc)
        {
            _userManager = userManager;
            _loc = loc;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = DataAnnotationsErrorMessages.RequiredAttribute_ValidationError)]
            [EmailAddress]
            public string Email { get; set; }

            [Required(ErrorMessage = DataAnnotationsErrorMessages.RequiredAttribute_ValidationError)]
            [StringLength(100, ErrorMessage = DataAnnotationsErrorMessages.StringLengthAttribute_ValidationErrorIncludingMinimum, MinimumLength = 6)]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = DataAnnotationsErrorMessages.CompareAttribute_MustMatch)]
            public string ConfirmPassword { get; set; }

            public string Code { get; set; }
        }

        public IActionResult OnGet(string code = null)
        {
            if (code == null)
            {
                return BadRequest("A code must be supplied for password reset.");
            }
            else
            {
                Input = new InputModel
                {
                    Code = code
                };
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var culture = CultureInfo.CurrentCulture.Name;

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.FindByEmailAsync(Input.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToPage("./ResetPasswordConfirmation", new { culture });
            }

            var result = await _userManager.ResetPasswordAsync(user, Input.Code, Input.Password);
            if (result.Succeeded)
            {
                return RedirectToPage("./ResetPasswordConfirmation", new { culture });
            }

            foreach (var error in result.Errors)
            {
                //ModelState.AddModelError(string.Empty, error.Description);
                TempData.Danger(_loc.Text(error.Description).Value);
            }
            return Page();
        }
    }
}
