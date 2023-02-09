using ConsoleInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EquationSolver
{
    public static class EquationValidator
    {
        //Ошибка ввода для вывода пользователю
        private static string errorMessage = "";
        //Список разрешенных символов не включая цифер
        private static char[] whiteListChar = { '+', '-', '=', 'x' };

        // Эти методы осуществляют условия возможности написания символов и если
        // нужно, обозначение ошибки
        // Их удобно изменять и добавлять новые
        // Правила их написания:
        // - Возвращение значения False должно быть в конце метода
        // - Всегда использовать ветвления
        // предотвращающие наибольшее количество выхода из размерности массива выше остальных
        // - В коде к этим методам обращается программа что бы проверить 
        private static bool NumberPlaceCondition(string line, char number, int index)
        {
            try
            {
                if (line[index - 1] == 'x')
                {
                    if (!(number == '1' || number == '2'))
                    {
                        errorMessage = "Максимальная степень 2";
                        return false;
                    }
                }
                if (!(line[index - 2] != 'x') && (char.IsDigit(line[index - 1])))
                {
                    errorMessage = "Максимальная степень 2";
                    return false;
                }
                else return true;
            }
            catch (IndexOutOfRangeException)
            {
                return true;
            }
        }
        private static bool SimbolsPlaceCondition(string line, char simbol, int index)
        {
            try
            {
                if (simbol == '-')
                {
                    if ((line[index - 1] == '-' || line[index + 1] == '-') || (line[index - 1] == '+' || line[index + 1] == '+'))
                    {
                        errorMessage = "Нельзя ставить рядом два знака";
                        return false;
                    }
                }
                if (simbol == '+')
                {
                    if ((line[index - 1] == '+' || line[index + 1] == '+') || (line[index - 1] == '-' || line[index + 1] == '-'))
                    {
                        errorMessage = "Нельзя ставить рядом два знака";
                        return false;
                    }
                }
                if (simbol == '=')
                {
                    if (!(line.IndexOf('=') == -1))
                    {
                        errorMessage = "В условии уравнения недопустимо два равно";
                        return false;
                    }
                }
                if (simbol == 'x')
                {
                    if (line[index - 1] == 'x' || line[index + 1] == 'x')
                    {
                        errorMessage = "Нельзя ставить рядом два x";
                        return false;
                    }
                    for (int i = index - 1; i > 0; i--)
                    {
                        if (new char[] { '=', '-', '+' }.Contains(line[i]))
                        {
                            break;
                        }
                        if (line[i] == 'x')
                        {
                            errorMessage = "Недопустима неясность с доп. x";
                            return false;
                        }
                    }
                }
                return true;
            }
            catch (IndexOutOfRangeException)
            {
                return true;
            }
        }
        private static bool LineCondition(string line)
        {
            try
            {
                if (line.Length == 0)
                {
                    errorMessage = "//Строка ввода пуста";
                    return false;
                }
                if (line[line.Length - 1] == '-')
                {
                    errorMessage = "//Минус на конце является ошибкой";
                    return false;
                }
                if (line[line.Length - 1] == '+')
                {
                    errorMessage = "//Плюс на конце является ошибкой";
                    return false;
                }
                if (!(line.Contains('=')))
                {
                    errorMessage = "//Равно отсутсвует, это не уравнение";
                    return false;
                }
                if (line[line.Length - 1] == '=')
                {
                    errorMessage = "//Одна часть равно пуста";
                    return false;
                }
                if (line[0] == '=')
                {
                    errorMessage = "//Одна часть равно пуста";
                    return false;
                }
            }
            catch (IndexOutOfRangeException)
            {
                return true;
            }
            return true;
        }

        //Титры, но не заюзаны
        static void Titles(List<string> titles)
        {
            for (int i = 0; ; i++)
            {
                Thread.Sleep(800);
                int position = i;
                foreach (var title in titles)
                {

                    position = position - 2;
                    if (position < 0) break;
                    else
                    {
                        ConsoleScreen.addLine(new String(' ', 25) + title, position);
                    }

                }
                ConsoleScreen.renderConsoleScreen();
                Console.SetCursorPosition(0, 0);
                ConsoleScreen.clearLines();
            }
        }
        public static string InputEquation()
        {
            //Константы здачи интерфеса
            const int inputLineIndex = 2;
            const int errortLineIndex = 4;

            //Строка ввода
            var inputString = new StringBuilder();

            //Статичная часть интерфейса
            ConsoleScreen.addLine("Ввод уравнения вида Ax2+Bx+C=Dx2+Ex+F", 0);
            ConsoleScreen.addLine("-------------------------------------------------", inputLineIndex + 1);
            ConsoleScreen.addLine("-------------------------------------------------", inputLineIndex - 1);

            //Цикл ввода уравнения
            while (true)
            {
                //Хранение всех необходимых данных о нажимаемой клавише
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                ConsoleKey keyConsole = keyInfo.Key;
                char keyChar = keyInfo.KeyChar;

                //Проверка, является ли число числом или разрешенным символом
                if (whiteListChar.Contains(keyChar) || char.IsNumber(keyChar))
                {
                    //Проверка на возможность написать цифру (ссылается на метод выше)
                    if (char.IsDigit(keyChar))
                    {
                        if (NumberPlaceCondition(inputString.ToString(), keyChar, inputString.Length))
                        {
                            inputString.Append(keyChar);
                        }
                    }
                    // Проверка на возможность написать разрешенный цифр (ссылается на метод выше)
                    else
                    {
                        if (SimbolsPlaceCondition(inputString.ToString(), keyChar, inputString.Length))
                        {
                            inputString.Append(keyChar);
                        }
                    }
                }
                //Возможность стирать строку, т.е. проверка нажатие на backspace и
                //соответсвующая функциональность
                else if (keyConsole == ConsoleKey.Backspace)
                {
                    if (inputString.Length > 0)
                    {
                        inputString.Remove(inputString.Length - 1, 1);
                    }
                }
                //Окончание ввода уравнения
                else if (keyConsole == ConsoleKey.Enter)
                {
                    //Проверка на наличае ошибок не учтенным автоблокированием
                    if (LineCondition(inputString.ToString()))
                    {
                        return inputString.ToString();
                    }
                }
                //Исключение из возможноси ввода остальных неразрешенных символов
                else
                {
                    errorMessage = $"Символ '{keyChar}' недопустим";
                }

                //Апдейт интерфесса консоли
                ConsoleScreen.addLine(inputString.ToString(), inputLineIndex);
                ConsoleScreen.addLine("|!|" + errorMessage, errortLineIndex);
                ConsoleScreen.renderConsoleScreen();
                errorMessage = "";
            }
        }
    }
}