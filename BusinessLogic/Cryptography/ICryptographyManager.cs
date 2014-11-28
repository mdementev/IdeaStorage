namespace BusinessLogic.Cryptography
{
    /// <summary>
    /// Defines supported method to work with Cryptography in Idea Storage project.
    /// </summary>
    public interface ICryptographyManager
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
        string CreateSalt(int size);

        /// <summary>
        /// Salts and hashes the password.
        /// </summary>
        /// <param name="password">The password to hash.</param>
        /// <param name="salt">The salt for a password.</param>
        /// <returns>A string representation (44 symbols) of salted hashed password.</returns>
        string HashPassword(string password, string salt);

        #endregion
    }
}