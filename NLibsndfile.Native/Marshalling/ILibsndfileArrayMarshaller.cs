namespace NLibsndfile.Native
{
    /// <summary>
    /// Method to marshal <paramref name="memory"/> from unmanaged memory to a managed array of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">Type of managed array you wish to receive.</typeparam>
    /// <param name="memory"><see cref="UnmanagedMemoryHandle"/> memory location containing an unmanaged array.</param>
    /// <returns>Copy of marshalled array to managed memory.</returns>
    internal delegate T[] LibsndfileArrayMarshallerDelegate<out T>(UnmanagedMemoryHandle memory);

    /// <summary>
    /// Interface to provide support for marshalling arrays.
    /// </summary>
    internal interface ILibsndfileArrayMarshaller
    {
        LibsndfileArrayMarshallerDelegate<T> GetMarshallerForType<T>() where T : struct;
    }
}