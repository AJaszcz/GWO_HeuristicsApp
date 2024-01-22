using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace GWO
{
    public class StateWriter : IStateWriter
    {
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
        // Dotyczące wykonania
        private fitnessFunction _func;
        private double[] _convCurve;
        private double _timerStart;
        private string _startDate;
        private double[] _parameters;

        // koniec parametrów
        public StateWriter(double[] XBest,
            double Fbest,
            int NumberOfEvaluationFitnessFunction,
            int funcCalls_no,
            int maxIter,
            int searchAgents_no,
            double[,] domain,
            int dim,
            int currentIteration,
            Random rand,
            double[,] positions,
            double[] alphaPos,
            double alphaScore,
            double[] betaPos,
            double betaScore,
            double[] deltaPos,
            double deltaScore,
            fitnessFunction func,
            double[] convCurve,
            double timerStart,
            string startDate,
            double[] parameters)
        {
            _XBest = XBest;
            _FBest = Fbest;
            _NumberOfEvaluationFitnessFunction = NumberOfEvaluationFitnessFunction;
            _funcCalls_no = funcCalls_no;
            _maxIter = maxIter;
            _searchAgents_no = searchAgents_no;
            _domain = domain;
            _dim = dim;
            _currentIteration = currentIteration;
            _rand = rand;
            _positions = positions;
            _alphaPos = alphaPos;
            _alphaScore = alphaScore;
            _betaPos = betaPos;
            _betaScore = betaScore;
            _deltaPos = deltaPos;
            _deltaScore = deltaScore;
            _func = func;
            _convCurve = convCurve;
            _timerStart = timerStart;
            _parameters = parameters;
            _startDate = startDate;
        }
        public void SaveToFileStateOfAlghoritm(string path)
        {
            var Dane = new
            {
                XBest = _XBest,
                FBest = _FBest,
                NumberOfEvaluationFitnessFunction = _NumberOfEvaluationFitnessFunction,
                funcCalls_no = _funcCalls_no,
                maxIter = _maxIter,
                searchAgents_no = _searchAgents_no,
                domain = _domain,
                dim = _dim,
                currentIteration = _currentIteration,
                rand = _rand,
                positions = _positions,
                alphaPos = _alphaPos,
                alphaScore = _alphaScore,
                betaPos = _betaPos,
                betaScore = _betaScore,
                deltaPos = _deltaPos,
                deltaScore = _deltaScore,
                func = _func,
                convCurve = _convCurve,
                timerStart = _timerStart,
                startDate = _startDate,
                parameters = _parameters
            };
            string jsonData = JsonConvert.SerializeObject(Dane, new JsonSerializerSettings
            {
                Formatting = Newtonsoft.Json.Formatting.Indented,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                TypeNameHandling = TypeNameHandling.None
            });
            System.IO.File.WriteAllText(path, jsonData);
        }
        public void DeleteSaveAfterCompletion(string path)
        {
            System.IO.File.Delete(path);
        }
    }
}
