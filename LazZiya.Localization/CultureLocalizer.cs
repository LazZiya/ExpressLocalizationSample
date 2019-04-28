using LazZiya.Localization.LocalizationResources;
using Microsoft.Extensions.Localization;
using System;
using System.Reflection;

namespace LazZiya.Localization
{
    /// <summary>
    /// Access shared localization resources under folder /Resources/CultureResource.xx.resx
    /// </summary>
    public class CultureLocalizer
    {
        //private readonly IStringLocalizer _localizer;

        public CultureLocalizer(IStringLocalizerFactory factory)
        {
            var type = typeof(CultureResource); // think of a way to pass culture resource type as parameter durin injection
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
            _localizer = factory.Create("CultureResource", assemblyName.Name);
        }

        public LocalizedString _(string key, params string[] arguments)
        {
            return arguments == null
                ? _localizer[key]
                : _localizer[key, arguments];
        }
    }
}
