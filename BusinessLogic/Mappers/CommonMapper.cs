namespace BusinessLogic.Mappers
{
    using IdeaSorage.DataModel;

    using IdeaStorage.EntriesModel.Entries;

    /// <summary>
    /// The common mapper.
    /// </summary>
    internal static class CommonMapper
    {
        #region Public Methods and Operators

        /// <summary>
        /// The NODE to model.
        /// </summary>
        /// <param name="node">
        /// The node DataModel.
        /// </param>
        /// <returns>
        /// The <see cref="Node"/>.
        /// </returns>
        public static Node ToModel(this NODE node)
        {
            if (node == null)
            {
                return null;
            }

            Node model = new Node
                             {
                                 Created = node.Created, 
                                 IsDeleted = node.IsDeleted, 
                                 Modified = node.Modified, 
                                 NodeId = node.NodeId, 
                                 OwnerId = node.OwnerId, 
                                 Text = node.Text, 
                                 Title = node.Title, 
                                 User = node.USER.ToModel()
                             };

            foreach (TAGSET tagset in node.TAGSETS)
            {
                //TODO: should be updated
                //model.Tags.Add(TagManager.GetTegById(tagset.TagId));
            }

            return model;
        }

        /// <summary>
        /// The TAG to model.
        /// </summary>
        /// <param name="tag">
        /// The tag DataModel.
        /// </param>
        /// <returns>
        /// The <see cref="Tag"/>.
        /// </returns>
        public static Tag ToModel(this TAG tag)
        {
            if (tag == null)
            {
                return null;
            }

            Tag model = new Tag { TagId = tag.TagId, Name = tag.Name };

            return model;
        }

        /// <summary>
        /// The TAGSET to model.
        /// </summary>
        /// <param name="tagset">
        /// The tagset DataModel.
        /// </param>
        /// <returns>
        /// The <see cref="TagSet"/>.
        /// </returns>
        public static TagSet ToModel(this TAGSET tagset)
        {
            if (tagset == null)
            {
                return null;
            }

            TagSet model = new TagSet
                               {
                                   TagId = tagset.TagId, 
                                   NodeId = tagset.NodeId, 
                                   TagSetId = tagset.TagSetId, 
                                   Node = tagset.NODE.ToModel(), 
                                   Tag = tagset.TAG.ToModel()
                               };
            return model;
        }

        /// <summary>
        /// The USER to model.
        /// </summary>
        /// <param name="user">
        /// The user DataModel.
        /// </param>
        /// <returns>
        /// The <see cref="User"/>.
        /// </returns>
        public static User ToModel(this USER user)
        {
            if (user == null)
            {
                return null;
            }

            User model = new User
                             {
                                 UserId = user.UserId, 
                                 Email = user.Email, 
                                 FirstName = user.FirstName, 
                                 IsDeleted = user.IsDeleted, 
                                 SecondName = user.SecondName
                             };
            foreach (NODE node in user.NODES)
            {
                model.Nodes.Add(node.ToModel());
            }

            return model;
        }

        #endregion
    }
}