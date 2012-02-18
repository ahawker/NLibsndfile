using System;
using System.Runtime.InteropServices;

namespace NLibsndfile.Native
{
    /// <summary>
    /// Class to provide easy to use helper functions for marshalling command methods.
    /// </summary>
    internal class LibsndfileCommandMarshaller : ILibsndfileCommandMarshaller
    {
        /// <summary>
        /// Create a new <see cref="UnmanagedMemoryHandle"/> allocated for <paramref name="size"/> bytes.
        /// </summary>
        /// <param name="size">Number of bytes of unmanaged memory requested.</param>
        /// <returns><see cref="UnmanagedMemoryHandle"/> with a chunk of memory allocated.</returns>
        public UnmanagedMemoryHandle Allocate(int size)
        {
            return new UnmanagedMemoryHandle(size);
        }

        /// <summary>
        /// Create a new <see cref="UnmanagedMemoryHandle"/> allocated for the size of a single Int32.
        /// </summary>
        /// <returns><see cref="UnmanagedMemoryHandle"/> with a chunk of memory allocated.</returns>
        public UnmanagedMemoryHandle AllocateInt()
        {
            return new UnmanagedMemoryHandle(Marshal.SizeOf(typeof(int)));
        }

        /// <summary>
        /// Create a new <see cref="UnmanagedMemoryHandle"/> allocated for the size of a single Double.
        /// </summary>
        /// <returns><see cref="UnmanagedMemoryHandle"/> with a chunk of memory allocated.</returns>
        public UnmanagedMemoryHandle AllocateDouble()
        {
            return new UnmanagedMemoryHandle(Marshal.SizeOf(typeof(double)));
        }

        /// <summary>
        /// Create a new <see cref="UnmanagedMemoryHandle"/> allocated for the size of a single Int64.
        /// </summary>
        /// <returns><see cref="UnmanagedMemoryHandle"/> with a chunk of memory allocated.</returns>
        public UnmanagedMemoryHandle AllocateLong()
        {
            return new UnmanagedMemoryHandle(Marshal.SizeOf(typeof(long)));
        }

        /// <summary>
        /// Explicitly disposes of the <paramref name="memory"/> object and deallocates its unmanaged memory.
        /// </summary>
        /// <param name="memory">UnmanagedMemoryHandle to deallocate.</param>
        public void Deallocate(UnmanagedMemoryHandle memory)
        {
            if (memory == null)
                return;

            memory.Dispose();
        }

        /// <summary>
        /// Marshal a <see cref="UnmanagedMemoryHandle"/> object to an ANSI string.
        /// </summary>
        /// <param name="memory">Refernce to UnmanagedMemoryHandle.</param>
        /// <returns>ANSI string conversion from unmanaged memory.</returns>
        public string MemoryHandleToString(UnmanagedMemoryHandle memory)
        {
            return Marshal.PtrToStringAnsi(memory.Handle);
        }

        /// <summary>
        /// Marshal a <see cref="UnmanagedMemoryHandle"/> object to an Int32.
        /// </summary>
        /// <param name="memory">Reference to UnmanagedMemoryHandle.</param>
        /// <returns>Int32 conversion from unmanaged memory.</returns>
        public int MemoryHandleToInt(UnmanagedMemoryHandle memory)
        {
            return (int)Marshal.PtrToStructure(memory, typeof(int));
        }

        /// <summary>
        /// Marshal a <see cref="UnmanagedMemoryHandle"/> object to an Double.
        /// </summary>
        /// <param name="memory">Reference to UnmanagedMemoryHandle.</param>
        /// <returns>Double conversion from unmanaged memory.</returns>
        public double MemoryHandleToDouble(UnmanagedMemoryHandle memory)
        {
            return (double)Marshal.PtrToStructure(memory, typeof(double));
        }

        /// <summary>
        /// Marshal a <see cref="UnmanagedMemoryHandle"/> object to an Int64.
        /// </summary>
        /// <param name="memory">Reference to UnmanagedMemoryHandle.</param>
        /// <returns>Int64 conversion from unmanaged memory.</returns>
        public long MemoryHandleToLong(UnmanagedMemoryHandle memory)
        {
            return (long)Marshal.PtrToStructure(memory, typeof(long));
        }
    }

    /// <summary>
    /// Internal container class that holds a pointer to an allocated chunk of unmanaged memory.
    /// </summary>
    /// <remarks>
    /// Use of this class enables allocation/deallocation of unmanaged memory within the scope of a 'using' statement.
    /// </remarks>
    internal class UnmanagedMemoryHandle : IDisposable
    {
        private bool m_IsDisposed;

        internal IntPtr Handle { get; private set; }
        internal int Size { get; private set; }

        /// <summary>
        /// Initializes a new instances of <see cref="UnmanagedMemoryHandle"/> on top of an empty pointer.
        /// </summary>
        /// <remarks>
        /// The default parameterless c'tor is here so we can mock the object.
        /// Defining an interface wouldn't work because we can have implicit operators on the interface.
        /// </remarks>
        internal UnmanagedMemoryHandle()
            : this(IntPtr.Zero)
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="UnmanagedMemoryHandle"/> on top of the given pointer.
        /// </summary>
        /// <param name="handle">IntPtr to unmanaged memory location.</param>
        internal UnmanagedMemoryHandle(IntPtr handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="UnmanagedMemoryHandle"/>.
        /// </summary>
        /// <param name="size">Size of unmanaged memory in bytes to allocate.</param>
        internal UnmanagedMemoryHandle(int size)
        {
            Size = size;
            Handle = Marshal.AllocHGlobal(size);
        }

        /// <summary>
        /// Implicitly convert our <see cref="UnmanagedMemoryHandle"/> to an IntPtr.
        /// </summary>
        /// <param name="memory">Reference to an UnmanagedMemoryHandle object.</param>
        /// <returns>IntPtr handle which points to allocated unmanaged memory.</returns>
        public static implicit operator IntPtr(UnmanagedMemoryHandle memory)
        {
            return memory.Handle;
        }

        /// <summary>
        /// Implicitly convert an IntPtr to a <see cref="UnmanagedMemoryHandle"/>.
        /// </summary>
        /// <param name="handle">Reference to an IntPtr object.</param>
        /// <returns><see cref="UnmanagedMemoryHandle"/> which wraps the given IntPtr handle.</returns>
        public static implicit operator UnmanagedMemoryHandle(IntPtr handle)
        {
            return new UnmanagedMemoryHandle(handle);
        }

        /// <summary>
        /// Disposes of the previously allocated unmanaged memory.
        /// </summary>
        /// <param name="disposing">Determines whether this was called by the public Dispose method or finalizer.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (m_IsDisposed)
                return;

            Marshal.FreeHGlobal(Handle);

            m_IsDisposed = true;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Allows an <see cref="T:System.Object"/> to attempt to free resources and perform other cleanup operations before the <see cref="T:System.Object"/> is reclaimed by garbage collection.
        /// </summary>
        ~UnmanagedMemoryHandle()
        {
            Dispose(false);
        }
    }
}