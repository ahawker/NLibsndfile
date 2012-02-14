using System;

namespace NLibsndfile.Native
{
    /// <summary>
    /// Provides access to all Libsndfile commands.
    /// </summary>
    public class LibsndfileCommandApi : ILibsndfileCommandApi
    {
        private readonly ILibsndfileCommandApi m_CommandApi;

        /// <summary>
        /// Initializes a new instance of LibsndfileCommandApi with the <paramref name="commandApi"/> command implementation.
        /// </summary>
        internal LibsndfileCommandApi(ILibsndfileCommandApi commandApi)
        {
            if (commandApi == null)
                throw new ArgumentNullException("commandApi");

            m_CommandApi = commandApi;
        }
    }
}