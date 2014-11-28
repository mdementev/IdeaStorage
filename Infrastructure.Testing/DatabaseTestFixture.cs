namespace Infrastructure.Testing
{
    using System;
    using System.Configuration;
    using System.Data.Entity.Core.EntityClient;
    using System.Data.SqlClient;
    using System.IO;

    using NUnit.Framework;

    /// <summary>
    /// Base class for all database dependent tests.
    /// </summary>
    [TestFixture]
    [SetUpFixture]
    public class DatabaseTestFixture
    {
        #region Public Methods and Operators

        /// <summary>
        /// This method is called before each test in fixture is run.
        /// Should be used to prepare database for running each test.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            string entityConnectionString =
                ConfigurationManager.ConnectionStrings["IdeaStorageEntities"].ConnectionString;
            string providerConnectionString =
                new EntityConnectionStringBuilder(entityConnectionString).ProviderConnectionString;

            using (SqlConnection connection = new SqlConnection(providerConnectionString))
            {
                connection.Open();

                string cleanSql = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"\SetUp.sql");
                SqlCommand sqlCommand = new SqlCommand(cleanSql, connection);
                sqlCommand.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// This method is called before any tests in fixture is run.
        /// Should be used to prepare database for running tests.
        /// </summary>
        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            string entityConnectionString =
                ConfigurationManager.ConnectionStrings["IdeaStorageEntities"].ConnectionString;
            string providerConnectionString =
                new EntityConnectionStringBuilder(entityConnectionString).ProviderConnectionString;

            using (SqlConnection connection = new SqlConnection(providerConnectionString))
            {
                connection.Open();

                string cleanSql = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"\TestFixtureSetUp.sql");
                SqlCommand sqlCommand = new SqlCommand(cleanSql, connection);
                sqlCommand.ExecuteNonQuery();

                string installSql = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"\install.sql");
                sqlCommand = new SqlCommand(installSql, connection);
                sqlCommand.ExecuteNonQuery();
            }
        }

        #endregion
    }
}