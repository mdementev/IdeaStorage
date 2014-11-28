namespace BusinessLogic.Searches
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Sockets;

    using BusinessLogic.Mappers;

    using IdeaSorage.DataModel;

    using IdeaStorage.EntriesModel.Entries;

    public sealed class SearchManager
    {
        public List<Node> FindNodesByString(string searchTerm)
        {
            List<string> searchTermList = searchTerm.Split().ToList();
            List<NODE> dbNodesList = new List<NODE>();

            using (IdeaStorageEntities context = new IdeaStorageEntities())
            {
                
                foreach (string term in searchTermList)
                {
                    dbNodesList.AddRange(context.NODES.Where(n => n.Text.Contains(term)));
                }

                List<Node> result = (context.NODES.GroupBy(n => n, n => n.NodeId)
                    .Select(g => new { g, count = g.Count() })
                    .OrderByDescending(@t => @t.count)
                    .Select(@t => @t.g.Key.ToModel())).ToList();

                return result;
            }

        } 
    }
}
