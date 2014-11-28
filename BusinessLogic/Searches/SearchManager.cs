namespace BusinessLogic.Searches
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using BusinessLogic.Mappers;

    using IdeaSorage.DataModel;

    using IdeaStorage.EntriesModel.Entries;

    public sealed class SearchManager
    {
        public List<Node> FindNodesByValue(string searchTerm, bool caseSensitive = false)
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

        public List<Node> FindNodesByTags(string searchTerm, bool caseSensitive = false)
        {
            List<string> searchTermList = searchTerm.Split(',').Select(a=> a.Trim()).ToList();
            
            List<TAG> searchTagsList = new List<TAG>();
            List<NODE> dbNodesList = new List<NODE>();

            using (IdeaStorageEntities context = new IdeaStorageEntities())
            {

                foreach (string tagString in searchTermList)
                {
                    searchTagsList.AddRange(context.TAGS.Where(t => t.Name.ToLower().Equals(tagString.ToLower())).ToList());
                }

                List<TAGSET> dbTagSets = new List<TAGSET>();
                foreach (TAG tag in searchTagsList)
                {
                    dbTagSets.AddRange(context.TAGSETS.Where(ts => ts.TagId == tag.TagId).ToList());
                }

                foreach (TAGSET dbTagSet in dbTagSets)
                {
                    dbNodesList.Add(context.NODES.Single(n => n.NodeId == dbTagSet.NodeId));
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
