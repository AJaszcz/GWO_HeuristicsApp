using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json;

using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace GWO
{
    [Serializable]
    public class StateWriter : IStateWriter
    {
        #region pola definiujące stan algorytmu
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

        #endregion
        public void SaveToFileStateOfAlghoritm(string path)
        {
            //SerializeObject(myObject, "serializedObject.dat");
        }
        public StateWriter(
            string _Name,
            double[] _XBest,
            double _FBest,
            int _NumberOfEvaluationFitnessFunction,
            int _funcCalls_no,
            int _maxIter,
            int _searchAgents_no,
            double[,] _domain,
            int _dim,
            int _currentIteration,
            Random _rand,
            double[,] _positions,
            double[] _alphaPos,
            double _alphaScore,
            double[] _betaPos,
            double _betaScore,

            double[] _deltaPos,
            double _deltaScore
            )
        {
            this._Name = _Name;
            // Parametry
            // Dotyczące iteracji
            this._XBest = _XBest;
            this._FBest = _FBest;
            this._NumberOfEvaluationFitnessFunction = _NumberOfEvaluationFitnessFunction;
            this._funcCalls_no = _funcCalls_no;
            // Dotyczące algorytmu
            this._maxIter = _maxIter;
            this._searchAgents_no = _searchAgents_no;
            this. _domain = _domain;
            this._dim = _dim;
            this._currentIteration = _currentIteration;
            this._rand = _rand;
            // Dotyczące wilków
            this._positions = _positions;

            this._alphaPos = _alphaPos;
            this._alphaScore = _alphaScore;

            this._betaPos = _betaPos;
            this._betaScore = _betaScore;

            this._deltaPos = _deltaPos;
            this._deltaScore = _deltaScore;
        }
    }

    //public class StateWriter : IStateWriter
    //{
    //    // Parametry
    //    // Dotyczące iteracji
    //    private double[] _XBest;
    //    private double _FBest;
    //    private int _NumberOfEvaluationFitnessFunction;
    //    private int _funcCalls_no;
    //    // Dotyczące algorytmu
    //    private int _maxIter;
    //    private int _searchAgents_no;
    //    private double[,] _domain;
    //    private int _dim;
    //    private int _currentIteration;
    //    private Random _rand;
    //    // Dotyczące wilków
    //    private double[,] _positions;

    //    private double[] _alphaPos;
    //    private double _alphaScore;

    //    private double[] _betaPos;
    //    private double _betaScore;

    //    private double[] _deltaPos;
    //    private double _deltaScore;
    //    // koniec parametrów
    //    public StateWriter(double[] XBest,
    //        double Fbest,
    //        int NumberOfEvaluationFitnessFunction,
    //        int funcCalls_no,
    //        int maxIter,
    //        int searchAgents_no,
    //        double[,] domain,
    //        int dim,
    //        int currentIteration,
    //        Random rand,
    //        double[,] positions,
    //        double[] alphaPos,
    //        double alphaScore,
    //        double[] betaPos,
    //        double betaScore,
    //        double[] deltaPos,
    //        double deltaScore)
    //    {
    //        _XBest = XBest;
    //        _FBest = Fbest;
    //        _NumberOfEvaluationFitnessFunction = NumberOfEvaluationFitnessFunction;
    //        _funcCalls_no = funcCalls_no;
    //        _maxIter = maxIter;
    //        _searchAgents_no = searchAgents_no;
    //        _domain = domain;
    //        _dim = dim;
    //        _currentIteration = currentIteration;
    //        _rand = rand;
    //        _positions = positions;
    //        _alphaPos = alphaPos;
    //        _alphaScore = alphaScore;
    //        _betaPos = betaPos;
    //        _betaScore = betaScore;
    //        _deltaPos = deltaPos;
    //        _deltaScore = deltaScore;
    //    }
    //    public void SaveToFileStateOfAlghoritm(string path)
    //    {
    //        var Dane = new
    //        {
    //            XBest = _XBest;
    //            FBest = _Fbest;
    //            NumberOfEvaluationFitnessFunction = _NumberOfEvaluationFitnessFunction;
    //            funcCalls_no = _funcCalls_no;
    //            maxIter = _maxIter;
    //            searchAgents_no = _searchAgents_no;
    //            domain = _domain;
    //            dim = _dim;
    //            currentIteration = _currentIteration;
    //            rand = _rand;
    //            positions = _positions;
    //            alphaPos = _alphaPos;
    //            alphaScore = _alphaScore;
    //            betaPos = _betaPos;
    //            betaScore = _betaScore;
    //            deltaPos = _deltaPos;
    //            deltaScore = _deltaScore;
    //    };
    //        string jsonData = JsonConvert.SerializeObject(Dane, new JsonSerializerSettings
    //        {
    //            Formatting = Formatting.Indented,
    //            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
    //            TypeNameHandling = TypeNameHandling.None
    //        });
    //        File.WriteAllText(path, jsonData);
    //    }
    //}
}
