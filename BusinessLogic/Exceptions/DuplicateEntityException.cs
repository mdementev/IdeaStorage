namespace BusinessLogic.Exceptions
{
    /// <summary>
    /// Exception occurs if some entity already exists in data base.
    /// </summary>
    public class DuplicateEntityException : System.Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DuplicateEntityException"/> class.
        /// </summary>
        public DuplicateEntityException()
            : base("Some entity already exists in data base.")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DuplicateEntityException"/> class.
        /// </summary>
        /// <param name="excMessage">The exception message.</param>
        public DuplicateEntityException(string excMessage)
            : base(excMessage)
        {
        }
    }
}
