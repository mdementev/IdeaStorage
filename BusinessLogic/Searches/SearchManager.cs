namespace BusinessLogic.Searches
{
    using System.Collections.Generic;
    using System.Linq;

    using BusinessLogic.Mappers;

    using FuzzyString;

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

        public List<Node> FindNodesByTags(string searchTerm, bool caseSens = false)
        {
            List<FuzzyStringComparisonOptions> options = new List<FuzzyStringComparisonOptions>();

            // Choose which algorithms should weigh in for the comparison
            options.Add(FuzzyStringComparisonOptions.UseOverlapCoefficient);
            options.Add(FuzzyStringComparisonOptions.UseLongestCommonSubsequence);
            options.Add(FuzzyStringComparisonOptions.UseLongestCommonSubstring);
            options.Add(FuzzyStringComparisonOptions.UseLevenshteinDistance);

            // Choose the relative strength of the comparison - is it almost exactly equal? or is it just close?
            FuzzyStringComparisonTolerance tolerance = FuzzyStringComparisonTolerance.Normal;

            List<string> searchTermList = searchTerm.Split(',').Select(a => a.Trim()).ToList();

            List<TAG> searchTagsList = new List<TAG>();
            List<NODE> dbNodesList = new List<NODE>();
            using (IdeaStorageEntities context = new IdeaStorageEntities())
            {
                List<TAG> allTagList = context.TAGS.Select(t => t).ToList();
                foreach (string tagString in searchTermList)
                {
                    searchTagsList.AddRange(
                        allTagList.Where(t => t.Name.ApproximatelyEquals(tagString, options, tolerance)));
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
