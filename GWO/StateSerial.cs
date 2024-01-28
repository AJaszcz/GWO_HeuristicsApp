using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWO
{
    public class StateSerial
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
        public StateSerial() { }
    }
}
