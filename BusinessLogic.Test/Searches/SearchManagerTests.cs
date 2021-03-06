﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Test.Searches
{
    using BusinessLogic.EntityManagers;
    using BusinessLogic.Searches;

    using IdeaStorage.EntriesModel.Entries;

    using NUnit.Framework;
    using Infrastructure.Testing;

    public class SearchManagerTests : DatabaseTestFixture
    {
        [Test]
        public void SearchNodes_SearchByValue_ResultsFound()
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
                Text = "text12",
                Title = "title",
                Tags = new List<Tag> { new Tag { Name = "Simple" } }
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
                Tags = new List<Tag> { new Tag { Name = "Bad" }, new Tag { Name = "Good" } }
            };

            s1.CreateNode(node2);


            SearchManager searchManager = new SearchManager();
            var s2 = searchManager.FindNodesByValue("text2 bad", true);
        }


        [Test]
        public void SearchNodes_SearchByTags_ResultsFound()
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
                Text = "text12",
                Title = "title",
                Tags = new List<Tag> { new Tag { Name = "Simple" } }
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
                Tags = new List<Tag> { new Tag { Name = "Bad1" }, new Tag { Name = "Good" } }
            };

            s1.CreateNode(node2);


            Node node3 = new Node
            {
                Created = DateTime.Now,
                IsDeleted = false,
                Modified = DateTime.Now,
                OwnerId = userId,
                Text = "text2",
                Title = "title2",
                Tags = new List<Tag> { new Tag { Name = "Bad1" }, new Tag { Name = "Good1" } }
            };

            s1.CreateNode(node3);


            SearchManager searchManager = new SearchManager();
            var s2 = searchManager.FindNodesByTags("Good2");

        }
    }
}
