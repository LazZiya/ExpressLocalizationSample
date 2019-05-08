using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System;
using System.Reflection;

namespace LazZiya.Localization
{
    public static class LocalizationExtensions
    {
        public static IMvcBuilder AddDataAnnotationsLocalization(this IMvcBuilder builder, Type cultureResourceType)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (cultureResourceType == null)
            {
                throw new ArgumentNullException(nameof(cultureResourceType));
            }

            builder.AddDataAnnotationsLocalization(o =>
            {
                var assemblyName = new AssemblyName(cultureResourceType.GetTypeInfo().Assembly.FullName);
                var factory = builder.Services.BuildServiceProvider().GetService<IStringLocalizerFactory>();
                var localizer = factory.Create(cultureResourceType.Name, assemblyName.Name);

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
                o.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor((x, y) 
                    => string.Format(ModelBindingMessages.AttemptedValueIsInvalid, x, y));

                o.ModelBindingMessageProvider.SetMissingBindRequiredValueAccessor((x) 
                    => string.Format(ModelBindingMessages.MissingBindRequiredValue, x));

                o.ModelBindingMessageProvider.SetMissingKeyOrValueAccessor(() 
                    => ModelBindingMessages.MissingKeyOrValue);

                o.ModelBindingMessageProvider.SetMissingRequestBodyRequiredValueAccessor(() 
                    => ModelBindingMessages.MissingRequestBodyRequiredValue);

                o.ModelBindingMessageProvider.SetNonPropertyAttemptedValueIsInvalidAccessor((x) 
                    => string.Format(ModelBindingMessages.NonPropertyAttemptedValueIsInvalid, x));

                o.ModelBindingMessageProvider.SetNonPropertyUnknownValueIsInvalidAccessor(() 
                    => ModelBindingMessages.NonPropertyUnknownValueIsInvalid);

                o.ModelBindingMessageProvider.SetNonPropertyValueMustBeANumberAccessor(() 
                    => ModelBindingMessages.NonPropertyValueMustBeANumber);

                o.ModelBindingMessageProvider.SetUnknownValueIsInvalidAccessor((x) 
                    => string.Format(ModelBindingMessages.UnknownValueIsInvalid, x));

                o.ModelBindingMessageProvider.SetValueIsInvalidAccessor((x) 
                    => string.Format(ModelBindingMessages.ValueIsInvalid, x));

                o.ModelBindingMessageProvider.SetValueMustBeANumberAccessor((x) 
                    => string.Format(ModelBindingMessages.ValueMustBeANumber, x));

                o.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor((x) 
                    => string.Format(ModelBindingMessages.ValueMustNotBeNull, x));
            });

            return builder;
        }
    }
}
