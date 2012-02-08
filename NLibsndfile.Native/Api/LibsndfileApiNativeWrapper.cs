using System;
using System.Runtime.InteropServices;

namespace NLibsndfile.Native
{
    public class LibsndfileApiNativeWrapper : ILibsndfileApi
    {
        public IntPtr Open(string path, LibsndfileMode mode, ref LibsndfileInfo info)
        {
            return LibsndfileApiNative.sf_open(path, mode, ref info);
        }

        public IntPtr OpenFileDescriptor(int handle, LibsndfileMode mode, ref LibsndfileInfo info, int closeHandle)
        {
            return LibsndfileApiNative.sf_open_fd(handle, mode, ref info, closeHandle);
        }

        public LibsndfileError Close(IntPtr sndfile)
        {
            return LibsndfileApiNative.sf_close(sndfile);
        }

        public int FormatCheck(ref LibsndfileInfo info)
        {
            return LibsndfileApiNative.sf_format_check(ref info);
        }

        public long Seek(IntPtr sndfile, long count, int whence)
        {
            return LibsndfileApiNative.sf_seek(sndfile, count, whence);
        }

        public void WriteSync(IntPtr sndfile)
        {
            LibsndfileApiNative.sf_write_sync(sndfile);
        }

        public LibsndfileError SetString(IntPtr sndfile, LibsndfileStringType type, string value)
        {
            return LibsndfileApiNative.sf_set_string(sndfile, type, value);
        }

        public string GetString(IntPtr sndfile, LibsndfileStringType type)
        {
            var ptr = LibsndfileApiNative.sf_get_String(sndfile, type);
            return Marshal.PtrToStringAnsi(ptr) ?? string.Empty;
        }
    }
}