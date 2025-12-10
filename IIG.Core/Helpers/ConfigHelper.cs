namespace IIG.Core.Helpers
{
    public static class ConfigHelper
    {

        /// <summary>
        /// new format: sec=second, min = minutes,h = hours,d=day,m=months . can stack example : 1 sec/1 min/1h ect...
        /// </summary>
        /// <param name="configValue"></param>
        /// <param name="needAddValue"></param>
        /// <returns></returns>
        public static DateTime GetDateTimeFromConfig(this string configValue, DateTime needAddValue)
        {
            if (string.IsNullOrWhiteSpace(configValue)) needAddValue = needAddValue.AddHours(1);
            var splitD = configValue.Split("/");
            if (splitD.Length <= 0) needAddValue = needAddValue.AddHours(1);
            foreach (var d in splitD)
            {
                var splitv = d.Split(" ");
                if (splitv.Length <= 0) continue;
                if (splitv[1] == "min")
                {
                    needAddValue = needAddValue.AddMinutes(Convert.ToInt32(splitv[0]));
                }
                else if (splitv[1] == "h")
                {
                    needAddValue = needAddValue.AddHours(Convert.ToInt32(splitv[0]));
                }
                else if (splitv[1] == "d")
                {
                    needAddValue = needAddValue.AddDays(Convert.ToInt32(splitv[0]));
                }
                else if (splitv[1] == "m")
                {
                    needAddValue = needAddValue.AddMonths(Convert.ToInt32(splitv[0]));
                }
                else
                {
                    needAddValue = needAddValue.AddYears(Convert.ToInt32(splitv[0]));
                }
            }
            return needAddValue;
        }
    }
}
