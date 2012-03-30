using System;
using NUnit.Framework;

namespace NLibsndfile.Native.Tests.Integration
{
    internal static class TestConfiguration
    {
        internal const string ValidWavFile = "White_Noise.wav";

        internal static LibsndfileInfo GetValidWavFileInfo()
        {
            return new LibsndfileInfo
            {
                Format = LibsndfileFormat.Wav | LibsndfileFormat.Pcm16,
                Channels = 1,
                SampleRate = 44100
            };
        }
    }
}