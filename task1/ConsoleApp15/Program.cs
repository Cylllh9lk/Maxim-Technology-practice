namespace ConsoleApp15
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string s , ans;
            s = Console.ReadLine();
            int len = s.Length;
            if (len % 2 == 0)
            {
                char[] s1 = s.Substring(0, len / 2).ToCharArray();
                char[] s2 = s.Substring(len / 2).ToCharArray();
                Array.Reverse(s1);
                Array.Reverse(s2);
                ans = new string(s1) + new string(s2);
            }
            else
            {
                char[] s3 = s.ToCharArray();
                Array.Reverse(s3);
                string s4 = new string(s3);
                ans = s4 + s;
            }
            Console.WriteLine(ans);
            Console.ReadKey();
        }
    }
} 