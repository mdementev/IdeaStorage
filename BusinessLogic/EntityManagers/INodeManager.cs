using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.EntityManagers
{
    using IdeaStorage.EntriesModel.Entries;

    public interface INodeManager
    {
        Node CreateNode(Node newNode);
    }
}
