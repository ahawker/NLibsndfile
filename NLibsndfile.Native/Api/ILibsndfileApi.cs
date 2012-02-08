using System;

namespace NLibsndfile.Native
{
    public interface ILibsndfileApi
    {
         IntPtr sf_open(string path, LibsndfileMode mode, ref LibsndfileInfo info);
    }
}