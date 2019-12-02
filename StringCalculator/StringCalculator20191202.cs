using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System;

namespace StringCalculator
{
    public class StringCalculator20191202
    {
        public int Add(string numbers)
        {
            if (numbers == "") return 0;

            var integers = GetIntegers(numbers);

            return integers.Sum();
        }

        private int[] GetIntegers(string numbers)
        {
            var delimiters = GetAndRemoveDelimiters(ref numbers);
            var integers = ParseIntegers(numbers, delimiters);
            var negatives = integers.Where(i => i < 0);

            if (negatives.Any())
                throw new Exception($"Negatives not allowed - {string.Join(",", negatives)}");

            integers = integers.Where(i => i <= 1000);

            return integers.ToArray();
        }

        private IEnumerable<int> ParseIntegers(string numbers, string[] delimiters)
        {
            var regex = new Regex($@"(-?\d+)(?:{string.Join("|", delimiters)})?");

            var match = regex.Match(numbers);

            while (match.Success)
            {
                yield return int.Parse(match.Groups[1].Value);
                match = match.NextMatch();
            }
        }

        private string[] GetAndRemoveDelimiters(ref string inputString)
        {
            if (GetDelimiters(inputString, out string[] delimiters))
            {
                inputString = RemoveDelimiters(inputString);
            }

            return delimiters;
        }

        private string RemoveDelimiters(string inputString)
        {
            return inputString.Substring(4);
        }

        private bool GetDelimiters(string inputString, out string[] delimiters)
        {
            if (inputString.StartsWith("//"))
            {
                delimiters = new string[] { ",", "\n", inputString.Substring(2, inputString.IndexOf("\n") - 2) };
                return true;
            }

            delimiters = new string[] { ",", "\n" };
            return false;
        }
    }
}
