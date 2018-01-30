using System;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace IRO.InfinityPhoto
{
    class BetaChecks
    {

        public static DateTime? GetAttr(string tag, string sub)
        {
            object obj = Registry.GetValue(string.Format("HKEY_CLASSES_ROOT\\CLSID\\{0}", (object)tag), sub, (object)null);
            if (obj == null)
                return new DateTime?();
            byte[] numArray = obj as byte[];
            if (numArray == null)
                return new DateTime?();
            byte num1 = numArray[1];
            byte num2 = numArray[0];
            if ((int)numArray[4] == (int)(byte)((uint)num2 ^ (uint)num1))
            {
                byte num3 = numArray[2];
                if ((int)numArray[5] == (int)(byte)((uint)num3 ^ (uint)num1))
                {
                    byte num4 = numArray[3];
                    if ((int)numArray[6] == (int)(byte)((uint)num4 ^ (uint)num3) && (int)numArray[7] == (int)(byte)((uint)num4 ^ (uint)num2))
                        return (DateTime?)new DateTime((int)num3 * 256 + (int)num4, (int)num1, (int)num2);
                }
            }
            return new DateTime?();
        }

        [return: MarshalAs(UnmanagedType.U1)]
        public static bool SetAttr(string tag, string sub, DateTime Value)
        {
            string tag1 = string.Format("CLSID\\{0}", (object)tag);
            byte[] numArray = new byte[8];
            byte day = (byte)Value.Day;
            numArray[0] = day;
            byte month = (byte)Value.Month;
            numArray[1] = month;
            byte num1 = (byte)(Value.Year / 256);
            numArray[2] = num1;
            byte num2 = (byte)(Value.Year % 256);
            numArray[3] = num2;
            numArray[4] = (byte)((int)numArray[1] ^ (int)numArray[0]);
            numArray[5] = (byte)((int)numArray[2] ^ (int)numArray[1]);
            numArray[6] = (byte)((int)numArray[3] ^ (int)numArray[2]);
            numArray[7] = (byte)((int)numArray[3] ^ (int)numArray[0]);
            string sub1 = sub;
            byte[] b1 = numArray;
            return BetaChecks.QuietSet(tag1, sub1, b1);
        }

        [return: MarshalAs(UnmanagedType.U1)]
        private static unsafe bool QuietSet(string tag, string sub, byte[] b1)
        {
            // ISSUE: untyped stack allocation
            long num1 = (long)__untypedstackalloc(Module.__CxxQueryExceptionSize());
            try
            {
                RegistryKey registryKey = Registry.ClassesRoot.OpenSubKey(tag, true);
                if (registryKey != null)
                {
                    registryKey.SetValue(sub, (object)b1);
                    registryKey.Close();
                    return true;
                }
            }
            catch (Exception ex1) when (
            {
                // ISSUE: unable to correctly present filter
                uint exceptionCode = (uint)Marshal.GetExceptionCode();
                if (Module.__CxxExceptionFilter((void*)Marshal.GetExceptionPointers(), (void*)0L, 0, (void*)0L) != 0)
                {
                    SuccessfulFiltering;
                }
                else
                    throw;
            }
            )
            {
                uint num2 = 0;
                Module.__CxxRegisterExceptionObject((void*)Marshal.GetExceptionPointers(), (void*)num1);
                try
                {
                    try
                    {
                    }
                    catch (Exception ex2) when (
                    {
                        // ISSUE: unable to correctly present filter
                        num2 = (uint) Module.__CxxDetectRethrow((void*)Marshal.GetExceptionPointers());
                        if ((int)num2 != 0)
                        {
                            SuccessfulFiltering;
                        }
                        else
                            throw;
                    }
                    )
                    {
                    }
                    goto label_11;
                    uint num3;
                    if ((int)num3 != 0)
                        throw;
                }
                finally
                {
                    Module.__CxxUnregisterExceptionObject((void*)num1, (int)num2);
                }
            }
            label_11:
            return false;
        }

    }
}
