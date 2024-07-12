using System;
using System.Text.RegularExpressions;
using System.Threading.Channels;

namespace ConsoleApp15
{
    internal class Program
    {
        static string WrongChars(string text)
        {                                           //Выявление неправильно введёных символов и склеивание в строку
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
        static string FindLongestVowelSubstring(string text)
        {
            string vowels = "aeiouy"; //Определение гласных
            int maxLength = 0;
            string longestSubstring = string.Empty;

            for (int i = 0; i < text.Length; i++) // Внешний цикл на поиск гласной
            {
                if (vowels.Contains(text[i]))
                {
                    for (int j = text.Length - 1; j > i; j--) // Внутренний цикл на поиск самой длинной подстроки
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
        public class QuickSortStrings
        {
            public static string Sort(string input)
            {
                char[] array = input.ToCharArray(); //Строка в массив символов
                Sort(array, 0, array.Length - 1);
                return new string(array);
            }

            private static void Sort(char[] array, int left, int right) 
            {
                if (left < right)
                {
                    int pivotIndex = Partition(array, left, right); //Быстрая сортировка на массиве символов
                    Sort(array, left, pivotIndex - 1);
                    Sort(array, pivotIndex + 1, right);
                }
            }

            private static int Partition(char[] array, int left, int right) // Сортирует по алфавитному порядку с определением опорного элемента
            {
                char pivot = array[right];
                int i = left - 1;

                for (int j = left; j < right; j++)
                {
                    if (array[j] <= pivot)
                    {
                        i++;
                        Swap(array, i, j); // Меняет местами два элемента массива
                    }
                }

                Swap(array, i + 1, right);
                return i + 1;
            }

            private static void Swap(char[] array, int a, int b)
            {
                char temp = array[a];
                array[a] = array[b];
                array[b] = temp;
            }
        }
        public class TreeNode
        {
            public char Value; // Узел бинарного дерева поиска , содержит значение узла и ссылки на поддеревья
            public TreeNode Left;
            public TreeNode Right;

            public TreeNode(char value)
            {
                Value = value;
                Left = null;
                Right = null;
            }
        }

        public class TreeSort
        {
            public static string Sort(string input) //Вставляет каждый символ строки в бинарное дерево
            {
                

                TreeNode root = null;
                foreach (char c in input)
                {
                    root = Insert(root, c);
                }

                string sortedString = InOrderTraversal(root);
                return sortedString;
            }

            private static TreeNode Insert(TreeNode node, char value) // Вставка нового узла
            {
                if (node == null)
                {
                    return new TreeNode(value);
                }

                if (value < node.Value)
                {
                    node.Left = Insert(node.Left, value);
                }
                else
                {
                    node.Right = Insert(node.Right, value);
                }

                return node;
            }

            private static string InOrderTraversal(TreeNode node) // Обход дерева в порядке in-order-travelsal
            {
                if (node == null)
                {
                    return string.Empty;
                }

                string left = InOrderTraversal(node.Left);
                string right = InOrderTraversal(node.Right);

                return left + node.Value + right;
            }
        }
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                string s = Console.ReadLine(); //Вход данных
                if (IsOnlyLetters_Method(s) == true) //Проверка на введение символов
                {
                    Console.WriteLine(ReversString(s)); // Переворот строки
                    var result = NumberOfOccurrences(s); //Получение количества вхождений символов
                    foreach (var item in result)
                    {
                        Console.WriteLine($"Символ: {item.Key}, Количество: {item.Value}"); // Вывод количества символов 
                    }
                    Console.WriteLine(FindLongestVowelSubstring(ReversString(s))); //Поиск самой длинной подстроки по гласным
                    Console.WriteLine("Выберите метод сортировки:");
                    Console.WriteLine("1. Быстрая сортировка (Quick Sort)");
                    Console.WriteLine("2. Сортировка деревом (Tree Sort)");
                    int choice = int.Parse(Console.ReadLine());

                    switch (choice) // Выбор метода сортировки
                    {
                        case 1:
                            QuickSortStrings.Sort(ReversString(s));
                            Console.WriteLine("Отсортировано с помощью быстрой сортировки:" + QuickSortStrings.Sort(s));
                            break;
                        case 2:
                            TreeSort.Sort(ReversString(s));
                            Console.WriteLine("Отсортировано с помощью сортировки деревом:" + TreeSort.Sort(s));
                            break;
                        default:
                            Console.WriteLine("Неверный выбор");
                            return;
                    }
                }
                else
                {
                    Console.WriteLine("Введены некорректные символы : " + WrongChars(s));

                    Console.ReadKey();
                }
                Console.WriteLine("Хотите перезапустить программу? (y - Да/n - Нет)");
                string restartChoice = Console.ReadLine().ToLower();
                if (restartChoice != "y")
                {
                    break;
                }
            }
        }
    }
} 