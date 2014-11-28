namespace IdeaStorage.EntriesModel.Entries
{
    using System.Collections.Generic;

    /// <summary>
    /// The tag model.
    /// </summary>
    public class Tag
    {

        #region Public Properties

        /// <summary>
        /// Gets or sets the name of tag.
        /// </summary>
        /// <value>
        /// The name of tag.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the tag id.
        /// </summary>
        public int TagId { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Represents Tag as string with property values.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("Name:'{0}', TagId:'{1}'", this.Name, this.TagId);
        }

        #endregion
    }
}