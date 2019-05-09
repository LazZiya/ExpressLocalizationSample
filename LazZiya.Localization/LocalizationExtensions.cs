using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System;
using System.Reflection;
using LazZiya.Localization.ResourceFiles;
using System.Globalization;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;

namespace LazZiya.Localization
{
    public static class LocalizationExtensions
    {
        public static IMvcBuilder ExpressLocalization(this IMvcBuilder builder)
        {
            var supportedCultures = new List<CultureInfo>
            {
                new CultureInfo("en"),
                new CultureInfo("tr"),
                new CultureInfo("ar"){ DateTimeFormat = {Calendar = new GregorianCalendar() }}
            };

            builder.Services.Configure<RequestLocalizationOptions>(ops =>
            {
                ops.SupportedCultures = supportedCultures;
                ops.SupportedUICultures = supportedCultures;
                ops.DefaultRequestCulture = new RequestCulture("en");
                ops.RequestCultureProviders.Insert(0, new RouteValueRequestCultureProvider(supportedCultures, "en"));
            });

            builder.Services.AddSingleton<CultureLocalizer>();

            builder
                .AddRazorPagesOptions(ops => ops.Conventions.Add(new GlobalTemplatePageRouteModelConvention()))
                .AddViewLocalization(ops => { ops.ResourcesPath = "ResourceFiles"; })
                .AddModelBindingLocalization()
                .AddCustomDataAnnotationsLocalization();

            return builder;
        }

        public static IMvcBuilder AddCustomDataAnnotationsLocalization(this IMvcBuilder builder, Type type = null)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            var _type = type ?? typeof(DataAnnotationsMessages);

            builder.AddDataAnnotationsLocalization(o =>
            {
                var assemblyName = new AssemblyName(_type.GetTypeInfo().Assembly.FullName);
                var factory = builder.Services.BuildServiceProvider().GetService<IStringLocalizerFactory>();
                var localizer = factory.Create(_type.Name, assemblyName.Name);

                o.DataAnnotationLocalizerProvider = (t, f) => localizer;
            });

            return builder;
        }

        public static IMvcBuilder AddModelBindingLocalization(this IMvcBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            // ModelBinding Error Messages Localization
            builder.AddMvcOptions(o =>
            {
                o.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor((x, y) => string.Format(ModelBindingResource.AttemptedValueIsInvalid, x, y));

                o.ModelBindingMessageProvider.SetMissingBindRequiredValueAccessor((x) => string.Format(ModelBindingResource.MissingBindRequiredValue, x));

                o.ModelBindingMessageProvider.SetMissingKeyOrValueAccessor(() => ModelBindingResource.MissingKeyOrValue);

                o.ModelBindingMessageProvider.SetMissingRequestBodyRequiredValueAccessor(() => ModelBindingResource.MissingRequestBodyRequiredValue);

                o.ModelBindingMessageProvider.SetNonPropertyAttemptedValueIsInvalidAccessor((x) => string.Format(ModelBindingResource.NonPropertyAttemptedValueIsInvalid, x));

                o.ModelBindingMessageProvider.SetNonPropertyUnknownValueIsInvalidAccessor(() => ModelBindingResource.NonPropertyUnknownValueIsInvalid);

                o.ModelBindingMessageProvider.SetNonPropertyValueMustBeANumberAccessor(() => ModelBindingResource.NonPropertyValueMustBeANumber);

                o.ModelBindingMessageProvider.SetUnknownValueIsInvalidAccessor((x) => string.Format(ModelBindingResource.UnknownValueIsInvalid, x));

                o.ModelBindingMessageProvider.SetValueIsInvalidAccessor((x) => string.Format(ModelBindingResource.ValueIsInvalid, x));

                o.ModelBindingMessageProvider.SetValueMustBeANumberAccessor((x) => string.Format(ModelBindingResource.ValueMustBeANumber, x));

                o.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor((x) => string.Format(ModelBindingResource.ValueMustNotBeNull, x));
            });

            return builder;
        }
    }
}
