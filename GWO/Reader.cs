using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GWO
{
    public class StateReader : IStateReader
    {
        // Parametry
        // Dotyczące iteracji
        public double[] XBest { get; set; }
        public double FBest { get; set; }
        public int NumberOfEvaluationFitnessFunction { get; set; }
        public int FuncCalls_no { get; set; }
        // Dotyczące algorytmu
        public int MaxIter;
        public int SearchAgents_no { get; set; }
        public double[,] Domain { get; set; }
        public int Dim { get; set; }
        public int CurrentIteration { get; set; }
        public Random Rand { get; set; }
        // Dotyczące wilków
        public double[,] Positions { get; set; }

        public double[] AlphaPos { get; set; }
        public double AlphaScore { get; set; }

        public double[] BetaPos { get; set; }
        public double BetaScore { get; set; }

        public double[] DeltaPos { get; set; }
        public double DeltaScore { get; set; }
        // Dotyczące wykonania
        public fitnessFunction Func { get; set; }
        public double[] ConvCurve { get; set; }
        public double TimerStart { get; set; }
        public string StartDate { get; set; }
        public double[] Parameters { get; set; }

        // koniec parametrów
        public StateReader() { }
        public StateReader LoadFromFileStateOfAlghoritm(string path)
        {
            string jsonData = System.IO.File.ReadAllText(path);
            StateReader Dane = JsonConvert.DeserializeObject<StateReader>(jsonData);

            /*            XBest = Dane.XBest;
                        FBest = Dane.FBest;
                        NumberOfEvaluationFitnessFunction = Dane.NumberOfEvaluationFitnessFunction;
                        FuncCalls_no = Dane.FuncCalls_no;
                        MaxIter = Dane.MaxIter;
                        SearchAgents_no = Dane.SearchAgents_no;
                        Domain = Dane.Domain;
                        Dim = Dane.Dim;
                        CurrentIteration = Dane.CurrentIteration;
                        Rand = Dane.Rand;
                        Positions = Dane.Positions;
                        AlphaPos = Dane.AlphaPos;
                        AlphaScore = Dane.AlphaScore;
                        BetaPos = Dane.BetaPos;
                        BetaScore = Dane.BetaScore;
                        DeltaPos = Dane.DeltaPos;
                        DeltaScore = Dane.DeltaScore;
                        Func = Dane.Func;
                        ConvCurve = Dane.ConvCurve;
                        TimerStart = Dane.TimerStart;
                        StartDate = Dane.StartDate;
                        Parameters = Dane.Parameters;*/

            return Dane;
        }
    }
}
