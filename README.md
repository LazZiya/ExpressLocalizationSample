> ### UPDATE Nov. 2020 : 
> Do you want even more exciting localization experience? If yes, please check [XLocalizer](https://github.com/LazZiya/XLocalizer), a localization nuget with online translation support and auto resource creating.
> * Repo: [XLocalizer](https://github.com/LazZiya/XLocalizer)
> * Samples: [XLocalizer Samples](https://github.com/LazZiya/XLocalizer.Samples)
> * Article: [XLocalizer for Asp.Net Core](http://ziyad.info/en/articles/1040-XLocalizer_for_Asp_Net_Core)

# ExpressLocalization Sample - Asp.Net Core 2.2
Fully localized project, based on the basic Asp.Net Core 2.2 template from Visual Studio, localized using [LazZiya.ExpressLocalization](https://github.com/LazZiya/ExpressLocalization).

# DotNetCore version
This project is based on **dotnetcore2.2**, a newer project based on **dotnetcore3.0** is available here: [ExpressLocalization Sample  for DotNetCore3.0](https://github.com/LazZiya/ExpressLocalizationSampleCore3)

## Features :
 - Custom (route value) RequestCultureProvider
 - Custom IHtmlStringLocalizer
 - Custom [LocalizeTagHelper](https://github.com/LazZiya/ExpressLocalization/wiki/Localize-TagHelper)
 - Validating localized input fields e.g. (12,34 and 12.34)
 - Localization of:
   - Razor Views (All views locailzed with [LocalizeTagHelper](https://github.com/LazZiya/ExpressLocalization/wiki/Localize-TagHelper))
   - DataAnnotations
   - Model binding and model validation error messages
   - IdentityErrorDescriber messages
   - Client side validation error messages

 
## Available Cultures
_some cultures needs fixing/adding few translations_
 - Arabic
 - Chinese
 - Czech
 - Dutch
 - English
 - French (thanks to [@werddomain](https://github.com/werddomain) for fixing)
 - German
 - Hindi
 - Hungarian
 - Italian
 - Japanese
 - Korean
 - Persian
 - Polish
 - Portuguese
 - Portuguese Brazil (thanks to [@ismaelgasparin](https://github.com/ismaelgasparin) for adding)
 - Russian (thanks to [@InfDev](https://github.com/InfDev) for fixing)
 - Spanish
 - Swedish
 - Turkish
 - Ukrainian (thanks to [@InfDev](https://github.com/InfDev) for adding)
 - Vietnamese

## Project site:
http://ziyad.info/en/articles/33-Express_Localization

## Step by step tutorial to build similar project
http://ziyad.info/en/articles/36-Develop_Multi_Cultural_Web_Application_Using_ExpressLocalization

## TagHelpers
Some parts of this project is using [LazZiya.TagHelpers](https://github.com/LazZiya/TagHelpers) like:
 - LanguageNav dropdown
 - Client side validation scripts
 - AlertTagHelper for bootstrap 4 alerts
 - Localization of razor views done with [LocalizeTagHelper](https://github.com/LazZiya/ExpressLocalization/wiki/Localize-TagHelper).

## Contributers
Contributer | Role
--- | ---
 [@LazZiya](https://github.com/LazZiya) | Owner
 [@ismaelgasparin](https://github.com/ismaelgasparin)  | Portuguese language
 
 
## License:
MIT
