using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Globalization;
using LazZiya.ExpressLocalization.Messages;
using LazZiya.TagHelpers.Alerts;
using LazZiya.ExpressLocalization;
using ExpressLocalizationSampleProject.LocalizationResources;

namespace ExpressLocalizationSampleProject.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private SharedCultureLocalizer _loc;

        private readonly string _culture;

        public LoginModel(SignInManager<IdentityUser> signInManager, ILogger<LoginModel> logger, SharedCultureLocalizer loc)
        {
            _signInManager = signInManager;
            _logger = logger;
            _loc = loc;
            _culture = CultureInfo.CurrentCulture.Name;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        //[TempData]
        //public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = DataAnnotationsErrorMessages.RequiredAttribute_ValidationError)]
            [EmailAddress(ErrorMessage = DataAnnotationsErrorMessages.EmailAddressAttribute_Invalid)]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required(ErrorMessage = DataAnnotationsErrorMessages.RequiredAttribute_ValidationError)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content($"~/{_culture}");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content($"~/{_culture}");

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    return LocalRedirect(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, Culture=_culture, RememberMe = Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout", new { Culture = _culture });
                }
                else
                {
                    TempData.Danger(_loc.Text(LocalizedBackendMessages.LoginInvalidAttempt).Value);
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
