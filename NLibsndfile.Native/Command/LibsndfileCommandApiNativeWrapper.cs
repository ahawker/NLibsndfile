using System;

namespace NLibsndfile.Native
{
    /// <summary>
    /// Provides native command API access and handles all native struct marshalling.
    /// </summary>
    public class LibsndfileCommandApiNativeWrapper : ILibsndfileCommandApi
    {
        private readonly ILibsndfileApi m_Api;
        private readonly ILibsndfileMarshaller m_Marshaller;

        /// <summary>
        /// Initialize a new instance of LibsndfileCommandApiNativeWrapper with the <paramref name="api"/> api implementation.
        /// </summary>
        internal LibsndfileCommandApiNativeWrapper(ILibsndfileApi api)
            : this(api, new LibsndfileMarshaller())
        {
        }

        /// <summary>
        /// Initialize a new instance of LibsndfileCommandApiNativeWrapper with the <paramref name="api"/> 
        /// and <paramref name="marshaller"/> implementations.
        /// </summary>
        /// <param name="api">LibsndfileApi implementation to use.</param>
        /// <param name="marshaller">LibsndfileMarshaller implementation to use.</param>
        internal LibsndfileCommandApiNativeWrapper(ILibsndfileApi api, ILibsndfileMarshaller marshaller)
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
            using (var memory = m_Marshaller.Allocate<double>())
            {
                var retval = m_Api.Command(sndfile, LibsndfileCommand.CalcSignalMax, memory, memory.Size);
                if (!LibsndfileCommandUtilities.IsValidResult(sndfile, LibsndfileCommand.CalcSignalMax, retval))
                    throw new LibsndfileException("Unable to calculate signal max for the given file.");

                return m_Marshaller.MemoryHandleTo<double>(memory);
            }
        }

        /// <summary>
        /// Scan <paramref name="sndfile"/> file and return normalized maximum calculated signal value.
        /// </summary>
        /// <param name="sndfile">Audio file we want to scan.</param>
        /// <returns>Normalized maximum signal value.</returns>
        public double CalcNormSignalMax(IntPtr sndfile)
        {
            using (var memory = m_Marshaller.Allocate<double>())
            {
                var retval = m_Api.Command(sndfile, LibsndfileCommand.CalcNormSignalMax, memory, memory.Size);
                if (!LibsndfileCommandUtilities.IsValidResult(sndfile, LibsndfileCommand.CalcNormSignalMax, retval))
                    throw new LibsndfileException("Unable to calculate normalized signal max for the given file.");

                return m_Marshaller.MemoryHandleTo<double>(memory);
            }
        }

        /// <summary>
        /// Scan <paramref name="sndfile"/> file and return single peak value for each channel.
        /// </summary>
        /// <param name="sndfile">Audio file we want to scan.</param>
        /// <param name="channels">Number of audio channels in the audio file.</param>
        /// <returns>Peak values for each channel.</returns>
        public double[] CalcMaxAllChannels(IntPtr sndfile, int channels)
        {
            using (var memory = m_Marshaller.AllocateArray<double>(channels))
            {
                var retval = m_Api.Command(sndfile, LibsndfileCommand.CalcMaxAllChannels, memory, memory.Size);
                if (!LibsndfileCommandUtilities.IsValidResult(sndfile, LibsndfileCommand.CalcMaxAllChannels, retval))
                    throw new LibsndfileException("Unable to calculate signal max of all channels for the given file.");

                return m_Marshaller.MemoryHandleToArray<double>(memory);
            }
        }

        /// <summary>
        /// Scan <paramref name="sndfile"/> file and return normalized peak value for each channel. 
        /// </summary>
        /// <param name="sndfile">Audio file we want to scan.</param>
        /// <param name="channels">Number of audio channels in the audio file.</param>
        /// <returns>Normalized Peak values for each channel.</returns>
        public double[] CalcNormMaxAllChannels(IntPtr sndfile, int channels)
        {
            using (var memory = m_Marshaller.AllocateArray<double>(channels))
            {
                var retval = m_Api.Command(sndfile, LibsndfileCommand.CalcNormMaxAllChannels, memory, memory.Size);
                if (!LibsndfileCommandUtilities.IsValidResult(sndfile, LibsndfileCommand.CalcNormMaxAllChannels, retval))
                    throw new LibsndfileException("Unable to calculate normalized signal max for all channels in the given file.");

                return m_Marshaller.MemoryHandleToArray<double>(memory);
            }
        }

