using System;
using System.Collections.Generic;
using System.Globalization;

namespace LazZiya.Localization
{
    public class LocalizationOptions
    {
        public LocalizationOptions() { }
        public List<CultureInfo> SupportedCultures { get; set; }
        public List<CultureInfo> SupportedUICultures { get; set; }
        public Type CultureResourceType { get; set; }
        public string DefaultCulture { get; set; }
        public string ResourceFolder { get; set; } = "LocalizationResources";

    }
}
