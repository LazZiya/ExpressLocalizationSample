using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

namespace LazZiya.Localization
{
    public static class ConfigureLocalization
    {
        /// <summary>
        /// localize ModelBinding messages, e.g. when user enters string value instead of number...
        /// these messages can't be localized like data attributes
        /// </summary>
        /// <param name="mvc"></param>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IMvcBuilder AddLocalization(this IMvcBuilder mvc, IServiceCollection services, ILogger _logger, List<CultureInfo> supportedCultures, string defaultCulture, Type cultureResourceType, string resourceFolder = "LocalizationResources")
        {
            // add list of supported languages
            // Configure route value request culture provider
            services.Configure<RequestLocalizationOptions>(ops =>
            {
                ops.DefaultRequestCulture = new RequestCulture(defaultCulture);
                ops.SupportedCultures = supportedCultures;
                ops.SupportedUICultures = supportedCultures;

                ops.RequestCultureProviders.Insert(
                    0,
                    new RouteValueRequestCultureProvider(_logger, supportedCultures, defaultCulture));
            });

            // usage: inject to views in _ViewImports.cshtml
            // think of a way to pass culture resource type as parameter durin injection
            services.AddSingleton<CultureLocalizer>(/*******/);

            return mvc
                // View Localization,
                // "Views" folder under ZPanelCore.Resources.Views library project
                .AddViewLocalization(o => o.ResourcesPath = resourceFolder)

                // Data annotation localization
                .AddDataAnnotationsLocalization(o =>
                {
                    var assemblyName = new AssemblyName(cultureResourceType.GetTypeInfo().Assembly.FullName);
                    var factory = services.BuildServiceProvider().GetService<IStringLocalizerFactory>();
                    var localizer = factory.Create(nameof(cultureResourceType), assemblyName.Name);

                    o.DataAnnotationLocalizerProvider = (t, f) => localizer;
                })

                // ModelBinding Error Messages Localization
                .AddMvcOptions(o =>
                {
                    o.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor((x, y) => string.Format(LocalizedFrameworkMessages.AttemptedValueIsInvalid, x, y));

                    o.ModelBindingMessageProvider.SetMissingBindRequiredValueAccessor((x) => string.Format(LocalizedFrameworkMessages.MissingBindRequiredValue, x));

                    o.ModelBindingMessageProvider.SetMissingKeyOrValueAccessor(() => LocalizedFrameworkMessages.MissingKeyOrValue);

                    o.ModelBindingMessageProvider.SetMissingRequestBodyRequiredValueAccessor(() => LocalizedFrameworkMessages.MissingRequestBodyRequiredValue);

                    o.ModelBindingMessageProvider.SetNonPropertyAttemptedValueIsInvalidAccessor((x) => string.Format(LocalizedFrameworkMessages.NonPropertyAttemptedValueIsInvalid, x));

                    o.ModelBindingMessageProvider.SetNonPropertyUnknownValueIsInvalidAccessor(() => LocalizedFrameworkMessages.NonPropertyUnknownValueIsInvalid);

                    o.ModelBindingMessageProvider.SetNonPropertyValueMustBeANumberAccessor(() => LocalizedFrameworkMessages.NonPropertyValueMustBeANumber);

                    o.ModelBindingMessageProvider.SetUnknownValueIsInvalidAccessor((x) => string.Format(LocalizedFrameworkMessages.UnknownValueIsInvalid, x));

                    o.ModelBindingMessageProvider.SetValueIsInvalidAccessor((x) => string.Format(LocalizedFrameworkMessages.ValueIsInvalid, x));

                    o.ModelBindingMessageProvider.SetValueMustBeANumberAccessor((x) => string.Format(LocalizedFrameworkMessages.ValueMustBeANumber, x));

                    o.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor((x) => string.Format(LocalizedFrameworkMessages.ValueMustNotBeNull, x));

                });
        }
    }
}
