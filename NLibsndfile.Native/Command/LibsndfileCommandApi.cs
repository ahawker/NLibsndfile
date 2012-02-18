using System;

namespace NLibsndfile.Native
{
    /// <summary>
    /// Provides access to all libsndfile commands.
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
            var version = m_Api.GetLibVersion();
            if (string.IsNullOrEmpty(version))
                throw new LibsndfileException("Unable to retrieve Libsndfile library version.");

            return version;
        }

        /// <summary>
        /// Returns the internal Libsndfile log generated when loading a file.
        /// </summary>
        /// <param name="sndfile">Audio file we want the log for.</param>
        /// <returns>Libsndfile log info.</returns>
        public string GetLogInfo(IntPtr sndfile)
        {
            if (sndfile == IntPtr.Zero)
                throw new ArgumentException("File handle is invalid/closed.");

            var log = m_Api.GetLogInfo(sndfile);
            if (string.IsNullOrEmpty(log))
                throw new LibsndfileException("Unable to retrieve Libsndfile log info for the given file.");

            return log;
        }
    }
}