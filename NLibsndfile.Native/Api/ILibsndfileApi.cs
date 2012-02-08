using System;

namespace NLibsndfile.Native
{
    public interface ILibsndfileApi
    {
        IntPtr Open(string path, LibsndfileMode mode, ref LibsndfileInfo info);

        IntPtr OpenFileDescriptor(int handle, LibsndfileMode mode, ref LibsndfileInfo info, int closeHandle);
    }
}