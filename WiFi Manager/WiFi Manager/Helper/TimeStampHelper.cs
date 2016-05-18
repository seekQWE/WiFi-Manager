using System;

namespace Tsing.Helper
{
    class TimeStampHelper
    {
        public string timeStamp;

        public TimeStampHelper()
        {
            TimeSpan timeSpan = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            timeStamp = Convert.ToInt64(timeSpan.TotalSeconds).ToString();
        }

        public TimeStampHelper(DateTime DateTime)
        {
            TimeSpan timeSpan = DateTime - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            timeStamp = Convert.ToInt64(timeSpan.TotalSeconds).ToString();
        }

        public DateTime toDateTime()
        {
            DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan toNow = new TimeSpan(long.Parse(timeStamp + "0000000"));
            return start.Add(toNow);
        }

        public override string ToString()
        {
            return timeStamp;
        }
    }
}
