using System;

namespace NLibsndfile.Native
{
    /// <summary>
    /// Provides access to all libsndfile functionality.
    /// </summary>
    public class LibsndfileApi : ILibsndfileApi
    {
        private readonly ILibsndfileApi m_Api;

        /// <summary>
        /// Initializes a new instance of <c>LibsndfileApi</c> with the default native implementation.
        /// </summary>
        public LibsndfileApi()
            : this(new LibsndfileApiNativeWrapper())
        {
        }

        /// <summary>
        /// Initializes a new instance of <c>LibsndfileApi</c> with the <paramref name="api"/> implementation.
        /// </summary>
        /// <param name="api">LibsndfileApi implementation to use.</param>
        /// <remarks>
        /// This constructor should only be used for testing when simulating the actual libsndfile library.
        /// </remarks>
        internal LibsndfileApi(ILibsndfileApi api)
        {
            if (api == null)
                throw new ArgumentNullException("api");

            m_Api = api;
        }

        /// <summary>
        /// Attempts to open an audio file at the <paramref name="path"/> location 
        /// with <paramref name="mode"/> based file access.
        /// </summary>
        /// <param name="path">Fully qualified path to location of audio file.</param>
        /// <param name="mode">File access to use when opening this file. ReadItems/Write/ReadWrite.</param>
        /// <param name="info"><see cref="LibsndfileInfo"/> structure contains information about the file we are opening.</param>
        /// <returns>Returns pointer to an internal object used by libsndfile that we can interact with.</returns>
        public IntPtr Open(string path, LibsndfileMode mode, ref LibsndfileInfo info)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException("path", "Path cannot be null/empty.");

            var sndfile = m_Api.Open(path, mode, ref info);

            if (sndfile == IntPtr.Zero)
                throw new LibsndfileException(string.Format("Unable to open file {0} in mode {1}.", path, mode));

            return sndfile;
        }

        /// <summary>
        /// Attempts to open an audio file with the <paramref name="handle"/> file descriptor 
        /// using <paramref name="mode"/> based file access.
        /// </summary>
        /// <param name="handle">File descriptor handle</param>
        /// <param name="mode">File access to use when opening this file. ReadItems/Write/ReadWrite</param>
        /// <param name="info"><see cref="LibsndfileInfo"/> structure contains information about the file we are opening.</param>
        /// <param name="closeHandle">Decide if we want libsndfile to close the file descriptor for us.</param>
        /// <returns>Returns pointer to an internal object used by libsndfile that we can interact with.</returns>
        public IntPtr OpenFileDescriptor(int handle, LibsndfileMode mode, ref LibsndfileInfo info, int closeHandle)
        {
            if (handle <= 0)
                throw new ArgumentOutOfRangeException("handle", "File handle cannot be zero/non-negative.");

            var sndfile = m_Api.OpenFileDescriptor(handle, mode, ref info, closeHandle);

            if (sndfile == IntPtr.Zero)
                throw new LibsndfileException(string.Format("Unable to open file descriptor {0} in mode {1}", handle, mode));

            return sndfile;
        }

        /// <summary>
        /// Closes the <paramref name="sndfile"/> audio file.
        /// </summary>
        /// <param name="sndfile">Audio file we want to close.</param>
        /// <returns><see cref="LibsndfileError"/> error code.</returns>
        public LibsndfileError Close(IntPtr sndfile)
        {
            if (sndfile == IntPtr.Zero)
                throw new ArgumentException("File handle is invalid/closed.");

            var retval = m_Api.Close(sndfile);
            if (retval != LibsndfileError.NoError)
                throw new LibsndfileException(string.Format("Close returned error code {0}.", retval));

            return retval;
        }

        /// <summary>
        /// Check to see if the parameters in the <paramref name="info"/> struct are
        /// valid and supported by libsndfile.
        /// </summary>
        /// <param name="info"><see cref="LibsndfileInfo"/> struct contains information about a target file.</param>
        /// <returns>Returns TRUE if the parameters are valid, FALSE otherwise.</returns>
        public int FormatCheck(ref LibsndfileInfo info)
        {
            return m_Api.FormatCheck(ref info);
        }

