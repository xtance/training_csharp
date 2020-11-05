using System;
using System.Timers;

namespace KES_Task_05
{
    class Program
    {
        // Файл level.txt должен лежать в KES_Task_05/bin/debug
        // Настройки:
        const char C_PLAYER = '■';
        const char C_ENEMY = '*';
        const char C_END = 'E';
        const int TURNS = 100;

        // Глобальные переменные
        static int _player = -1, _enemy = -1, _len = 0, _len2 = 0, _bar = -1;
        static Random _random = new Random();
        static char[] _chars;
        static Timer _timer;
        static bool _dead = false;

        static void Main(string[] args)
        {
            // Читаем файл, узнаем сколько в нём строк
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader(@"level.txt");
            while ((line = file.ReadLine()) != null) _len += line.Length;
            _chars = new char[_len];

            // Ещё раз читаем, заносим символы в массив _chars
            _len = 0;
            line = null;
            file = new System.IO.StreamReader(@"level.txt");
            while ((line = file.ReadLine()) != null)
            {
                char[] temp = line.ToString().ToCharArray();
                Console.WriteLine(temp);
                temp.CopyTo(_chars, _len);
                _len += _len2 = line.Length;
            }

            // Инструкция
            Console.SetCursorPosition(35, 0);
            Console.Write($"Вы - {C_PLAYER}. Цель игры - попасть в точку {C_END},");
            Console.SetCursorPosition(35, 1);
            Console.Write($"не встретившись с врагом {C_ENEMY}!");
            Console.SetCursorPosition(35, 2);
            Bar();

            // Ищем где находятся игрок, враг, цель, вылетаем если не нашлись
            _player = GetChar(C_PLAYER);
            _enemy = GetChar(C_ENEMY);
            int e = GetChar(C_END);
            if (_player == -1 || _enemy == -1 || GetChar(C_END) == -1)
            {
                Console.WriteLine($"Ошибка в уровне, отсутствует игрок {C_PLAYER}, или враг {C_ENEMY}, или цель {C_END}...");
                System.Environment.Exit(1);
            }

            // Таймер врага
            _timer = new Timer(200);
            _timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            _timer.Start();

            // Слушаем нажатия
            while (!_dead)
            {
                int next = -1;
                char key = Console.ReadKey(true).KeyChar;

                if (key == 'w' || key == 'ц') next = _player - 31;
                else if (key == 's' || key == 'ы') next = _player + 31;
                else if (key == 'a' || key == 'ф') next = _player - 1;
                else if (key == 'd' || key == 'в') next = _player + 1;
                else next = -1;

                // Если вылетели за пределы _chars - пропускаем
                if (next < 0 || next >= _len) continue;

                if (_chars[next] == C_ENEMY)
                {
                    WriteAt(_player, ' ');
                    WriteAt(next, C_ENEMY);
                    GameOver("Вы проиграли, встретившись с врагом! ");
                }
                else if (_chars[next] == ' ')
                {
                    WriteAt(_player, ' ');
                    WriteAt(next, C_PLAYER);
                    _player = next;
                    Bar();
                }
                else if (_chars[next] == C_END)
                {
                    WriteAt(_player, ' ');
                    WriteAt(next, 'E');
                    GameOver("Вы выиграли! Ура! ");
                }
            }

            // Закрываем файл, не закрываем программу
            file.Close();
            Console.ReadLine();
        }

        // Ищет номер символа в _chars
        public static int GetChar( char c)
        {
            for (int i = 0; i < _len; i++)
            {
                if (_chars[i] == c) return i;
            }
            return -1;
        }

        // Пишет символ по номеру в _chars
        public static void WriteAt(int where, char what)
        {
            int top = where / _len2;
            int left = where - (top * _len2);
            Console.SetCursorPosition(left, top);
            Console.Write(what);
            _chars[where] = what;
        }

        // Таймер противника
        private static void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            // Ходит как повезёт
            int next = -1;
            switch (_random.Next(0, 3))
            {
                case 0: next = _enemy - 31; break;
                case 1: next = _enemy + 31; break;
                case 2: next = _enemy - 1; break;
                case 3: next = _enemy + 1; break;
            }

            if (next >= 0 && next < _len)
            {
                if (_chars[next] == ' ')
                {
                    WriteAt(_enemy, ' ');
                    WriteAt(next, C_ENEMY);
                    _enemy = next;
                }
                else if (_chars[next] == C_PLAYER)
                {
                    WriteAt(_enemy, ' ');
                    WriteAt(next, C_ENEMY);
                    _enemy = next;
                    GameOver("Вы проиграли - вас съел враг! ");
                }
            }
        }

        // Конец игры
        private static void GameOver(string why)
        {
            Console.SetCursorPosition(0, 29);
            Console.Write(why);
            _timer.Stop();
            _timer.Enabled = false;
            _dead = true;
        }

        // Рисует полоску ходов, чем больше тем хуже
        private static void Bar() 
        {
            _bar++;
            int temp = 0;
            Console.SetCursorPosition(35, 2);
            Console.Write($"{_bar} / {TURNS} ходов...");
            Console.SetCursorPosition(35, 3);
            Console.Write("[");
            for (int i = 0; i <= TURNS; i++) if (i % 5 == 0) 
            {
                temp++;
                Console.SetCursorPosition(35 + temp, 3);
                Console.Write(i <= _bar ? '#' : '_');
            }
            Console.Write("]");

            if (_bar >= TURNS)
            {
                GameOver("Вы проиграли - кончились ходы! ");
            }
        }
    }
}
