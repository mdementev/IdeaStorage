namespace BusinessLogic.Test.EntityManagers
{
    using System;
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
                Title = "title"
            };
            NodeManager s1 = new NodeManager();
            s1.CreateNode(node);
        }
    }
}
