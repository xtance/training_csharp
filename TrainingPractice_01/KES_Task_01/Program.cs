using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KES_Task_01
{
    class Program
    {
        public const int rate = 100;
        static void Main(string[] args)
        {
            Console.WriteLine("Введите золото: ");
            int gold = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine($"Сколько кристаллов вы хотите купить? Курс 1 кр. = {rate} золота");
            int crystals = Convert.ToInt32(Console.ReadLine());

            try
            {
                int[] arr = new int[gold+1];
                int res = gold - crystals * rate;
                arr[res] = 1;
                Console.WriteLine($"У вас {crystals} кристаллов и {res} золота.");
            }
            catch (Exception e) {
                Console.WriteLine($"Сделка не удалась - у вас {gold} золота и 0 кристаллов.");
            };
        }
    }
}
