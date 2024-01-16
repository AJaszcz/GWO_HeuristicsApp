using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

public interface IStateWriter
{
    // Metoda zapisuj ąca do pliku tekstowego stan algorytmu (w odpowiednim formacie
    void SaveToFileStateOfAlghoritm(string path);
    // Stan algorytmu : numer iteracji , liczba wywo łań funkcji celu ,
    // populacja wraz z warto ścią funkcji dopasowania
}

public interface IStateReader
{
    // Metoda wczytuj ąca z pliku stan algorytmu (w odpowiednim formacie ).
    // Stan algorytmu : numer iteracji , liczba wywo łań funkcji celu ,
    // populacja wraz z warto ścią funkcji dopasowania
    void LoadFromFileStateOfAlghoritm(string path);
}


public interface IGeneratePDFReport
{
    // Tworzy raport w okre ś lonym stylu w formacie PDF
    // w raporcie powinny znale źć się informacje o:
    // najlepszym osobniku wraz z warto ścią funkcji celu ,
    // liczbie wywo łań funkcji celu ,
    // parametrach algorytmu
    void GenerateReport(string path);
}

public interface IGenerateTextReport
{
    // Tworzy raport w postaci łań cucha znak ów
    // w raporcie powinny znale źć się informacje o:
    // najlepszym osobniku wraz z warto ścią funkcji celu ,
    // liczbie wywo łań funkcji celu ,
    // parametrach algorytmu
    string ReportString { get; set; }   // Od wersji 8.0
}
public class ParamInfo
{
    public string Name { get; set; }
    public string Description { get; set; }
    public double UpperBoundary { get; set; }
    public double LowerBoundary { get; set; }
}
public delegate double fitnessFunction(params double[] arg);
interface IOptimizationAlgorithm
{
    // Nazwa algorytmu
    string Name { get; set; }

    // Metoda zaczynaj ąca rozwi ą zywanie zagadnienia poszukiwania minimum funkcji celu.
    // Jako argument przyjmuje :
    // funkcj ę celu ,
    // dziedzin ę zadania w postaci tablicy 2D,
    // list ę pozosta łych wymaganych parametr ów algorytmu ( tylko warto ści , w kolejności takiej jak w ParamsInfo ).
    // Po wykonaniu ustawia odpowiednie właś ciwo ści: XBest , Fbest , NumberOfEvaluationFitnessFunction
    void Solve(fitnessFunction f, double[,] domain, params double[] parameters); // TODO: zobaczyć, bo nie działało

    //Lista informacji o kolejnych parametrach algorytmu
    ParamInfo[] ParamsInfo { get; set; }

    // Obiekt odpowiedzialny za zapis stanu algorytmu do pliku
    // Po każ dej iteracji algorytmu , powinno się wywo łać metod ę
    // SaveToFileStateOfAlghoritm tego obiektu w celu zapisania stanu algorytmu
    IStateWriter writer { get; set; }

    // Obiekt odpowiedzialny za odczyt stanu algorytmu z pliku
    // Na pocz ątku metody Solve , obiekt ten powinien wczyta ć stan algorytmu
    // jeśli stan zosta ł zapisany
    IStateReader reader { get; set; }

    // Obiekt odpowiedzialny za generowanie napisu z raportem
    IGenerateTextReport stringReportGenerator { get; set; }

    // Obiekt odpowiedzialny za generowanie raportu do pliku pdf
    IGeneratePDFReport pdfReportGenerator { get; set; }

    // Właś ciow ść zwracaj ąca tablic ę z najlepszym osobnikiem
    double[] XBest { get; set; }

    // Właś ciwo ść zwracaj ąca warto ść funkcji dopasowania dla najlepszego osobnika
    double FBest { get; set; }

    // Właś ciwo ść zwracaj ąca liczb ę wywo łań funkcji dopasowania
    int NumberOfEvaluationFitnessFunction { get; set; }

}

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
                throw new NotImplementedException();
            }
        }
        class Writer : IStateWriter
        {
            public void SaveToFileStateOfAlghoritm(string str)
            {
                throw new NotImplementedException();
            }
        }
        public IStateWriter writer { get => new Writer(); set => throw new NotImplementedException(); }

        public IStateWriter SaveToFileStateOfAlghoritm(string str)
        {
            throw new NotImplementedException();
        }

        public IStateReader reader { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IGenerateTextReport stringReportGenerator { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IGeneratePDFReport pdfReportGenerator { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }


        public double[] XBest { get => _XBest; set => _XBest = value; }
        public double FBest { get => _FBest; set => _FBest = value; }
        public int NumberOfEvaluationFitnessFunction { get => _NumberOfEvaluationFitnessFunction; set => _NumberOfEvaluationFitnessFunction = value; }

        public void InitializeWolves(double[,] domain, params double[] parameters)
        {
            _domain = domain;
            _rand = new Random();
            _dim = _domain.GetLength(0); // Można zawsze pobierać parametr z _dim

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
                    System.Console.Error.WriteLine(String.Format("The following error occure while running {0} at iteraton {1} :\n",
                         _Name, _currentIteration), e.ToString());
                    break;
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