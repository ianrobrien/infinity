using System;
using Microsoft.Win32;

namespace IRO.InfinityPhoto
{
    internal class BetaChecks
    {
        public static DateTime? GetEncryptedDate(string tag, string sub)
        {
            var registryKey = Registry.GetValue($"HKEY_CLASSES_ROOT\\CLSID\\{tag}", sub, null);
            if (registryKey == null)
            {
                return new DateTime?();
            }
            if (!(registryKey is byte[] numArray))
            {
                return new DateTime?();
            }
            var day = numArray[0];
            var month = numArray[1];            
            if (numArray[4] == (byte)((uint)day ^ month))
            {
                var yearDivide = numArray[2];
                if (numArray[5] == (byte)((uint)yearDivide ^ month))
                {
                    var yearMod = numArray[3];
                    if (numArray[6] == (byte) ((uint) yearMod ^ yearDivide) && numArray[7] == (byte) ((uint) yearMod ^ day))
                    {
                        return new DateTime(yearDivide * 256 + yearMod, month, day);
                    }
                }
            }
            return new DateTime?();
        }

        public static bool SetEncryptedDate(string tag, string subKey, DateTime value)
        {
            var registryKeyName = $"CLSID\\{tag as object}";
            var numArray = new byte[8];

            var day = (byte)value.Day;
            numArray[0] = day;

            var month = (byte)value.Month;
            numArray[1] = month;

            var yearDivide = (byte)(value.Year / 256);
            numArray[2] = yearDivide;

            var yearMod = (byte)(value.Year % 256);
            numArray[3] = yearMod;

            numArray[4] = (byte)(numArray[1] ^ numArray[0]);
            numArray[5] = (byte)(numArray[2] ^ numArray[1]);
            numArray[6] = (byte)(numArray[3] ^ numArray[2]);
            numArray[7] = (byte)(numArray[3] ^ numArray[0]);

            return QuietSet(registryKeyName, subKey, numArray);
        }

        private static bool QuietSet(string tag, string subKey, byte[] value)
        {           
            var registryKey = Registry.ClassesRoot.OpenSubKey(tag, true);
            if (registryKey != null)
            {
                registryKey.SetValue(subKey, value);
                registryKey.Close();
                return true;
            }
            return false;
        }
    }
}
