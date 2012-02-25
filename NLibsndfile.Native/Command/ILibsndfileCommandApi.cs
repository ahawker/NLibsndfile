using System;

namespace NLibsndfile.Native
{
    /// <summary>
    /// Interface to public Libsndfile Command API.
    /// </summary>
    public interface ILibsndfileCommandApi
    {
        /// <summary>
        /// Returns the version of the Libsndfile library.
        /// </summary>
        /// <returns>Libsndfile library version.</returns>
        string GetLibVersion();

        /// <summary>
        /// Returns the internal Libsndfile log generated when loading a file.
        /// </summary>
        /// <param name="sndfile">Audio file we want the log for.</param>
        /// <returns>Libsndfile log info.</returns>
        string GetLogInfo(IntPtr sndfile);

        /// <summary>
        /// Scan <paramref name="sndfile"/> file and return maximum calculated signal value. 
        /// </summary>
        /// <param name="sndfile">Audio file we want to scan.</param>
        /// <returns>Maximum signal value.</returns>
        double CalcSignalMax(IntPtr sndfile);

        /// <summary>
        /// Scan <paramref name="sndfile"/> file and return normalized maximum calculated signal value.
        /// </summary>
        /// <param name="sndfile">Audio file we want to scan.</param>
        /// <returns>Normalized maximum signal value.</returns>
        double CalcNormSignalMax(IntPtr sndfile);

        /// <summary>
        /// Scan <paramref name="sndfile"/> file and return single peak value for each channel.
        /// </summary>
        /// <param name="sndfile">Audio file we want to scan.</param>
        /// <param name="channels">Number of audio channels in the audio file.</param>
        /// <returns>Peak values for each channel.</returns>
        double[] CalcMaxAllChannels(IntPtr sndfile, int channels);

        /// <summary>
        /// Scan <paramref name="sndfile"/> file and return normalized peak value for each channel. 
        /// </summary>
        /// <param name="sndfile">Audio file we want to scan.</param>
        /// <param name="channels">Number of audio channels in the audio file.</param>
        /// <returns>Normalized peak values for each channel.</returns>
        double[] CalcNormMaxAllChannels(IntPtr sndfile, int channels);

        /// <summary>
        /// Retrieve the peak value for the file as stored in the file header.
        /// </summary>
        /// <param name="sndfile">Audio file we want to examine.</param>
        /// <returns>Peak value from file header.</returns>
        double GetSignalMax(IntPtr sndfile);

        /// <summary>
        /// Retrieve the peak value for each channel for the file as stored in the file header.
        /// </summary>
        /// <param name="sndfile">Audio file we want to examine.</param>
        /// <param name="channels">Number of audio channels in the audio file.</param>
        /// <returns>Peak values for each channel from file header.</returns>
        double[] GetMaxAllChannels(IntPtr sndfile, int channels);

        /// <summary>
        /// Sets float normalization for read and write functions on the <paramref name="sndfile"/> file.
        /// </summary>
        /// <param name="sndfile">Audio file to set float normalization on.</param>
        /// <param name="normalize">Flag to enable or disable float normalization.</param>
        /// <returns>Previous float normalization state.</returns>
        bool SetNormFloat(IntPtr sndfile, bool normalize);

        /// <summary>
        /// Sets double normalization for read and write functions on the <paramref name="sndfile"/> file.
        /// </summary>
        /// <param name="sndfile">Audio file to set double normalization on.</param>
        /// <param name="normalize">Flag to enable or disable double normalization.</param>
        /// <returns>Previous double normalization state.</returns>
        bool SetNormDouble(IntPtr sndfile, bool normalize);

        /// <summary>
        /// Gets float normalization for read and write functions on the <paramref name="sndfile"/> file.
        /// </summary>
        /// <param name="sndfile">Audio file to retrieve float normalization for.</param>
        /// <returns>Current float normalization state.</returns>
        bool GetNormFloat(IntPtr sndfile);

        /// <summary>
        /// Gets double normalization for read and write functions on the <paramref name="sndfile"/> file.
        /// </summary>
        /// <param name="sndfile">Audio file to retrieve double normalization for.</param>
        /// <returns>Current double normalization state.</returns>
        bool GetNormDouble(IntPtr sndfile);

        /// <summary>
        /// Sets the scale factor for when integer data is read from the <paramref name="sndfile"/>
        /// which contains floating point data.
        /// </summary>
        /// <param name="sndfile">Audio file to set int to float scaling.</param>
        /// <param name="enable">Flag to enable or disable int to float scaling.</param>
        /// <returns>Previous scaling state.</returns>
        bool SetScaleFloatIntRead(IntPtr sndfile, bool enable);

        /// <summary>
        /// Sets the scale factor for when integer data is written from the <paramref name="sndfile"/>
        /// as floating point data.
        /// </summary>
        /// <param name="sndfile">Audio file to set float to int scaling.</param>
        /// <param name="enable">Flag to enable or disable float to int scaling.</param>
        /// <returns>Previous scaling state.</returns>
        bool SetScaleIntFloatWrite(IntPtr sndfile, bool enable);

        /// <summary>
        /// Returns the number of simple formats supported by Libsndfile.
        /// </summary>
        /// <returns>Number of simple formats supported.</returns>
        int GetSimpleFormatCount();

        /// <summary>
        /// Retrieves information about the given <paramref name="format"/>.
        /// </summary>
        /// <param name="format">Format to retrieve information about.</param>
        /// <returns><see cref="LibsndfileFormatInfo"/> object containing format information.</returns>
        LibsndfileFormatInfo GetSimpleFormat(LibsndfileFormat format);

        /// <summary>
        /// Retrieve information about the given <paramref name="format"/> major/subtype format.
        /// </summary>
        /// <param name="format">Major or subtype format to retrieve information about.</param>
        /// <returns><see cref="LibsndfileFormatInfo"/> object containing format information.</returns>
        LibsndfileFormatInfo GetFormatInfo(LibsndfileFormat format);

        /// <summary>
        /// Returns the number of major formats supported by Libsndfile.
        /// </summary>
        /// <returns>Number of major formats supported.</returns>
        int GetFormatMajorCount();
    }
}