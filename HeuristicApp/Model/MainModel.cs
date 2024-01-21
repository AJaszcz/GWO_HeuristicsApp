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
        
        public object[,] GetAlgInfo(string algName)
        {
            // To jest taki "mock", jakby (raczej) byly informacje o parametrach przekazywane na widok
            object[,] parameters = { { "name", "describtion", 1.0, 2.0 }, { "name", "describtion", 1.0, 2.0 } };
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

                double[,] multiDimensionalArray = { { -5, -5}, { 5, 5}};
                object[] allParameters = { fitnessDelegate, multiDimensionalArray, alg.algParameters};

                alg.optAlgMethods["Solve"].Invoke(alg.optAlgObj, allParameters);
                double score = (double)alg.optAlgMethods["get_FBest"].Invoke(alg.optAlgObj, null);
            }
        }
        public void runFitFunTest(string[] algNames, string fitFuncName)
        {

        }

        //MethodInfo fitFuncInfo = typeof(Program).GetMethod("HimmelblauFunction"); // Gathers method info about
        //var fitnessDelegate = Delegate.CreateDelegate(algDict["GWO"].fitFuncType, null, fitFuncInfo); // Creates instance of a delegate using obtained mathod (fitness fucntion) info

        //// Choose 'Solve' method and invoke it
        //MethodInfo currentSovle = algDict["GWO"].optAlgMethods["Solve"];
        //Console.WriteLine("Using method: " + currentSovle.Name);

        //    ////// TODO: Get parameters from the user (through presenter/controller) //////
        //    // Create array of parameters to be sent to Solve
        //    //double[,] multiDimensionalArray = { { -100, -100, -100 }, { 100, 100, 100 } }; // 
        //    double[,] multiDimensionalArray = { { -5, -5 }, { 5, 5 } }; // 
        //double[] parameters = { 100, 100 };
        //object[] allParameters = { fitnessDelegate, multiDimensionalArray, parameters };

        //// Invoke 'Solve' method
        //currentSovle.Invoke(algDict["GWO"].optAlgObj,
        //        allParameters
        //    );

        //    Console.Read();
    }
}
