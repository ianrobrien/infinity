using System;
using System.Globalization;

namespace IRO.InfinityPhoto
{
    internal class Application
    {
        private readonly string PRODUCT_ID = "{921C7219-D941-4F7E-BA74-724A28C8B4EB}";
        private readonly string INSTALL_DATE_FLAG = "s0";
        private readonly string FIRST_RUN_DATE_FLAG = "s1";
        private readonly string LAST_RUN_DATE_FLAG = "s2";
        private readonly double TRIAL_PERIOD_LENGTH = 10.0;

        public bool TrialExpiredCheck()
        {
            var now = DateTime.Now;

            var installDate = BetaChecks.GetAttr(this.PRODUCT_ID, INSTALL_DATE_FLAG) ?? now;

            var firstRunDate = BetaChecks.GetAttr(this.PRODUCT_ID, FIRST_RUN_DATE_FLAG);
            if (!firstRunDate.HasValue)
            {
                firstRunDate = now;
                BetaChecks.SetAttr(this.PRODUCT_ID, FIRST_RUN_DATE_FLAG, now);
            }

            // verify that clock hasn't been turned back
            var expired = !CheckAndUpdateLastRunDate(now) && now < installDate && now < firstRunDate.Value;

            // verify that the 10 days have not elapsed
            var expirationDate = firstRunDate.Value.AddDays(TRIAL_PERIOD_LENGTH);
            expired = now > expirationDate || expired;

            // verify that the trial period is within the valid date
            var buildValidStartDate = Convert.ToDateTime("Nov 16 2017", CultureInfo.InvariantCulture);
            var buildValidEndDate = buildValidStartDate.AddYears(1);
            expired = now > buildValidEndDate || now < buildValidStartDate || expired;
                        
            if (expired)
            {
                throw new Exception($"Product expired on {expirationDate}");
            }

            return false;
        }

        private bool CheckAndUpdateLastRunDate(DateTime now)
        {
            var lastRunDate = BetaChecks.GetAttr(this.PRODUCT_ID, LAST_RUN_DATE_FLAG);
            if (lastRunDate.HasValue)
            {
                if (now < lastRunDate.Value)
                {
                    return false;
                }
            }
            return BetaChecks.SetAttr(this.PRODUCT_ID, LAST_RUN_DATE_FLAG, now);
        }

    }
}
