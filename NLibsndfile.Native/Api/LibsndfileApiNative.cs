using System;
using System.Security;
using System.Runtime.InteropServices;

namespace NLibsndfile.Native
{
    [SuppressUnmanagedCodeSecurity]
    internal static class LibsndfileApiNative
    {
        [DllImport(DllImports.Libsndfile)]
        internal static extern IntPtr sf_open(string path, LibsndfileMode mode, ref LibsndfileInfo info);

        [DllImport(DllImports.Libsndfile)]
        internal static extern IntPtr sf_open_fd(int handle, LibsndfileMode mode, ref LibsndfileInfo info, int closeHandle);

        [DllImport(DllImports.Libsndfile)]
        internal static extern int sf_format_check(ref LibsndfileInfo info);

        [DllImport(DllImports.Libsndfile)]
        internal static extern long sf_seek(IntPtr sndfile, long count, int whence);
    }
}