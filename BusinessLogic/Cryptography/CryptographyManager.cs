namespace BusinessLogic.Cryptography
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// Implements methods to work with Cryptography in Idea Storage project.
    /// </summary>
    public class CryptographyManager : ICryptographyManager
    {
        #region Public Methods and Operators

        /// <summary>
        /// Creates the random salt (size-length random string).
        /// </summary>
        /// <param name="size">The size of an array of bytes with a cryptographically strong sequence of random values. </param>
        /// <returns>
        /// Random string value, which can be used as salt for passwords.
        /// </returns>
        /// <remarks>
        /// Output string length not equals to the <see cref="size"/> parameter.
        /// For example, if you want to have 16-digit salt you should provide 12 as size of input.
        /// </remarks>
        public string CreateSalt(int size)
        {
            // Generate a cryptographic random number using the cryptographic service provider.
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[size];
            rng.GetBytes(buff);

            // Return a Base64 string representation of the random number.
            return Convert.ToBase64String(buff);
        }

        /// <summary>
        /// Salts and hashes the password.
        /// </summary>
        /// <param name="password">The password to hash.</param>
        /// <param name="salt">The salt for a password.</param>
        /// <returns>
        /// A string representation (44 symbols) of salted hashed password.
        /// </returns>
        public string HashPassword(string password, string salt)
        {
            // Create a new instance of the hash crypto service provider.
            HashAlgorithm hashAlg = new SHA256CryptoServiceProvider();

            // Convert the data to hash to an array of Bytes.
            byte[] bytValue = Encoding.UTF8.GetBytes(password + salt);

            // Compute the Hash. This returns an array of Bytes.
            byte[] bytHash = hashAlg.ComputeHash(bytValue);

            // Represent the hash value as a base64-encoded string, 
            return Convert.ToBase64String(bytHash);
        }

        #endregion
    }
}