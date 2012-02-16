using System;

namespace NLibsndfile.Native
{
    /// <summary>
    /// Provides native command API access and handles all native struct marshalling.
    /// </summary>
    public class LibsndfileCommandApiNativeWrapper : ILibsndfileCommandApi
    {
        private readonly ILibsndfileApi m_Api;
        private readonly ILibsndfileCommandMarshaller m_Marshaller;

        /// <summary>
        /// Initialize a new instance of LibsndfileCommandApiNativeWrapper with the <paramref name="api"/> api implementation.
        /// </summary>
        internal LibsndfileCommandApiNativeWrapper(ILibsndfileApi api)
            : this(api, new LibsndfileCommandMarshaller())
        {
        }

        /// <summary>
        /// Initialize a new instance of LibsndfileCommandApiNativeWrapper with the <paramref name="api"/> 
        /// and <paramref name="marshaller"/> implementations.
        /// </summary>
        /// <param name="api">LibsndfileApi implementation to use.</param>
        /// <param name="marshaller">LibsndfileCommandMarshaller implementation to use.</param>
        internal LibsndfileCommandApiNativeWrapper(ILibsndfileApi api, ILibsndfileCommandMarshaller marshaller)
        {
            if (api == null)
                throw new ArgumentNullException("api");
            if (marshaller == null)
                throw new ArgumentNullException("marshaller");

            m_Api = api;
            m_Marshaller = marshaller;
        }

        /// <summary>
        /// Returns the version of the Libsndfile library.
        /// </summary>
        /// <returns>Libsndfile library version.</returns>
        public string GetLibVersion()
        {
            const int MaxVersionLength = 128;
            using (var memory = m_Marshaller.Allocate(MaxVersionLength))
            {
                var retval = m_Api.Command(IntPtr.Zero, LibsndfileCommand.GetLibVersion, memory, MaxVersionLength);
                if (!LibsndfileCommandUtilities.IsValidResult(IntPtr.Zero, LibsndfileCommand.GetLibVersion, retval))
                    throw new LibsndfileException("Unable to retrieve Libsndfile library version.");

                return m_Marshaller.MemoryHandleToString(memory);
            }
        }
    }
}