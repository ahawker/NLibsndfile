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
        /// Create a new <see cref="UnmanagedMemoryHandle"/> allocated for the size of a single <typeparamref name="T"/> structure.
        /// </summary>
        /// <returns><see cref="UnmanagedMemoryHandle"/> with a chunk of memory allocated.</returns>
        UnmanagedMemoryHandle Allocate<T>() where T : struct;

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
        /// Marshal a <see cref="UnmanagedMemoryHandle"/> object to a <typeparamref name="T"/> structure.
        /// </summary>
        /// <typeparam name="T">Type of structure to marshal from unmanaged memory.</typeparam>
        /// <param name="memory">Reference to <see cref="UnmanagedMemoryHandle"/>.</param>
        /// <returns>Marshalled structure stored in managed memory.</returns>
        T MemoryHandleTo<T>(UnmanagedMemoryHandle memory) where T : struct;
    }
}