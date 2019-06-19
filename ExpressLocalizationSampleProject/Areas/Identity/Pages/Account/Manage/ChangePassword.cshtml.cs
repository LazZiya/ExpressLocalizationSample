using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using LazZiya.ExpressLocalization.Messages;
using LazZiya.TagHelpers.Alerts;
using ExpressLocalizationSampleProject.LocalizationResources;
using System.Globalization;
using LazZiya.ExpressLocalization;

namespace ExpressLocalizationSampleProject.Areas.Identity.Pages.Account.Manage
{
    public class ChangePasswordModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<ChangePasswordModel> _logger;
        private readonly SharedCultureLocalizer _loc;

        public ChangePasswordModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<ChangePasswordModel> logger,
            SharedCultureLocalizer loc)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _loc = loc;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        //[TempData]
        //public string StatusMessage { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = DataAnnotationsErrorMessages.RequiredAttribute_ValidationError)]
            [DataType(DataType.Password)]
            [Display(Name = "Current password")]
            public string OldPassword { get; set; }

            [Required(ErrorMessage = DataAnnotationsErrorMessages.RequiredAttribute_ValidationError)]
            [StringLength(100, ErrorMessage = DataAnnotationsErrorMessages.StringLengthAttribute_ValidationErrorIncludingMinimum, MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "New password")]
            public string NewPassword { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm new password")]
            [Compare("NewPassword", ErrorMessage = DataAnnotationsErrorMessages.CompareAttribute_MustMatch)]
            public string ConfirmPassword { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var hasPassword = await _userManager.HasPasswordAsync(user);
            if (!hasPassword)
            {
                return RedirectToPage("./SetPassword", new { culture = CultureInfo.CurrentCulture.Name });
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, Input.OldPassword, Input.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                foreach (var error in changePasswordResult.Errors)
                {
                    //ModelState.AddModelError(string.Empty, error.Description);
                    TempData.Danger(_loc.Text(error.Description).Value);
                }
                return Page();
            }

            await _signInManager.RefreshSignInAsync(user);
            _logger.LogInformation("User changed their password successfully.");
            //StatusMessage = "Your password has been changed.";
            TempData.Success(_loc.Text(LocalizedBackendMessages.ChangePasswordSuccess).Value);

            return RedirectToPage();
        }
    }
}
