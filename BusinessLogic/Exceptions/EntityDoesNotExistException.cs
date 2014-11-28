namespace BusinessLogic.Exceptions
{
    /// <summary>
    /// Exception occurs if entity not exists in data base.
    /// </summary>
    public class EntityDoesNotExistException : System.Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityDoesNotExistException"/> class.
        /// </summary>
        public EntityDoesNotExistException()
            : base("Entity does not exists in data base.")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityDoesNotExistException"/> class.
        /// </summary>
        /// <param name="excMessage">The exception message.</param>
        public EntityDoesNotExistException(string excMessage)
            : base(excMessage)
        {
        }
    }
}
