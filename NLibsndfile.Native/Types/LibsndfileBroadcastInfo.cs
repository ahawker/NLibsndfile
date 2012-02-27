using System;
using System.Runtime.InteropServices;

namespace NLibsndfile.Native
{
    [StructLayout(LayoutKind.Sequential)]
    public struct LibsndfileBroadcastInfo
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        internal string Description;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        internal string Originator;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        internal string OriginatorReference;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 10)]
        internal string OriginationDate;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)]
        internal string OriginationTime;

        internal uint TimeReferenceLow;
        internal uint TimeReferenceHigh;
        internal short Version;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        internal string Umid;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 190)]
        internal string Reserved;

        internal uint CodingHistorySize;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        internal string CodingHistory;
    }
}