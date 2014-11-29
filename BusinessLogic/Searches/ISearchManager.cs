using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Searches
{
    using IdeaStorage.EntriesModel.Entries;

    public interface ISearchManager
    {
        List<Node> FindNodesByValue(string searchTerm, bool caseSensitive = false);

        List<Node> FindNodesByTags(string searchTerm, bool caseSens = false);

    }
}
