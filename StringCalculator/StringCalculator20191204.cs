using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace StringCalculator
{
    public class StringCalculator20191204
    {
        public int Add(string numbers)
        {
            if (numbers == "") return 0;

            var delimiters = GetAndRemoveDelimiters(ref numbers);
            var integers = GetIntegers(numbers, delimiters);

            return integers.Sum();
        }

        private IEnumerable<int> GetIntegers(string numbers, string[] delimiters)
        {
            var integers = ParseIntegers(numbers, delimiters);
            integers = integers.Where(i => i <= 1000);
            ThrowIfNegatives(integers);
            return integers;
        }

        private IEnumerable<int> ParseIntegers(string numbers, string[] delimiters)
        {
            var escapedDelimiters = Regex.Escape(string.Join("|", delimiters));
            var regex = new Regex($@"(-?\d+)(?:{escapedDelimiters})?");

            var match = regex.Match(numbers);

            while (match.Success)
            {
                yield return int.Parse(match.Groups[1].Value);
                match = match.NextMatch();
            }
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
                var indexOfNewLine = numbers.IndexOf('\n');
                var delimiterLine = numbers.Substring(2, indexOfNewLine - 2);
                var delimiters = ExtractDelimiters(delimiterLine);
                numbers = numbers.Substring(indexOfNewLine + 1);
                return new string[] { ",", "\n"}.Concat(delimiters).ToArray();
            }

            return new string[] { ",", "\n" };
        }

        private IEnumerable<string> ExtractDelimiters(string delimiterLine)
        {
            var regex = new Regex(@"\[(.+)\]");

            var match = regex.Match(delimiterLine);

            while (match.Success)
            {
                yield return match.Groups[1].Value;
                match = match.NextMatch();
            }
        }
    }
}
