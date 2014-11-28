namespace BusinessLogic.Searches
{
    using System.Collections.Generic;
    using System.Linq;

    using BusinessLogic.Mappers;

    using IdeaSorage.DataModel;

    using IdeaStorage.EntriesModel.Entries;

    public sealed class SearchManager
    {
        public List<Node> FindNodesByString(string searchTerm, bool caseSensitive)
        {
            List<string> searchTermList = searchTerm.Split().ToList();
            List<NODE> dbNodesList = new List<NODE>();

            using (IdeaStorageEntities context = new IdeaStorageEntities())
            {

                foreach (string term in searchTermList)
                {
                    dbNodesList.AddRange(
                        caseSensitive
                            ? context.NODES.Where(n => n.Text.Contains(term))
                            : context.NODES.Where(n => n.Text.ToLower().Contains(term.ToLower())));
                }

                List<NODE> dbResult = (dbNodesList.GroupBy(n => n, n => n.NodeId)
                    .Select(g => new { g, count = g.Count() })
                    .OrderByDescending(@t => @t.count)
                    .Select(@t => @t.g.Key)).ToList();

                List<Node> result = dbResult.Select(n => n.ToModel()).ToList();

                return result;
            }

        } 
    }
}
