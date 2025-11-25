using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhraseUtils
{
    public static class TimeoutChecker
    {
        private static DateTime lastCheck = DateTime.MinValue;

        public static bool ShouldUpdate()
        {
            if ((DateTime.Now - lastCheck).TotalMinutes >= 0.16)
            {
                lastCheck = DateTime.Now;
                return true;
            }

            return false;
        }
    }
}
