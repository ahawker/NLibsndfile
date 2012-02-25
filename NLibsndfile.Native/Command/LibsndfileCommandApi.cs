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

        /// <summary>
        /// Scan <paramref name="sndfile"/> file and return maximum calculated signal value. 
        /// </summary>
        /// <param name="sndfile">Audio file we want to scan.</param>
        /// <returns>Maximum signal value.</returns>
        public double CalcSignalMax(IntPtr sndfile)
        {
            if (sndfile == IntPtr.Zero)
                throw new ArgumentException("File handle is invalid/closed.");
            
            return m_Api.CalcSignalMax(sndfile);
        }

        /// <summary>
        /// Scan <paramref name="sndfile"/> file and return normalized maximum calculated signal value.
        /// </summary>
        /// <param name="sndfile">Audio file we want to scan.</param>
        /// <returns>Normalized maximum signal value.</returns>
        public double CalcNormSignalMax(IntPtr sndfile)
        {
            if (sndfile == IntPtr.Zero)
                throw new ArgumentException("File handle is invalid/closed.");

            return m_Api.CalcNormSignalMax(sndfile);
        }

        /// <summary>
        /// Scan <paramref name="sndfile"/> file and return single peak value for each channel.
        /// </summary>
        /// <param name="sndfile">Audio file we want to scan.</param>
        /// <param name="channels">Number of audio channels in the audio file.</param>
        /// <returns>Peak values for each channel.</returns>
        public double[] CalcMaxAllChannels(IntPtr sndfile, int channels)
        {
            if (sndfile == IntPtr.Zero)
                throw new ArgumentException("File handle is invalid/closed.");
            if (channels <= 0)
                throw new ArgumentOutOfRangeException("channels", channels, "Channels must be greater than zero.");

            var max = m_Api.CalcMaxAllChannels(sndfile, channels);
            if (max == null || max.Length == 0)
                throw new LibsndfileException("Unable to retrieve signal max for all channels.");

            return max;
        }

        /// <summary>
        /// Scan <paramref name="sndfile"/> file and return normalized peak value for each channel. 
        /// </summary>
        /// <param name="sndfile">Audio file we want to scan.</param>
        /// <param name="channels">Number of audio channels in the audio file.</param>
        /// <returns>Normalized Peak values for each channel.</returns>
        public double[] CalcNormMaxAllChannels(IntPtr sndfile, int channels)
        {
            if (sndfile == IntPtr.Zero)
                throw new ArgumentException("File handle is invalid/closed.");
            if (channels <= 0)
                throw new ArgumentOutOfRangeException("channels", channels, "Channels must be greater than zero.");

            var max = m_Api.CalcNormMaxAllChannels(sndfile, channels);
            if (max == null || max.Length == 0)
                throw new LibsndfileException("Unable to retrieve normalized signal max for all channels.");

            return max;
        }

        /// <summary>
        /// Retrieve the peak value for the file as stored in the file header.
        /// </summary>
        /// <param name="sndfile">Audio file we want to examine.</param>
        /// <returns>Peak value from file header.</returns>
        public double GetSignalMax(IntPtr sndfile)
        {
            if (sndfile == IntPtr.Zero)
                throw new ArgumentException("File handle is invalid/closed.");

            return m_Api.GetSignalMax(sndfile);
        }
    }
}