using System;

namespace Lab_02 {
    class Program {
        #region Input

        static string InputData(string message) {
            Console.WriteLine(message);

            Console.Write("> ");

            return Console.ReadLine();
        }

        #endregion

        #region Input Checkers

        static uint NotNullInputCheck(string message)
        {
            bool input_first_scan = true;
            uint input;

            do
            {
                if (!input_first_scan)
                {
                    Console.WriteLine("\n!!!                     Ошибка ввода                    !!!\n" +
                        "--- Допустим ввод в формате целого положительного числа ---" +
                        "---                   Не равного нулю                   ---");
                }
                else input_first_scan = false;

                Console.WriteLine(message);

                Console.Write("> ");

            } while (!uint.TryParse(Console.ReadLine(), out input) || input == 0);

            return input;
        }

        static uint InputCheck(string message) {
            bool input_first_scan = true;
            uint input;

            do {
                if (!input_first_scan) {
                    Console.WriteLine("\n!!!                     Ошибка ввода                    !!!\n" +
                        "--- Допустим ввод в формате целого положительного числа ---");
                }
                else input_first_scan = false;

                Console.WriteLine(message);

                Console.Write("> ");

            } while (!uint.TryParse(Console.ReadLine(), out input));

            return input;
        }

        static uint InRangeInputCheck(int begin, int end, string message) {
            bool input_first_scan = true;
            uint input;

            do {
                if (!input_first_scan) {
                    Console.WriteLine("\n!!!                     Ошибка ввода                    !!!\n" +
                        "--- Допустим ввод в формате целого положительного числа ---\n" +
                        "---                 Заданного диапазона                 ---");
                }
                else input_first_scan = false;

                Console.WriteLine(
                    message +
                    "***                  В диапазоне от " +
                    begin.ToString() +
                    " до                ***\n" +
                    "***                  " +
                    end.ToString() +
                    "                ***\n"
                    );

                Console.Write("> ");

            } while (!uint.TryParse(Console.ReadLine(), out input) || input < begin || input > end);

            return input;
        }

        #endregion

        #region Menu

        static void Menu() {
            uint menu;

            do {
                menu = InputCheck(
                    "\n***                    Основное меню                    ***\n" +
                    "1. Создание нового блока\n" +
                    "2. Вывод информации о ранее созданых блоках\n" +
                    "3. Проверка всех созданных блоков\n" +
                    "4. Изменение блока\n" +
                    "5. Сохранение в файл\n" +
                    "6. Загрузка из файла\n" +
                    "0. Выход"
                    );

                switch (menu) {
                    case 1:
                        Blockchain.AddBlock(InputData("\n>>>                   Задайте данные                    <<<"));

                        break;

                    case 2:
                        Blockchain.Show();

                        break;

                    case 3:
                        Blockchain.Verification();

                        break;

                    case 4:
                        if (Blockchain.GetBlockchain.Count != 0) {
                            Blockchain.EditBlock(
                            (int)InRangeInputCheck(0, Blockchain.GetBlockchain.Count - 1, "\n>>>          Задайте номер блока для изменени           <<<\n"),
                            InputData("\n>>>                Задайте новые данные                 <<<"));
                        }

                        break;

                    case 5:
                        Blockchain.SaveToFile(InputData("\n>>>          Задайте имя файла для сохранения           <<<"));

                        break;

                    case 6:
                        Blockchain.LoadFromFile(InputData("\n>>>           Задайте имя файла для загрузки            <<<"));

                        break;
                }

            } while (menu != 0);
        }

        #endregion

        static void Main(string[] args) {
            Console.WriteLine("-----------------------------------------------------------\n" +
        "---                Лабораторная работа 02               ---\n" +
        "---                                                     ---\n" +
        "-----------------------------------------------------------");

            Menu();
        }
    }
}
