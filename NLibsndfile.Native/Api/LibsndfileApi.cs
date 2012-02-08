using System;

namespace NLibsndfile.Native
{
    public class LibsndfileApi : ILibsndfileApi
    {
        public IntPtr Open(string path, LibsndfileMode mode, ref LibsndfileInfo info)
        {
            return LibsndfileApiNative.sf_open(path, mode, ref info);
        }

        public IntPtr OpenFileDescriptor(int handle, LibsndfileMode mode, ref LibsndfileInfo info, int closeHandle)
        {
            return LibsndfileApiNative.sf_open_fd(handle, mode, ref info, closeHandle);
        }
    }
}