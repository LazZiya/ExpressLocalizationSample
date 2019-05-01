using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System;
using System.Reflection;

namespace LazZiya.Localization
{
    internal static class MvcBuilderExtensions
    {
        internal static IMvcBuilder ConfigureRequestLocalizationOptions(this IMvcBuilder builder, LocalizationOptions options)
        {
            var _defCulture = options.DefaultCulture ?? options.SupportedCultures[0].Name;

            builder.Services.Configure<RequestLocalizationOptions>(ops =>
            {
                ops.DefaultRequestCulture = new RequestCulture(_defCulture);
                ops.SupportedCultures = options.SupportedCultures;
                ops.SupportedUICultures = options.SupportedUICultures ?? options.SupportedCultures;

                ops.RequestCultureProviders.Insert(
                    0,
                    new RouteValueRequestCultureProvider(options.SupportedCultures, _defCulture));
            });

            return builder;
        }

        internal static IMvcBuilder ConfigureDataAnnotationLocalization(this IMvcBuilder builder, Type resourceType)
        {
            // Data annotation localization
            builder.AddDataAnnotationsLocalization(o =>
             {
                 var assemblyName = new AssemblyName(resourceType.GetTypeInfo().Assembly.FullName);
                 var factory = builder.Services.BuildServiceProvider().GetService<IStringLocalizerFactory>();
                 var localizer = factory.Create(resourceType.Name, assemblyName.Name);

                 o.DataAnnotationLocalizerProvider = (t, f) => localizer;
             });

            return builder;
        }

        internal static IMvcBuilder ConfigureModelBindingMessages(this IMvcBuilder builder)
        {
            // ModelBinding Error Messages Localization
            builder.AddMvcOptions(o =>
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

            return builder;
        }
    }
}
