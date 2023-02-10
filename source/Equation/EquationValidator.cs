using ConsoleInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EquationSolver;
using System.Threading;
using System.Threading.Tasks;

namespace EquationSolver
{
    public static class EquationValidator
    {
        //Ошибка ввода для вывода пользователю
        private static string errorMessage = "";
        //Список разрешенных символов не включая цифер
        private static char[] whiteListChar = { '+', '-', '=', 'x', ','};

        // Эти методы осуществляют условия возможности написания символов и если
        // нужно, обозначение ошибки
        // Их удобно изменять и добавлять новые
        // Правила их написания:
        // - Возвращение значения False должно быть в конце метода
        // - Всегда использовать ветвления
        // предотвращающие наибольшее количество выхода из размерности массива выше остальных
        // Try Catch нуно что бы упростить код без ущерба его надежности
        /// <summary>
        /// Проверяет возможность размещения числа на позиции index в строке line
        /// </summary>
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

        /// <summary>
        /// Проверяет возможность размещения символа на позиции index в строке line
        /// </summary>
        private static bool SimbolsPlaceCondition(string line, char simbol, int index)
        {
            try
            {
                switch (simbol)
                {
                    case '-':
                        if ((line[index - 1] == '-' || line[index - 1] == '+'))
                        {
                            errorMessage = "Нельзя ставить рядом два знака";
                            return false;
                        }
                        break;

                    case '+':
                        if (index == 1)
                        {
                            errorMessage = "Нельзя ставить рядом два знака";
                            return false;
                        }
                        if ((line[index - 1] == '+' || line[index - 1] == '-'))
                        {
                            errorMessage = "Нельзя ставить рядом два знака";
                            return false;
                        }
                        if ((line[index - 1] == '='))
                        {
                            errorMessage = "Нельзя ставить плючс после равно";
                            return false;
                        }
                        break;

                    case '=':
                        if (!(line.IndexOf('=') == -1))
                        {
                            errorMessage = "В условии уравнения недопустимо два равно";
                            return false;
                        }
                        if (line[index - 1] == '+' || line[index - 1] == '-')
                        {
                            errorMessage = "";
                            return false;
                        }
                        break;

                    case 'x':
                        if (line[index - 1] == ',')
                        {
                            errorMessage = "Нельзя ставить x после запятой";
                            return false;
                        }
                        if (line[index - 1] == 'x')
                        {
                            errorMessage = "Нельзя ставить рядом два x";
                            return false;
                        }
                        for (int i = index - 1; i >= 0; i--)
                        {
                            if (line[i] == 'x')
                            {
                                errorMessage = "Недопустима неясность с доп. x";
                                return false;
                            }
                            if (new char[] { '=', '-', '+' }.Contains(line[i]))
                            {
                                break;
                            }
                        }
                        break;
                    case ',':
                        if ((index == 0) || (!char.IsNumber(line[index - 1])))
                        {
                            errorMessage = "Запятая может стоять только после числа";
                            return false;
                        }
                        if (line[index - 2] == 'x')
                        {
                            errorMessage = "Степень не может быть десятичным числом";
                            return false;
                        }
                        for (int i = index - 1; i >= 0; i--)
                        {
                            if (line[i] == ',')
                            {
                                errorMessage = "Неможет быть число с двумя запятыми";
                                return false;
                            }
                            if (new char[] { '=', '-', '+'}.Contains(line[i]))
                            {
                                break;
                            }
                        }
                        break;
                } 
                return true;
            }
            catch (IndexOutOfRangeException)
            {
                return true;
            }
        }

        /// <summary>
        /// Проверяет возможность обработки строки line
        /// </summary>
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
                if (line[line.Length - 1] == ',')
                {
                    errorMessage = "//После запятой должно стоять число";
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

        /// <summary>
        /// Вызывает интерфейс ввода уравнения
        /// </summary>
        public static string InputEquation()
        {
            //Константы здачи интерфеса
            const int inputLineIndex = 2;
            const int errortLineIndex = 4;

            //Строка ввода
            var inputString = new StringBuilder();

            //Задатеся статичная часть интерфейса ввода
            ConsoleScreen.ClearLines();

            ConsoleScreen.SetLine("Ввод уравнения вида Ax2+Bx+C=Dx2+Ex+F", 0);
            ConsoleScreen.SetLine("-------------------------------------------------", inputLineIndex + 1);
            ConsoleScreen.SetLine("", inputLineIndex);
            ConsoleScreen.SetLine("-------------------------------------------------", inputLineIndex - 1);

            ConsoleScreen.RenderConsoleScreen();
            //Загрузка стратового статуса интерфейса


            errorMessage = "";
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
                    // Проверка на возможность написать разрешенный символ (ссылается на метод выше)
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
                ConsoleScreen.SetLine(inputString.ToString(), inputLineIndex);
                //Условие вывода ошибки ввода
                if (errorMessage.Length > 0)//Если есть ошиька
                {
                    ConsoleScreen.SetLine("|!|" + errorMessage, errortLineIndex);
                
                }
                else//Иначе
                {
                    ConsoleScreen.SetLine("", errortLineIndex);
                }
                ConsoleScreen.RenderConsoleScreen();

                errorMessage = ""; //Сброс ошибки
            }
        }
    }
}