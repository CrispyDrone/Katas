using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace StringCalculator
{
    public class StringCalculator20191203
    {
        public int Add(string numbers)
        {
            if (numbers == "") return 0;
            var integers = GetIntegers(numbers);
            return integers.Sum();
        }

        public int[] GetIntegers(string numbers)
        {
            var delimiters = GetAndRemoveDelimiters(ref numbers);
            var integers = ParseIntegers(numbers, delimiters);
            ThrowIfNegatives(integers);
            integers = IgnoreGreaterThan1000(integers);

            return integers.ToArray();
        }

        private IEnumerable<int> ParseIntegers(string numbers, string[] delimiters)
        {
            var alternationConstruct = Regex.Escape(string.Join("|", delimiters));
            var regex = new Regex($@"(-?\d+)(?:{alternationConstruct})?");

            var match = regex.Match(numbers);
            while (match.Success)
            {
                yield return int.Parse(match.Groups[1].Value);
                match = match.NextMatch();
            }
        }

        private IEnumerable<int> IgnoreGreaterThan1000(IEnumerable<int> integers)
        {
            return integers.Where(i => i <= 1000);
        }

        private void ThrowIfNegatives(IEnumerable<int> integers)
        {
            var negatives = integers.Where(i => i < 0);

            if (negatives.Any())
                throw new Exception($"Negatives not allowed - {string.Join(",", negatives)}");
        }

        private string[] GetAndRemoveDelimiters(ref string numbers)
        {
            if (numbers.StartsWith("//"))
            {
                var specifiedDelimiter = numbers.Substring(2, numbers.IndexOf('\n') - 2).Trim('[', ']');
                var delimiters = new string[] { ",", "\n", specifiedDelimiter };
                numbers = numbers.Substring(4);
                return delimiters;
            }

            return new string[] { ",", "\n" };
        }
    }
}
