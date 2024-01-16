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

namespace HeuristicApp.Model
{
    internal class OptAlg
    {
        public string name;
        public Type optAlgType;
        public Type fitFuncType;
        public object optAlgObj;
        public Dictionary<string, MethodInfo> optAlgMethods;
        public Type[] ParamsInfo;
        public Type paramInfoType;

        public OptAlg(string dllPath)
        {
            LoadFromDll(dllPath);
        }
        private void LoadFromDll(string dllPath)
        {
            Assembly assembly = Assembly.LoadFrom(dllPath);
            //Console.WriteLine("assembly name: " + assembly.GetName());
            foreach (Type type in assembly.GetTypes())
            {
                if (type.Name == "fitnessFunction")
                {
                    this.fitFuncType = type;
                    continue;
                }
                if (type.Name == "ParamInfo")
                {
                    this.paramInfoType = type;
                    continue;
                }
                // Get all methods in the type
                MethodInfo[] methods_arr = type.GetMethods();
                foreach (MethodInfo m in methods_arr)
                {
                    // Warunek, ze to faktycznie algorytm TODO: To jest okropne, rozwiazac pozniej
                    if (m.Name == "Solve" && type.GetTypeInfo().ToString() != "IOptimizationAlgorithm")
                    {

                        // Zakladamy, ze 1 alg na 1 dll
                        this.optAlgType = type;
                        this.optAlgObj = Activator.CreateInstance(optAlgType);
                        this.name = type.Name;

                        this.optAlgMethods = new Dictionary<string, MethodInfo>();
                        MethodInfo[] methods = optAlgType.GetMethods(); // Gather GWO class methods
                        foreach (MethodInfo method in methods)
                        {
                            optAlgMethods.Add(method.Name, method);
                        }
                        break;
                    }
                }
            }
            LoadParamsInfo();
        }
        private void LoadParamsInfo()
        {
            MethodInfo m = this.optAlgMethods["get_ParamsInfo"];
            //this.ParamsInfo = (this.paramInfoType)m.Invoke(this.optAlgObj, null);
        }
    }
}
