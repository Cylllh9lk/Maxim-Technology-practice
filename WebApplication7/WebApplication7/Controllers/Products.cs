using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using WebApplication7.Models;

namespace YourNamespace.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StringProcessingController : ControllerBase
    {
        private static readonly HttpClient client = new HttpClient();
        private readonly StringChecker _stringChecker;

        public StringProcessingController(IConfiguration configuration)
        {
            _stringChecker = new StringChecker(configuration);
        }
        [HttpGet]
        public async Task<IActionResult> ProcessString([FromQuery] string input)
        {
            if (string.IsNullOrEmpty(input)) //Проверка на ввод данных 
            {
                return BadRequest("Неверно введённая строка.");
            }
            if (!_stringChecker.IsStringAllowed(input)) //Проверка по черному списку
            {
                return BadRequest("Строка содержит запрещенные слова.");
            }
            if (!IsOnlyLetters_Method(input)) //Проверка символов строки
            {
                return BadRequest("Неправильно введёные символы: " + WrongChars(input));
            }


            string reversedString = StringManipulator.ReversString(input);
            var occurrences = NumberOfOccurrences(input);
            string longestVowelSubstring = FindLongestVowelSubstring(reversedString);

            int randomIndex = await GetRandomNumberAsync(reversedString.Length);
            char removedChar = reversedString[randomIndex];
            string sortedString = QuickSortStrings.Sort(reversedString);
            string resultString = RemoveCharacterAt(reversedString, randomIndex);

            var response = new
            {
                ReversedString = reversedString,
                Occurrences = occurrences,
                LongestVowelSubstring = longestVowelSubstring,
                SortedString = sortedString,
                RemovedChar = $"Символ - '{removedChar}' был удалён , находился на {randomIndex + 1} позиции в строке",
                ResultString = resultString
            };

            return Ok(response);

        }

        public class StringChecker
        {
            private readonly List<string> _blacklist;

            public StringChecker(IConfiguration configuration)
            {
                _blacklist = configuration.GetSection("Settings:Blacklist").Get<List<string>>();
            }

            public bool IsStringAllowed(string input)
            {
                return !_blacklist.Contains(input);
            }
        }
        public static string WrongChars(string text)
        {
            var wrongChars = new List<char>();

            foreach (var c in text)
            {
                // Проверяем, что символ не является буквой от 'a' до 'z'
                if (!(c >= 'a' && c <= 'z'))
                {
                    // Добавляем в список, если это не буква и не пробел
                    if (c != ' ')
                    {
                        wrongChars.Add(c);
                    }
                }
            }

            // Преобразуем список в строку и возвращаем
            return new string(wrongChars.ToArray());
        }


        public static bool IsOnlyLetters_Method(string text)
        {
            if (Regex.IsMatch(text, "^[a-z]*$"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public class StringManipulator
        {
            public static string ReversString(string text)
            {
                string ans;
                int len = text.Length;
                if (len % 2 == 0)
                {
                    char[] s1 = text.Substring(0, len / 2).ToCharArray();
                    char[] s2 = text.Substring(len / 2).ToCharArray();
                    Array.Reverse(s1);
                    Array.Reverse(s2);
                    ans = new string(s1) + new string(s2);
                    return ans;
                }
                else
                {
                    char[] s3 = text.ToCharArray();
                    Array.Reverse(s3);
                    string s4 = new string(s3);
                    ans = s4 + text;
                    return ans;
                }
            }
        }
        public static Dictionary<char, int> NumberOfOccurrences(string text)
        {
            return text.GroupBy(c => c)
                       .ToDictionary(g => g.Key, g => g.Count());
        }

        public static string FindLongestVowelSubstring(string text)
        {
            string vowels = "aeiouy";
            int maxLength = 0;
            string longestSubstring = string.Empty;

            for (int i = 0; i < text.Length; i++)
            {
                if (vowels.Contains(text[i]))
                {
                    for (int j = text.Length - 1; j > i; j--)
                    {
                        if (vowels.Contains(text[j]))
                        {
                            int length = j - i + 1;
                            if (length > maxLength)
                            {
                                maxLength = length;
                                longestSubstring = text.Substring(i, length);
                            }
                            break;
                        }
                    }
                }
            }

            return longestSubstring;
        }

        public static async Task<int> GetRandomNumberAsync(int max)
        {
            string apiUrlTemplate = AppConfig.Configuration["RandomApi"];
            string apiUrl = $"{apiUrlTemplate}?min=0&max={max - 1}&count=1";
            if (!Uri.IsWellFormedUriString(apiUrl, UriKind.Absolute))
            {
                throw new InvalidOperationException("The request URI must be an absolute URI.");
            }
            HttpResponseMessage response = await client.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();
            responseBody = new string(responseBody.Where(char.IsDigit).ToArray());

            if (int.TryParse(responseBody, out int randomNumber))
            {
                return randomNumber;
            }
            else
            {
                throw new FormatException($"Некорректный формат данных: не удалось преобразовать '{responseBody}' в число.");
            }
        }

        public static string RemoveCharacterAt(string input, int index)
        {
            return input.Remove(index, 1);
        }
        public static class QuickSortStrings
        {
            public static string Sort(string input)
            {
                char[] array = input.ToCharArray();
                QuickSort(array, 0, array.Length - 1);
                return new string(array);
            }

            private static void QuickSort(char[] array, int low, int high)
            {
                if (low < high)
                {
                    int pi = Partition(array, low, high);

                    QuickSort(array, low, pi - 1);
                    QuickSort(array, pi + 1, high);
                }
            }

            private static int Partition(char[] array, int low, int high)
            {
                char pivot = array[high];
                int i = (low - 1);

                for (int j = low; j < high; j++)
                {
                    if (array[j] < pivot)
                    {
                        i++;
                        Swap(ref array[i], ref array[j]);
                    }
                }

                Swap(ref array[i + 1], ref array[high]);
                return i + 1;
            }

            private static void Swap(ref char a, ref char b)
            {
                char temp = a;
                a = b;
                b = temp;
            }
        }
    }
}
