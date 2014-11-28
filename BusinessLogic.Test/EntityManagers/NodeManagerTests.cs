namespace BusinessLogic.Test.EntityManagers
{
    using System;
    using System.Collections.Generic;

    using BusinessLogic.EntityManagers;

    using IdeaStorage.EntriesModel.Entries;

    using Infrastructure.Testing;

    using NUnit.Framework;

    public class NodeManagerTests : DatabaseTestFixture
    {
        [Test]
        public void CreateNode_ValidNode_Success()
        {
            User user = new User
            {
                Email = "user@mail.com",
                FirstName = "First",
                SecondName = "Second"
            };

            const string Password = "qwerty";

            IUserManager userManager = new UserManager();
            int userId = userManager.CreateUser(user, Password);

            Node node = new Node
            {
                Created = DateTime.Now,
                IsDeleted = false,
                Modified = DateTime.Now,
                OwnerId = userId,
                Text = "text",
                Title = "title",
                Tags = new List<Tag>{ new Tag { Name = "Sample tag"} }
            };
            NodeManager s1 = new NodeManager();

            s1.CreateNode(node);

            Node node2 = new Node
            {
                Created = DateTime.Now,
                IsDeleted = false,
                Modified = DateTime.Now,
                OwnerId = userId,
                Text = "text2",
                Title = "title2",
                Tags = new List<Tag> { new Tag { Name = "Sample tag" }, new Tag { Name = "Sample tag 1" } }
            };

            s1.CreateNode(node2);
        }

        [Test]
        public void UpdateNode_ValidNode_Success()
        {
            User user = new User
            {
                Email = "user@mail.com",
                FirstName = "First",
                SecondName = "Second"
            };

            const string Password = "qwerty";

            IUserManager userManager = new UserManager();
            int userId = userManager.CreateUser(user, Password);

            Node node = new Node
            {
                Created = DateTime.Now,
                IsDeleted = false,
                Modified = DateTime.Now,
                OwnerId = userId,
                Text = "text",
                Title = "title",
                Tags = new List<Tag> { new Tag { Name = "Sample tag" } }
            };
            NodeManager s1 = new NodeManager();

            s1.CreateNode(node);

            node.Title = "title2";

            s1.UpdateNode(node);
        }
    }
}
