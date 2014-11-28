namespace IdeaStorage.EntriesModel.Entries
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The node model.
    /// </summary>
    public class Node
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Node"/> class.
        /// </summary>
        public Node()
        {
            this.TagSets = new List<TagSet>();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the value which indicated when a node was created.
        /// </summary>
        /// <value>
        /// The Date and Time when node was created.
        /// </value>
        public DateTime Created { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is deleted.
        /// </summary>
        /// <value>
        ///     True if this instance is deleted; otherwise, false.
        /// </value>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the value which indicated when a node was modified.
        /// </summary>
        /// <value>
        /// The modified.
        /// </value>
        public DateTime Modified { get; set; }

        /// <summary>
        /// Gets or sets the node id.
        /// </summary>
        public int NodeId { get; set; }

        /// <summary>
        /// Gets or sets the id of user, who create that node.
        /// </summary>
        public int OwnerId { get; set; }

        /// <summary>
        /// Gets or sets the tag sets which bind note to tags.
        /// </summary>
        /// <value>
        /// The tag sets which bind note to tags.
        /// </value>
        public List<TagSet> TagSets { get; set; }

        /// <summary>
        /// Gets or sets the node text.
        /// </summary>
        /// <value>
        /// The node text.
        /// </value>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the title of node.
        /// </summary>
        /// <value>
        /// The title of node.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the user who create that node.
        /// </summary>
        /// <value>
        /// the user who create that node.
        /// </value>
        public User User { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Represents Node as string with property values.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("NodeId:'{0}', OwnerId:'{1}', Text:'{2}', Title:'{3}', Created:'{4}', Modified:'{5}'", this.NodeId, this.OwnerId, this.Text, this.Title, this.Created, this.Modified);
        }

        #endregion
    }
}