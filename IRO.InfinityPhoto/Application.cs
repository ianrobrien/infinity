using System;
using System.Globalization;

namespace IRO.InfinityPhoto
{
    internal class Application
    {
        private readonly string INSTALLER_VALID_START_DATE = "Nov 16 2017";
        private readonly double TRIAL_PERIOD_LENGTH = 10.0;

        public bool TrialExpiredCheck(string productId, string installDateFlag, string firstRunDateFlag, string latestRunFlag)
        {
            var now = DateTime.Now;

            var installDate = RegistryHelper.GetEncryptedDate(productId, installDateFlag) ?? now;

            var firstRunDate = RegistryHelper.GetEncryptedDate(productId, firstRunDateFlag);
            if (!firstRunDate.HasValue)
            {
                firstRunDate = now;
                RegistryHelper.SetEncryptedDate(productId, firstRunDateFlag, now);
            }

            // verify that clock hasn't been turned back
            var expired = !CheckLastRunDate(now, productId, latestRunFlag) && now < installDate && now < firstRunDate.Value;

            // verify that the 10 days have not elapsed
            var expirationDate = firstRunDate.Value.AddDays(TRIAL_PERIOD_LENGTH);
            expired = now > expirationDate || expired;

            // verify that the trial period is within the valid date
            var buildValidStartDate = Convert.ToDateTime(INSTALLER_VALID_START_DATE, CultureInfo.InvariantCulture);
            var buildValidEndDate = buildValidStartDate.AddYears(1);
            expired = now > buildValidEndDate || now < buildValidStartDate || expired;

            if (expired)
            {
                throw new Exception($"Product expired on {expirationDate}");
            }

            return false;
        }

        public bool ResetExpirationKeys(string productId, string installDateFlag, string firstRunDateFlag, string lastRunDateFlag, DateTime validDate)
        {
            RegistryHelper.SetEncryptedDate(productId, installDateFlag, validDate);
            RegistryHelper.SetEncryptedDate(productId, firstRunDateFlag, validDate);
            RegistryHelper.SetEncryptedDate(productId, lastRunDateFlag, validDate);
            return TrialExpiredCheck(productId, installDateFlag, firstRunDateFlag, lastRunDateFlag);
        }

        // returns false if the clock has been set behind the last run date
        private bool CheckLastRunDate(DateTime now, string productId, string lastRunDateFlag)
        {
            var lastRunDate = RegistryHelper.GetEncryptedDate(productId, lastRunDateFlag);
            return !(lastRunDate.HasValue && now < lastRunDate.Value);
        }
    }
}
