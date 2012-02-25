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
        /// <returns>Normalized Peak values for each channel.</returns>
        double[] CalcNormMaxAllChannels(IntPtr sndfile, int channels);

        /// <summary>
        /// Retrieve the peak value for the file as stored in the file header.
        /// </summary>
        /// <param name="sndfile">Audio file we want to examine.</param>
        /// <returns>Peak value from file header.</returns>
        double GetSignalMax(IntPtr sndfile);
    }
}