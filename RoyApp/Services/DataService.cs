using System;
using System.Linq;

namespace RoyApp.Services
{
    public interface IDataService
    {
        decimal TimeToDecimal(string time);
        decimal TimeDuration(string bedtimeRec, string waketimeRec);
        decimal TimeAverage(decimal timeTotal, int count);
    }

    public class DataService : IDataService
    {
        public decimal TimeToDecimal(string time)
        {
            var timeNumsOnly = new string(time.Where(c => char.IsDigit(c)).ToArray());
            if (timeNumsOnly.Length < 3)
            {
                return 0;
            }

            bool IsTimeAM = IsTimeAm(time);

            if ((timeNumsOnly.Length > 3) && (Convert.ToDecimal(timeNumsOnly[0..2]) > 12))
            {
                IsTimeAM = false;
            }

            string minutesAsString = timeNumsOnly[^2..^0];

            var minutes = Convert.ToDecimal(minutesAsString);
            var fractionalHours = minutes / 60;

            string hoursOnly = timeNumsOnly[0..^2];
            var hours = Convert.ToInt32(hoursOnly);
            if (!IsTimeAM && hours < 12)
            {
                hours += 12;
            }
            else if (IsTimeAM && hours == 12)
            {
                hours = 0;
            }

            return hours + Math.Round(fractionalHours, 2);
        }

        private bool IsTimeAm(string time)
        {
            return !time.Trim()
                        .EndsWith("PM");
        }

        public decimal TimeDuration(string bedtimeRec, string waketimeRec)
        {
            if (String.IsNullOrEmpty(bedtimeRec) || String.IsNullOrEmpty(waketimeRec))
            {
                return ((int)-1);
            }

            var bedtime = Convert.ToDecimal(bedtimeRec);
            var waketime = Convert.ToDecimal(waketimeRec);
            if (bedtime > 12)
            {
                if (waketime > 12)
                {
                    return DecimalFormat((24 - bedtime) - (24 - waketime));
                }
                else
                {
                    return DecimalFormat((24 - bedtime) + waketime);
                }
            }
            else
            {
                return DecimalFormat(waketime - bedtime);
            }
        }

        public static decimal DecimalFormat(decimal time)
        {
            var s = String.Format("{0:0.00}", time);

            if (s.EndsWith("00"))
            {
                return ((int)time);
            }
            else
            {
                return Convert.ToDecimal(s);
            }
        }

        public decimal TimeAverage(decimal timeTotal, int count)
        {
            if (count > 0)
            {
                return Math.Round(Convert.ToDecimal(timeTotal) / count, 2);
            }
            return 0;
        }
    }
}
