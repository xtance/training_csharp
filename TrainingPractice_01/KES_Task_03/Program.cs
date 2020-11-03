using System;

namespace KES_Task_03
{
    class Program
    {
        private const string key = "csharp";
        private const string message = "Hello!";
        static void Main(string[] args)
        {
            int attempts = 0;
            string pass = "";
            while (!pass.Equals(key)) {
                attempts++;
                if (attempts > 3) System.Environment.Exit(1);
                Console.WriteLine($"Введите пароль. Попытка {attempts} из 3");
                pass = Console.ReadLine();
            }
            Console.WriteLine($"Секретное сообщение: {message}");
        }
    }
}
