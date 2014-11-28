namespace IdeaStorage.EntriesModel.Entries
{
    using System.Collections.Generic;

    /// <summary>
    /// The user model.
    /// </summary>
    public class User
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        public User()
        {
            this.Nodes = new List<Node>();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the email value to\from user.
        /// </summary>
        /// <value>
        /// The email string.
        /// </value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the first name of the <see cref="User"/>.
        /// </summary>
        /// <value>
        /// The first name string.
        /// </value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether <see cref="User"/> is deleted.
        /// </summary>
        /// <value>
        ///     True if this instance is deleted; otherwise, false.
        /// </value>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the nodes created by that <see cref="User"/>.
        /// </summary>
        /// <value>
        /// The nodes created by that <see cref="User"/>.
        /// </value>
        public List<Node> Nodes { get; set; }

        /// <summary>
        /// Gets or sets the second name.
        /// </summary>
        /// <value>
        /// The name of the second.
        /// </value>
        public string SecondName { get; set; }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        public int UserId { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Represents User as string with property values.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("UserId:'{0}', Email:'{1}', FirstName:'{2}', IsDeleted:'{3}', SecondName:'{4}'", this.UserId, this.Email, this.FirstName, this.IsDeleted, this.SecondName);
        }

        #endregion
    }
}