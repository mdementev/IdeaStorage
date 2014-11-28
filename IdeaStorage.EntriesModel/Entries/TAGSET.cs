namespace IdeaStorage.EntriesModel.Entries
{
    /// <summary>
    /// The tagset model which bind tag to nodes..
    /// </summary>
    public class TagSet
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the node which binded tag to tag.
        /// </summary>
        /// <value>
        /// The node which binded tag to tag.
        /// </value>
        public Node Node { get; set; }

        /// <summary>
        /// Gets or sets the node id.
        /// </summary>
        public int NodeId { get; set; }

        /// <summary>
        /// Gets or sets the tag which binded to node.
        /// </summary>
        /// <value>
        /// The tag which binded to node.
        /// </value>
        public virtual Tag Tag { get; set; }

        /// <summary>
        /// Gets or sets the tag id which binded to node..
        /// </summary>
        public int TagId { get; set; }

        /// <summary>
        /// Gets or sets the tag set id.
        /// </summary>
        public int TagSetId { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Represents TagSet as string with property values.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("NodeId:'{0}', TagId:'{1}', TagSetId:'{2}'", this.NodeId, this.TagId, this.TagSetId);
        }

        #endregion
    }
}