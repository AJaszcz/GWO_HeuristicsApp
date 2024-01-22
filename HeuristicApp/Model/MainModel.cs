using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Reflection.Emit;
using System.Security.Permissions;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.IO;
using System.Diagnostics;

namespace HeuristicApp.Model
{
    internal class MainModel
    {
        private Dictionary<string, OptAlg> algDict = new Dictionary<string, OptAlg>();
        private Dictionary<string, FitFunc> fitFuncDict = new Dictionary<string, FitFunc>();

        // events
        public event Action <string> AddAlgorithm;
        public event Action <string> AddFitFunc;
        public void AddAlgToDict(string dllPath)
        {
            OptAlg alg = new OptAlg(dllPath);
            if (!algDict.ContainsKey(alg.name))
            {
                this.algDict.Add(alg.name, alg);
                // Notify that algortihm has just been added
                AddAlgorithm?.Invoke(alg.name);
            }
        }
       
        public void AddFitFuncToDict(string dllPath)
        {
            FitFunc fitFunc = new FitFunc(dllPath);
            if (!algDict.ContainsKey(fitFunc.name))
            {
                this.fitFuncDict.Add(fitFunc.name, fitFunc);
                // Notify that algortihm has just been added
                AddFitFunc?.Invoke(fitFunc.name);
            }
        }
        public void LoadBaseAlgorithms()
        {
            // THIS IS UTTER SHIT
            string cwd = Directory.GetCurrentDirectory();
            cwd = Directory.GetParent(cwd).Parent.FullName;

            string path = cwd + @"\Algorithms\";
            string filter = "*.dll";
            string[] files = Directory.GetFiles(path, filter);
            foreach (string file in files)
            {
                AddAlgToDict(file);
            }
        }
        public void LoadBaseFitFuncs()
        {
            // THIS IS UTTER SHIT
            string cwd = Directory.GetCurrentDirectory();
            cwd = Directory.GetParent(cwd).Parent.FullName;

            string path = cwd + @"\FitnessFunctions\";
            string filter = "*.dll";
            string[] files = Directory.GetFiles(path, filter);
            foreach (string file in files)
            {
                //GetFitnessesFromDll(file);
                AddFitFuncToDict(file);
            }
        }

        public void SaveAlgParameters(string name, double[] parameters)
        {
            this.algDict[name].algParameters = parameters;
        }

        public void SaveFitFuncParameters(string name, double[] parameters)
        {
            //int n = parameters.Length-1;
            int n = (int)parameters[0];
            this.fitFuncDict[name].n = n;
            double [,] domain = new double[2, n];
            for (int i = 0; i < n; i++)
            {   
                domain[0,i] = parameters[i*2+1]; //lb
                domain[1,i] = parameters[i*2+2]; //ub
            }
            this.fitFuncDict[name].domain = domain;
        }

        public object[,] GetAlgInfo(string algName)
        {
            // To jest taki "mock", jakby (raczej) byly informacje o parametrach przekazywane na widok
            object[,] parameters = { { "noAgents", "Number of Agents", 1.0, 250.0 }, { "maxIter", "Number of iterations", 1.0, 200.0 } };
            return parameters;
            //OptAlg alg = algDict["algName"];
            //object[] arrayInstance = (object[])Array.CreateInstance(alg.paramInfoType, 2);
            //arrayInstance = alg.ParamsInfo;

            //int noParam = 2;
            //for (int i = 0; i < noParam; i++)
            //{
            //    arrayInstance[i];
            //}
            //// TODO: See how it can be done 
        }
        public double[] GetAlgParams(string algName)
        {
            return algDict[algName].algParameters;
        }

        public object[] GetFitFuncInfo(string fitFuncName)
        {
            FitFunc fitFunc = fitFuncDict[fitFuncName];
            object[] arr = { fitFunc.name, fitFunc.describtion, fitFunc.n, fitFunc.fixedDimensionality, fitFunc.domain};
            return arr;
        }

        public void runAlgTest(string algName, string[] fitFuncNames)
        {
            OptAlg alg = algDict[algName];
            foreach(string fitFuncName in fitFuncNames)
            {
                FitFunc curFitFunc = fitFuncDict[fitFuncName];
                var fitnessDelegate = Delegate.CreateDelegate(alg.fitFuncType, null, curFitFunc.fitFuncMethod);

                //double[,] multiDimensionalArray = { { -5, -5}, { 5, 5}};
                double[,] domain = curFitFunc.domain;
                object[] allParameters = { fitnessDelegate, domain, alg.algParameters};

                alg.optAlgMethods["Solve"].Invoke(alg.optAlgObj, allParameters);
                double score = (double)alg.optAlgMethods["get_FBest"].Invoke(alg.optAlgObj, null);
            }
        }
        public void runFitFunTest(string[] algNames, string fitFuncName)
        {

        }

    }
}
