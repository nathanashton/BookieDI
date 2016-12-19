using System;

namespace Bookie.Core
{
    public static class Utils
    {
        public static int CalculatePercentage(int current, int startIndex, int endIndex)
        {
            float range = endIndex - startIndex;
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (range == 0)
            {
                range = 1;
            }
            var percentage = (current - startIndex) / range * 100;
            return Convert.ToInt32(percentage);
        }

        public static int CalculatePercentage(long current, long startIndex, long endIndex)
        {
            var range = endIndex - startIndex;
            var percentage = (current - startIndex) / range * 100;
            return Convert.ToInt32(percentage);
        }

        public static string Truncate(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }
    }
}