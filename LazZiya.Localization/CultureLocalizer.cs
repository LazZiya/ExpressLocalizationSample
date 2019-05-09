using LazZiya.Localization.ResourceFiles;
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

        public CultureLocalizer(IStringLocalizerFactory factory, Type type = null)
        {
            var _type = type ?? typeof(ViewLocalizationResource);

            var assemblyName = new AssemblyName(_type.GetTypeInfo().Assembly.FullName);
            _localizer = factory.Create(_type.Name, assemblyName.Name);
        }

        public LocalizedString Text(string key, params string[] arguments)
        {
            return arguments == null
                ? _localizer[key]
                : _localizer[key, arguments];
        }
    }
}
