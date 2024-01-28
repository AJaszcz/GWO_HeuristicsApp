using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using GWO;
using GWO.Interfaces;
using System.Reflection;

public delegate double fitnessFunction(params double[] arg);

namespace GWO
{

    //public delegate double fitnessFunction(params double[] arg);
    public class GWO : IOptimizationAlgorithm
    {
        string _Name = "GWO";
        // Parametry
            // Dotyczące iteracji
            private double[] _XBest;
            private double _FBest;
            private int _NumberOfEvaluationFitnessFunction;
            private int _funcCalls_no;
            // Dotyczące algorytmu
            private int _maxIter;
            private int _searchAgents_no;
            private double[,] _domain;
            private int _dim;
            private int _currentIteration;
            private Random _rand;
            // Dotyczące wilków
            private double[,] _positions;

            private double[] _alphaPos; 
            private double _alphaScore; 

            private double[] _betaPos; 
            private double _betaScore; 

            private double[] _deltaPos; 
            private double _deltaScore;
        // koniec parametrów

        string IOptimizationAlgorithm.Name { get => _Name; set => _Name = value; }

        // TODO: implementacja
        public ParamInfo[] ParamsInfo
        {
            get
            {
                ParamInfo noAgents = new ParamInfo();
                noAgents.Name = "noAgents";
                noAgents.Description = "Number of wolves";
                noAgents.UpperBoundary = 10000.0;
                noAgents.LowerBoundary = 1.0;

                ParamInfo maxIter = new ParamInfo();
                noAgents.Name = "maxIter";
                noAgents.Description = "Number of iterations";
                noAgents.UpperBoundary = 10000.0;
                noAgents.LowerBoundary = 1.0;

                return new ParamInfo[] {noAgents, maxIter};
            }
            set
            {
                return;
            }
        }
        public IStateWriter Writer { get; set; }
        public IStateReader Reader { get; set; }
        public IGenerateTextReport StringReportGenerator { get; set; }
        public IGeneratePDFReport PdfReportGenerator { get; set; }


        public double[] XBest { get => _XBest; set => _XBest = value; }
        public double FBest { get => _FBest; set => _FBest = value; }
        public int NumberOfEvaluationFitnessFunction { get => _NumberOfEvaluationFitnessFunction; set => _NumberOfEvaluationFitnessFunction = value; }

        public void InitializeWolves(double[,] domain, params double[] parameters)
        {
            _domain = domain;
            _rand = new Random();
            _dim = _domain.GetLength(1); // Można zawsze pobierać parametr z _dim

            // Parametry: func, domain, searchAgents_no, _maxIter
            // Nasze paremtry są typu int, od razu możemy je scastować, żeby nie robić tego za każdym razem gdy ich użyjemy
            // Przy okazji rozpakujemy parametry (mamy tylko 2 w algorytmie), przejrzysciej bedzie jesli je 'rozpakujemy'
            _searchAgents_no = (int)parameters[0];
            _maxIter = (int)parameters[1];

            // Inicjalizacja populacji
            _alphaPos = new double[_dim];
            _alphaScore = double.PositiveInfinity;

            _betaPos = new double[_dim];
            _betaScore = double.PositiveInfinity;

            _deltaPos = new double[_dim];
            _deltaScore = double.PositiveInfinity;

            _positions = new double[_searchAgents_no, _dim];
            for (int i = 0; i < _searchAgents_no; i++)
            {
                for (int j = 0; j < _dim; j++)
                {
                    // wiersz 0 domain: lower bounds
                    // wiersz 1 domain: upper bounds
                    _positions[i, j] = _rand.NextDouble() * (domain[1, j] - domain[0, j]) + domain[0, j];
                }
            }
        }
        public void Solve(global::fitnessFunction f, double[,] domain, params double[] parameters)
        {
            InitializeWolves(domain, parameters);
            _funcCalls_no = 0;

            // TODO: Można zaimplementować do raportu
            double[] convCurve = new double[_maxIter];
            double timerStart = DateTime.Now.Ticks / TimeSpan.TicksPerSecond;
            string startDate = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"); // idk czy potrzebne

            // algorytm:
            for (int l = _currentIteration; l < _maxIter; l++)
            {
                try
                {
                    RunIteration(f, l);
                }
                catch (Exception e)
                {
                    System.Console.Error.WriteLine(String.Format("The following error occure while running {0} at iteration {1} :\n",
                         _Name, _currentIteration), e.ToString());
                    break;
                }
                try
                {
                    //SaveProcess(f, domain, convCurve, timerStart, startDate, parameters);
                }
                catch (Exception e)
                {
                    System.Console.Error.WriteLine(String.Format("The following error occure while saving {0} at iteration {1} :\n"),
                        _Name, _currentIteration, e.ToString());
                }

                _currentIteration=l;
                convCurve[l] = _alphaScore;
            }
            System.Console.WriteLine(_alphaScore.ToString());
            System.Console.WriteLine(_alphaPos[0].ToString());
        }
        public void RunIteration(global::fitnessFunction f, double l)
        {
            double a;
            for (int i = 0; i < _searchAgents_no; i++)
            {
                for (int j = 0; j < _dim; j++)
                {
                    // 'Przycina' pozycje, które wyszły poza Dziedzinę przy kroku
                    _positions[i, j] = Clamp(_positions[i, j], _domain[0, j], _domain[1, j]);
                }

                double fitness = f(SubArray(_positions, i));
                _funcCalls_no++;

                if (fitness < _alphaScore)
                {
                    _deltaScore = _betaScore;
                    _deltaPos = (double[])_betaPos.Clone();
                    _betaScore = _alphaScore;
                    _betaPos = (double[])_alphaPos.Clone();
                    _alphaScore = fitness;
                    _alphaPos = (double[])_positions.CloneRow(i);
                }

                if (fitness > _alphaScore && fitness < _betaScore)
                {
                    _deltaScore = _betaScore;
                    _deltaPos = (double[])_betaPos.Clone();
                    _betaScore = fitness;
                    _betaPos = (double[])_positions.CloneRow(i);
                }

                if (fitness > _alphaScore && fitness > _betaScore && fitness < _deltaScore)
                {
                    _deltaScore = fitness;
                    _deltaPos = (double[])_positions.CloneRow(i);
                }
            }

            a = 2 - l * (2.0 / _maxIter);

            for (int i = 0; i < _searchAgents_no; i++)
            {
                for (int j = 0; j < _dim; j++)
                {
                    double r1 = _rand.NextDouble();
                    double r2 = _rand.NextDouble();

                    double A1 = 2 * a * r1 - a;
                    double C1 = 2 * r2;
                    double D_alpha = Math.Abs(C1 * _alphaPos[j] - _positions[i, j]);
                    double X1 = _alphaPos[j] - A1 * D_alpha;

                    r1 = _rand.NextDouble();
                    r2 = _rand.NextDouble();

                    double A2 = 2 * a * r1 - a;
                    double C2 = 2 * r2;
                    double D_beta = Math.Abs(C2 * _betaPos[j] - _positions[i, j]);
                    double X2 = _betaPos[j] - A2 * D_beta;

                    r1 = _rand.NextDouble();
                    r2 = _rand.NextDouble();

                    double A3 = 2 * a * r1 - a;
                    double C3 = 2 * r2;
                    double D_delta = Math.Abs(C3 * _deltaPos[j] - _positions[i, j]);
                    double X3 = _deltaPos[j] - A3 * D_delta;

                    _positions[i, j] = (X1 + X2 + X3) / 3;
                }
            }
        }

