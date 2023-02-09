namespace EquationSolverLibrary
{
    public static class EquationSolver
    {
        private static string Reverse(string text)
        {
            char[] cArray = text.ToCharArray();
            string reverse = String.Empty;
            for (int i = cArray.Length - 1; i > -1; i--)
            {
                reverse += cArray[i];
            }
            return reverse;
        }

        private static double CoaficentCath(string equation, string variable)
        {
            string Temp = "";


            for (int i = equation.IndexOf(variable) - 1; i > -1; i--)
            {
                if ((char.IsNumber(equation[i])))
                {
                    Temp += equation[i];
                }
                else if (equation[i] == '-')
                {
                    Temp += equation[i];
                    break;
                }
                else
                {
                    break;
                }
            }

            try
            {
                return Convert.ToDouble(Reverse(Temp));
            }
            catch
            {
                return 0;
            }
        }
        private static double CoaficentCath(string equation, string variable, string[] whiteList)
        {
            string Temp = "";

            foreach (string str in whiteList)
            {
                for (int i = equation.IndexOf(variable + str) - 1; i > -1; i--)
                {
                    if ((char.IsNumber(equation[i])))
                    {
                        Temp += equation[i];
                    }
                    else if (equation[i] == '-')
                    {
                        Temp += equation[i];
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                if (Temp.Length != 0)
                {
                    break;
                }
            }

            try
            {
                return Convert.ToDouble(Reverse(Temp));
            }
            catch
            {
                return 0;
            }
        }

        public static double[] SolveQuadraticEquation(double A, double B, double C, double D, double E, double F)
        {
            A = A - D;
            B = B - E;
            C = C - F;


            if (A == 0)
            {
                if (B == 0)
                {
                    return new double[3] { 4, 0, 4 };
                }
                double x1 = (-C / B);
                return new double[] { x1 };
            }

            double discriminant = B * B - (4 * A * C);

            if (discriminant > 0)
            {
                double x1 = (-B + Math.Sqrt(discriminant)) / (2 * A);
                double x2 = (-B - Math.Sqrt(discriminant)) / (2 * A);
                return new double[] { x1, x2 };
            }
            else if (discriminant == 0)
            {
                double x1 = (-B) / (2 * A);
                return new double[] { x1 };
            }
            else
            {
                return new double[] { };
            }

        }
        
    }
}