using System;

namespace NLibsndfile.Native
{
    public class LibsndfileApi : ILibsndfileApi
    {
        public IntPtr sf_open(string path, LibsndfileMode mode, ref LibsndfileInfo info)
        {
            return LibsndfileApiNative.sf_open(path, mode, ref info);
        }
    }
}