        /// <summary>
        /// Retrieve the peak value for the file as stored in the file header.
        /// </summary>
        /// <param name="sndfile">Audio file we want to examine.</param>
        /// <returns>Peak value from file header.</returns>
        public double GetSignalMax(IntPtr sndfile)
        {
            using (var memory = m_Marshaller.Allocate<double>())
            {
                var retval = m_Api.Command(sndfile, LibsndfileCommand.GetSignalMax, memory, memory.Size);
                if (!LibsndfileCommandUtilities.IsValidResult(sndfile, LibsndfileCommand.CalcNormMaxAllChannels, retval))
                    throw new LibsndfileException("Unable to retrieve peak value from file header.");

                return m_Marshaller.MemoryHandleTo<double>(memory);
            }
        }

        /// <summary>
        /// Retrieve the peak value for each channel for the file as stored in the file header.
        /// </summary>
        /// <param name="sndfile">Audio file we want to examine.</param>
        /// <param name="channels">Number of audio channels in the audio file.</param>
        /// <returns>Peak values for each channel from file header.</returns>
        public double[] GetMaxAllChannels(IntPtr sndfile, int channels)
        {
            using (var memory = m_Marshaller.AllocateArray<double>(channels))
            {
                var retval = m_Api.Command(sndfile, LibsndfileCommand.GetMaxAllChannels, memory, memory.Size);
                if (!LibsndfileCommandUtilities.IsValidResult(sndfile, LibsndfileCommand.CalcNormMaxAllChannels, retval))
                    throw new LibsndfileException("Unable to retrieve peak values for all channels from file header.");

                return m_Marshaller.MemoryHandleToArray<double>(memory);
            }
        }

        /// <summary>
        /// Sets float normalization for read and write functions on the <paramref name="sndfile"/> file.
        /// </summary>
        /// <param name="sndfile">Audio file to set float normalization on.</param>
        /// <param name="normalize">Flag to enable or disable float normalization.</param>
        /// <returns>Previous float normalization state.</returns>
        public bool SetNormFloat(IntPtr sndfile, bool normalize)
        {
            var retval = m_Api.Command(sndfile, LibsndfileCommand.SetNormFloat, IntPtr.Zero, Convert.ToInt32(normalize));
            if (!LibsndfileCommandUtilities.IsValidResult(sndfile, LibsndfileCommand.SetNormFloat, retval))
                throw new LibsndfileException("Unable to set float normalization for the given file.");

            return Convert.ToBoolean(retval);
        }

        /// <summary>
        /// Sets double normalization for read and write functions on the <paramref name="sndfile"/> file.
        /// </summary>
        /// <param name="sndfile">Audio file to set double normalization on.</param>
        /// <param name="normalize">Flag to enable or disable double normalization.</param>
        /// <returns>Previous double normalization state.</returns>
        public bool SetNormDouble(IntPtr sndfile, bool normalize)
        {
            var retval = m_Api.Command(sndfile, LibsndfileCommand.SetNormDouble, IntPtr.Zero, Convert.ToInt32(normalize));
            if (!LibsndfileCommandUtilities.IsValidResult(sndfile, LibsndfileCommand.SetNormDouble, retval))
                throw new LibsndfileException("Unable to set double normalization for the given file.");

            return Convert.ToBoolean(retval);
        }

        /// <summary>
        /// Gets float normalization for read and write functions on the <paramref name="sndfile"/> file.
        /// </summary>
        /// <param name="sndfile">Audio file to retrieve float normalization for.</param>
        /// <returns>Current float normalization state.</returns>
        public bool GetNormFloat(IntPtr sndfile)
        {
            var retval = m_Api.Command(sndfile, LibsndfileCommand.GetNormFloat, IntPtr.Zero, 0);
            if (!LibsndfileCommandUtilities.IsValidResult(sndfile, LibsndfileCommand.GetNormFloat, retval))
                throw new LibsndfileException("Unable to retrieve float normalization for the given file.");

            return Convert.ToBoolean(retval);
        }

