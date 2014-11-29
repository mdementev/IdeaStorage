namespace IdeaStorage.WebAPI.Models
{
    using IdeaStorage.EntriesModel.Entries;
    public class SearchModel
    {

        public LoginCredentials Credentials { get; set; }

        public string SearchTerm { get; set; }
    }
}