using NumericalIntegral.Model;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumericalIntegral.ViewModel
{
    public enum SelectedParameter
    {
        Alpha,
        Betta,
        Gamma,
        Delta,
        Epsilon
    }

    public class SimpsonIntegralGraph : INotifyPropertyChanged
    {
        #region Function Parameters

        private double _alpha = 1.1;
        public double Alpha {
            get
            {
                return _alpha;
            }
            set
            {
                _alpha = Clamp(value, -100, 100);
                NotifyPropertyChanged("Alpha");
            }
        }

        private double _betta = 0.05;
        public double Betta
        {
            get
            {
                return _betta;
            }
            set
            {
                _betta = Clamp(value, -100, 100);
                NotifyPropertyChanged("Betta");
            }
        }

        private double _gamma = 1.4;
        public double Gamma
        {
            get
            {
                return _gamma;
            }
            set
            {
                _gamma = Clamp(value, -100, 100);
                NotifyPropertyChanged("Gamma");
            }
        }

        private double _epsilon = 8;
        public double Epsilon
        {
            get
            {
                return _epsilon;
            }
            set
            {
                _epsilon = Clamp(value, -100, 100);
                NotifyPropertyChanged("Epsilon");
            }
        }

        private double _delta = 0.8;
        public double Delta
        {
            get
            {
                return _delta;
            }
            set
            {
                _delta = Clamp(value, -100, 100);
                NotifyPropertyChanged("Delta");
            }
        }

        #endregion

        #region Integral Parameters

        private double _integralStart = -25;
        public double IntegralStart
        {
            get
            {
                return _integralStart;
            }
            set
            {
                _integralStart = Clamp(value, -100, 100);
                NotifyPropertyChanged("IntegralStart");
            }
        }

        private double _integralEnd = 25;
        public double IntegralEnd
        {
            get
            {
                return _integralEnd;
            }
            set
            {
                _integralEnd = Clamp(value, -100, 100);
                NotifyPropertyChanged("IntegralEnd");
            }
        }

        private SelectedParameter _selectedParameter = SelectedParameter.Betta;
        public SelectedParameter SelectedParameter
        {
            get
            {
                return _selectedParameter;
            }
            set
            {
                _selectedParameter = value;
                NotifyPropertyChanged("SelectedParameter");
            }
        }

        #endregion

        #region Screen Parameters

        private double _leftBorder = -4;
        public double LeftBorder
        {
            get
            {
                return _leftBorder;
            }
            set
            {
                _leftBorder = Clamp(value, -100, 100);
                NotifyPropertyChanged("LeftBorder");
            }
        }

        private double _rightBorder = 2;
        public double RightBorder
        {
            get
            {
                return _rightBorder;
            }
            set
            {
                _rightBorder = Clamp(value, -100, 100);
                NotifyPropertyChanged("RightBorder");
            }
        }


        private double _bottomBorder = -50;
        public double BottomBorder
        {
            get
            {
                return _bottomBorder;
            }
            set
            {
                _bottomBorder = Clamp(value, -100, 100);
                NotifyPropertyChanged("BottomBorder");
            }
        }

        private double _topBorder = 50;
        public double TopBorder
        {
            get
            {
                return _topBorder;
            }
            set
            {
                _topBorder = Clamp(value, -100, 100);
                NotifyPropertyChanged("TopBorder");
            }
        }

        #endregion

        #region Precision Parameters

        private int _initialNumberOfParts = 50;
        public int InitialNumberOfParts
        {
            get
            {
                return _initialNumberOfParts;
            }
            set
            {
                _initialNumberOfParts = Clamp(value, 1, 500);
                NotifyPropertyChanged("InitialNumberOfParts");
            }
        }

        private int _maximumNumberOfParts = 0;
        public int MaximumNumberOfParts
        {
            get
            {
                return _maximumNumberOfParts;
            }
            private set
            {
                _maximumNumberOfParts = value;
                NotifyPropertyChanged("MaximumNumberOfParts");
            }
        }

        private double _calculationError = 0.1;
        public double CalculationError
        {
            get
            {
                return _calculationError;
            }
            set
            {
                _calculationError = value > 0 ? value : 1;
                NotifyPropertyChanged("CalculationError");
            }
        }

        #endregion

        private List<DataPoint> _dataPoints = new List<DataPoint>();
        public List<DataPoint> DataPoints
        {
            get
            {
                return _dataPoints;
            }
            set
            {
                _dataPoints = value;
                NotifyPropertyChanged("DataPoints");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private static T Clamp<T>(T a, T min, T max) where T : IComparable
        {
            if (a.CompareTo(min) < 0)
            {
                return min;
            }
            else if (a.CompareTo(max) > 0)
            {
                return max;
            }

            return a;
        }

        public void Update(double plotWidth)
        {
            double step = (RightBorder - LeftBorder) / plotWidth;
            SimpsonIntegral calculator = new SimpsonIntegral
            {
                Alpha = Alpha,
                Betta = Betta,
                Gamma = Gamma,
                Delta = Delta,
                Epsilon = Epsilon,
                IntegralStart = IntegralStart,
                IntegralEnd = IntegralEnd,
                NumberOfParts = InitialNumberOfParts,
                Precision = CalculationError
            };
            List<DataPoint> graphPoints = new List<DataPoint>();
            double currentPosition = LeftBorder;
            int maxNumberOfParts = -1;

            while (currentPosition < RightBorder + 1) // margin of error
            {
                UpdateChangingValue(calculator, currentPosition);
                calculator.NumberOfParts = InitialNumberOfParts;

                graphPoints.Add(new DataPoint(currentPosition, calculator.CalculateIntegral()));

                if (calculator.NumberOfParts > maxNumberOfParts)
                {
                    maxNumberOfParts = calculator.NumberOfParts;
                }

                currentPosition += step;
            }

            MaximumNumberOfParts = maxNumberOfParts;
            DataPoints = graphPoints;
        }

        private void UpdateChangingValue(SimpsonIntegral integral, double value)
        {
            switch (SelectedParameter)
            {
                case SelectedParameter.Alpha:
                    integral.Alpha = value;
                    break;
                case SelectedParameter.Betta:
                    integral.Betta = value;
                    break;
                case SelectedParameter.Gamma:
                    integral.Gamma = value;
                    break;
                case SelectedParameter.Delta:
                    integral.Delta = value;
                    break;
                case SelectedParameter.Epsilon:
                    integral.Epsilon = value;
                    break;
            }
        }
    }
}
