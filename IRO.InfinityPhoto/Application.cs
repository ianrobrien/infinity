using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;

namespace IRO.InfinityPhoto
{
    class Application
    {

        private bool IsTrial = true;
        private String ExpReg = "{921C7219-D941-4F7E-BA74-724A28C8B4EB}";
        private string PurchaseLink = "https://www.google.com";

        [return: MarshalAs(UnmanagedType.U1)]
        public bool TrialExpiredCheck()
        {
            if (!this.IsTrial)
                return false;
            bool flag1 = false;
            DateTime now = DateTime.Now;
            DateTime? nullable1 = BetaChecks.GetAttr(this.ExpReg, "s0");
            if (!nullable1.HasValue)
            {
                nullable1 = (DateTime?)now;
                flag1 = true;
            }
            DateTime? nullable2 = BetaChecks.GetAttr(this.ExpReg, "s1");
            if (!nullable2.HasValue)
            {
                //int num = (int)MessageBox.Show(string.Format(this.GetString("NewTrial"), (object)this.Name, (object)10), "Affinity", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                nullable2 = (DateTime?)now;
                flag1 = !BetaChecks.SetAttr(this.ExpReg, "s1", now) || flag1;
            }
            if (this.CheckAndUpdateLastRunDate(now))
            {
                DateTime dateTime1 = nullable1.Value;
                if (!(now < dateTime1))
                {
                    DateTime dateTime2 = nullable2.Value;
                    if (!(now < dateTime2))
                        goto label_10;
                }
            }
            flag1 = true;
        label_10:
            DateTime dateTime3 = nullable2.Value.AddDays(10.0);
            bool flag2 = now > dateTime3 || flag1;
            DateTime dateTime4 = System.Convert.ToDateTime("Nov 16 2017", (IFormatProvider)CultureInfo.InvariantCulture);
            DateTime dateTime5 = dateTime4.AddYears(1);
            if (now > dateTime5 || now < dateTime4)
                flag2 = true;
            else if (!flag2)
                goto label_16;
            try
            {
                Process.Start(this.PurchaseLink);
            }
            catch (System.Exception ex)
            {
            }
        label_16:
            return flag2;
        }

        [return: MarshalAs(UnmanagedType.U1)]
        private bool CheckAndUpdateLastRunDate(DateTime now)
        {
            DateTime? attr = BetaChecks.GetAttr(this.ExpReg, "s2");
            if (attr.HasValue)
            {
                DateTime dateTime = attr.Value;
                if (now < dateTime)
                    return false;
            }
            return BetaChecks.SetAttr(this.ExpReg, "s2", now);
        }

    }
}
