using LazZiya.TagHelpers.Alerts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;
using System;

namespace ExpressLocalizationSampleProject.Pages
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }

        public IActionResult OnGetSetCultureCookie(string cltr, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(cltr)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
                );

            return LocalRedirect(returnUrl);
        }
    }
}
