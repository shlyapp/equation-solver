﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationSolver.Equation
{
    public class EquationParser
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

        public static double CoaficentCath(string equation, string variable)
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
        public static double CoaficentCath(string equation, string variable, string[] whiteList)
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
    }
}
