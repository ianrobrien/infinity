using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace IRO.InfinityPhoto
{
    internal class Module
    {
        [SuppressUnmanagedCodeSecurity]
        [DllImport("", EntryPoint = "", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
        [MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
        internal static extern int __CxxQueryExceptionSize();

        [SuppressUnmanagedCodeSecurity]
        [DllImport("", EntryPoint = "", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
        [MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
        internal static extern unsafe int __CxxExceptionFilter([In] void* obj0, [In] void* obj1, [In] int obj2, [In] void* obj3);

        [SuppressUnmanagedCodeSecurity]
        [DllImport("", EntryPoint = "", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
        [MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
        internal static extern unsafe int __CxxRegisterExceptionObject([In] void* obj0, [In] void* obj1);

        [SuppressUnmanagedCodeSecurity]
        [DllImport("", EntryPoint = "", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
        [MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
        internal static extern unsafe int __CxxDetectRethrow([In] void* obj0);

        [SuppressUnmanagedCodeSecurity]
        [DllImport("", EntryPoint = "", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
        [MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
        internal static extern unsafe void __CxxUnregisterExceptionObject([In] void* obj0, [In] int obj1);
    }
}
