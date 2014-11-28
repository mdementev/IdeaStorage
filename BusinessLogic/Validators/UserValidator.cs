namespace BusinessLogic.Validators
{
    using FluentValidation;

    using IdeaStorage.EntriesModel.Entries;

    /// <summary>
    /// The validator for <see cref="User"/> instance.
    /// </summary>
    public class UserValidator : AbstractValidator<User>
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UserValidator"/> class.
        /// </summary>
        public UserValidator()
        {
            this.RuleFor(user => user.Email).NotNull().Length(1, 100).EmailAddress();

            this.RuleFor(user => user.FirstName).NotNull().Length(1, 100);
            this.RuleFor(user => user.SecondName).NotNull().Length(1, 100);

            this.RuleFor(user => user.Nodes).NotNull().SetCollectionValidator(new NodeValidator());
        }

        #endregion
    }
}