﻿using System;
using System.Linq;

namespace RoyApp
{
    static class TimeToData
    {
        public static double TimeToDecimal(string time)
        {
            bool IsTimeAM = true;
            string timeNoSpace = time.Trim();
            if (timeNoSpace.EndsWith("PM") || (timeNoSpace.Length > 2 && Convert.ToDouble(timeNoSpace.Substring(0, 2)) > 12))
            {
                IsTimeAM = false;
            }

            string timeNumsOnly = new string(time.Where(c => char.IsDigit(c)).ToArray());

            if (timeNumsOnly.Length >= 3)
            {
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

        public static double TimeDuration(string bedtimeRec, string waketimeRec)
        {
            double bedtime = Convert.ToDouble(bedtimeRec);
            double waketime = Convert.ToDouble(waketimeRec);
            double duration = Math.Round((waketime - bedtime), 2);
            return duration;
        }

        public static double TimeAverage(double timeTotal, int count)
        {
            return Math.Round((Convert.ToDouble(timeTotal) / count), 2);
        }
    }
}