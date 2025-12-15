using System;
using System.Numerics;
using System.Collections.Generic;

namespace MyCat.Domain
{
    public static class FormatNumbers
    {
        private static readonly Dictionary<long, string> units = new Dictionary<long, string>()
        {
            { 0, "" },
            { 1, "K" },
            { 2, "M" },
            { 3, "B" },
            { 4, "T" }
        };

        // T 단위를 넘어가면 알파벳 소문자로 표기함 (ex. 10aa)
        private static readonly int charA = Convert.ToInt32('a');

        // 빅넘버 기반의 포매팅
        public static string Format(BigNumber number)
        {
            if (number.Value == 0)
            {
                return "0";
            }

            var exp = (int)(number.Exponent / 3);
            var value = (number.Value) * Math.Pow(10, number.Exponent % 3);
            var unit = "";
            if (exp < units.Count)
            {
                unit = units[exp];
            }
            else
            {
                var unitInt = exp - units.Count;
                var secondUnit = unitInt % 26;
                var firstUnit = unitInt / 26;
                unit = Convert.ToChar(firstUnit + charA).ToString()
                       + Convert.ToChar(secondUnit + charA).ToString();
            }
            
            return value.ToString("0.##") + unit;
        }
    }
}