        /// <summary>
        /// Gets double normalization for read and write functions on the <paramref name="sndfile"/> file.
        /// </summary>
        /// <param name="sndfile">Audio file to retrieve double normalization for.</param>
        /// <returns>Current double normalization state.</returns>
        public bool GetNormDouble(IntPtr sndfile)
        {
            var retval = m_Api.Command(sndfile, LibsndfileCommand.GetNormDouble, IntPtr.Zero, 0);
            if (!LibsndfileCommandUtilities.IsValidResult(sndfile, LibsndfileCommand.GetNormDouble, retval))
                throw new LibsndfileException("Unable to retrieve double normalization for the given file.");

            return Convert.ToBoolean(retval);
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
            var retval = m_Api.Command(sndfile, LibsndfileCommand.SetScaleFloatIntRead, IntPtr.Zero, Convert.ToInt32(enable));
            if (!LibsndfileCommandUtilities.IsValidResult(sndfile, LibsndfileCommand.SetScaleFloatIntRead, retval))
                throw new LibsndfileException("Unable to set int to float scaling for the given file.");

            return Convert.ToBoolean(retval);
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
            var retval = m_Api.Command(sndfile, LibsndfileCommand.SetScaleIntFloatWrite, IntPtr.Zero, Convert.ToInt32(enable));
            if (!LibsndfileCommandUtilities.IsValidResult(sndfile, LibsndfileCommand.SetScaleIntFloatWrite, retval))
                throw new LibsndfileException("Unable to set float to int scaling for the given file.");

            return Convert.ToBoolean(retval);
        }

        /// <summary>
        /// Returns the number of simple formats supported by Libsndfile.
        /// </summary>
        /// <returns>Number of simple formats supported.</returns>
        public int GetSimpleFormatCount()
        {
            using (var memory = m_Marshaller.Allocate<int>())
            {
                var retval = m_Api.Command(IntPtr.Zero, LibsndfileCommand.GetSimpleFormatCount, memory, memory.Size);
                if (!LibsndfileCommandUtilities.IsValidResult(IntPtr.Zero, LibsndfileCommand.GetSimpleFormatCount, retval))
                    throw new LibsndfileException("Unable to retrieve number of support simple formats.");

                return m_Marshaller.MemoryHandleTo<int>(memory);
            }
        }

        /// <summary>
        /// Retrieves information about the given <paramref name="format"/>.
        /// </summary>
        /// <param name="format">Format to retrieve information about.</param>
        /// <returns><see cref="LibsndfileFormatInfo"/> object containing format information.</returns>
        public LibsndfileFormatInfo GetSimpleFormat(LibsndfileFormat format)
        {
            var formatInfo = new LibsndfileFormatInfo { Format = format };
            using (var memory = m_Marshaller.Allocate(formatInfo))
            {
                var retval = m_Api.Command(IntPtr.Zero, LibsndfileCommand.GetSimpleFormat, memory, memory.Size);
                if (!LibsndfileCommandUtilities.IsValidResult(IntPtr.Zero, LibsndfileCommand.GetSimpleFormat, retval))
                    throw new LibsndfileException(string.Format("Unable to retrieve format info for {0}.", format));

                return m_Marshaller.MemoryHandleTo<LibsndfileFormatInfo>(memory);
            }
        }

        /// <summary>
        /// Retrieve information about the given <paramref name="format"/> major/subtype format.
        /// </summary>
        /// <param name="format">Major or subtype format to retrieve information about.</param>
        /// <returns><see cref="LibsndfileFormatInfo"/> object containing format information.</returns>
        public LibsndfileFormatInfo GetFormatInfo(LibsndfileFormat format)
        {
            var formatInfo = new LibsndfileFormatInfo { Format = format };
            using (var memory = m_Marshaller.Allocate(formatInfo))
            {
                var retval = m_Api.Command(IntPtr.Zero, LibsndfileCommand.GetFormatInfo, memory, memory.Size);
                if (!LibsndfileCommandUtilities.IsValidResult(IntPtr.Zero, LibsndfileCommand.GetFormatInfo, retval))
                    throw new LibsndfileException(string.Format("Unable to retrieve format info for {0}.", format));

                return m_Marshaller.MemoryHandleTo<LibsndfileFormatInfo>(memory);
            }
        }

