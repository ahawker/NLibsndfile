﻿using System;

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
    }
}