using EquationSolver.Equation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationSolver.Equation
{
    public class EquationParser
    {
        /// <summary>
        /// Переворачивает строку
        /// </summary>
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

        /// <summary>
        /// Извлекает и возвращает коэфиценты из строки
        /// </summary>
        public static double[] ReturnCoaficents(string equation)
        {
            string equationLeftSide = equation.Split('=')[0];
            string equationRightSide = equation.Split('=')[1];
            //тут просто возвращаем масив из 6 коэффицентов найденых по методам ниже
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

        /// <summary>
        /// Извлекает и возвращает коэфицент символа variable строки equation
        /// </summary>
        private static double CoaficentCath(string equation, string variable)
        {
            if (!equation.Contains(variable)) return 0; //Если нет искомой переменной с ее коэфицентом

            string temp = "";

            //Просто пылесосим все числа между знаком и нужным нам X
            for (int i = equation.IndexOf(variable) - 1; i > -1; i--) 
            {
                if ((char.IsNumber(equation[i])) || equation[i] == ',')
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
                return 1; //1 возвращаем, т.к. если не был найден коэффицент, то он равен 1
            }
        }

        /// <summary>
        /// Извлекает и возвращает коэфицент символа variable с допустимыми символами whiteList после строки equation,
        /// </summary>
        private static double CoaficentCath(string equation, string variable, string[] whiteList)
        {
            if (!equation.Contains(variable)) return 0;

            string temp = ""; // будет хранить собирающееся число

            foreach (string str in whiteList)
            {
                for (int i = equation.IndexOf(variable + str) - 1; i > -1; i--) //Пылисосим все числа в обратном порядке от индекса x-а, если встречается не число, то выходим из цикла
                {
                    if ((char.IsNumber(equation[i])) || equation[i] == ',')
                    {
                        temp += equation[i];
                    }
                    else if (equation[i] == '-') //Тут очевтдно нужно учесть еще минус
                    {
                        temp += equation[i];
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                if (temp.Length != 0) //Проверка на то было ли что то найдено, что бы не считать лишний раз
                {
                    break;
                }
                else if (equation[equation.Length-1].ToString() == variable) //Проверка, был ли символ последним в строке
                {
                    for (int i = equation.Length-2; i > -1; i--)
                    {
                        if ((char.IsNumber(equation[i])) || equation[i] == ',')
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

        /// <summary>
        /// Извлекает и возвращает коэфицент строки equation
        /// </summary>
        private static double CoaficentCath(string equation)
        {
            string temp = "";

            for (int i = 0; i < equation.Length; i++) 
            {
                if (new char[] { '+', '='}.Contains(equation[i])) //Пылесосим все символы в temp пока не встретится + или =
                                                                  //после того как встретились, проверяем был ли среди символов x или нет
                                                                  //если был, то сбрасываем строку
                {
                    if (temp.Contains('x'))
                    {
                        temp = "";
                    }
                    else
                    {
                        break;
                    }
                }
                else if (equation[i] == '-') //Минус обработать надо отдельно
                {
                    if (temp.Contains('x'))
                    {
                        temp = "";
                    }
                    else
                    {
                        break;
                    }
                    temp += equation[i];
                }
                else temp += equation[i];
            }

            try
            {
                return Convert.ToDouble(temp);
            }
            catch { return 0; }
        }
    }
}