using System;
using System.Collections;
using System.Runtime.InteropServices;
namespace TomKCPGameLib
{
    public class DLL
    {
        private const string dllName = "kcplib";


        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void KCPReset(IntPtr sess);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr KCPDial(string ip, ushort port, int dataShards, int parityShards);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        private static extern int KCPWrite(IntPtr sess,IntPtr buf, int size);
        public static  int Write(IntPtr sess, byte[] buf, int size)
        {
            int n = 0;
            unsafe
            {
                fixed (byte* p = &buf[0])
                {
                    n = KCPWrite(sess,(IntPtr)p, size);
                }
            }
            return n;
        }

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        private static extern int KCPRead(IntPtr sess, IntPtr buf, int size);
        public static int Read(IntPtr sess, byte[] buf, int size)
        {
            int n = 0;
            unsafe
            {
                fixed (byte* p = &buf[0])
                {
                    n = DLL.KCPRead(sess,(IntPtr)p, size);
                }
            }
            return n;
        }

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void KCPUpdate(IntPtr sess);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void KCPSetNoDelay(IntPtr sess, int nodelay, int interval, int resend, int nc);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void KCPSetWndSize(IntPtr sess, int sndwnd, int rcvwnd);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void KCPSetMtu(IntPtr sess, int mtu);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void KCPSetStreamMode(IntPtr sess, bool stream);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void KCPSetDSCP(IntPtr sess, int dscp);


    }
}

