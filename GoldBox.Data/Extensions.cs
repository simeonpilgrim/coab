using System;
using System.Runtime.InteropServices;

namespace GoldBox.Data
{
    public static class Extensions
    {
        public static byte[] ToByteArray<T>(T dataStructure) where T : struct
        {
            int size = Marshal.SizeOf(dataStructure);
            byte[] arr = new byte[size];

            IntPtr ptr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(dataStructure, ptr, true);
            Marshal.Copy(ptr, arr, 0, size);
            Marshal.FreeHGlobal(ptr);
            return arr;
        }

        public static T MarshalAs<T>(byte[] rawDataStructure) where T : struct
        {
            var type = typeof(T);
            int size = Marshal.SizeOf(type);
            IntPtr ptr = Marshal.AllocHGlobal(size);
            Marshal.Copy(rawDataStructure, 0, ptr, size);
            T structure = (T)Marshal.PtrToStructure(ptr, type);
            Marshal.FreeHGlobal(ptr);

            return structure;
        }
    }
}
