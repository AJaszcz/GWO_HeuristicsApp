using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace GWO
{
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
