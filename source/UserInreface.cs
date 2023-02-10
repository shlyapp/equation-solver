using EquationSolver.Equation;
using ConsoleInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using equationSolver.Equation;

namespace EquationSolver
{
    public class UserInreface
    {

        /// <summary>
        /// Отображение информации о разработчиках
        /// </summary>
        private static void ShowTitles()
        {
            // Список титров
            List<string> titles = new List<string>() {
                "TeamLeader",
                "Насибуллин Даниил",
                "", "КОМАНДА ТЕСТИРОВЩИКОВ", "",
                "Бимаков Даниил",
                "Федотов Павел",
                "Меркульев Никита",
                "", "КОМАНДА РАЗРАБОТЧИКОВ БИБЛИОТЕКИ", "",
                "Шкляев Дмитрий",
                "Колбин Илья",
                "","КОМАНДА РАЗРАБОТЧИКОВ UI", "",
                "Костенков Даниил",
                "Соболев Артур",
                "Широбоков Илья",
                "", "GITHUB РЕПОЗИТОРИЙ", "",
                "https://github.com/shlyapp/equation-solver"};

            // Бесконечный цикл for по возрастанию
            for (int i = 0; ; i++) //i здесь играет роль конвеера, по которому тянутся элементы титров
            {
                //Задрежка 400
                Thread.Sleep(400);
                //Эта переменная нужна для расчета позиции каждого элемента титров
                int position = i;
                //Цикл подсчета позиции каждого элемента титров
                foreach (var title in titles)
                {
                    position = position - 2;
                    if (position < 0) break; //Отмена выведения элементов титров позицией ниже нуля (за кадром)
                    else
                    {
                        //Вывод посредине экрана элемента титров
                        ConsoleScreen.SetLine(new String(' ', Math.Abs((Console.WindowWidth / 2) - (title.Length / 2))) + title, position);
                    }
                }

                ConsoleScreen.RenderConsoleScreen(); //Обновление экрана заготовленными строками
                Console.SetCursorPosition(0, 0); //Перемещение каретки на начало консоли, что бы она не смещалась когда элементы выйдут за экран
                ConsoleScreen.ClearLines(); //Очистка всех заготовленных строк консоли
            }
        }

        /// <summary>
        /// Отображение результатов решения
        /// </summary>
        /// <param name="results">Результаты решения</param>
        /// <param name="equation">Уравнение</param>
        private static void ShowResults(double[] results, string equation)
        {
            //Очистка заготовленных строк консоли для размещения других
            ConsoleScreen.ClearLines();

            //Разметка элементов интерфейса
            ConsoleScreen.SetLine($"Уравнение:{equation}", 0);
            ConsoleScreen.SetLine($"---------------------------", 1);
            ConsoleScreen.SetLine($"Найденые корни:", 2);
            ConsoleScreen.SetLine("Что бы вернуться нажмите Enter", 5);

            //Свич вывода результатов в зависимости от количества корней
            switch (results.Length)
            {
                case 0: //Если 0 корней
                    ConsoleScreen.SetLine("Нет корней", 3);
                    break;
                case 1: //Перескок на кейс 2
                case 2:
                    for (int i = 0; i < results.Length; i++) //Цикл вывода корней
                    {
                        ConsoleScreen.SetLine(Convert.ToString(results[i]), i + 3);
                    }
                    break;
                case 3: //3 корней не бывает, значит фатальная ошибка
                    ConsoleScreen.SetLine("Введеное выражение не уравнение", 3);
                    break;
                default: //На всякий
                    ConsoleScreen.SetLine("Невозможно определить", 3);
                    break;
            }

            //Обновление экрана консоли, отображение всех заготовленных строк
            ConsoleScreen.RenderConsoleScreen();

            //Цикл ожидания ввода Enter, что бы вернуться в главное меню
            while (true)
            {
                ConsoleKey keyConsole = Console.ReadKey(true).Key;

                if (keyConsole == ConsoleKey.Enter)
                {
                    ConsoleScreen.ClearLines(); //Очищаем строки консоли
                    SelectUserAction(); //Вызов главного меню
                }
            }

        }

