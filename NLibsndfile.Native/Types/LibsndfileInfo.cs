using System.Runtime.InteropServices;

namespace NLibsndfile.Native
{
    [StructLayout(LayoutKind.Sequential)]
    public struct LibsndfileInfo
    {
        public long Frames;
        public int SampleRate;
        public int Channels;
        public LibsndfileFormat Format;
        public int Sections;
        public int Seekable;
    }
}