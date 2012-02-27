using System;
using System.Runtime.InteropServices;

namespace NLibsndfile.Native
{
    /// <summary>
    /// Provides methods for converting <see cref="UnmanagedMemoryHandle"/> to managed arrays.
    /// </summary>
    internal class LibsndfileArrayMarshaller : ILibsndfileArrayMarshaller
    {
        /// <summary>
        /// Returns delegate to correct conversion method based on <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">Type of managed array you wish to convert to.</typeparam>
        /// <returns>Method to convert unmanaged memory to managed array of <typeparamref name="T"/>.</returns>
        public LibsndfileArrayMarshallerDelegate<T> GetMarshallerForType<T>()
            where T : struct
        {
            Type type = typeof(T);

            if (type == typeof(short))
                return x => ToShortArray(x) as T[];
            if (type == typeof(int))
                return x => ToIntArray(x) as T[];
            if (type == typeof(float))
                return x => ToFloatArray(x) as T[];
            if (type == typeof(double))
                return x => ToDoubleArray(x) as T[];
            if (type == typeof(long))
                return x => ToLongArray(x) as T[];

            throw new NotSupportedException(string.Format("No marshalling support for array of type {0}.", type));
        }

        /// <summary>
        /// Marshal <see cref="UnmanagedMemoryHandle"/> to managed <see cref="System.Int16"/> array.
        /// </summary>
        /// <param name="memory"><see cref="UnmanagedMemoryHandle"/> containing pointer to native array.</param>
        /// <returns>Managed <see cref="System.Int16"/> array.</returns>
        private static short[] ToShortArray(UnmanagedMemoryHandle memory)
        {
            int length = CalculateArrayLength<short>(memory);
            var array = new short[length];
            Marshal.Copy(memory, array, 0, length);
            return array;
        }

        /// <summary>
        /// Marshal <see cref="UnmanagedMemoryHandle"/> to managed <see cref="System.Int32"/> array.
        /// </summary>
        /// <param name="memory"><see cref="UnmanagedMemoryHandle"/> containing pointer to native array.</param>
        /// <returns>Managed <see cref="System.Int32"/> array.</returns>
        private static int[] ToIntArray(UnmanagedMemoryHandle memory)
        {
            int length = CalculateArrayLength<int>(memory);
            var array = new int[length];
            Marshal.Copy(memory, array, 0, length);
            return array;
        }

        /// <summary>
        /// Marshal <see cref="UnmanagedMemoryHandle"/> to managed <see cref="System.Single"/> array.
        /// </summary>
        /// <param name="memory"><see cref="UnmanagedMemoryHandle"/> containing pointer to native array.</param>
        /// <returns>Managed <see cref="System.Single"/> array.</returns>
        private static float[] ToFloatArray(UnmanagedMemoryHandle memory)
        {
            int length = CalculateArrayLength<float>(memory);
            var array = new float[length];
            Marshal.Copy(memory, array, 0, length);
            return array;
        }

        /// <summary>
        /// Marshal <see cref="UnmanagedMemoryHandle"/> to managed <see cref="System.Double"/> array.
        /// </summary>
        /// <param name="memory"><see cref="UnmanagedMemoryHandle"/> containing pointer to native array.</param>
        /// <returns>Managed <see cref="System.Double"/> array.</returns>
        private static double[] ToDoubleArray(UnmanagedMemoryHandle memory)
        {
            int length = CalculateArrayLength<double>(memory);
            var array = new double[length];
            Marshal.Copy(memory, array, 0, length);
            return array;
        }

        /// <summary>
        /// Marshal <see cref="UnmanagedMemoryHandle"/> to managed <see cref="System.Int64"/> array.
        /// </summary>
        /// <param name="memory"><see cref="UnmanagedMemoryHandle"/> containing pointer to native array.</param>
        /// <returns>Managed <see cref="System.Int64"/> array.</returns>
        private static long[] ToLongArray(UnmanagedMemoryHandle memory)
        {
            int length = CalculateArrayLength<long>(memory);
            var array = new long[length];
            Marshal.Copy(memory, array, 0, length);
            return array;
        }

        /// <summary>
        /// Determine length required for managed array based on <typeparamref name="T"/> and <paramref name="memory"/> size.
        /// </summary>
        /// <typeparam name="T">Underlying array type.</typeparam>
        /// <param name="memory"><see cref="UnmanagedMemoryHandle"/> to location of native array.</param>
        /// <returns>Length of marshalled array.</returns>
        private static int CalculateArrayLength<T>(UnmanagedMemoryHandle memory)
        {
            return Marshal.SizeOf(typeof(T)) * memory.Size;
        }
    }
}