using System;

namespace IRO.Infinity
{
    internal class Program
    {
        private static readonly string PRODUCT_ID = "{921C7219-D941-4F7E-BA74-724A28C8B4EB}";
        private static readonly string INSTALL_DATE_FLAG = "s0";
        private static readonly string FIRST_RUN_DATE_FLAG = "s1";
        private static readonly string LAST_RUN_DATE_FLAG = "s2";

        static void Main(string[] args)
        {
            var application = new Infinity();
            application.ResetExpirationKeys(PRODUCT_ID, INSTALL_DATE_FLAG, FIRST_RUN_DATE_FLAG, LAST_RUN_DATE_FLAG);
            if (application.IsExpired(PRODUCT_ID, INSTALL_DATE_FLAG, FIRST_RUN_DATE_FLAG, LAST_RUN_DATE_FLAG))
            {
                throw new Exception("ResetExpirationKeys did not work.");
            }
        }
    }
}
