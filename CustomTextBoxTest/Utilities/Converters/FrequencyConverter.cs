using CustomTextBoxTest.Utilities.Converters.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CustomTextBoxTest.Utilities.Converters
{
    public class FrequencyConverter : IConverter
    {
        private const int MEGA = 1000000;
        private const int KILO = 1000;

        public string ConvertToString(double value)
        {
            if (value >= MEGA)
                return Convert(value, MEGA, "МГц");

            if (value >= KILO)
                return Convert(value, KILO, "кГц");

            return Convert(value, 1, "Гц");
        }

        public double? ParseFromString(string value)
        {
            var preparedString = value.Replace(" ", "").ToLower();
            var pattern = "^[0-9]+[.,]*[0-9]*";
            var regex = new Regex(pattern);
            var match = regex.Match(preparedString);

            if (!match.Success || !double.TryParse(match.Value, out double result))
                return null;

            var matchEndIndex = match.Index + match.Length;
            var units = preparedString.Substring(matchEndIndex);
            var multiplier = GetMultiplier(units);

            if (multiplier <= 0)
                return null;

            return result * multiplier;
        }

        private string Convert(double value, double divisor, string units)
        {
            var result = value / divisor;
            var roundedResult = Math.Round(result, 2);
            return roundedResult + " " + units;
        }

        private double GetMultiplier(string value)
        {
            switch (value)
            {
                case "мгц":
                case "м":
                    return MEGA;
                case "кгц":
                case "к":
                    return KILO;
                case "гц":
                case "г":
                case "":
                    return 1;
                default:
                    return 0;
            }
        }
    }
}