        /// <summary>
        /// Отображение истории введенных уравнений
        /// </summary>
        public static void ShowHistory()
        {
            EquationLogger.SetLogPath("history.txt"); //Путь к логу
            List<string> solutions = EquationLogger.GetHistory(); //Получает список истории

            ConsoleScreen.ClearLines(); //Очищаем заранее консоль

            for (int i = 0; i < solutions.Count; i++) //База
            {
                ConsoleScreen.SetLine(solutions[i], i); 
            }

            ConsoleScreen.SetLine("Нажмите Enter чтобы вернуться назад", solutions.Count);

            ConsoleScreen.RenderConsoleScreen(); //Обновляем экран(выводим строки)
            ConsoleScreen.ClearLines(); //Очищаем заготовленные строки

            //Цикл ожидания ввода Enter, что бы вернуться в главное меню
            while (true)
            {
                ConsoleKey keyConsole = Console.ReadKey(true).Key;

                if (keyConsole == ConsoleKey.Enter)
                {
                    ConsoleScreen.ClearLines(); //Очищаем строки консоли
                    SelectUserAction(); //Вызов главного меню
                }
            }
        }

        /// <summary>
        /// Взаимодействие с пользователем, выбор действия
        /// </summary>
        public static void SelectUserAction()
        {
            //Константы визуального выделения элементов главного меню (выбрано/невыбрано)
            const string SelectedMarker = "## "; //выбрано
            const string UnSelectedMarker = "-- "; //невыбрано

            int selectIndex = 0; //Индекс выбранного элемента меню

            //Список элементов меню
            var elections = new List<string>()
            {
                    "Решить уравнение",
                    "История",
                    "Титры",
            };

            //Бесконечный цикл отображения и обновления меню
            while (true)
            {
                //Цикл добавления (добавления заготовленных строк) строк элементов меню
                for (int i = 0; i < elections.Count; i++)
                {
                    if (i == selectIndex) //Если i равно выбранному индексу, т.е. этот элемент меню выбран
                    {// то добавляем к нему маркер выбранного элементов
                        ConsoleScreen.SetLine(SelectedMarker + elections[i], i);
                    }
                    else //Иначае он не выбран и приписываем ему маркер невыбранного элемента
                    {
                        ConsoleScreen.SetLine(UnSelectedMarker + elections[i], i);
                    }
                }

                //Отображение элементов меню
                ConsoleScreen.RenderConsoleScreen();

                //Ввод символа с клавиатуры
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                ConsoleKey keyConsole = keyInfo.Key;
                char keyChar = keyInfo.KeyChar;

                switch (keyConsole) //Свитч на основе нажатой клавиши
                {
                    case ConsoleKey.DownArrow: //Если нажата стрелка вниз (продвижение вниз по списку(индекс становится больше))
                        if ((selectIndex < elections.Count - 1))
                        {
                            selectIndex++;
                        }
                        break;

                    case ConsoleKey.UpArrow: //Если зажата стрелка вниз (продвижение вверх по списку(индекс становится меньше))
                        if (selectIndex > 0)
                        {
                            selectIndex--;
                        }
                        break;

                    case ConsoleKey.Enter: //Если нажат ентер
                        switch (selectIndex) //Свитч на основе выбранного элемента меню
                        {
                            case 0: //Нулевой элемент (решить уравнение)
                                //Вызывается интерфес ввода уравнения и вовзращает строку(уравнение) в переменную equation
                                string equation = EquationValidator.InputEquation();

                                //Вызывает метод нахождения коэфицентов уравнения и возвращает массив величины 6 в перменную coafficents 
                                double[] coafficents = EquationParser.ReturnCoaficents(equation);

                                //Вызывает метод нахаждения корней уравнения и возвращает массив корней в переменную result
                                var result = EquationSolver.SolveQuadraticEquation(coafficents[0], coafficents[1], coafficents[2], coafficents[3], coafficents[4], coafficents[5]);

                                //Устноваливает путь файла истории уравнений
                                EquationLogger.SetLogPath("history.txt");
                                //Добавляет уарвнение в историю
                                EquationLogger.AddEquationSolving(equation, result);

                                //Вызов метода показа результатов
                                ShowResults(result, equation);

                                break;
                            case 1: //Первый элемент (История)
                                ShowHistory(); //Показ истории
                                break;

                            case 2: //Второй элемент (Титры)
                                ShowTitles(); //Показ титров
                                break;
                        }
                        break;
                }
            }
        }
    }
}