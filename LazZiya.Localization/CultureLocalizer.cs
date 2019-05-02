using Microsoft.Extensions.Localization;
using System;
using System.Reflection;

namespace LazZiya.Localization
{
    /// <summary>
    /// Access shared localization resources under folder
    /// </summary>
    public class CultureLocalizer
    {
        private readonly IStringLocalizer _localizer;

        public CultureLocalizer(IStringLocalizerFactory factory, Type cultureResourceType)
        {
            var assemblyName = new AssemblyName(cultureResourceType.GetTypeInfo().Assembly.FullName);
            _localizer = factory.Create(cultureResourceType.Name, assemblyName.Name);
        }

        public LocalizedString Localize(string key, params string[] arguments)
        {
            return arguments == null
                ? _localizer[key]
                : _localizer[key, arguments];
        }
    }
}
