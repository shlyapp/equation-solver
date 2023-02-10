using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;

namespace EquationSolver
{
    public static class EquationSolver
    {
        /// <summary>
        /// Возвращает корни квадратного уравнения заданного 6-ю коэфицентами
        /// </summary>
        public static double[] SolveQuadraticEquation(double A, double B, double C, double D, double E, double F)
        {
            //Приведение подобных
            A = A - D; 
            B = B - E;
            C = C - F;

            //Проверка наличия квадратного уравнения
            if (A == 0)
            {
                if (B == 0)
                {
                    return new double[3] { 4, 0, 4 }; //Возврощает 3 корня, т.к. это не уравнение
                }
                double x1 = (-C / B); // Это корень уравнения первой степени
                return new double[] { x1 }; 
            }

            double discriminant = B * B - (4 * A * C); //Дискриминант

            if (discriminant > 0) //База
            {
                double x1 = (-B + Math.Sqrt(discriminant)) / (2 * A);
                double x2 = (-B - Math.Sqrt(discriminant)) / (2 * A);
                return new double[] { x1, x2 }; //Корня два т.к. дискриминант > 0
            }
            else if (discriminant == 0) //База
            {
                double x1 = (-B) / (2 * A);
                return new double[] { x1 }; //Корень один т.к. дискриминант = 0
            }
            else //Корней нет
            {
                return new double[] { };
            }
        }
    }
}