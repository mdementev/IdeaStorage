namespace BusinessLogic.Test.EntityManagers
{
    using System.Security.Authentication;

    using BusinessLogic.Authorization;
    using BusinessLogic.Cryptography;
    using BusinessLogic.EntityManagers;
    using BusinessLogic.Exceptions;

    using FluentValidation;

    using IdeaStorage.EntriesModel.Entries;

    using Infrastructure.Testing;

    using NUnit.Framework;

    public class NodeManagerTests : DatabaseTestFixture
    {

        [Test]
        public void CreateNode_ValidNode_Success()
        {
            Node node = new Node
            {
                
            }
            NodeManager s1 = new NodeManager();
            s1.CreateNode()
        }
    }
}
