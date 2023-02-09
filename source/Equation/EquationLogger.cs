using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationSolver.Equation
{
    public class EquationLogger
    {
        public static void ShowErrorMessage(string message)
        {
            Console.WriteLine($"Ошибка!\n{message}");
        }

        public static void ShowResult(double[] result)
        {
            Console.WriteLine($"Успешно решено!\nКорень первый: {result[0]}\nКорень второй: {result[1]}");
        }
    }
}
