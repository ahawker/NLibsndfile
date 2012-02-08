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
            return m_Api.Open(path, mode, ref info);
        }

        public IntPtr OpenFileDescriptor(int handle, LibsndfileMode mode, ref LibsndfileInfo info, int closeHandle)
        {
            return m_Api.OpenFileDescriptor(handle, mode, ref info, closeHandle);
        }

        public int FormatCheck(ref LibsndfileInfo info)
        {
            return m_Api.FormatCheck(ref info);
        }
    }
}