namespace IdeaStorage.WebAPI.Models
{
    using IdeaStorage.EntriesModel.Entries;

    public class NodeModel
    {
        #region Public Properties

        public LoginCredentials Credentials { get; set; }

        public Node Node { get; set; }

        #endregion
    }
}