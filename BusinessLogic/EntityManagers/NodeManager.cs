namespace BusinessLogic.EntityManagers
{
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Reflection;

    using BusinessLogic.Exceptions;
    using BusinessLogic.Mappers;
    using BusinessLogic.Validators;

    using IdeaSorage.DataModel;

    using IdeaStorage.EntriesModel.Entries;

    using log4net;

    public sealed class NodeManager : INodeManager
    {
        #region Constants and Fields

        /// <summary>
        /// The log.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        #region Public Methods and Operators

        public Node CreateNode(Node newNode)
        {
            NodeValidator nodeValidator = new NodeValidator();
            nodeValidator.Validate(newNode);

            using (IdeaStorageEntities context = new IdeaStorageEntities())
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

        public void DeleteNode(int id)
        {
            Log.DebugFormat("Begin DeleteNode(id:'{0}')", id);

            using (IdeaStorageEntities context = new IdeaStorageEntities())
            {
                NODE node = context.NODES.SingleOrDefault(u => u.NodeId == id);

                if (node == null)
                {
                    string message = string.Format("Node with id:'{0}' doesn't exist in data base.", id);
                    Log.Debug(message);
                    throw new EntityDoesNotExistException(message);
                }

                node.IsDeleted = true;
                context.SaveChanges();
            }

            Log.DebugFormat("Node with ID:'{0}' deleted", id);
        }

        public void UpdateNode(Node node)
        {
            NodeValidator nodeValidator = new NodeValidator();
            nodeValidator.Validate(node);

            using (IdeaStorageEntities context = new IdeaStorageEntities())
            {
                NODE dbNode = context.NODES.FirstOrDefault(n => n.NodeId == node.NodeId);

                if (dbNode == null)
                {
                    string message = string.Format("Node with ID:'{0}' doesn't exist in data base.", node.NodeId);
                    Log.Debug(message);
                    throw new EntityDoesNotExistException(message);
                }

                dbNode.Modified = node.Modified;
                dbNode.Text = node.Text;
                dbNode.Title = node.Title;

                List<TAGSET> dbDeleteTagsets = context.TAGSETS.Where(ts => ts.NodeId == node.NodeId).ToList();
                context.TAGSETS.RemoveRange(dbDeleteTagsets);

                TagManager tagManager = new TagManager();
                foreach (Tag tag in node.Tags)
                {
                    tag.TagId = tagManager.CreateTag(tag).TagId;
                    context.TAGSETS.Add(new TAGSET { NodeId = dbNode.NodeId, TagId = tag.TagId });
                }

                context.NODES.AddOrUpdate(dbNode);
                context.SaveChanges();
            }
        }

        #endregion
    }
}