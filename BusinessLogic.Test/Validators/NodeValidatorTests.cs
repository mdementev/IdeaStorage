namespace BusinessLogic.Test.Validators
{
    using BusinessLogic.Validators;

    using FluentValidation.TestHelper;

    using NUnit.Framework;

    /// <summary>
    /// Contains tests, which verifies <see cref="NodeValidator"/> logic.
    /// </summary>
    [TestFixture]
    public class NodeValidatorTests
    {
        #region Public Methods and Operators

        /// <summary>
        /// Verifies, that <see cref="NodeValidator"/> has validation errors for empty text.
        /// </summary>
        [Test]
        public void ValidateNode_EmptyText_NotValid()
        {
            NodeValidator validator = new NodeValidator();
            validator.ShouldHaveValidationErrorFor(node => node.Text, string.Empty);
        }

        /// <summary>
        /// Verifies, that <see cref="NodeValidator"/> has validation errors for empty title.
        /// </summary>
        [Test]
        public void ValidateNode_EmptyTitle_NotValid()
        {
            NodeValidator validator = new NodeValidator();
            validator.ShouldHaveValidationErrorFor(node => node.Title, string.Empty);
        }

        /// <summary>
        /// Verifies, that <see cref="NodeValidator"/> has validation errors for very long title (> 255 symbols).
        /// </summary>
        [Test]
        public void ValidateNode_LongTitle_NotValid()
        {
            NodeValidator validator = new NodeValidator();

            const string String256Lenght = @"1_____________________________________________________
___________________________________________________________________________________________________
________________________________________________________________________________________________256";

            validator.ShouldHaveValidationErrorFor(node => node.Title, String256Lenght);
        }

        /// <summary>
        /// Verifies, that <see cref="NodeValidator"/> has validation errors for null text.
        /// </summary>
        [Test]
        public void ValidateNode_NullTExt_NotValid()
        {
            NodeValidator validator = new NodeValidator();
            validator.ShouldHaveValidationErrorFor(node => node.Text, null as string);
        }

        /// <summary>
        /// Verifies, that <see cref="NodeValidator"/> has validation errors for null title.
        /// </summary>
        [Test]
        public void ValidateNode_NullTitle_NotValid()
        {
            NodeValidator validator = new NodeValidator();
            validator.ShouldHaveValidationErrorFor(node => node.Title, null as string);
        }

        /// <summary>
        /// Verifies, that <see cref="NodeValidator"/> doesn't have validation errors for valid text.
        /// </summary>
        [Test]
        public void ValidateNode_ValidText_Valid()
        {
            NodeValidator validator = new NodeValidator();
            validator.ShouldNotHaveValidationErrorFor(node => node.Text, "Text");
        }

        /// <summary>
        /// Verifies, that <see cref="NodeValidator"/> doesn't have validation errors for valid title.
        /// </summary>
        [Test]
        public void ValidateNode_ValidTitle_Valid()
        {
            NodeValidator validator = new NodeValidator();
            validator.ShouldNotHaveValidationErrorFor(node => node.Title, "Title");
        }

        #endregion
    }
}