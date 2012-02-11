using System;

namespace NLibsndfile.Native
{
    public interface ILibsndfileApi
    {
        IntPtr Open(string path, LibsndfileMode mode, ref LibsndfileInfo info);

        IntPtr OpenFileDescriptor(int handle, LibsndfileMode mode, ref LibsndfileInfo info, int closeHandle);

        LibsndfileError Close(IntPtr sndfile);

        int FormatCheck(ref LibsndfileInfo info);

        long Seek(IntPtr sndfile, long count, int whence);

        void WriteSync(IntPtr sndfile);

        LibsndfileError SetString(IntPtr sndfile, LibsndfileStringType type, string value);

        string GetString(IntPtr sndfile, LibsndfileStringType type);

        long Read(IntPtr sndfile, short[] buffer, long items);
        long Read(IntPtr sndfile, int[] buffer, long items);
    }
}