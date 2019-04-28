using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace LazZiya.Localization
{
    public class RouteValueRequestCultureProvider : IRequestCultureProvider
    {
        private readonly ILogger _logger;
        private List<CultureInfo> SupportedCultures { get; set; }

        private string DefaultCulture { get; set; }

        public RouteValueRequestCultureProvider(ILogger logger, List<CultureInfo> supportedCultures, string defaultCulture)
        {
            _logger = logger;
            SupportedCultures = supportedCultures;
            DefaultCulture = defaultCulture;
        }

        public Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
        {
            var path = httpContext.Request.Path;

            if (string.IsNullOrWhiteSpace(path))
            {
                _logger.LogInformation($"Path is empty! returning default culture ({DefaultCulture}).");
                return Task.FromResult(new ProviderCultureResult(DefaultCulture));
            }

            var routeValues = httpContext.Request.Path.Value.Split('/');
            if (routeValues.Count() <= 1)
            {
                _logger.LogInformation($"No path parameter detected! returning default culture ({DefaultCulture}).");
                return Task.FromResult(new ProviderCultureResult(DefaultCulture));
            }

            if (!SupportedCultures.Any(x =>
                 x.TwoLetterISOLanguageName.ToLower() == routeValues[1].ToLower() ||
                 x.Name.ToLower() == routeValues[1].ToLower()))
            {
                _logger.LogInformation($"Path culture ({routeValues[1]}) not ercognized! returning default culture ({DefaultCulture}).");
                return Task.FromResult(new ProviderCultureResult(DefaultCulture));
            }

            _logger.LogInformation($"Selected culture ({routeValues[1]})");
            return Task.FromResult(new ProviderCultureResult(routeValues[1]));
        }
    }
}
