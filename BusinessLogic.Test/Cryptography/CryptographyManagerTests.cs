namespace BusinessLogic.Test.Cryptography
{
    using BusinessLogic.Cryptography;

    using NUnit.Framework;

    /// <summary>
    /// Implements tests for check CryptographyManager working.
    /// </summary>
    [TestFixture]
    public class CryptographyManagerTests
    {
        #region Public Methods and Operators

        /// <summary>
        /// Verifies, that CreateSalt() method returns unique value each time.
        /// </summary>
        [Test]
        public void CreateSalt_SimilarCondition_SaltIsUnique()
        {
            ICryptographyManager cryptographyManager = new CryptographyManager();
            string salt1 = cryptographyManager.CreateSalt(12);
            string salt2 = cryptographyManager.CreateSalt(12);

            Assert.AreNotEqual(salt1, salt2);
        }

        /// <summary>
        /// Verifies, that CreateSalt() method returns salt according to input size.
        /// Note: output string length not equals to the size parameter.
        /// </summary>
        [Test]
        public void CreateSalt_SpecificSize_CorrectStringLengthIsReturned()
        {
            ICryptographyManager cryptographyManager = new CryptographyManager();
            string salt = cryptographyManager.CreateSalt(12);

            Assert.AreEqual(16, salt.Length);
        }

        /// <summary>
        /// Verifies, that HashPassword() method generates hash with 44 symbols length (not depending on input password and salt length).
        /// </summary>
        /// <param name="password">The input password to hash.</param>
        /// <param name="saltSize">Size of the salt.</param>
        [Test]
        [TestCase("1", 256)]
        [TestCase("P@$$w0rd", 12)]
        [TestCase("VERyVeryVeryLongPasswordWhichStillBeConvertedTo44SymbolsSalt", 1)]
        public void HashPassword_AnyInputPassword_44SymbolsOutputHashIsGenerated(string password, int saltSize)
        {
            ICryptographyManager cryptographyManager = new CryptographyManager();

            string salt = cryptographyManager.CreateSalt(256);
            string hash = cryptographyManager.HashPassword(password, salt);

            // Check, that password length is 44 symbols always.
            Assert.AreEqual(44, hash.Length);
        }

        /// <summary>
        /// Verifies, that HashPassword() method generates hash, which doesn't contain input password in any form.
        /// </summary>
        [Test]
        public void HashPassword_AnyInputPassword_HashDoesNotContainInputPassword()
        {
            ICryptographyManager cryptographyManager = new CryptographyManager();

            string salt = cryptographyManager.CreateSalt(12);
            const string Password = "qwerty";
            string hash = cryptographyManager.HashPassword(Password, salt);

            // Check, that output hash doesn't contain input password in any form.
            Assert.IsFalse(hash.Contains(Password));
        }

        /// <summary>
        /// Verifies, that HashPassword() method generates the same hash for the same input password and salt (consistently).
        /// </summary>
        [Test]
        public void HashPassword_ConstantPasswordAndSalt_HashIsConstant()
        {
            ICryptographyManager cryptographyManager = new CryptographyManager();

            string salt = cryptographyManager.CreateSalt(12);
            const string Password = "qwerty";
            string hash1 = cryptographyManager.HashPassword(Password, salt);
            string hash2 = cryptographyManager.HashPassword(Password, salt);

            Assert.AreEqual(hash1, hash2);
        }

        /// <summary>
        /// Verifies, that HashPassword() method generates different hash for the same input password and different salts.
        /// </summary>
        [Test]
        public void HashPassword_TheSamePasswordsDifferentSalts_HashesAreDifferent()
        {
            ICryptographyManager cryptographyManager = new CryptographyManager();

            string salt1 = cryptographyManager.CreateSalt(12);
            string salt2 = cryptographyManager.CreateSalt(12);
            const string Password = "qwerty";
            string hash1 = cryptographyManager.HashPassword(Password, salt1);
            string hash2 = cryptographyManager.HashPassword(Password, salt2);

            Assert.AreNotEqual(hash1, hash2);
        }

        #endregion
    }
}