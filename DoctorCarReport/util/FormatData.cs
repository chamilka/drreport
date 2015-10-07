using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorCarReport.util
{
    class FindDifference
    {
        public static string calculateDuration(Nullable<DateTime> startDate, Nullable<DateTime> endDate)
        {
            string duration = null;
            //return ((DateTime)endDate - (DateTime)startDate).TotalHours.ToString();
            if (endDate == null)
            {
                endDate = DateTime.Now;
            }
            TimeSpan diff = ((DateTime)endDate - (DateTime)startDate);

            int days=diff.Days;
            if (days > 0)
            {
                if (days == 1)
                {
                    duration += days.ToString() + " day: ";
                }
                else
                {
                    duration += days.ToString() + " days: ";
                }
            }

            int hours = diff.Hours;
            if (hours > 0)
            {
                if (hours == 1)
                {
                    duration += hours.ToString() + " hour: ";
                }
                else
                {
                    duration += hours.ToString() + " hours: ";
                }
            }

            int minutes = diff.Minutes;
            if (minutes > 0)
            {
                if (minutes == 1)
                {
                    duration += minutes.ToString() + " minute ";
                }
                else
                {
                    duration += minutes.ToString() + " minutes ";
                }
            }

            //int seconds = diff.Seconds;
            //if (seconds > 0)
            //{
            //    if (seconds == 1)
            //    {
            //        duration += seconds.ToString() + " second";
            //    }
            //    else
            //    {
            //        duration += seconds.ToString() + " seconds";
            //    }
            //}

            return duration;
        }

        public static string calculateDistance(Nullable<double> startOdometer, Nullable<double> endOdometer) {
            if (endOdometer > startOdometer)
                return (endOdometer - startOdometer).ToString();
            else
                return "-";
        }
    }
}
