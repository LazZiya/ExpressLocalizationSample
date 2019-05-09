namespace LazZiya.Localization.ResourceFiles
{
    public class DataAnnotationsMessages
    {
    }

    public struct DataAnnotationMessage
    {
        public const string RequiredErrorMessage = "'{0}' is required";
        public const string StringLengthErrorMessgae = "The '{0}' must be at least '{2}' and at max '{1}' characters long";
        public const string CompareErrorMessage = "'{0}' must be similar to '{1}'";
        public const string EmailAddressErrorMessage = "Email address is not valid";
        public const string PhoneErrorMessage = "Phone number is invalid";
        public const string RangeErrorMessage = "'{0}' must be between [{1} - {2}]";
        public const string DataTypeMustBeANumber = "Please enter a valid number.";
    }
}
