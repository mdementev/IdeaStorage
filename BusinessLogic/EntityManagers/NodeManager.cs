namespace BusinessLogic.EntityManagers
{
    using System.Reflection;

    using BusinessLogic.Mappers;
    using BusinessLogic.Validators;

    using IdeaSorage.DataModel;

    using IdeaStorage.EntriesModel.Entries;

    using log4net;

    public sealed class NodeManager: INodeManager
    {
        /// <summary>
        /// The log.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public Node CreateNode(Node newNode)
        {
            NodeValidator nodeValidator = new NodeValidator();
            nodeValidator.Validate(newNode);

            using (var context = new IdeaStorageEntities())
            {
                NODE newDbNode = new NODE
                {
                    Created = newNode.Created,
                    IsDeleted = newNode.IsDeleted,
                    Modified = newNode.Modified,
                    OwnerId = newNode.OwnerId,
                    Text = newNode.Text,
                    Title = newNode.Title
                };

                TagManager tagManager = new TagManager();
                foreach (Tag tag in newNode.Tags)
                {
                    tag.TagId = tagManager.CreateTag(tag).TagId;
                    context.TAGSETS.Add(new TAGSET { NodeId = newDbNode.NodeId, TagId = tag.TagId });
                }

                context.NODES.Add(newDbNode);
                context.SaveChanges();

                return newDbNode.ToModel();
            }
        }

        public void UpdateNode(Node node)
        {
            throw new System.NotImplementedException();
        }
    }
}
