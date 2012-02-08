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
    }
}