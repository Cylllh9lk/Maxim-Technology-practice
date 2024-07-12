
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ConsoleApp15
{
    internal class Program

    {


        static string WrongChars(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return text;
            }

            StringBuilder result = new StringBuilder();
            foreach (char c in text)
            {
                if (!(c >= 'a' && c <= 'z'))
                {
                    result.Append(c);
                }
            }

            return result.Length > 0 ? result.ToString() : text;
        }

        static bool IsOnlyLetters_Method(string text)
        {
            return Regex.IsMatch(text, "^[a-z]*$");
        }

        static string ReversString(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return text;
            }

            int len = text.Length;
            if (len % 2 == 0)
            {
                char[] s1 = text.Substring(0, len / 2).ToCharArray();
                char[] s2 = text.Substring(len / 2).ToCharArray();
                Array.Reverse(s1);
                Array.Reverse(s2);
                return new string(s1) + new string(s2);
            }
            else
            {
                char[] s3 = text.ToCharArray();
                Array.Reverse(s3);
                return new string(s3) + text;
            }
        }

        static Dictionary<char, int> NumberOfOccurrences(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return new Dictionary<char, int>();
            }

            return text.GroupBy(c => c)
                       .ToDictionary(g => g.Key, g => g.Count());
        }

        static void Main(string[] args)
        {
            string s = Console.ReadLine();
            if (IsOnlyLetters_Method(s))
            {
                Console.WriteLine(ReversString(s));
                var result = NumberOfOccurrences(s);
                foreach (var item in result)
                {
                    Console.WriteLine($"Символ: {item.Key}, Количество: {item.Value}");
                }
            }
            else
            {
                Console.WriteLine("Введены некорректные символы: " + WrongChars(s));
            }

            Console.ReadKey();
        }
    }
}
