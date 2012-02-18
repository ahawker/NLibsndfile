using System;
using System.Runtime.InteropServices;

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

        /// <summary>
        /// Returns the internal Libsndfile log generated when loading a file.
        /// </summary>
        /// <param name="sndfile">Audio file we want the log for.</param>
        /// <returns>Libsndfile log info.</returns>
        public string GetLogInfo(IntPtr sndfile)
        {
            const int MaxLogSize = 2048;
            using (var memory = m_Marshaller.Allocate(MaxLogSize))
            {
                var retval = m_Api.Command(sndfile, LibsndfileCommand.GetLogInfo, memory, MaxLogSize);
                if (!LibsndfileCommandUtilities.IsValidResult(sndfile, LibsndfileCommand.GetLogInfo, retval))
                    throw new LibsndfileException("Unable to retrieve Libsndfile log info for the given file.");

                return m_Marshaller.MemoryHandleToString(memory);
            }
        }

        /// <summary>
        /// Scan <paramref name="sndfile"/> file and return maximum calculated signal value. 
        /// </summary>
        /// <param name="sndfile">Audio file we want to scan.</param>
        /// <returns>Maximum signal value.</returns>
        public double CalcSignalMax(IntPtr sndfile)
        {
            using (var memory = m_Marshaller.AllocateDouble())
            {
                var retval = m_Api.Command(sndfile, LibsndfileCommand.CalcSignalMax, memory, memory.Size);
                if (!LibsndfileCommandUtilities.IsValidResult(sndfile, LibsndfileCommand.CalcSignalMax, retval))
                    throw new LibsndfileException("Unable to calculate signal max for the given file.");

                return m_Marshaller.MemoryHandleToDouble(memory);
            }
        }
    }
}