namespace LazZiya.Localization
{
    /// <summary>
    /// provides access to DataAttributes resources via strongly typed string names 
    /// better than using string texts in c# code pages
    /// </summary>
    public struct LocalizedFrameworkMessages
    {
        #region DataAnnotations [Required, StringLength, Range, ...etc.)
        public const string RequiredErrorMessage = "'{0}' is required";
        public const string StringLengthErrorMessgae = "The '{0}' must be at least '{2}' and at max '{1}' characters long";
        public const string CompareErrorMessage = "'{0}' must be similar to '{1}'";
        public const string EmailAddressErrorMessage = "Email address is not valid";
        public const string PhoneErrorMessage = "Phone number is invalid";
        public const string RangeErrorMessage = "'{0}' must be between [{1} - {2}]";
        public const string DataTypeMustBeANumber = "Please enter a valid number.";
        #endregion

        #region Identity error messages
        public const string DuplicateEmail = "Email '{0}' already exists";
        public const string DuplicateUserName = "User name '{0}' already exists";
        public const string InvalidEmail = "Email '{0} is invalid";
        public const string DuplicateRoleName = "Role name '{0}' already exists";
        public const string InvalidRoleName = "Role name '{0}' is invalid";
        public const string InvalidToken = "Invalid token";
        public const string InvalidUserName = "User name '{0}' is invalid";
        public const string LoginAlreadyAssociated = "An external login is already associated with this account";
        public const string PasswordMismatch = "The provided password dosen't match your saved password";
        public const string PasswordRequiresDigit = "The password must contain digits (0 - 9)";
        public const string PasswordRequiresLower = "PasswordRequiresLower";
        public const string PasswordRequiresNonAlphanumeric = "The password must contain non-alphanumeric characters (! + . - ? ...)";
        public const string PasswordRequiresUniqueChars = "The password must contain '{0}' unique chracters";
        public const string PasswordRequiresUpper = "The password must contain upper case letters (A - Z)";
        public const string PasswordTooShort = "The password must be at least '{0}' character long";
        public const string UserAlreadyHasPassword = "User already has a password";
        public const string UserAlreadyInRole = "The user is already in '{0}' role";
        public const string UserNotInRole = "User is not in '{0}' role";
        public const string UserLockoutNotEnabled = "User lockout is not enabled";
        public const string RecoveryCodeRedemptionFailed = "Recovery code was not redeemed";
        public const string ConcurrencyFailure = "Concurrency failure";
        public const string DefaultIdentityError = "Identity system error";
        #endregion

        #region ModelBinding
        public const string AttemptedValueIsInvalid = "The value '{0}' is not valid for '{1}'";
        public const string MissingBindRequiredValue = "A value for the '{0}' property was not provided";
        public const string MissingKeyOrValue = "A value is required";
        public const string MissingRequestBodyRequiredValue = "A non-empty request body is required";
        public const string NonPropertyAttemptedValueIsInvalid = "The value '{0}' is not valid";
        public const string NonPropertyUnknownValueIsInvalid = "The supplied value is invalid";
        public const string NonPropertyValueMustBeANumber = "The field must be a number";
        public const string UnknownValueIsInvalid = "The supplied value is invalid for {0}";
        public const string ValueIsInvalid = "The value '{0}' is invalid";
        public const string ValueMustBeANumber = "The field '{0}' must be a number";
        public const string ValueMustNotBeNull = "The field '{0}' must not be null";
        #endregion
    }
}
