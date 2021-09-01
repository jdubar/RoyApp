using System;
using System.Linq;

namespace RoyApp.Services
{
    public interface IDataService
    {
        double TimeToDecimal(string time);
        double TimeDuration(string bedtimeRec, string waketimeRec);
        double TimeAverage(double timeTotal, int count);
    }

    public class DataService : IDataService
    {
        public double TimeToDecimal(string time)
        {
            bool IsTimeAM = true;
            var timeNoSpace = time.Trim();
            if (timeNoSpace.EndsWith("PM"))
            {
                IsTimeAM = false;
            }

            var timeNumsOnly = new string(time.Where(c => char.IsDigit(c)).ToArray());

            if (timeNumsOnly.Length >= 3)
            {
                if ((timeNumsOnly.Length > 3)  && (Convert.ToDouble(timeNumsOnly.Substring(0, 2)) > 12))
                {
                    IsTimeAM = false;
                }
                string minutesOnly = timeNumsOnly.Substring((timeNumsOnly.Length - 2), 2);

                double minutesAsDouble = Convert.ToDouble(minutesOnly);
                double minutes = (Math.Round(minutesAsDouble, 2) / 60);

                string hoursOnly = timeNumsOnly.Substring(0, (timeNumsOnly.Length - 2));
                double hours = Convert.ToDouble(hoursOnly);
                if (!IsTimeAM && hours < 12)
                {
                    hours += 12;
                }

                return hours + Math.Round(minutes, 2);
            }
            return 0;
        }

        public double TimeDuration(string bedtimeRec, string waketimeRec)
        {
            double bedtime = Convert.ToDouble(bedtimeRec);
            double waketime = Convert.ToDouble(waketimeRec);
            double duration;
            if (bedtime > 12)
            {
                if (waketime > 12)
                {
                    duration = Math.Round(((24 - bedtime) - (24 - waketime)), 2);
                }
                else
                {
                    duration = Math.Round(((24 - bedtime) + waketime), 2);
                }
            }
            else
            {
                duration = Math.Round(waketime - bedtime, 2);
            }
            return duration;
        }

        public double TimeAverage(double timeTotal, int count) => Math.Round(Convert.ToDouble(timeTotal) / count, 2);
    }
}
