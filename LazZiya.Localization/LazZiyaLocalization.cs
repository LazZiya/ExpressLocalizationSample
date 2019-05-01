using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System;

namespace LazZiya.Localization
{
    public static class LazZiyaLocalization
    {
        public static IMvcBuilder AddLocalization(this IMvcBuilder builder, LocalizationOptions localizationOptions)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (localizationOptions == null)
            {
                throw new ArgumentNullException(nameof(localizationOptions));
            }

            if (localizationOptions.SupportedCultures == null || localizationOptions.SupportedCultures.Count == 0)
            {
                throw new ArgumentNullException(nameof(localizationOptions.SupportedCultures));
            }

            if (localizationOptions.CultureResourceType == null)
            {
                throw new ArgumentNullException(nameof(localizationOptions.CultureResourceType));
            }

            // usage: inject to views in _ViewImports.cshtml
            builder.Services.AddSingleton<CultureLocalizer>(x => new CultureLocalizer(
                    x.GetRequiredService<IStringLocalizerFactory>(),
                    localizationOptions.CultureResourceType));

            builder
                .ConfigureRequestLocalizationOptions(localizationOptions)
                
                .AddViewLocalization(o => o.ResourcesPath = localizationOptions.ResourceFolder)

                .ConfigureDataAnnotationLocalization(localizationOptions.CultureResourceType)

                .ConfigureModelBindingMessages()
                
                .AddRazorPagesOptions(ops => ops.Conventions.Add(new GlobalTemplatePageRouteModelConvention()));

            return builder;
        }
    }
}
