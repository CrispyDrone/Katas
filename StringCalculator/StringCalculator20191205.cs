using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StringCalculator
{
    public class StringCalculator20191205
    {
        public int Add(string numbers)
        {
            if (numbers == "") return 0;

            var integers = GetIntegers(numbers);
            ThrowIfNegatives(integers);
            integers = IgnoreNumbersGreaterThan1000(integers);

            return integers.Sum();
        }

        private IEnumerable<int> IgnoreNumbersGreaterThan1000(IEnumerable<int> integers)
        {
            return integers .Where(i => i <= 1000);
        }

        private IEnumerable<int> GetIntegers(string numbers)
        {
            var delimiters = GetAndRemovetDelimiters(ref numbers);

            return ParseIntegers(numbers, delimiters);
        }

        private IEnumerable<int> ParseIntegers(string numbers, string[] delimiters)
        {
            var escapedDelimiters = delimiters.Select(d => Regex.Escape(d));
            var regex = new Regex($@"(-?\d+)(?:{string.Join("|", escapedDelimiters)})");

            var match = regex.Match(numbers);

            var lastMatch = match;

            while (match.Success)
            {
                yield return int.Parse(match.Groups[1].Value);
                lastMatch = match;
                match = match.NextMatch();
            }

            if (lastMatch.Success)
            {
                yield return int.Parse(numbers.Substring(lastMatch.Groups[1].Index + lastMatch.Groups[0].Value.Length));
            }
            else
            {
                yield return int.Parse(numbers);
            }
        }

        private void ThrowIfNegatives(IEnumerable<int> integers)
        {
            var negatives = integers.Where(i => i < 0);

            if (negatives.Any())
                throw new Exception($"Negatives not allowed - {string.Join(",", negatives)}");
        }

        private string[] GetAndRemovetDelimiters(ref string numbers)
        {
            if (numbers.StartsWith("//"))
            {
                var customDelimiters = GetCustomDelimiters(numbers.Trim('/'));
                numbers = RemoveFirstLine(numbers);
                return new string[] { ",", "\n" }.Concat(customDelimiters).ToArray();
            }

            return new string[] { ",", "\n" };
        }

        private string RemoveFirstLine(string numbers)
        {
            return numbers.Substring(numbers.IndexOf('\n') + 1);
        }

        private IEnumerable<string> GetCustomDelimiters(string numbers)
        {
            var delimiterLine = GetDelimiterLine(numbers);
            var regex = new Regex(@"([^\[\]]+)");

            var match = regex.Match(delimiterLine);

            while (match.Success)
            {
                yield return match.Groups[1].Value;
                match = match.NextMatch();
            }
        }

        private string GetDelimiterLine(string numbers)
        {
            return numbers.Substring(0, numbers.IndexOf('\n'));
        }
    }
}
