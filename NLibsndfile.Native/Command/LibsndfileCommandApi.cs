using System;

namespace NLibsndfile.Native
{
    /// <summary>
    /// Returns the version of the Libsndfile library.
    /// </summary>
    /// <returns></returns>
    public class LibsndfileCommandApi : ILibsndfileCommandApi
    {
        private readonly ILibsndfileCommandApi m_Api;

        /// <summary>
        /// Initializes a new instance of LibsndfileCommandApi with the <paramref name="api"/> command implementation.
        /// </summary>
        internal LibsndfileCommandApi(ILibsndfileCommandApi api)
        {
            if (api == null)
                throw new ArgumentNullException("api");

            m_Api = api;
        }

        /// <summary>
        /// Returns the version of the Libsndfile library.
        /// </summary>
        /// <returns>Libsndfile library version.</returns>
        public string GetLibVersion()
        {
            return m_Api.GetLibVersion();
        }
    }
}