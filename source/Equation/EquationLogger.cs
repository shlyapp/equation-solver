using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace equationSolver.Equation
{
    public class EquationLogger
    {
        /// <summary>
        /// Путь до файла с историей уравнений
        /// </summary>
        static string path;

        public static void SetLogPath(string pathToLog)
        {
            path = pathToLog;
        }

        /// <summary>
        /// Возвращает всю историю введеных раннее уравнений
        /// </summary>
        /// <returns>История уравнений</returns>
        public static List<string> GetHistory()
        {
            List<string> solutions = new List<string>();
            StreamReader file = new StreamReader(path);

            while (!file.EndOfStream)
            {
                solutions.Add(file.ReadLine());
            }

            file.Close();
            return solutions;
        }

        /// <summary>
        /// Добавить уравнение в историю
        /// </summary>
        /// <param name="equation">Уравнение</param>
        /// <param name="result">Решения уравнения</param>
        public static void AddEquationSolving(string equation, double[] result)
        {
            StreamWriter file = new StreamWriter(path, true);

            //Кейс определяющий запись в файл истории уравнений по количеству корней
            switch (result.Length)
            {
                case 0: //Если 0 корней
                    file.WriteLine($"Уравнение: {equation}\nНет корней\n");
                    file.Close();
                    break;

                case 1: // Если 1 корень
                    file.WriteLine($"Уравнение: {equation}\nКорень: {result[0]}\n");
                    file.Close();
                    break;

                case 2: // Если 2 корня
                    file.WriteLine($"Уравнение: {equation}\nКорни: {result[0]} и {result[1]}\n");
                    file.Close();
                    break;

                case 3: // Если 3 корня (3 корня - фатальная ошибка)
                    file.WriteLine($"Выражение: {equation}\nКорни нельзя найти т.к. это не уравнение\n");
                    file.Close();
                    break;
                default: // На случай непредвиденой ошибки
                    file.WriteLine($"Выражение: {equation}\nНевозможно распознать\n");
                    break;
            }
        }
    }
}
