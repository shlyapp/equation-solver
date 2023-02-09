using EquationSolver.Equation;
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
        public static double[] ReturnCoaficents(string equation)
        {
            string equationLeftSide = equation.Split('=')[0];
            string equationRightSide = equation.Split('=')[1];
            return new double[6]
            {
                CoaficentCath(equationLeftSide,"x2"),
                CoaficentCath(equationLeftSide,"x",new string[]{"-","+","="}),
                CoaficentCath(equationLeftSide),
                CoaficentCath(equationRightSide,"x2"),
                CoaficentCath(equationRightSide,"x",new string[]{"-","+","="}),
                CoaficentCath(equationRightSide),
            };
        }
        private static double CoaficentCath(string equation, string variable)
        {
            if (!equation.Contains(variable)) return 0;

            string temp = "";

            for (int i = equation.IndexOf(variable) - 1; i > -1; i--)
            {
                if ((char.IsNumber(equation[i])))
                {
                    temp += equation[i];
                }
                else if (equation[i] == '-')
                {
                    temp += equation[i];
                    break;
                }
                else
                {
                    break;
                }
            }

            try
            {
                return Convert.ToDouble(Reverse(temp));
            }
            catch
            {
                return 1;
            }
        }
        private static double CoaficentCath(string equation, string variable, string[] whiteList)
        {
            if (!equation.Contains(variable)) return 0;

            string temp = "";

            foreach (string str in whiteList)
            {
                for (int i = equation.IndexOf(variable + str) - 1; i > -1; i--)
                {
                    if ((char.IsNumber(equation[i])))
                    {
                        temp += equation[i];
                    }
                    else if (equation[i] == '-')
                    {
                        temp += equation[i];
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                if (temp.Length != 0)
                {
                    break;
                }
            }

            try
            {
                return Convert.ToDouble(Reverse(temp));
            }
            catch
            {
                return 1;
            }
        }
        private static double CoaficentCath(string equation)
        {
            string temp = "";

            for (int i = 0; i < equation.Length; i++)
            {
                if (char.IsNumber(equation[i]))
                {
                    temp += equation[i];
                }
                else if (equation[i] == 'x')
                {
                    temp = "";
                }
            }

            try
            {
                return Convert.ToDouble(Reverse(temp));
            }
            catch { return 0; }
        }
    }
}