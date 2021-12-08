using RoyApp.Interfaces;
using System;
using System.Linq;

namespace RoyApp.Services
{
    public class DataService : IDataService
    {
        private static decimal DecimalFormat(decimal time)
        {
            var timeAsString = String.Format("{0:0.00}", time);

            if (timeAsString.EndsWith("00"))
            {
                return ((int)time);
            }
            else
            {
                return Convert.ToDecimal(timeAsString);
            }
        }

        private bool IsTimeAm(string time)
        {
            return !time.Trim()
                        .EndsWith("PM");
        }

        public string[] SplitLineData(string currentLine)
        {
            string[] cols = currentLine.Split(',');
            // 0 - id, 1 - bedtime raw, 2 - waketime raw
            string[] row = {
                        cols[0].Trim('"'),
                        cols[1].Trim('"'),
                        TimeToDecimal(cols[1].Trim('"')).ToString(),
                        cols[2].Trim('"'),
                        TimeToDecimal(cols[2].Trim('"')).ToString(),
                        TimeDuration(TimeToDecimal(cols[1].Trim('"')).ToString(), TimeToDecimal(cols[2].Trim('"')).ToString()).ToString()
                    };
            return row;
        }

        public decimal TimeAverage(decimal timeTotal, int count)
        {
            if (count > 0)
            {
                return Math.Round(Convert.ToDecimal(timeTotal) / count, 2);
            }
            return 0;
        }

        public decimal TimeDuration(string bedtimeRec, string waketimeRec)
        {
            if (String.IsNullOrEmpty(bedtimeRec) || String.IsNullOrEmpty(waketimeRec))
            {
                return -1;
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
    }
}