        /// <summary>
        /// Attempts to move the read/write data pointers to a specific location
        /// specified by the <paramref name="whence"/> and <paramref name="count"/> values
        /// in the <paramref name="sndfile"/> audio file.
        /// 
        /// Whence values can be the following:
        ///     0 - SEEK_SET  - The offset is set to the start of the audio data plus offset (multichannel) frames.
        ///     1 - SEEK_CUR  - The offset is set to its current location plus offset (multichannel) frames.
        ///     2 - SEEK_END  - The offset is set to the end of the data plus offset (multichannel) frames.
        ///     
        /// If the <paramref name="sndfile"/> audio file was opened in ReadWrite mode, the whence parameter
        /// can be bit-wise OR'd with <see cref="LibsndfileMode"/> SFM_READ or SFM_WRITE values to modify each pointer
        /// separately.
        /// </summary>
        /// <param name="sndfile">Audio file we wish to seek in.</param>
        /// <param name="count">Number of multichannel frames to offset from our <paramref name="whence"/> position.</param>
        /// <param name="whence">The position where our seek offset begins.</param>
        /// <returns>Returns offset in multichannel frames from the beginning of the audio file.</returns>
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

        /// <summary>
        /// Forces operating system to write buffers to disk. Only works if <paramref name="sndfile"/> is
        /// opened in <see cref="LibsndfileMode"/> SFM_WRITE or SFM_RDWR.
        /// </summary>
        /// <param name="sndfile">Audio file you wish to flush buffers on.</param>
        public void WriteSync(IntPtr sndfile)
        {
            if (sndfile == IntPtr.Zero)
                throw new ArgumentException("File handle is invalid/closed.");

            m_Api.WriteSync(sndfile);
        }

        /// <summary>
        /// Writes the <paramref name="value"/> to the ID3 tag of <paramref name="type"/> 
        /// in the <paramref name="sndfile"/> audio file.
        /// </summary>
        /// <param name="sndfile">Audio file to write tags to.</param>
        /// <param name="type"><see cref="LibsndfileStringType"/> tag to change.</param>
        /// <param name="value">New value of <see cref="LibsndfileStringType"/> tag.</param>
        /// <returns>Returns an <see cref="LibsndfileError"/> error code.</returns>
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

        /// <summary>
        /// Reads the <paramref name="type"/> tag from the <paramref name="sndfile"/> audio file.
        /// </summary>
        /// <param name="sndfile">Audio file to read tags from.</param>
        /// <param name="type"><see cref="LibsndfileStringType"/> tag to read.</param>
        /// <returns>Returns the value of the <paramref name="type"/> tag.</returns>
        public string GetString(IntPtr sndfile, LibsndfileStringType type)
        {
            if (sndfile == IntPtr.Zero)
                throw new ArgumentException("File handle is invalid/closed.");

            return m_Api.GetString(sndfile, type);
        }

        /// <summary>
        /// ReadItems <paramref name="items"/> from the <paramref name="sndfile"/> audio file into the audio
        /// <paramref name="buffer"/>. Items must be a product of the # of channels for
        /// the <paramref name="sndfile"/>. 
        /// </summary>
        /// <param name="sndfile">Audio file to read from.</param>
        /// <param name="buffer">Buffer to fill.</param>
        /// <param name="items">Number of items to put in the <paramref name="buffer"/>.</param>
        /// <returns>Returns the number of items read. Should be equal to <paramref name="items"/> unless
        /// you've reached EOF.</returns>
        public long ReadItems(IntPtr sndfile, short[] buffer, long items)
        {
            if (sndfile == IntPtr.Zero)
                throw new ArgumentException("File handle is invalid/closed.");
            if (buffer == null)
                throw new ArgumentNullException("buffer", "Buffer cannot be null.");
            if (buffer.Length == 0)
                throw new ArgumentNullException("buffer", "Buffer must be initialized.");
            if (items < 0)
                throw new ArgumentOutOfRangeException("items", items, "Items must be positive.");

            return m_Api.ReadItems(sndfile, buffer, items);
        }

