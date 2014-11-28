namespace BusinessLogic.EntityManagers
{
    using System;
    using System.Reflection;

    using BusinessLogic.Mappers;
    using BusinessLogic.Validators;

    using IdeaSorage.DataModel;

    using IdeaStorage.EntriesModel.Entries;

    using log4net;

    public sealed class NodeManager
    {
        /// <summary>
        /// The log.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public Node CreateNode(Node newNode)
        {
            using (var context = new IdeaStorageEntities())
            {
                NodeValidator nodeValidator = new NodeValidator();
                nodeValidator.Validate(newNode);


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

                return newDbNode.ToModel();
            }
        }



    }
}
