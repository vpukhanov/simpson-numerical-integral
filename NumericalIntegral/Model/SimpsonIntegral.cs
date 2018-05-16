using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumericalIntegral.Model
{
    class SimpsonIntegral
    {
        public double Alpha { get; set; }
        public double Betta { get; set; }
        public double Gamma { get; set; }
        public double Delta { get; set; }
        public double Epsilon { get; set; }

        public double IntegralStart { get; set; }
        public double IntegralEnd { get; set; }

        public int NumberOfParts { get; set; }
        public double Precision { get; set; }

        private double F(double x)
        {
            return Epsilon * Math.Sin(Alpha / (Betta * x * x + Gamma * x + Delta));
        }

        private double CalculateIntegralWithNumberOfParts(int numberOfParts)
        {
            double result = 0;
            double step = (IntegralEnd - IntegralStart) / NumberOfParts;
            double halfStep = step / 2;
            double coefficient = halfStep / 3;
            
            for (int i = 0; i < numberOfParts; i++)
            {
                double currentPoint = IntegralStart + i * step;
                result += coefficient * (F(currentPoint) + 4 * F(currentPoint + halfStep) + F(currentPoint + step));
            }

            return result;
        }

        public double CalculateIntegral()
        {
            double currentIntegral = CalculateIntegralWithNumberOfParts(NumberOfParts);
            NumberOfParts *= 2;
            double nextIntegral = CalculateIntegralWithNumberOfParts(NumberOfParts);

            while (Math.Abs(currentIntegral - nextIntegral) > Precision)
            {
                currentIntegral = nextIntegral;
                NumberOfParts *= 2;
                nextIntegral = CalculateIntegralWithNumberOfParts(NumberOfParts);
            }

            return nextIntegral;
        }
    }
}
