using LazZiya.ExpressLocalization;
using LazZiya.TagHelpers.Alerts;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ExpressLocalizationSampleProject.Pages
{
    public class IndexModel : PageModel
    {
        private readonly SharedCultureLocalizer _loc;

        public IndexModel(SharedCultureLocalizer loc)
        {
            _loc = loc;
        }

        public void OnGet()
        {
            TempData.Primary(_loc.Text("click here").Value);
        }
    }
}
