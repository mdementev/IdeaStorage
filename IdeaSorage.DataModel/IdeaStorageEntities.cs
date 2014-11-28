namespace IdeaSorage.DataModel
{
    using System;

    /// <summary>
    /// The idea storage entities disposable data base context.
    /// </summary>
    public partial class IdeaStorageEntities : IDisposable
    {
        #region Public Methods and Operators

        /// <summary>
        /// The create data base context.
        /// </summary>
        /// <returns>
        /// The <see cref="IdeaStorageEntities"/>.
        /// </returns>
        public static IdeaStorageEntities Create()
        {
            return new IdeaStorageEntities();
        }

        /// <summary>
        /// The dispose data base context.
        /// </summary>
        public new void Dispose()
        {
            base.Dispose();
        }

        #endregion
    }
}