namespace BusinessLogic.Test.Validators
{
    using BusinessLogic.Validators;

    using FluentValidation.TestHelper;

    using NUnit.Framework;

    /// <summary>
    /// Contains tests, which verifies <see cref="UserValidator"/> logic.
    /// </summary>
    [TestFixture]
    public class UserValidatorTests
    {
        #region Public Methods and Operators

        /// <summary>
        /// Verifies, that <see cref="UserValidator"/> has validation errors for empty email.
        /// </summary>
        [Test]
        public void ValidateUser_EmptyEmail_NotValid()
        {
            UserValidator validator = new UserValidator();
            validator.ShouldHaveValidationErrorFor(user => user.Email, string.Empty);
        }

        /// <summary>
        /// Verifies, that <see cref="UserValidator"/> has validation errors for empty first name.
        /// </summary>
        [Test]
        public void ValidateUser_EmptyFirstName_NotValid()
        {
            UserValidator validator = new UserValidator();
            validator.ShouldHaveValidationErrorFor(user => user.FirstName, string.Empty);
        }

        /// <summary>
        /// Verifies, that <see cref="UserValidator"/> has validation errors for empty second name.
        /// </summary>
        [Test]
        public void ValidateUser_EmptySecondName_NotValid()
        {
            UserValidator validator = new UserValidator();
            validator.ShouldHaveValidationErrorFor(user => user.SecondName, string.Empty);
        }

        /// <summary>
        /// Verifies, that <see cref="UserValidator"/> has validation errors for very long email (> 100 symbols).
        /// </summary>
        [Test]
        public void ValidateUser_LongEmail_NotValid()
        {
            UserValidator validator = new UserValidator();
            validator.ShouldHaveValidationErrorFor(
                user => user.Email, 
                "ThisVeryLongEmailContainsMoreThanAllowedSymbols@AndWeDoNotWantToWorkWithSuchUserAsItReallyNotGoodToday");
        }

        /// <summary>
        /// Verifies, that <see cref="UserValidator"/> has validation errors for very long first name (> 100 symbols).
        /// </summary>
        [Test]
        public void ValidateUser_LongFirstName_NotValid()
        {
            UserValidator validator = new UserValidator();
            validator.ShouldHaveValidationErrorFor(
                user => user.FirstName, 
                "ThisVeryLongFirstNameContainsMoreThanAllowedSymbolsAndWeDoNotWantToWorkWithSuchUserAsItReallyNotGoodToday");
        }

        /// <summary>
        /// Verifies, that <see cref="UserValidator"/> has validation errors for very long second name (> 100 symbols).
        /// </summary>
        [Test]
        public void ValidateUser_LongSecondName_NotValid()
        {
            UserValidator validator = new UserValidator();
            validator.ShouldHaveValidationErrorFor(
                user => user.SecondName, 
                "ThisVeryLongSecondNameContainsMoreThanAllowedSymbolsAndWeDoNotWantToWorkWithSuchUserAsItReallyNotGoodToday");
        }

        /// <summary>
        /// Verifies, that <see cref="UserValidator"/> has validation errors when 'not-email-formatted' string is used.
        /// </summary>
        [Test]
        public void ValidateUser_NotEmailFormattedText_NotValid()
        {
            UserValidator validator = new UserValidator();
            validator.ShouldHaveValidationErrorFor(user => user.Email, "thisIsNoEmailFormat");
        }

        /// <summary>
        /// Verifies, that <see cref="UserValidator"/> has validation errors for null email.
        /// </summary>
        [Test]
        public void ValidateUser_NullEmail_NotValid()
        {
            UserValidator validator = new UserValidator();
            validator.ShouldHaveValidationErrorFor(user => user.Email, null as string);
        }

        /// <summary>
        /// Verifies, that <see cref="UserValidator"/> has validation errors for null first name.
        /// </summary>
        [Test]
        public void ValidateUser_NullFirstName_NotValid()
        {
            UserValidator validator = new UserValidator();
            validator.ShouldHaveValidationErrorFor(user => user.FirstName, null as string);
        }

        /// <summary>
        /// Verifies, that <see cref="UserValidator"/> has validation errors for null second name.
        /// </summary>
        [Test]
        public void ValidateUser_NullSecondName_NotValid()
        {
            UserValidator validator = new UserValidator();
            validator.ShouldHaveValidationErrorFor(user => user.SecondName, null as string);
        }

        /// <summary>
        /// Verifies, that <see cref="UserValidator"/> doesn't have validation errors for valid email.
        /// </summary>
        [Test]
        public void ValidateUser_ValidEmail_Valid()
        {
            UserValidator validator = new UserValidator();
            validator.ShouldNotHaveValidationErrorFor(user => user.Email, "name@address.com");
        }

        /// <summary>
        /// Verifies, that <see cref="UserValidator"/> doesn't have validation errors for valid first name.
        /// </summary>
        [Test]
        public void ValidateUser_ValidFirstName_Valid()
        {
            UserValidator validator = new UserValidator();
            validator.ShouldNotHaveValidationErrorFor(user => user.FirstName, "Ivan");
        }

        /// <summary>
        /// Verifies, that <see cref="UserValidator"/> doesn't have validation errors for valid second name.
        /// </summary>
        [Test]
        public void ValidateUser_ValidSecondName_Valid()
        {
            UserValidator validator = new UserValidator();
            validator.ShouldNotHaveValidationErrorFor(user => user.SecondName, "Smirnov");
        }

        #endregion
    }
}