        /// <summary>
        /// ReadItems <paramref name="items"/> from the <paramref name="sndfile"/> audio file into the audio
        /// <paramref name="buffer"/>. Items must be a product of the # of channels for
        /// the <paramref name="sndfile"/>. 
        /// </summary>
        /// <param name="sndfile">Audio file to read from.</param>
        /// <param name="buffer">Buffer to fill.</param>
        /// <param name="items">Number of items to put in the <paramref name="buffer"/>.</param>
        /// <returns>Returns the number of items read. Should be equal to <paramref name="items"/> unless
        /// you've reached EOF.</returns>
        public long ReadItems(IntPtr sndfile, int[] buffer, long items)
        {
            if (sndfile == IntPtr.Zero)
                throw new ArgumentException("File handle is invalid/closed.");
            if (buffer == null)
                throw new ArgumentNullException("buffer", "Buffer cannot be null.");
            if (buffer.Length == 0)
                throw new ArgumentNullException("buffer", "Buffer must be initialized.");
            if (items < 0)
                throw new ArgumentOutOfRangeException("items", items, "Items must be positive.");

            return m_Api.ReadItems(sndfile, buffer, items);
        }

        /// <summary>
        /// ReadItems <paramref name="items"/> from the <paramref name="sndfile"/> audio file into the audio
        /// <paramref name="buffer"/>. Items must be a product of the # of channels for
        /// the <paramref name="sndfile"/>. 
        /// </summary>
        /// <param name="sndfile">Audio file to read from.</param>
        /// <param name="buffer">Buffer to fill.</param>
        /// <param name="items">Number of items to put in the <paramref name="buffer"/>.</param>
        /// <returns>Returns the number of items read. Should be equal to <paramref name="items"/> unless
        /// you've reached EOF.</returns>
        public long ReadItems(IntPtr sndfile, float[] buffer, long items)
        {
            if (sndfile == IntPtr.Zero)
                throw new ArgumentException("File handle is invalid/closed.");
            if (buffer == null)
                throw new ArgumentNullException("buffer", "Buffer cannot be null.");
            if (buffer.Length == 0)
                throw new ArgumentNullException("buffer", "Buffer must be initialized.");
            if (items < 0)
                throw new ArgumentOutOfRangeException("items", items, "Items must be positive.");

            return m_Api.ReadItems(sndfile, buffer, items);
        }

        /// <summary>
        /// ReadItems <paramref name="items"/> from the <paramref name="sndfile"/> audio file into the audio
        /// <paramref name="buffer"/>. Items must be a product of the # of channels for
        /// the <paramref name="sndfile"/>. 
        /// </summary>
        /// <param name="sndfile">Audio file to read from.</param>
        /// <param name="buffer">Buffer to fill.</param>
        /// <param name="items">Number of items to put in the <paramref name="buffer"/>.</param>
        /// <returns>Returns the number of items read. Should be equal to <paramref name="items"/> unless
        /// you've reached EOF.</returns>
        public long ReadItems(IntPtr sndfile, double[] buffer, long items)
        {
            if (sndfile == IntPtr.Zero)
                throw new ArgumentException("File handle is invalid/closed.");
            if (buffer == null)
                throw new ArgumentNullException("buffer", "Buffer cannot be null.");
            if (buffer.Length == 0)
                throw new ArgumentNullException("buffer", "Buffer must be initialized.");
            if (items < 0)
                throw new ArgumentOutOfRangeException("items", items, "Items must be positive.");

            return m_Api.ReadItems(sndfile, buffer, items);
        }

