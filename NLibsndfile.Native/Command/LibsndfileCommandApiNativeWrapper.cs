using System;

namespace NLibsndfile.Native
{
    /// <summary>
    /// Provides native command API access and handles all native struct marshalling.
    /// </summary>
    public class LibsndfileCommandApiNativeWrapper : ILibsndfileCommandApi
    {
        private readonly ILibsndfileApi m_Api;

        /// <summary>
        /// Initialize a new instance of LibsndfileCommandApiNativeWrapper with the <paramref name="api"/> api implementation.
        /// </summary>
        internal LibsndfileCommandApiNativeWrapper(ILibsndfileApi api)
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
            const int MaxVersionLength = 128;
            using (var memory = LibsndfileCommandMarshaller.Allocate(MaxVersionLength))
            {
                var retval = m_Api.Command(IntPtr.Zero, LibsndfileCommand.GetLibVersion, memory, MaxVersionLength);
                if (!LibsndfileCommandUtilities.IsValidResult(IntPtr.Zero, LibsndfileCommand.GetLibVersion, retval))
                    throw new LibsndfileException("Unable to retrieve Libsndfile library version.");

                return LibsndfileCommandMarshaller.MemoryHandleToString(memory);
            }
        }
    }
}