        // helper methods
        private double[] SubArray(double[,] array, int row)
        {
            int columns = array.GetLength(1);
            double[] result = new double[columns];
            for (int i = 0; i < columns; i++)
            {
                result[i] = array[row, i];
            }
            return result;
        }
        public static double Clamp(double value, double min, double max)
        {
            if (min > max)
            {
                throw new ArgumentException("min > max");
            }
 
            if (value < min)
            {
                return min;
            }
            else if (value > max)
            {
                return max;
            }
 
            return value;
        }
        //public void SaveProcess(fitnessFunction f,
        //                    double[,] domain,
        //                    double[] convCurve,
        //                    double timerStart,
        //                    string startDate,
        //                    params double[] parameters)
        //{
        //    StateWriterBuilder Builder = new StateWriterBuilder();
        //    Builder.XBest = _XBest;
        //    Builder.FBest = _FBest;
        //    Builder.NumberOfEvaluationFitnessFunction = _NumberOfEvaluationFitnessFunction;
        //    Builder.FuncCalls_no = _funcCalls_no;
        //    Builder.MaxIter = _maxIter;
        //    Builder.SearchAgents_no = _searchAgents_no;
        //    Builder.Domain = domain;
        //    Builder.Dim = _dim;
        //    Builder.CurrentIteration = _currentIteration;
        //    Builder.Rand = _rand;
        //    Builder.Positions = _positions;
        //    Builder.AlphaPos = _alphaPos;
        //    Builder.AlphaScore = _alphaScore;
        //    Builder.BetaPos = _betaPos;
        //    Builder.BetaScore = _betaScore;
        //    Builder.DeltaPos = _deltaPos;
        //    Builder.DeltaScore = _deltaScore;
        //    Builder.ConvCurve = convCurve;
        //    Builder.StartDate = startDate;
        //    Builder.TimerStart = timerStart;
        //    Builder.Parameters = parameters;
        //    Builder.F = f;


        //    Writer = Builder.build();
        //    Writer.SaveToFileStateOfAlghoritm("/save.json");
        //}

        public void Hello()
        {
            System.Console.WriteLine("Hello World!");
        }
    }
}

// helper class
public static class ArrayExtensions
{
    public static double[] CloneRow(this double[,] array, int row)
    {
        int columns = array.GetLength(1);
        double[] result = new double[columns];
        for (int i = 0; i < columns; i++)
        {
            result[i] = array[row, i];
        }
        return result;
    }
}