        /// <summary>
        /// ReadItems <paramref name="frames"/> from the <paramref name="sndfile"/> audio file into the audio
        /// <paramref name="buffer"/>.
        /// </summary>
        /// <param name="sndfile">Audio file to read from.</param>
        /// <param name="buffer">Buffer to fill.</param>
        /// <param name="frames">Number of frames to put in the <paramref name="buffer"/>.</param>
        /// <returns>Returns the number of frames read. Should be equal to <paramref name="frames"/> unless
        /// you've reached EOF.</returns>
        public long ReadFrames(IntPtr sndfile, short[] buffer, long frames)
        {
            if (sndfile == IntPtr.Zero)
                throw new ArgumentException("File handle is invalid/closed.");
            if (buffer == null)
                throw new ArgumentNullException("buffer", "Buffer cannot be null.");
            if (buffer.Length == 0)
                throw new ArgumentNullException("buffer", "Buffer must be initialized.");
            if (frames < 0)
                throw new ArgumentOutOfRangeException("frames", frames, "Frames must be positive.");

            return m_Api.ReadFrames(sndfile, buffer, frames);
        }

        /// <summary>
        /// Read <paramref name="frames"/> from the <paramref name="sndfile"/> audio file into the audio
        /// <paramref name="buffer"/>.
        /// </summary>
        /// <param name="sndfile">Audio file to read from.</param>
        /// <param name="buffer">Buffer to fill.</param>
        /// <param name="frames">Number of frames to put in the <paramref name="buffer"/>.</param>
        /// <returns>Returns the number of frames read. Should be equal to <paramref name="frames"/> unless
        /// you've reached EOF.</returns>
        public long ReadFrames(IntPtr sndfile, int[] buffer, long frames)
        {
            if (sndfile == IntPtr.Zero)
                throw new ArgumentException("File handle is invalid/closed.");
            if (buffer == null)
                throw new ArgumentNullException("buffer", "Buffer cannot be null.");
            if (buffer.Length == 0)
                throw new ArgumentNullException("buffer", "Buffer must be initialized.");
            if (frames < 0)
                throw new ArgumentOutOfRangeException("frames", frames, "Frames must be positive.");

            return m_Api.ReadFrames(sndfile, buffer, frames);
        }

        /// <summary>
        /// Read <paramref name="frames"/> from the <paramref name="sndfile"/> audio file into the audio
        /// <paramref name="buffer"/>.
        /// </summary>
        /// <param name="sndfile">Audio file to read from.</param>
        /// <param name="buffer">Buffer to fill.</param>
        /// <param name="frames">Number of frames to put in the <paramref name="buffer"/>.</param>
        /// <returns>Returns the number of frames read. Should be equal to <paramref name="frames"/> unless
        /// you've reached EOF.</returns>
        public long ReadFrames(IntPtr sndfile, float[] buffer, long frames)
        {
            if (sndfile == IntPtr.Zero)
                throw new ArgumentException("File handle is invalid/closed.");
            if (buffer == null)
                throw new ArgumentNullException("buffer", "Buffer cannot be null.");
            if (buffer.Length == 0)
                throw new ArgumentNullException("buffer", "Buffer must be initialized.");
            if (frames < 0)
                throw new ArgumentOutOfRangeException("frames", frames, "Frames must be positive.");

            return m_Api.ReadFrames(sndfile, buffer, frames);
        }

        /// <summary>
        /// Read <paramref name="frames"/> from the <paramref name="sndfile"/> audio file into the audio
        /// <paramref name="buffer"/>.
        /// </summary>
        /// <param name="sndfile">Audio file to read from.</param>
        /// <param name="buffer">Buffer to fill.</param>
        /// <param name="frames">Number of frames to put in the <paramref name="buffer"/>.</param>
        /// <returns>Returns the number of frames read. Should be equal to <paramref name="frames"/> unless
        /// you've reached EOF.</returns>
        public long ReadFrames(IntPtr sndfile, double[] buffer, long frames)
        {
            if (sndfile == IntPtr.Zero)
                throw new ArgumentException("File handle is invalid/closed.");
            if (buffer == null)
                throw new ArgumentNullException("buffer", "Buffer cannot be null.");
            if (buffer.Length == 0)
                throw new ArgumentNullException("buffer", "Buffer must be initialized.");
            if (frames < 0)
                throw new ArgumentOutOfRangeException("frames", frames, "Frames must be positive.");

            return m_Api.ReadFrames(sndfile, buffer, frames);
        }

