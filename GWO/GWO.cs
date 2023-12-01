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
    string Name { get; set; }
    string Description { get; set; }
    double UpperBoundary { get; set; }
    double LowerBoundary { get; set; }
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

        string IOptimizationAlgorithm.Name { get => _Name; set => _Name = value; }
        public ParamInfo[] ParamsInfo { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IStateWriter writer { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IStateReader reader { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IGenerateTextReport stringReportGenerator { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IGeneratePDFReport pdfReportGenerator { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double[] XBest { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double FBest { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int NumberOfEvaluationFitnessFunction { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Solve(global::fitnessFunction f, double[,] domain, params double[] parameters)
        {
            Random rand = new Random();
            int dim = domain.GetLength(0); // Można zawsze pobierać parametr z dim

            // Parametry: func, domain, searchAgents_no, maxIter
            // Nasze paremtry są typu int, od razu możemy je scastować, żeby nie robić tego za każdym razem gdy ich użyjemy
            // Przy okazji rozpakujemy parametry (mamy tylko 2 w algorytmie, przejrzysciej bedzie jesli je 'rozpakujemy'
            int searchAgents_no = (int)parameters[0];
            int maxIter = (int)parameters[1];

            // Inicjalizacja populacji
            double[] alphaPos = new double[dim];
            double alphaScore = double.PositiveInfinity;

            double[] betaPos = new double[dim];
            double betaScore = double.PositiveInfinity;

            double[] deltaPos = new double[dim];
            double deltaScore = double.PositiveInfinity;

            double[,] Positions = new double[searchAgents_no, dim];
            for (int i = 0; i < searchAgents_no; i++)
            {
                for (int j = 0; j < dim; j++)
                {
                    Positions[i, j] = rand.NextDouble() * (domain[j,1] - domain[j,0]) + domain[j,0];
                }
            }

            // Dane do raportu:
            double[] convCurve = new double[maxIter];
            double a;
            int funcCalss_no = 0;
            double timerStart = DateTime.Now.Ticks / TimeSpan.TicksPerSecond;
            string startDate = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"); // idk czy potrzebne

            //algorytm:
            for (int l = 0; l < maxIter; l++)
            {
                for (int i = 0; i < searchAgents_no; i++)
                {
                    for (int j = 0; j < dim; j++)
                    {
                        Positions[i, j] = Math.Max(Math.Min(Positions[i, j], domain[j, 0]), domain[j, 0]);
                    }

                    double fitness = f(SubArray(Positions, i));
                    funcCalss_no++;

                    if (fitness < alphaScore)
                    {
                        deltaScore = betaScore;
                        deltaPos = (double[])betaPos.Clone();
                        betaScore = alphaScore;
                        betaPos = (double[])alphaPos.Clone();
                        alphaScore = fitness;
                        alphaPos = (double[])Positions.CloneRow(i);
                    }

                    if (fitness > alphaScore && fitness < betaScore)
                    {
                        deltaScore = betaScore;
                        deltaPos = (double[])betaPos.Clone();
                        betaScore = fitness;
                        betaPos = (double[])Positions.CloneRow(i);
                    }

                    if (fitness > alphaScore && fitness > betaScore && fitness < deltaScore)
                    {
                        deltaScore = fitness;
                        deltaPos = (double[])Positions.CloneRow(i);
                    }
                }

                a = 2 - l * (2.0 / maxIter);

                for (int i = 0; i < searchAgents_no; i++)
                {
                    for (int j = 0; j < dim; j++)
                    {
                        double r1 = rand.NextDouble();
                        double r2 = rand.NextDouble();

                        double A1 = 2 * a * r1 - a;
                        double C1 = 2 * r2;
                        double D_alpha = Math.Abs(C1 * alphaPos[j] - Positions[i, j]);
                        double X1 = alphaPos[j] - A1 * D_alpha;

                        r1 = rand.NextDouble();
                        r2 = rand.NextDouble();

                        double A2 = 2 * a * r1 - a;
                        double C2 = 2 * r2;
                        double D_beta = Math.Abs(C2 * betaPos[j] - Positions[i, j]);
                        double X2 = betaPos[j] - A2 * D_beta;

                        r1 = rand.NextDouble();
                        r2 = rand.NextDouble();

                        double A3 = 2 * a * r1 - a;
                        double C3 = 2 * r2;
                        double D_delta = Math.Abs(C3 * deltaPos[j] - Positions[i, j]);
                        double X3 = deltaPos[j] - A3 * D_delta;

                        Positions[i, j] = (X1 + X2 + X3) / 3;
                    }
                }

                convCurve[l] = alphaScore;
            }
            System.Console.WriteLine(alphaScore.ToString());
            System.Console.WriteLine(alphaPos[0].ToString());
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





//namespace GWO_test
//{
//    public class GWO
//    {
//        private Random rand;

//        public virtual Solution Optimize(Func<double[], double> objf, double lb, double ub, int dim, int searchAgents_no, int Max_iter)
//        {
//            rand = new Random();

//            double[] Alpha_pos = new double[dim];
//            double Alpha_score = double.PositiveInfinity;

//            double[] Beta_pos = new double[dim];
//            double Beta_score = double.PositiveInfinity;

//            double[] Delta_pos = new double[dim];
//            double Delta_score = double.PositiveInfinity;

//            double[] lbArray = new double[dim];
//            double[] ubArray = new double[dim];
//            for (int i = 0; i < dim; i++)
//            {
//                lbArray[i] = lb;
//                ubArray[i] = ub;
//            }

//            double[,] Positions = new double[SearchAgents_no, dim];
//            for (int i = 0; i < SearchAgents_no; i++)
//            {
//                for (int j = 0; j < dim; j++)
//                {
//                    Positions[i, j] = rand.NextDouble() * (ubArray[j] - lbArray[j]) + lbArray[j];
//                }
//            }

//            double[] Convergence_curve = new double[Max_iter];
//            Solution s = new Solution();

//            double a;
//            double timerStart = DateTime.Now.Ticks / TimeSpan.TicksPerSecond;
//            s.startTime = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");

//            for (int l = 0; l < Max_iter; l++)
//            {
//                for (int i = 0; i < SearchAgents_no; i++)
//                {
//                    for (int j = 0; j < dim; j++)
//                    {
//                        Positions[i, j] = Math.Max(Math.Min(Positions[i, j], ubArray[j]), lbArray[j]);
//                    }

//                    double fitness = objf(SubArray(Positions, i));
//                    s.func_calls++;

//                    if (fitness < Alpha_score)
//                    {
//                        Delta_score = Beta_score;
//                        Delta_pos = (double[])Beta_pos.Clone();
//                        Beta_score = Alpha_score;
//                        Beta_pos = (double[])Alpha_pos.Clone();
//                        Alpha_score = fitness;
//                        Alpha_pos = (double[])Positions.CloneRow(i);
//                    }

//                    if (fitness > Alpha_score && fitness < Beta_score)
//                    {
//                        Delta_score = Beta_score;
//                        Delta_pos = (double[])Beta_pos.Clone();
//                        Beta_score = fitness;
//                        Beta_pos = (double[])Positions.CloneRow(i);
//                    }

//                    if (fitness > Alpha_score && fitness > Beta_score && fitness < Delta_score)
//                    {
//                        Delta_score = fitness;
//                        Delta_pos = (double[])Positions.CloneRow(i);
//                    }
//                }

//                a = 2 - l * (2.0 / Max_iter);

//                for (int i = 0; i < SearchAgents_no; i++)
//                {
//                    for (int j = 0; j < dim; j++)
//                    {
//                        double r1 = rand.NextDouble();
//                        double r2 = rand.NextDouble();

//                        double A1 = 2 * a * r1 - a;
//                        double C1 = 2 * r2;
//                        double D_alpha = Math.Abs(C1 * Alpha_pos[j] - Positions[i, j]);
//                        double X1 = Alpha_pos[j] - A1 * D_alpha;

//                        r1 = rand.NextDouble();
//                        r2 = rand.NextDouble();

//                        double A2 = 2 * a * r1 - a;
//                        double C2 = 2 * r2;
//                        double D_beta = Math.Abs(C2 * Beta_pos[j] - Positions[i, j]);
//                        double X2 = Beta_pos[j] - A2 * D_beta;

//                        r1 = rand.NextDouble();
//                        r2 = rand.NextDouble();

//                        double A3 = 2 * a * r1 - a;
//                        double C3 = 2 * r2;
//                        double D_delta = Math.Abs(C3 * Delta_pos[j] - Positions[i, j]);
//                        double X3 = Delta_pos[j] - A3 * D_delta;

//                        Positions[i, j] = (X1 + X2 + X3) / 3;
//                    }
//                }

//                Convergence_curve[l] = Alpha_score;
//            }

//            double timerEnd = DateTime.Now.Ticks / TimeSpan.TicksPerSecond;
//            s.endTime = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
//            s.executionTime = timerEnd - timerStart;
//            s.convergence = Convergence_curve;
//            s.optimizer = "GWO";
//            s.bestIndividual = Alpha_pos;
//            s.objfname = objf.Method.Name;

//            s.best = Alpha_score;
//            s.iterations = Max_iter;
//            s.popnum = SearchAgents_no;
//            s.dim = dim;
//            s.lb = lb;
//            s.ub = ub;

//            return s;
//        }

//        private double[] SubArray(double[,] array, int row)
//        {
//            int columns = array.GetLength(1);
//            double[] result = new double[columns];
//            for (int i = 0; i < columns; i++)
//            {
//                result[i] = array[row, i];
//            }
//            return result;
//        }
//    }

//    public class Solution
//    {
//        public double[] bestIndividual;
//        public double best;
//        public double[] convergence;
//        public double executionTime;
//        public string startTime;
//        public string endTime;
//        public string optimizer;
//        public int iterations;
//        public int popnum;
//        public int dim;
//        public double lb;
//        public double ub;
//        public string objfname;
//        public int func_calls;
//    }

//    public static class ArrayExtensions
//    {
//        public static double[] CloneRow(this double[,] array, int row)
//        {
//            int columns = array.GetLength(1);
//            double[] result = new double[columns];
//            for (int i = 0; i < columns; i++)
//            {
//                result[i] = array[row, i];
//            }
//            return result;
//        }
//    }
//}

