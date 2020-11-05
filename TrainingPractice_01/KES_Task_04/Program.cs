using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace KES_Task_04
{
    class Program
    {
        //Класс для заклинаний
        public struct Spell {
            public string name;
            public int damage, cost;
            public double chance;
            public bool used, depends;
            public Spell(string name, int damage, int cost, bool depends, double chance) {
                this.name = name;
                this.damage = damage;
                this.cost = cost;
                this.depends = depends;
                this.chance = chance;
                used = false;
            }
        }
        static void Main(string[] args)
        {
            const int MAX = 5; // Максимум возможных заклинаний
            Spell[] arr = new Spell[MAX]; // Массив для заклинаний
            Random random = new Random(); // Рандом
            String[] names = { "акулеус", "бомбардо", "редукто", "фенестрам", "экспульсо" }; // Названия заклинаний
            names = names.OrderBy(x => random.Next()).ToArray(); // Перемешиваем их в случайном порядке
            
            int hp = random.Next(200, 300);
            int boss = random.Next(450, 500);

            Console.WriteLine($"Игра\"Победи босса\"..\nСписок заклинаний:");
            for (int i = 0; i < MAX; i++)
            {
                arr[i] = new Spell(
                    names[i], // имя
                    random.Next(75, 150), // отнятие хп у врага
                    random.Next(5, 50), // отнятие хп у использовавшего заклинание (цена)
                    (random.Next(0, 100) > 70 && i > 0) ? true : false, // зависит от предыдущего или нет
                    random.NextDouble() * (1.0 - 0.75) + 0.75 // шанс
                );
                Console.WriteLine($"\n {i + 1}) Заклинание: {arr[i].name} \n Нанесёт урон: {arr[i].damage} ХП, отнимет у вас: {arr[i].cost} ХП \n Шанс успеха: {(arr[i].chance * 100).ToString("#")}%");
                if (arr[i].depends) Console.WriteLine($" Сработает, если вы уже использовали заклинание {arr[i-1].name}!");
            }

            Console.WriteLine("\nБой начался!\n");
            while (boss > 0 && hp > 0) {
                Console.WriteLine($"\nУ вас {hp} ХП, у босса {boss} ХП!\nВведите заклинание..");
                string input = Console.ReadLine();
                bool found = false;
                for (int i = 0; i < MAX; i++){
                    if (arr[i].name.Equals(input))
                    {
                        if (arr[i].depends && !arr[i - 1].used)
                        {
                            Console.WriteLine($"Для вызова этого заклинания вначале примените: {arr[i - 1].name}!");
                        }
                        else if (random.NextDouble() > arr[i].chance)
                        {
                            Console.WriteLine("Упс, босс увернулся от заклинания!");
                        }
                        else
                        {
                            boss -= arr[i].damage;
                            hp -= arr[i].cost;
                            Console.WriteLine($"Вы нанесли {arr[i].damage} ХП урона, но вам это стоило {arr[i].cost} ХП");
                        }
                        arr[i].used = true;
                        found = true;
                        break;
                    }
                }
                if (!found) Console.WriteLine("Вы переволновались и вызвали несуществующее заклинание..");
                if (boss > 0)
                {
                    int temp = random.Next(0, 50);
                    hp -= temp;
                    Console.WriteLine($"Босс наносит ответный удар: -{temp} ХП...");
                }
                else Console.WriteLine("Босс уже не дышит и не может нанести ответный удар..");
            }

            Console.WriteLine("\n\nИгра окончена!");
            if (boss <= 0 && hp <= 0) Console.WriteLine("Вы пожертвовали собой ради победы над боссом, т.е. умерли оба!");
            else if (boss <= 0) Console.WriteLine($"Вы убили босса, у вас осталось {hp} ХП!");
            else Console.WriteLine($"Вы проиграли, поскольку умерли, у босса осталось {boss} ХП!");
        }
    }
}
