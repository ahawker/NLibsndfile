using System;
using System.Runtime.InteropServices;

namespace NLibsndfile.Native
{
    /// <summary>
    /// Provides native API access and marshalling by forwarding calls to P/Invoke LibsndfileApiNative static class.
    /// </summary>
    public class LibsndfileApiNativeWrapper : ILibsndfileApi
    {
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
            return LibsndfileApiNative.sf_open(path, mode, ref info);
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
            return LibsndfileApiNative.sf_open_fd(handle, mode, ref info, closeHandle);
        }

        /// <summary>
        /// Closes the <paramref name="sndfile"/> audio file.
        /// </summary>
        /// <param name="sndfile">Audio file we want to close.</param>
        /// <returns><see cref="LibsndfileError"/> error code.</returns>
        public LibsndfileError Close(IntPtr sndfile)
        {
            return LibsndfileApiNative.sf_close(sndfile);
        }

        /// <summary>
        /// Check to see if the parameters in the <paramref name="info"/> struct are
        /// valid and supported by libsndfile.
        /// </summary>
        /// <param name="info"><see cref="LibsndfileInfo"/> struct contains information about a target file.</param>
        /// <returns>Returns TRUE if the parameters are valid, FALSE otherwise.</returns>
        public int FormatCheck(ref LibsndfileInfo info)
        {
            return LibsndfileApiNative.sf_format_check(ref info);
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
            return LibsndfileApiNative.sf_seek(sndfile, count, whence);
        }

        /// <summary>
        /// Forces operating system to write buffers to disk. Only works if <paramref name="sndfile"/> is
        /// opened in <see cref="LibsndfileMode"/> SFM_WRITE or SFM_RDWR.
        /// </summary>
        /// <param name="sndfile">Audio file you wish to flush buffers on.</param>
        public void WriteSync(IntPtr sndfile)
        {
            LibsndfileApiNative.sf_write_sync(sndfile);
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
            return LibsndfileApiNative.sf_set_string(sndfile, type, value);
        }

        /// <summary>
        /// Reads the <paramref name="type"/> tag from the <paramref name="sndfile"/> audio file.
        /// </summary>
        /// <param name="sndfile">Audio file to read tags from.</param>
        /// <param name="type"><see cref="LibsndfileStringType"/> tag to read.</param>
        /// <returns>Returns the value of the <paramref name="type"/> tag.</returns>
        public string GetString(IntPtr sndfile, LibsndfileStringType type)
        {
            var ptr = LibsndfileApiNative.sf_get_string(sndfile, type);
            return Marshal.PtrToStringAnsi(ptr) ?? string.Empty;
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
            return LibsndfileApiNative.sf_read_short(sndfile, buffer, items);
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
            return LibsndfileApiNative.sf_read_int(sndfile, buffer, items);
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
            return LibsndfileApiNative.sf_read_float(sndfile, buffer, items);
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
            return LibsndfileApiNative.sf_read_double(sndfile, buffer, items);
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
            return LibsndfileApiNative.sf_readf_short(sndfile, buffer, frames);
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
            return LibsndfileApiNative.sf_readf_int(sndfile, buffer, frames);
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
            return LibsndfileApiNative.sf_readf_float(sndfile, buffer, frames);
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
            return LibsndfileApiNative.sf_readf_double(sndfile, buffer, frames);
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
            return LibsndfileApiNative.sf_write_short(sndfile, buffer, items);
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
            return LibsndfileApiNative.sf_write_int(sndfile, buffer, items);
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
            return LibsndfileApiNative.sf_write_float(sndfile, buffer, items);
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
            return LibsndfileApiNative.sf_write_double(sndfile, buffer, items);
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
            return LibsndfileApiNative.sf_writef_short(sndfile, buffer, frames);
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
            return LibsndfileApiNative.sf_writef_int(sndfile, buffer, frames);
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
            return LibsndfileApiNative.sf_writef_float(sndfile, buffer, frames);
        }

        /// <summary>
        /// Write <paramref name="frames"/> from the <paramref name="buffer"/> into the audio <paramref name="sndfile"/>.
        /// </summary>
        /// <param name="sndfile">Audio file to write to.</param>
        /// <param name="buffer">Buffer to write from.</param>
        /// <param name="frames">Number of frames to read from the <paramref name="buffer"/>.</param>
        /// <returns>Returns the number of frames written. Should be equal to <paramref name="frames"/> unless
        /// you've reached EOF.</returns>
        public long WriteFrames(IntPtr sndfile, double[] buffer, long frames)
        {
            return LibsndfileApiNative.sf_writef_double(sndfile, buffer, frames);
        }

        /// <summary>
        /// Reads <paramref name="bytes"/> amount of raw audio data from 
        /// <paramref name="sndfile"/> into <paramref name="buffer"/>.
        /// </summary>
        /// <param name="sndfile">Audio file to read from.</param>
        /// <param name="buffer">Buffer to fill.</param>
        /// <param name="bytes">Number of bytes to read from <paramref name="sndfile"/>.</param>
        /// <returns>Returns the number of frames written. Should be equal to <paramref name="bytes"/> unless
        /// you've reached EOF.</returns>
        public long ReadRaw(IntPtr sndfile, byte[] buffer, long bytes)
        {
            return LibsndfileApiNative.sf_read_raw(sndfile, buffer, bytes);
        }

        /// <summary>
        /// Writes <paramref name="bytes"/> amount of raw audio data from
        /// <paramref name="buffer"/> into <paramref name="sndfile"/>.
        /// </summary>
        /// <param name="sndfile">Audio file to write to.</param>
        /// <param name="buffer">Buffer to write from.</param>
        /// <param name="bytes">Number of bytes to read from <paramref name="sndfile"/>.</param>
        /// <returns>Returns the number of frames written. Should be equal to <paramref name="bytes"/> unless
        /// you've reached EOF.</returns>
        public long WriteRaw(IntPtr sndfile, byte[] buffer, long bytes)
        {
            return LibsndfileApiNative.sf_write_raw(sndfile, buffer, bytes);
        }
    }
}