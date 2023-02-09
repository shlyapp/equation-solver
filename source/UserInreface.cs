using solvingEquations.Equation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace solvingEquations
{
    public class UserInreface
    {
        private static void InputEquations()
        {
            Console.Write("Введите приложение: ");
            string equations = Console.ReadLine();

            EquationParser parser = new EquationParser(equations);
            

            //double[] solve = Equation.SolveQuadraticEquation(parser.Odds[0], parser.Odds[1], parser.Odds[2], parser.Odds[3], parser.Odds[4], parser.Odds[5]);


        }

        public static void SelectUserAction()
        {
            bool isOpen = true;
            while (isOpen)
            {
                Console.SetCursorPosition(0, 20);
                Console.WriteLine("Допустимые значения: цифры от 0 до 9, 'x', 'X', '=', '+', '-'.");
                Console.SetCursorPosition(0, 0);
                Console.Write("Чтобы ввести выражение нажмите 1\n" +
                    "Чтобы закрыть прграмму нажмите 2\n" +
                    "\nВыберите действие: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        InputEquations();
                        break;

                    case "2":
                        isOpen = false;
                        break;

                    case "3":
                        break;

                    default:
                        Console.WriteLine("Сударь, Вы неправы! Соизвольте испробовать еще раз.");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }
            }
        }
    }
}
