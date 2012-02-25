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

        /// <summary>
        /// Retrieve the peak value for each channel for the file as stored in the file header.
        /// </summary>
        /// <param name="sndfile">Audio file we want to examine.</param>
        /// <param name="channels">Number of audio channels in the audio file.</param>
        /// <returns>Peak values for each channel from file header.</returns>
        public double[] GetMaxAllChannels(IntPtr sndfile, int channels)
        {
            if (sndfile == IntPtr.Zero)
                throw new ArgumentException("File handle is invalid/closed.");
            if (channels <= 0)
                throw new ArgumentOutOfRangeException("channels", channels, "Channels must be greater than zero.");

            var max = m_Api.GetMaxAllChannels(sndfile, channels);
            if (max == null || max.Length == 0)
                throw new LibsndfileException("Unable to retrieve signal max for all channels from file header.");

            return max;
        }

        /// <summary>
        /// Sets float normalization for read and write functions on the <paramref name="sndfile"/> file.
        /// </summary>
        /// <param name="sndfile">Audio file to set float normalization on.</param>
        /// <param name="normalize">Flag to enable or disable float normalization.</param>
        /// <returns>Previous float normalization state.</returns>
        public bool SetNormFloat(IntPtr sndfile, bool normalize)
        {
            if (sndfile == IntPtr.Zero)
                throw new ArgumentException("File handle is invalid/closed.");

            return m_Api.SetNormFloat(sndfile, normalize);
        }

        /// <summary>
        /// Sets double normalization for read and write functions on the <paramref name="sndfile"/> file.
        /// </summary>
        /// <param name="sndfile">Audio file to set double normalization on.</param>
        /// <param name="normalize">Flag to enable or disable double normalization.</param>
        /// <returns>Previous double normalization state.</returns>
        public bool SetNormDouble(IntPtr sndfile, bool normalize)
        {
            if (sndfile == IntPtr.Zero)
                throw new ArgumentException("File handle is invalid/closed.");

            return m_Api.SetNormDouble(sndfile, normalize);
        }

        /// <summary>
        /// Gets float normalization for read and write functions on the <paramref name="sndfile"/> file.
        /// </summary>
        /// <param name="sndfile">Audio file to retrieve float normalization for.</param>
        /// <returns>Current float normalization state.</returns>
        public bool GetNormFloat(IntPtr sndfile)
        {
            if (sndfile == IntPtr.Zero)
                throw new ArgumentException("File handle is invalid/closed.");

            return m_Api.GetNormFloat(sndfile);
        }

        /// <summary>
        /// Gets double normalization for read and write functions on the <paramref name="sndfile"/> file.
        /// </summary>
        /// <param name="sndfile">Audio file to retrieve double normalization for.</param>
        /// <returns>Current double normalization state.</returns>
        public bool GetNormDouble(IntPtr sndfile)
        {
            if (sndfile == IntPtr.Zero)
                throw new ArgumentException("File handle is invalid/closed.");

            return m_Api.GetNormDouble(sndfile);
        }

        /// <summary>
        /// Sets the scale factor for when integer data is read from the <paramref name="sndfile"/>
        /// which contains floating point data.
        /// </summary>
        /// <param name="sndfile">Audio file to set int to float scaling.</param>
        /// <param name="enable">Flag to enable or disable int to float scaling.</param>
        /// <returns>Previous scaling state.</returns>
        public bool SetScaleFloatIntRead(IntPtr sndfile, bool enable)
        {
            if (sndfile == IntPtr.Zero)
                throw new ArgumentException("File handle is invalid/closed.");

            return m_Api.SetScaleFloatIntRead(sndfile, enable);
        }

        /// <summary>
        /// Sets the scale factor for when integer data is written from the <paramref name="sndfile"/>
        /// as floating point data.
        /// </summary>
        /// <param name="sndfile">Audio file to set float to int scaling.</param>
        /// <param name="enable">Flag to enable or disable float to int scaling.</param>
        /// <returns>Previous scaling state.</returns>
        public bool SetScaleIntFloatWrite(IntPtr sndfile, bool enable)
        {
            if (sndfile == IntPtr.Zero)
                throw new ArgumentException("File handle is invalid/closed.");

            return m_Api.SetScaleIntFloatWrite(sndfile, enable);
        }

        /// <summary>
        /// Returns the number of simple formats supported by Libsndfile.
        /// </summary>
        /// <returns>Number of simple formats supported.</returns>
        public int GetSimpleFormatCount()
        {
            return m_Api.GetSimpleFormatCount();
        }

        /// <summary>
        /// Retrieves information about the given <paramref name="format"/>.
        /// </summary>
        /// <param name="format">Format to retrieve information about.</param>
        /// <returns><see cref="LibsndfileFormatInfo"/> object containing format information.</returns>
        public LibsndfileFormatInfo GetSimpleFormat(LibsndfileFormat format)
        {
            if (format == LibsndfileFormat.Submask ||
                format == LibsndfileFormat.Typemask ||
                format == LibsndfileFormat.Endmask)
                throw new ArgumentException("Format must be set.");

            return m_Api.GetSimpleFormat(format);
        }

        /// <summary>
        /// Retrieve information about the given <paramref name="format"/> major/subtype format.
        /// </summary>
        /// <param name="format">Major or subtype format to retrieve information about.</param>
        /// <returns><see cref="LibsndfileFormatInfo"/> object containing format information.</returns>
        public LibsndfileFormatInfo GetFormatInfo(LibsndfileFormat format)
        {
            if (format == LibsndfileFormat.Submask ||
                format == LibsndfileFormat.Typemask ||
                format == LibsndfileFormat.Endmask)
                throw new ArgumentException("Format must be set.");

            return m_Api.GetFormatInfo(format);
        }

        /// <summary>
        /// Returns the number of major formats supported by Libsndfile.
        /// </summary>
        /// <returns>Number of major formats supported.</returns>
        public int GetFormatMajorCount()
        {
            return m_Api.GetFormatMajorCount();
        }

        /// <summary>
        /// Retrieves information about the given <paramref name="format"/> major type.
        /// </summary>
        /// <param name="format">Major format to retrieve information about.</param>
        /// <returns><see cref="LibsndfileFormatInfo"/> object containing format information.</returns>
        public LibsndfileFormatInfo GetFormatMajor(LibsndfileFormat format)
        {
            if (format == LibsndfileFormat.Submask ||
                format == LibsndfileFormat.Typemask ||
                format == LibsndfileFormat.Endmask)
                throw new ArgumentException("Format must be set.");

            return m_Api.GetFormatMajor(format);
        }

        /// <summary>
        /// Returns the number of available subformats.
        /// </summary>
        /// <returns>Number of subtype formats supported.</returns>
        public int GetFormatSubtypeCount()
        {
            return m_Api.GetFormatSubtypeCount();
        }

        /// <summary>
        /// Retrieves information about the given <paramref name="format"/> subtype.
        /// </summary>
        /// <param name="format">Subtype format to retrieve information about.</param>
        /// <returns><see cref="LibsndfileFormatInfo"/> object containing format information.</returns>
        public LibsndfileFormatInfo GetFormatSubtype(LibsndfileFormat format)
        {
            if (format == LibsndfileFormat.Submask ||
                format == LibsndfileFormat.Typemask ||
                format == LibsndfileFormat.Endmask)
                throw new ArgumentException("Format must be set.");

            return m_Api.GetFormatSubtype(format);
        }

        /// <summary>
        /// Sets PEAK chunk in <paramref name="sndfile"/> file which contains floating point data.
        /// </summary>
        /// <param name="sndfile">Audio file to set PEAK chunk.</param>
        /// <param name="enable">Flag to enable or disable PEAK chunk.</param>
        /// <returns>True/False on whether the PEAK chunk will be written on the next write call.</returns>
        /// <remarks>
        /// This call must be made before any data is written to the file.
        /// </remarks>
        public bool SetAddPeakChunk(IntPtr sndfile, bool enable)
        {
            if (sndfile == IntPtr.Zero)
                throw new ArgumentException("File handle is invalid/closed.");

            return m_Api.SetAddPeakChunk(sndfile, enable);
        }

        /// <summary>
        /// Updates file header to reflect updated file information.
        /// </summary>
        /// <param name="sndfile">Audio file to update file header of.</param>
        public void UpdateHeaderNow(IntPtr sndfile)
        {
            if (sndfile == IntPtr.Zero)
                throw new ArgumentException("File handle is invalid/closed.");

            m_Api.UpdateHeaderNow(sndfile);
        }

        /// <summary>
        /// Sets file header auto update for <paramref name="sndfile"/> after every subsequent write call.
        /// </summary>
        /// <param name="sndfile">Audio file to enable automatic file header updating.</param>
        /// <param name="enable">Flag to enable or file header auto-update.</param>
        /// <returns>Current file header auto-update state.</returns>
        public bool SetUpdateHeaderAuto(IntPtr sndfile, bool enable)
        {
            if (sndfile == IntPtr.Zero)
                throw new ArgumentException("File handle is invalid/closed.");

            return m_Api.SetUpdateHeaderAuto(sndfile, enable);
        }

        /// <summary>
        /// Truncate a file opened for write or read/write.
        /// </summary>
        /// <param name="sndfile">Audio file to truncate.</param>
        /// <param name="length">Number of frames remaining after truncation.</param>
        /// <returns>Success of file truncation.</returns>
        public bool FileTruncate(IntPtr sndfile, long length)
        {
            if (sndfile == IntPtr.Zero)
                throw new ArgumentException("File handle is invalid/closed.");
            if (length <= 0)
                throw new ArgumentOutOfRangeException("length", length, "Length must be positive.");

            return m_Api.FileTruncate(sndfile, length);
        }
    }
}