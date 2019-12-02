using System;
using System.Collections.Generic;
using System.Linq;

namespace StringCalculator
{
    public class StringCalculator20191201
    {
        public int Add(string numbers)
        {
            if (numbers == "") return 0;
            var integers = GetIntegers(numbers);
            return integers.Sum();
        }

        private IEnumerable<int> GetIntegers(string numbers)
        {
            var startIndexNumbers = GetDelimiters(numbers, out char[] delimiters);
            var actualNumbers = RemoveDelimiters(numbers, startIndexNumbers);
            return GetIntegers(actualNumbers, delimiters);
        }

        private int GetDelimiters(string numbers, out char[] delimiters)
        {
            var delimitersAsList = new List<char>();
            var startIndexNumbers = 0;
            delimitersAsList.Add(',');
            delimitersAsList.Add('\n');

            if (numbers.StartsWith("//"))
            {
                delimitersAsList.Add(numbers.Substring(2, 1)[0]);
                startIndexNumbers = 4;
            }

            delimiters = delimitersAsList.ToArray();
            return startIndexNumbers;
        }

        private string RemoveDelimiters(string numbers, int startIndexNumbers)
        {
            return numbers.Substring(startIndexNumbers);
        }

        private IEnumerable<int> GetIntegers(string actualNumbers, char[] delimiters)
        {
            var integers = actualNumbers.Split(delimiters).Select(i => int.Parse(i));
            var negatives = integers.Where(i => i < 0);
            
            if (negatives.Any())
                throw new Exception($"Negatives are not valid - {string.Join(",", negatives)}");

            return integers;
        }
    }
}
