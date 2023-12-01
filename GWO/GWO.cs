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

        // TODO: implementacja
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
            // Przy okazji rozpakujemy parametry (mamy tylko 2 w algorytmie), przejrzysciej bedzie jesli je 'rozpakujemy'
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
                    // wiersz 0 domain: lower bounds
                    // wiersz 1 domain: upper bounds
                    Positions[i, j] = rand.NextDouble() * (domain[1,j] - domain[0,j]) + domain[0,j];
                }
            }

            // Dane do raportu:
            double[] convCurve = new double[maxIter];
            double a;
            int funcCalss_no = 0;
            double timerStart = DateTime.Now.Ticks / TimeSpan.TicksPerSecond;
            string startDate = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"); // idk czy potrzebne

            // algorytm:
            for (int l = 0; l < maxIter; l++)
            {
                for (int i = 0; i < searchAgents_no; i++)
                {
                    for (int j = 0; j < dim; j++)
                    {
                        // 'Przycina' pozycje, które wyszły poza Dziedzinę przy kroku
                        Positions[i, j] = Clamp(Positions[i, j], domain[0, j], domain[1, j]);
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