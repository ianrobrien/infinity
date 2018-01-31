using System.Runtime.CompilerServices;
using System.Security;

namespace IRO.InfinityPhoto
{
    internal class Module
    {
        [SuppressUnmanagedCodeSecurity]
        [MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
        internal static extern int __CxxQueryExceptionSize();        
    }
}
