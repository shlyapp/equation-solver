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
        private string path;

        public EquationLogger(string path)
        {
            this.path = path;
        }

        public List<string> GetHistory()
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

        public void AddEquationSolving(string equation, double[] result)
        {
            StreamWriter file = new StreamWriter(path, true);

            file.WriteLine($"Уравнение: {equation}\n{result[0]} и {result[1]}\n");
            file.Close();
        }

        
    }
}
