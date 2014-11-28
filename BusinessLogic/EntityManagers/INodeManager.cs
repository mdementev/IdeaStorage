namespace BusinessLogic.EntityManagers
{
    using IdeaStorage.EntriesModel.Entries;

    public interface INodeManager
    {
        #region Public Methods and Operators

        Node CreateNode(Node newNode);

        void UpdateNode(Node node);

        #endregion
    }
}