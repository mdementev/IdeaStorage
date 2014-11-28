namespace BusinessLogic.EntityManagers
{
    using System;
    using System.Reflection;

    using IdeaSorage.DataModel;

    using IdeaStorage.EntriesModel.Entries;

    using log4net;

    public sealed class NodeManager
    {
        /// <summary>
        /// The log.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public int CreateNode(Node newNode)
        {
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

                context.NODES.Add(newDbNode);
                context.SaveChanges();

                return newDbNode.NodeId;
            }
        }



    }
}
