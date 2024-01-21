using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWO.Interfaces
{
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
        IStateWriter Writer { get; set; }

        // Obiekt odpowiedzialny za odczyt stanu algorytmu z pliku
        // Na pocz ątku metody Solve , obiekt ten powinien wczyta ć stan algorytmu
        // jeśli stan zosta ł zapisany
        IStateReader Reader { get; set; }

        // Obiekt odpowiedzialny za generowanie napisu z raportem
        IGenerateTextReport StringReportGenerator { get; set; }

        // Obiekt odpowiedzialny za generowanie raportu do pliku pdf
        IGeneratePDFReport PdfReportGenerator { get; set; }

        // Właś ciow ść zwracaj ąca tablic ę z najlepszym osobnikiem
        double[] XBest { get; set; }

        // Właś ciwo ść zwracaj ąca warto ść funkcji dopasowania dla najlepszego osobnika
        double FBest { get; set; }

        // Właś ciwo ść zwracaj ąca liczb ę wywo łań funkcji dopasowania
        int NumberOfEvaluationFitnessFunction { get; set; }

    }
}
