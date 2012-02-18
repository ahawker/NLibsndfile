namespace NLibsndfile.Native
{
    /// <summary>
    /// Interface to define an object that can marshal LibsndfileCommandApi objects.
    /// </summary>
    /// <remarks>
    /// This interface is internal and only used so we can mock the managed->unmanaged marshalling of commands.
    /// </remarks>
    internal interface ILibsndfileCommandMarshaller
    {
        /// <summary>
        /// Create a new <see cref="UnmanagedMemoryHandle"/> allocated for <paramref name="size"/> bytes.
        /// </summary>
        /// <param name="size">Number of bytes of unmanaged memory requested.</param>
        /// <returns><see cref="UnmanagedMemoryHandle"/> with a chuck of memory allocated.</returns>
        UnmanagedMemoryHandle Allocate(int size);

        /// <summary>
        /// Create a new <see cref="UnmanagedMemoryHandle"/> allocated for the size of a single Int32.
        /// </summary>
        /// <returns><see cref="UnmanagedMemoryHandle"/> with a chunk of memory allocated.</returns>
        UnmanagedMemoryHandle AllocateInt();

        /// <summary>
        /// Create a new <see cref="UnmanagedMemoryHandle"/> allocated for the size of a single Double.
        /// </summary>
        /// <returns><see cref="UnmanagedMemoryHandle"/> with a chunk of memory allocated.</returns>
        UnmanagedMemoryHandle AllocateDouble();

        /// <summary>
        /// Create a new <see cref="UnmanagedMemoryHandle"/> allocated for the size of a single Int64.
        /// </summary>
        /// <returns><see cref="UnmanagedMemoryHandle"/> with a chunk of memory allocated.</returns>
        UnmanagedMemoryHandle AllocateLong();

        /// <summary>
        /// Explicitly disposes of the <paramref name="memory"/> object and deallocates its unmanaged memory.
        /// </summary>
        /// <param name="memory">UnmanagedMemoryHandle to deallocate.</param>
        void Deallocate(UnmanagedMemoryHandle memory);

        /// <summary>
        /// Marshal a <see cref="UnmanagedMemoryHandle"/> object to an ANSI string.
        /// </summary>
        /// <param name="memory">Reference to UnmanagedMemoryHandle.</param>
        /// <returns>ANSI string conversion from unmanaged memory.</returns>
        string MemoryHandleToString(UnmanagedMemoryHandle memory);

        /// <summary>
        /// Marshal a <see cref="UnmanagedMemoryHandle"/> object to an Int32.
        /// </summary>
        /// <param name="memory">Reference to UnmanagedMemoryHandle.</param>
        /// <returns>Int32 conversion from unmanaged memory.</returns>
        int MemoryHandleToInt(UnmanagedMemoryHandle memory);

        /// <summary>
        /// Marshal a <see cref="UnmanagedMemoryHandle"/> object to an Double.
        /// </summary>
        /// <param name="memory">Reference to UnmanagedMemoryHandle.</param>
        /// <returns>Double conversion from unmanaged memory.</returns>
        double MemoryHandleToDouble(UnmanagedMemoryHandle memory);

        /// <summary>
        /// Marshal a <see cref="UnmanagedMemoryHandle"/> object to an Int64.
        /// </summary>
        /// <param name="memory">Reference to UnmanagedMemoryHandle.</param>
        /// <returns>Int64 conversion from unmanaged memory.</returns>
        long MemoryHandleToLong(UnmanagedMemoryHandle memory);
    }
}