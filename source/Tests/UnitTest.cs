using EquationSolver;
using System;
using System.Security.Cryptography.X509Certificates;

namespace Testing
{
    [TestClass]
    public class Tests
    {

        public void ModuleTestSolver(double A, double B, double C, double D, double E, double F, params double[] expectedResults)
        {
            double[] result = EquationSolver.EquationSolver.SolveQuadraticEquation(A, B, C, D, E, F);
            Assert.AreEqual(expectedResults.Length, result.Length);
            for (int i = 0; i < result.Length; i++)
            {
                Assert.AreEqual(expectedResults[i], result[i]);
            }
        }

        public void FuncTestSolver(double A, double B, double C, double D, double E, double F)
        {
            double[] result = EquationSolver.EquationSolver.SolveQuadraticEquation(A, B, C, D, E, F);
            if (result.Length == 3)
            {
                Assert.AreEqual(new double[3] { 4, 0, 4 }, result);
            }
            foreach (var x in result)
            {
                Assert.AreEqual(0, (A - D) * x * x + (B - E) * x + (C - F), 1e-10);
            }
        }

        [TestMethod]
        public void PositiveDiscriminant()
        {
            ModuleTestSolver(1, 2, 6, 0, 5, 4, 2, 1);
            ModuleTestSolver(1, 4, 8, 0.5, 7.5, 2, 4, 3);
        }

        [TestMethod]
        public void NegativeDiscriminant()
        {
            ModuleTestSolver(5, 6, 3, 4, 7, 1);
            ModuleTestSolver(2, 6, 2, 9, 8, 6);
            ModuleTestSolver(1.6, 3.4, 0, 4.3, 7.5, 8);
        }

        [TestMethod]
        public void ZeroDiscriminant()
        {
            ModuleTestSolver(3, 5, 7, 2, 1, 3, -2);
            ModuleTestSolver(4, 7, 2, 3, 5, 1, -1);
        }

        [TestMethod]
        public void FuncTest()
        {
            Random rnd = new Random();
            for (int i = 0; i < 10; i++)
            {
                var A = rnd.NextDouble() * 10;
                var B = rnd.NextDouble() * 10;
                var C = rnd.NextDouble() * 10;
                var D = rnd.NextDouble() * 10;
                var E = rnd.NextDouble() * 10;
                var F = rnd.NextDouble() * 10;

                FuncTestSolver(A, B, C, D, E, F);
            }
        }
    }
}