        /// <summary>
        /// Write <paramref name="items"/> from the <paramref name="buffer"/> into the audio <paramref name="sndfile"/>.
        /// </summary>
        /// <param name="sndfile">Audio file to write to.</param>
        /// <param name="buffer">Buffer to write from.</param>
        /// <param name="items">Number of items to read from the <paramref name="buffer"/>.</param>
        /// <returns>Returns the number of items written. Should be equal to <paramref name="items"/> unless
        /// you've reached EOF.</returns>
        public long WriteItems(IntPtr sndfile, short[] buffer, long items)
        {
            if (sndfile == IntPtr.Zero)
                throw new ArgumentException("File handle is invalid/closed.");
            if (buffer == null)
                throw new ArgumentNullException("buffer", "Buffer cannot be null.");
            if (buffer.Length == 0)
                throw new ArgumentNullException("buffer", "Buffer must be initialized.");
            if (items < 0)
                throw new ArgumentOutOfRangeException("items", items, "Items must be positive.");

            return m_Api.WriteItems(sndfile, buffer, items);
        }

        /// <summary>
        /// Write <paramref name="items"/> from the <paramref name="buffer"/> into the audio <paramref name="sndfile"/>.
        /// </summary>
        /// <param name="sndfile">Audio file to write to.</param>
        /// <param name="buffer">Buffer to write from.</param>
        /// <param name="items">Number of items to read from the <paramref name="buffer"/>.</param>
        /// <returns>Returns the number of items written. Should be equal to <paramref name="items"/> unless
        /// you've reached EOF.</returns>
        public long WriteItems(IntPtr sndfile, int[] buffer, long items)
        {
            if (sndfile == IntPtr.Zero)
                throw new ArgumentException("File handle is invalid/closed.");
            if (buffer == null)
                throw new ArgumentNullException("buffer", "Buffer cannot be null.");
            if (buffer.Length == 0)
                throw new ArgumentNullException("buffer", "Buffer must be initialized.");
            if (items < 0)
                throw new ArgumentOutOfRangeException("items", items, "Items must be positive.");

            return m_Api.WriteItems(sndfile, buffer, items);
        }

        /// <summary>
        /// Write <paramref name="items"/> from the <paramref name="buffer"/> into the audio <paramref name="sndfile"/>.
        /// </summary>
        /// <param name="sndfile">Audio file to write to.</param>
        /// <param name="buffer">Buffer to write from.</param>
        /// <param name="items">Number of items to read from the <paramref name="buffer"/>.</param>
        /// <returns>Returns the number of items written. Should be equal to <paramref name="items"/> unless
        /// you've reached EOF.</returns>
        public long WriteItems(IntPtr sndfile, float[] buffer, long items)
        {
            if (sndfile == IntPtr.Zero)
                throw new ArgumentException("File handle is invalid/closed.");
            if (buffer == null)
                throw new ArgumentNullException("buffer", "Buffer cannot be null.");
            if (buffer.Length == 0)
                throw new ArgumentNullException("buffer", "Buffer must be initialized.");
            if (items < 0)
                throw new ArgumentOutOfRangeException("items", items, "Items must be positive.");

            return m_Api.WriteItems(sndfile, buffer, items);
        }

