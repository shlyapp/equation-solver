using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace solvingEquations.Equation
{
    public class EquationValidator
    {
        private string _equation;

        private bool IsCorrectNumberOfVariable()
        {
            string[] chasti = _equation.Split('=');
            int schet1 = 0;
            int schet2 = 0;
            int schet3 = 0;
            int schet4 = 0;

            for (int i = 0; i < chasti[0].Length - 1; i++)
            {
                if (chasti[0][i] == 'x') schet1++;
                if (chasti[0][i] == 'x' && chasti[0][i + 1] == '2') schet2++;
            }
            for (int i = 0; i < chasti[1].Length - 1; i++)
            {
                if (chasti[1][i] == 'x') schet3++;
                if (chasti[1][i] == 'x' && chasti[1][i + 1] == '2') schet4++;
            }

            if (schet1 > 1 || schet2 > 1 || schet3 > 1 || schet4 > 1)
            {
                EquationLogger.ShowErrorMessage("Неверное количество переменных");
                return false;
            }
            return true;
        }

        private bool IsCorrestEqualSymbol()
        {
            int schet = 0;

            for (int i = 0; i < _equation.Length; i++)
            {
                if (_equation[i] == '=') schet++;
            }

            if (schet == 1)
            {
                return true;
            }
            else
            {
                EquationLogger.ShowErrorMessage("Некорректное количество знаков равно");
                return false;
            }
        }
        private bool IsMissingX()
        {
            int countOfX = 0;

            for (int i = 0; i < _equation.Length; i++)
            {
                if (_equation[i] == 'x')
                    countOfX++;
            }

            if (countOfX == 0)
            {
                EquationLogger.ShowErrorMessage("Нет переменной x");
                return false;
            }

            return true;
        }

        private bool HaveUncorrectSymbols()
        {
            char[] symbols = new char[] {'0', '1', '2', '3', '4', '5', '6', '7',
                                        '8', '9', 'x', 'X', '=', '+', '-'};
            bool result = true;
            for (int i = 0; i < _equation.Length; i++)
            {
                if (!symbols.Contains(_equation[i]))
                {
                    EquationLogger.ShowErrorMessage("Формула содержит некорректные символы");
                    return true;
                }
            }
            return !result;
        }

        public EquationValidator(string equationForValidate)
        {
            _equation = equationForValidate;
        }

        public bool Validate()
        {
            try
            {
                if (!HaveUncorrectSymbols() && IsCorrectNumberOfVariable() && !IsMissingX() && IsCorrestEqualSymbol() && (string.IsNullOrEmpty(_equation)))
                {
                    return true;
                }

            }
            catch (Exception ex)
            {
                EquationLogger.ShowErrorMessage(ex.Message);
                return false;
            }

            return false;
        }
    }
}
