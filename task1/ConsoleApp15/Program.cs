using System.Text.RegularExpressions;

namespace ConsoleApp15
{
    internal class Program
    {
        static string WrongChars(string text)
        {
            int count = 0;
            char[] chars = text.ToCharArray();
            char[] result = new char[chars.Length];
            for (int i = 0; i < chars.Length; i++)
            {
                if (!(chars[i] >= 'a' && chars[i] <= 'z'))
                {
                    char a = chars[i];
                    result[i] = a;
                    count++;
                }
            }
            var b = string.Join("", result);
            if (count > 0)
                return b;
            else
                return text;
        }
        static bool IsOnlyLetters_Method(string text)
        {
            if (Regex.IsMatch(text, "^[a-z]*$")) //Метод проверяет символы по шаблону
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        static string ReversString(string text)
        {
            string ans;
            int len = text.Length;
            if (len % 2 == 0) //Проверка на чётность , если чётное поделить на две части и перевернуть
            {
                char[] s1 = text.Substring(0, len / 2).ToCharArray();
                char[] s2 = text.Substring(len / 2).ToCharArray();
                Array.Reverse(s1);
                Array.Reverse(s2);
                ans = new string(s1) + new string(s2);
                return ans;
            }
            else // Перевернуть строку
            {
                char[] s3 = text.ToCharArray();
                Array.Reverse(s3);
                string s4 = new string(s3);
                ans = s4 + text;
                return ans;
            }
        }
        static Dictionary<char, int> NumberOfOccurrences(string text)
        {
            {   
                return text.GroupBy(c => c)//Строка - коллекция символов , поэтому при помощи GroupBy перебираем её
                       .ToDictionary(g => g.Key, g => g.Count());
            }

        }
        static void Main(string[] args)
        {
            string s;
            s = Console.ReadLine(); //Вход данных
            if (IsOnlyLetters_Method(s) == true) //Проверка на введение символов
            {
                Console.WriteLine(ReversString(s)); // Переворот строки
                var result = NumberOfOccurrences(s); //Получение количества вхождений символов
                foreach (var item in result)
                {
                    Console.WriteLine($"Символ: {item.Key}, Количество: {item.Value}"); // Вывод количества символов 
                }
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Введены некорректные символы : " + WrongChars(s));
                
                Console.ReadKey();
            }
        }
    }
} 