        /// <summary>
        /// Write <paramref name="items"/> from the <paramref name="buffer"/> into the audio <paramref name="sndfile"/>.
        /// </summary>
        /// <param name="sndfile">Audio file to write to.</param>
        /// <param name="buffer">Buffer to write from.</param>
        /// <param name="items">Number of items to read from the <paramref name="buffer"/>.</param>
        /// <returns>Returns the number of items written. Should be equal to <paramref name="items"/> unless
        /// you've reached EOF.</returns>
        public long WriteItems(IntPtr sndfile, double[] buffer, long items)
        {
            if (sndfile == IntPtr.Zero)
                throw new ArgumentException("File handle is invalid/closed.");
            if (buffer == null)
                throw new ArgumentNullException("buffer", "Buffer cannot be null.");
            if (buffer.Length == 0)
                throw new ArgumentNullException("buffer", "Buffer must be initialized.");
            if (items < 0)
                throw new ArgumentOutOfRangeException("items", items, "Items must be positive.");

            return m_Api.WriteItems(sndfile, buffer, items);
        }

        /// <summary>
        /// Write <paramref name="frames"/> from the <paramref name="buffer"/> into the audio <paramref name="sndfile"/>.
        /// </summary>
        /// <param name="sndfile">Audio file to write to.</param>
        /// <param name="buffer">Buffer to write from.</param>
        /// <param name="frames">Number of frames to read from the <paramref name="buffer"/>.</param>
        /// <returns>Returns the number of frames written. Should be equal to <paramref name="frames"/> unless
        /// you've reached EOF.</returns>
        public long WriteFrames(IntPtr sndfile, short[] buffer, long frames)
        {
            if (sndfile == IntPtr.Zero)
                throw new ArgumentException("File handle is invalid/closed.");
            if (buffer == null)
                throw new ArgumentNullException("buffer", "Buffer cannot be null.");
            if (buffer.Length == 0)
                throw new ArgumentNullException("buffer", "Buffer must be initialized.");
            if (frames < 0)
                throw new ArgumentOutOfRangeException("frames", frames, "Frames must be positive.");

            return m_Api.WriteFrames(sndfile, buffer, frames);
        }

        /// <summary>
        /// Write <paramref name="frames"/> from the <paramref name="buffer"/> into the audio <paramref name="sndfile"/>.
        /// </summary>
        /// <param name="sndfile">Audio file to write to.</param>
        /// <param name="buffer">Buffer to write from.</param>
        /// <param name="frames">Number of frames to read from the <paramref name="buffer"/>.</param>
        /// <returns>Returns the number of frames written. Should be equal to <paramref name="frames"/> unless
        /// you've reached EOF.</returns>
        public long WriteFrames(IntPtr sndfile, int[] buffer, long frames)
        {
            if (sndfile == IntPtr.Zero)
                throw new ArgumentException("File handle is invalid/closed.");
            if (buffer == null)
                throw new ArgumentNullException("buffer", "Buffer cannot be null.");
            if (buffer.Length == 0)
                throw new ArgumentNullException("buffer", "Buffer must be initialized.");
            if (frames < 0)
                throw new ArgumentOutOfRangeException("frames", frames, "Frames must be positive.");

            return m_Api.WriteFrames(sndfile, buffer, frames);
        }

        /// <summary>
        /// Write <paramref name="frames"/> from the <paramref name="buffer"/> into the audio <paramref name="sndfile"/>.
        /// </summary>
        /// <param name="sndfile">Audio file to write to.</param>
        /// <param name="buffer">Buffer to write from.</param>
        /// <param name="frames">Number of frames to read from the <paramref name="buffer"/>.</param>
        /// <returns>Returns the number of frames written. Should be equal to <paramref name="frames"/> unless
        /// you've reached EOF.</returns>
        public long WriteFrames(IntPtr sndfile, float[] buffer, long frames)
        {
            if (sndfile == IntPtr.Zero)
                throw new ArgumentException("File handle is invalid/closed.");
            if (buffer == null)
                throw new ArgumentNullException("buffer", "Buffer cannot be null.");
            if (buffer.Length == 0)
                throw new ArgumentNullException("buffer", "Buffer must be initialized.");
            if (frames < 0)
                throw new ArgumentOutOfRangeException("frames", frames, "Frames must be positive.");

            return m_Api.WriteFrames(sndfile, buffer, frames);
        }
    }
}