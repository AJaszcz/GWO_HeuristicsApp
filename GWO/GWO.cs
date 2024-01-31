using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using GWO;
using GWO.Interfaces;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

[Serializable]
public delegate double fitnessFunction(params double[] arg);

namespace GWO
{

    //public delegate double fitnessFunction(params double[] arg);
    [Serializable]
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
        public IGenerateTextReport StringReportGenerator { get; set; } = new GenerateTextReport();
        public IGeneratePDFReport PdfReportGenerator { get; set; } = new GeneratePDFReport();

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
            string solveName = this._Name + "_" + f.GetMethodInfo().Name + ".dat";
            string saveDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            InitializeWolves(domain, parameters);
            _funcCalls_no = 0;

            // TODO: Można zaimplementować do raportu
            double[] convCurve = new double[_maxIter];
            double timerStart = DateTime.Now.Ticks / TimeSpan.TicksPerSecond;
            string startDate = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"); // idk czy potrzebne

            // Chech if there already is solution:
            if (File.Exists(Path.Combine(saveDir, solveName)))
            {
                // Read state
                Console.WriteLine("Read saved state of the algorithm");
                LoadFromFileStateOfAlghoritm(this, Path.Combine(saveDir, solveName));
            }

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
                    //Assembly.GetExecutingAssembly().Location
                    SaveToFileStateOfAlghoritm(
                        Path.Combine(
                            saveDir, solveName)
                        );
                }
                catch (Exception e)
                {
                    //System.Console.Error.WriteLine(String.Format("The following error occure while saving {0} at iteration {1} :\n"),
                    //    _Name, _currentIteration, e.Message);
                    System.Console.Error.WriteLine(e.Message);
                    System.Console.Error.WriteLine(e.StackTrace);
                }

                _currentIteration=l;
                convCurve[l] = _alphaScore;
            }
            //Generate String
            StringReportGenerator = new GenerateTextReport(FBest, XBest);
            PdfReportGenerator = new GeneratePDFReport(StringReportGenerator.ReportString);

            //System.Console.WriteLine(_alphaScore.ToString());
            //System.Console.WriteLine(_alphaPos[0].ToString());
            // print result
            Console.WriteLine(String.Format("Result for {0}:\n", solveName));
            Console.WriteLine(this.getStringResult());
            // delete file if solve complete
            if (File.Exists(Path.Combine(saveDir, solveName)))
            {
                try
                {
                    // Delete the file
                    File.Delete(Path.Combine(saveDir, solveName));
                    Console.WriteLine("Solve finished. Saved state deleted successfully.");
                }
                catch (IOException e)
                {
                    Console.WriteLine($"Error deleting the file: {e.Message}");
                }
            }

        }
        public string getStringResult()
        {
            string result = String.Format("Best fitness: {0}\nArguments:\n", _FBest);
            for (int i = 0; i < _XBest.Length; i++)
            {
                result += String.Format("x{0}: {1}\n", i, _XBest[i]);
            }
            return result;
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
            FBest = _alphaScore;
            XBest = _alphaPos;
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

        public void SetDummyVal()
        {
            _FBest = 123;
            _XBest = new double[6];
            for(int i = 0; i<6; i++)
            {
                _XBest[i] = i;
            }
            StringReportGenerator = new GenerateTextReport(_FBest, _XBest);
            PdfReportGenerator = new GeneratePDFReport(StringReportGenerator.ReportString);
        }

        // serialization

        void SaveToFileStateOfAlghoritm(string path)
        {
            SerializeObject(this, path);
            //SerializeToXml(this, path);
        }
        static void LoadFromFileStateOfAlghoritm(GWO gwo, string path)
        {
            gwo = DeserializeObject<GWO>(path);
            //gwo = DeserializeFromXml<GWO>(path);
        }
        static void SerializeObject<T>(T obj, string filePath)
        {
            // Use BinaryFormatter for serialization
            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                formatter.Serialize(stream, obj);
            }

            Console.WriteLine($"Object serialized to {filePath}");
        }
        //static void SerializeToXml<T>(T obj, string filePath)
        //{
        //    var serializer = new XmlSerializer(typeof(T));

        //    using (var writer = new StringWriter())
        //    {
        //        serializer.Serialize(writer, obj);
        //        File.WriteAllText(filePath, writer.ToString());
        //    }
        //}

        static T DeserializeObject<T>(string filePath)
        {
            // Use BinaryFormatter for deserialization
            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream stream = new FileStream(filePath, FileMode.Open))
            {
                return (T)formatter.Deserialize(stream);
            }
        }
        //static T DeserializeFromXml<T>(string filePath)
        //{
        //    var serializer = new XmlSerializer(typeof(T));

        //    using (var reader = new StringReader(filePath))
        //    {
        //        return (T)serializer.Deserialize(reader);
        //    }
        //}
 
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