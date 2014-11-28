namespace BusinessLogic.Validators
{
    using FluentValidation;

    using IdeaStorage.EntriesModel.Entries;

    /// <summary>
    /// The validator for <see cref="Node"/> instance.
    /// </summary>
    public class NodeValidator : AbstractValidator<Node>
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NodeValidator"/> class.
        /// </summary>
        public NodeValidator()
        {
            this.RuleFor(node => node.Title).NotNull().Length(1, 255);
            this.RuleFor(node => node.Text).NotNull().NotEmpty();
        }

        #endregion
    }
}