using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab1
{
    class Solve
    {
        double _a;
        double _b;
        double _c;
        public double A
        {
            get
            {
                return _a;
            }
            set
            {
                if (value > 0)
                    _a = value;
                else
                    throw new DataException("Error! A side's fault!");
            }
        }

        public double B
        {
            get
            {
                return _b;
            }
            set
            {
                if (value > 0)
                    _b = value;
                else
                    throw new DataException("Error! B side's fault!");
            }
        }

        public double C
        {
            get
            {
                return _c;
            }
            set
            {
                if (value > 0)
                    _c = value;
                else
                    throw new DataException("Error! C side's fault!");
            }
        }

        double _alpha;
        double _beta;
        double _gamma;

        public double Alpha
        {
            get
            {
                return _alpha;
            }
            set
            {
                if (value > 0 && value < 180)
                    _alpha = value;
                else
                    throw new DataException("Error! Alpha angle's fault!");
            }
        }

        public double Beta
        {
            get
            {
                return _beta;
            }
            set
            {
                if (value > 0 && value < 180)
                    _beta = value;
                else
                    throw new DataException("Error! Beta angle's fault!");
            }
        }

        public double Gamma
        {
            get
            {
                return _gamma;
            }
            set
            {
                if (value > 0 && value < 180)
                    _gamma = value;
                else
                    throw new DataException("Error! Gamma angle's fault!");
            }
        }


        public Solve(string s)
        {
            s = s.Replace(" ","");
            string[] data = s.Split(new char[] { ';' });
            if (data.Length == 3)
                for (int i = 0; i < 3;i++)
                {
                    double buf = -1;
                    double.TryParse(data[i], out buf);
                    if (i == 0) A = buf;
                    if (i == 1) B = buf;
                    if (i == 2) Alpha = buf;
                }
        }

        public double GetSide()
        {
            return Math.Sqrt(A * A + B * B - 2 * A * B * Math.Cos(Alpha * Math.PI / 180.0));
        }

        public double[] CalculateAngles()
        {
            double b = Math.Acos((A * A + C * C - B * B) / (2 * A * C)) * 180 / Math.PI;
            double g = 180 - Alpha - b;
            return new double[] { b, g };
        }

        public void Calculate()
        {
            C = GetSide();
            double[] buf = CalculateAngles();
            Beta = buf[0];
            Gamma = buf[1];
        }
    }
}
