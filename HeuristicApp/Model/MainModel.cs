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
using System.Linq;

namespace HeuristicApp.Model
{
    internal class MainModel
    {
        private Dictionary<string, OptAlg> algDict = new Dictionary<string, OptAlg>();
        private Dictionary<string, MethodInfo> fitFuncDict = new Dictionary<string, MethodInfo>();

        // events
        public event Action <string> AddAlgorithm;
        public event Action <string> AddFitFunc;

        public void GetFitnessesFromDll(string dllPath)
        {
            Assembly assembly = Assembly.LoadFrom(dllPath);
            foreach (Type type in assembly.GetTypes())
            {
                foreach (MethodInfo m in type.GetMethods())
                {
                    this.AddFitFuncToDict(m);
                }
            }
        }
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
        public void AddFitFuncToDict(MethodInfo m)
        {
            // troche na okolo ale dziala
            string[] standard = { "Equals", "GetHashCode", "GetType", "ToString" };
            if (!fitFuncDict.ContainsKey(m.Name) && !standard.Contains(m.Name))
            {
                this.fitFuncDict.Add(m.Name, m);
                // Notify that fitfunc has just been added
                AddFitFunc?.Invoke(m.Name);
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
                GetFitnessesFromDll(file);
            }
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
    }
}
