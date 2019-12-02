using System;
using System.Linq;

namespace StringCalculator
{
    public class StringCalculator20191130
    {
        public int Add(string numbers)
        {
            int startIndexNumbers = GetDelimiters(numbers, out char[] delimiters);
            var actualNumbers = numbers.Substring(startIndexNumbers);
            if (actualNumbers.Equals("")) return 0;
            if (actualNumbers.Length == 1) return int.Parse(actualNumbers);
            else return actualNumbers.Split(delimiters).Select(x => int.Parse(x)).Aggregate(
                (x, y) => 
                {
                    if (y < 0) throw new Exception($"negatives not allowed - {y}");
                    return x + y;
                }
            );
        }

        private int GetDelimiters(string numbers, out char[] delimiters)
        {
            if (numbers.StartsWith("//"))
            {
                delimiters = new char[3] { ',', '\n', numbers[2] };
                return 4;
            }
            else
            {
                delimiters = new char[2] { ',', '\n' };
                return 0;
            }
        }
    }
}
