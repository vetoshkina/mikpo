using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lab1;

namespace FirstTest
{
    [TestClass]
    public class SolveTest
    {
        [TestMethod]
        public void CheckInputData()
        {
            Solve solve = new Solve("3,1; 5.2 ; 90");
            Assert.AreEqual(solve.A, 3.1);
            Assert.AreEqual(solve.B, 5.2);
            Assert.AreEqual(solve.Alpha, 90);
        }
        
        [TestMethod]
        public void CheckAngles()
        {
            Solve solve = new Solve("3,1; 5,2 ; 35");
            solve.Calculate();
            Assert.AreEqual(solve.Alpha + solve.Beta + solve.Gamma, 180);
        }

        [TestMethod]
        public void CheckSides()
        {
            Solve solve = new Solve("3,1; 5,2 ; 35");
            solve.Calculate();
            double A = Math.Sqrt(solve.B * solve.B + solve.C * solve.C - 2 * solve.B * solve.C * Math.Cos(solve.Gamma * Math.PI / 180.0));
            double B = Math.Sqrt(solve.A * solve.A + solve.C * solve.C - 2 * solve.A * solve.C * Math.Cos(solve.Beta * Math.PI / 180.0));
            Assert.AreEqual(solve.A, A, 0.0001);
            Assert.AreEqual(solve.B, B, 0.0001);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CheckWrongArguments()
        {
            Solve solve = new Solve(";;");
        }

        [TestMethod]
        [ExpectedException(typeof(DataException))]
        public void CheckWrongData()
        {
            Solve solve = new Solve("9/5;5..3;180");
        }
    }
}
