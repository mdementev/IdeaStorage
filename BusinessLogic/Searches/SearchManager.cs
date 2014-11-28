namespace BusinessLogic.Searches
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Sockets;

    using IdeaSorage.DataModel;

    using IdeaStorage.EntriesModel.Entries;

    public sealed class SearchManager
    {
        public List<Node> FindNodesByString(string searchTerm)
        {
            List<Node> result = new List<Node>();
            List<string> searchTermList = searchTerm.Split().ToList();


            using (IdeaStorageEntities context = new IdeaStorageEntities())
            {
                var ress = from r in context.NODES
                    select r;
            }


            for (int a = 0; a < searchTermList.Count; a++)
            {
                
            }
        } 
    }
}
