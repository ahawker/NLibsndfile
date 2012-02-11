using System;

namespace NLibsndfile.Native
{
    public class LibsndfileApi : ILibsndfileApi
    {
        private readonly ILibsndfileApi m_Api;

        public LibsndfileApi()
            : this(new LibsndfileApiNativeWrapper())
        {
        }

        public LibsndfileApi(ILibsndfileApi api)
        {
            if (api == null)
                throw new ArgumentNullException("api");

            m_Api = api;
        }

        public IntPtr Open(string path, LibsndfileMode mode, ref LibsndfileInfo info)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException("path", "Path cannot be null/empty.");

            var sndfile = m_Api.Open(path, mode, ref info);

            if (sndfile == IntPtr.Zero)
                throw new LibsndfileException(string.Format("Unable to open file {0} in mode {1}.", path, mode));

            return sndfile;
        }

        public IntPtr OpenFileDescriptor(int handle, LibsndfileMode mode, ref LibsndfileInfo info, int closeHandle)
        {
            if (handle <= 0)
                throw new ArgumentOutOfRangeException("handle", "File handle cannot be zero/non-negative.");

            var sndfile = m_Api.OpenFileDescriptor(handle, mode, ref info, closeHandle);

            if (sndfile == IntPtr.Zero)
                throw new LibsndfileException(string.Format("Unable to open file descriptor {0} in mode {1}", handle, mode));

            return sndfile;
        }

        public LibsndfileError Close(IntPtr sndfile)
        {
            if (sndfile == IntPtr.Zero)
                throw new ArgumentException("File handle is invalid/closed.");

            var retval = m_Api.Close(sndfile);
            if (retval != LibsndfileError.NoError)
                throw new LibsndfileException(string.Format("Close returned error code {0}.", retval));

            return retval;
        }

        public int FormatCheck(ref LibsndfileInfo info)
        {
            return m_Api.FormatCheck(ref info);
        }

        public long Seek(IntPtr sndfile, long count, int whence)
        {
            if (sndfile == IntPtr.Zero)
                throw new ArgumentException("File handle is invalid/closed.");
            if (count == 0)
                throw new ArgumentOutOfRangeException("count", "Count must be positive.");
            if (whence < 0 || whence > 2)
                throw new ArgumentOutOfRangeException("whence", whence, "Whence must be between zero and two.");

            long offset = m_Api.Seek(sndfile, count, whence);
            if (offset == -1)
                throw new LibsndfileException("Seek failed.");

            return offset;
        }

        public void WriteSync(IntPtr sndfile)
        {
            if (sndfile == IntPtr.Zero)
                throw new ArgumentException("File handle is invalid/closed.");

            m_Api.WriteSync(sndfile);
        }

        public LibsndfileError SetString(IntPtr sndfile, LibsndfileStringType type, string value)
        {
            if (sndfile == IntPtr.Zero)
                throw new ArgumentException("File handle is invalid/closed.");
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException("value", "Value cannot be null/empty.");

            var retval = m_Api.SetString(sndfile, type, value);
            if (retval != LibsndfileError.NoError)
                throw new LibsndfileException(string.Format("SetString returned error code {0}.", retval));

            return retval;
        }

        public string GetString(IntPtr sndfile, LibsndfileStringType type)
        {
            if (sndfile == IntPtr.Zero)
                throw new ArgumentException("File handle is invalid/closed.");

            return m_Api.GetString(sndfile, type);
        }

        public long Read(IntPtr sndfile, short[] buffer, long items)
        {
            if (sndfile == IntPtr.Zero)
                throw new ArgumentException("File handle is invalid/closed.");
            if (buffer == null)
                throw new ArgumentNullException("buffer", "Buffer cannot be null.");
            if (buffer.Length == 0)
                throw new ArgumentNullException("buffer", "Buffer must be initialized.");
            if (items < 0)
                throw new ArgumentOutOfRangeException("items", items, "Items must be positive.");

            return m_Api.Read(sndfile, buffer, items);
        }

        public long Read(IntPtr sndfile, int[] buffer, long items)
        {
            if (sndfile == IntPtr.Zero)
                throw new ArgumentException("File handle is invalid/closed.");
            if (buffer == null)
                throw new ArgumentNullException("buffer", "Buffer cannot be null.");
            if (buffer.Length == 0)
                throw new ArgumentNullException("buffer", "Buffer must be initialized.");
            if (items < 0)
                throw new ArgumentOutOfRangeException("items", items, "Items must be positive.");

            return m_Api.Read(sndfile, buffer, items);
        }
    }
}