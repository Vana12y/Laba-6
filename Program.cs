using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Test("5", "5", "5");
            Test("5", "5", "3");
            Test("6", "8", "10");
            Test("1", "1", "3");
            Test("abc", "5", "5");
            Test("-5", "5", "5");

            Console.ReadKey();
        }

        static void Test(string a, string b, string c)
        {
            Console.WriteLine($"\nВход: {a}, {b}, {c}");
            var result = triangle(a, b, c);
            Console.WriteLine($"Тип: {result.type}");
            Console.WriteLine($"Точки: A{result.points[0]} B{result.points[1]} C{result.points[2]}");
        }

        static (string type, List<(int, int)> points) triangle(string s1, string s2, string s3)
        {
            float a, b, c;

            if (!float.TryParse(s1, out a) || !float.TryParse(s2, out b) || !float.TryParse(s3, out c))
            {
                Log(false, $"{s1},{s2},{s3}", "нечисловые данные");
                return ("", new List<(int, int)> { (-2, -2), (-2, -2), (-2, -2) });
            }

            if (a <= 0 || b <= 0 || c <= 0)
            {
                Log(false, $"{a},{b},{c}", "стороны должны быть >0");
                return ("не треугольник", new List<(int, int)> { (-1, -1), (-1, -1), (-1, -1) });
            }

            if (a + b <= c || a + c <= b || b + c <= a)
            {
                Log(false, $"{a},{b},{c}", "треугольник не существует");
                return ("не треугольник", new List<(int, int)> { (-1, -1), (-1, -1), (-1, -1) });
            }

            string type = "разносторонний";
            if (a == b && b == c)
                type = "равносторонний";
            else if (a == b || a == c || b == c)
                type = "равнобедренный";

            var points = Coords(a, b, c);
            Log(true, $"{a},{b},{c}", $"{type}");

            return (type, points);
        }

        static List<(int, int)> Coords(float a, float b, float c)
        {
            int x1 = 10, y1 = 90;
            int x2 = x1 + (int)(c + 0.5f);
            int y2 = y1;

            double xx = (a * a - b * b + c * c) / (2 * c);
            double yy = Math.Sqrt(Math.Abs(a * a - xx * xx));

            int x3 = x1 + (int)(xx + 0.5);
            int y3 = y1 - (int)(yy + 0.5);

            if (x3 < 0) x3 = 0;
            if (x3 > 100) x3 = 100;
            if (y3 < 0) y3 = 0;
            if (y3 > 100) y3 = 100;

            return new List<(int, int)> { (x1, y1), (x2, y2), (x3, y3) };
        }

        static void Log(bool success, string data, string result)
        {
            string msg = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {data} {result}";
            Console.WriteLine(msg);

            try
            {
                File.AppendAllText("log.txt", msg + Environment.NewLine);
            }
            catch { }
        }
    }
}
