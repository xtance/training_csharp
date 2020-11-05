using System;

namespace KES_Task_07
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите размер массива: ");
            int size = Convert.ToInt32(Console.ReadLine());
            int[] arr = new int[size];
            Random random = new Random();
            for (int i = 0; i < size; i++) arr[i] = random.Next(0, 100);

            Console.WriteLine("\nИзначальный массив:");
            Print(arr, size);

            // Перемешиваем массив
            for (int i = size - 1; i > 0; i--) {
                int j = random.Next(0, i + 1);
                int temp = arr[j];
                arr[j] = arr[i];
                arr[i] = temp;
            }

            Console.WriteLine("Перемешанный массив:");
            Print(arr, size);
            Console.ReadLine();
        }

        // Удобный вывод массива
        static void Print(int[] arr, int size)
        {
            Console.Write("\n");
            for (int i = 0; i < size; i++)
            {
                Console.Write($"{arr[i]}\t");
                if ((i+1) % 5 == 0) Console.Write("\n");
            }
            Console.Write("\n");
        }
    }
}