        /// <summary>
        /// Returns the number of major formats supported by Libsndfile.
        /// </summary>
        /// <returns>Number of major formats supported.</returns>
        public int GetFormatMajorCount()
        {
            using (var memory = m_Marshaller.Allocate<int>())
            {
                var retval = m_Api.Command(IntPtr.Zero, LibsndfileCommand.GetFormatMajorCount, memory, memory.Size);
                if (!LibsndfileCommandUtilities.IsValidResult(IntPtr.Zero, LibsndfileCommand.GetFormatMajorCount, retval))
                    throw new LibsndfileException("Unable to retrieve number of supported major formats.");

                return m_Marshaller.MemoryHandleTo<int>(memory);
            }
        }

        /// <summary>
        /// Retrieves information about the given <paramref name="format"/> major type.
        /// </summary>
        /// <param name="format">Major format to retrieve information about.</param>
        /// <returns><see cref="LibsndfileFormatInfo"/> object containing format information.</returns>
        public LibsndfileFormatInfo GetFormatMajor(LibsndfileFormat format)
        {
            var formatInfo = new LibsndfileFormatInfo { Format = format };
            using (var memory = m_Marshaller.Allocate(formatInfo))
            {
                var retval = m_Api.Command(IntPtr.Zero, LibsndfileCommand.GetFormatMajor, memory, memory.Size);
                if (!LibsndfileCommandUtilities.IsValidResult(IntPtr.Zero, LibsndfileCommand.GetFormatMajor, retval))
                    throw new LibsndfileException(string.Format("Unable to retrieve format info for {0}.", format));

                return m_Marshaller.MemoryHandleTo<LibsndfileFormatInfo>(memory);
            }
        }

        /// <summary>
        /// Returns the number of available subformats.
        /// </summary>
        /// <returns>Number of subtype formats supported.</returns>
        public int GetFormatSubtypeCount()
        {
            using (var memory = m_Marshaller.Allocate<int>())
            {
                var retval = m_Api.Command(IntPtr.Zero, LibsndfileCommand.GetFormatSubtypeCount, memory, memory.Size);
                if (!LibsndfileCommandUtilities.IsValidResult(IntPtr.Zero, LibsndfileCommand.GetFormatSubtypeCount, retval))
                    throw new LibsndfileException("Unable to retrieve number of supported subtype formats.");

                return m_Marshaller.MemoryHandleTo<int>(memory);
            }
        }

        /// <summary>
        /// Retrieves information about the given <paramref name="format"/> subtype.
        /// </summary>
        /// <param name="format">Subtype format to retrieve information about.</param>
        /// <returns><see cref="LibsndfileFormatInfo"/> object containing format information.</returns>
        public LibsndfileFormatInfo GetFormatSubtype(LibsndfileFormat format)
        {
            var formatInfo = new LibsndfileFormatInfo { Format = format };
            using (var memory = m_Marshaller.Allocate(formatInfo))
            {
                var retval = m_Api.Command(IntPtr.Zero, LibsndfileCommand.GetFormatSubtype, memory, memory.Size);
                if (!LibsndfileCommandUtilities.IsValidResult(IntPtr.Zero, LibsndfileCommand.GetFormatSubtypeCount, retval))
                    throw new LibsndfileException(string.Format("Unable to retrieve format info for {0}.", format));

                return m_Marshaller.MemoryHandleTo<LibsndfileFormatInfo>(memory);
            }
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
            var retval = m_Api.Command(sndfile, LibsndfileCommand.SetAddPeakChunk, IntPtr.Zero, Convert.ToInt32(enable));
            if (!LibsndfileCommandUtilities.IsValidResult(sndfile, LibsndfileCommand.SetAddPeakChunk, retval))
                throw new LibsndfileException("Unable to set PEAK chunk for the given file.");

            return Convert.ToBoolean(retval);
        }

        /// <summary>
        /// Updates file header to reflect updated file information.
        /// </summary>
        /// <param name="sndfile">Audio file to update file header of.</param>
        public void UpdateHeaderNow(IntPtr sndfile)
        {
            var retval = m_Api.Command(sndfile, LibsndfileCommand.UpdateHeaderNow, IntPtr.Zero, 0);
            if (!LibsndfileCommandUtilities.IsValidResult(sndfile, LibsndfileCommand.UpdateHeaderNow, retval))
                throw new LibsndfileException("Unable to update header for the given file.");
        }
    }
}
