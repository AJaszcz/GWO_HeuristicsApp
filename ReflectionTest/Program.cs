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
using static ReflectionTest.Program;

namespace ReflectionTest
{
    internal class Program
    {
        // Example fitness functions:
        public static double CalculateFitness(params double[] args)
        {
            // Your fitness calculation logic here
            double result = 0;
            foreach (var arg in args)
            {
                result += arg*arg;
            }
            return result;
        }
        public static double HimmelblauFunction(params double[] args)
        {
            if(args.Length > 2)
            {
                throw new Exception("args length > 2");
            }
            double x = args[0];
            double y = args[1];
            return Math.Pow((x * x + y - 11), 2) + Math.Pow((x + y * y - 7), 2);
        }

        // 
        public static void GetTypesFromDll(string dllPath, ref Dictionary<string, Type> dict) {
            Assembly assembly = Assembly.LoadFrom(dllPath);
            Console.WriteLine("assembly name: " + assembly.GetName());
            foreach (Type type in assembly.GetTypes())
            {
                if (dict.ContainsKey(type.FullName))
                    continue;
                // Adds to dict 
                dict.Add(type.FullName, type);

                //// Prints all fields and methods for each type extracted
                //Console.WriteLine("Type: " + type.FullName);
                //FieldInfo[] field_arr = type.GetFields();
                //foreach (FieldInfo field in field_arr)
                //{
                //    Console.WriteLine($"  Field: {field.Name}");
                //}
                //// Get all methods in the type
                //MethodInfo[] methods_arr = type.GetMethods();
                //foreach (MethodInfo m in methods_arr)
                //{
                //    Console.WriteLine("  Method: " + m.Name);
                //}
            }
        }
        public static void GetFitnessesFromDll(string dllPath, ref Dictionary<string, MethodInfo> dict) {
            Assembly assembly = Assembly.LoadFrom(dllPath);
            foreach (Type type in assembly.GetTypes())
            {
                foreach (MethodInfo m in type.GetMethods())
                {
                    //Console.WriteLine("  Found Fitness: " + m.Name);
                    dict.Add(m.Name, m);
                }
            }
        }
        public class OptAlg {
            public string name;
            public Type optAlgType;
            public Type fitFuncType;
            public object optAlgObj;
            public Dictionary<string, MethodInfo> optAlgMethods;

        public OptAlg(string dllPath){
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
                    // Get all methods in the type
                    MethodInfo[] methods_arr = type.GetMethods();
                    foreach (MethodInfo m in methods_arr)
                    {
                        // Warunek, ze to faktycznie algorytm TODO: To jest okropne, rozwiazac pozniej
                        if (m.Name == "Solve" && type.GetTypeInfo().ToString()!="IOptimizationAlgorithm")
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
            }

        }
        //public static Dictionary<string, MethodInfo> DictOfTypesMethods(string typeName)
        //{
        //    Type optAlg = typesDict["GWO.GWO"];
        //    MethodInfo[] methods = optAlg.GetMethods(); // Gather GWO class methods
        //    Dictionary<string, MethodInfo> optAlgMethodsDict = new Dictionary<string, MethodInfo>();
        //    foreach (MethodInfo method in methods)
        //    {
        //        optAlgMethodsDict.Add(method.Name, method);
        //    }
        //    return optAlgMethodsDict;
        //}
        static void Main(string[] args)
        {
            // Specify the path to your DLL
            ///// TODO: implement defeault path to find dlls in and create logic for loading from many different assemblies //////
            string dllPath = "C:\\Users\\antek\\source\\repos\\GWO_HeuristicsApp\\GWO\\obj\\Debug\\GWO.dll"; // Full path to dll file
            //string dllPath2 = "C:\\Users\\antek\\source\\repos\\GWO_HeuristicsApp\\ReflectionTest\\algorithms\\TestLibrary2.dll"; // Full path to dll file
            string dllPath2 = "C:\\Users\\antek\\source\\repos\\TestLibrary2\\obj\\Debug\\TestLibrary2.dll";
            
            // Mock list with dll paths
            string[] dllPathsArr = {dllPath, dllPath2};

            // Load algorithm objects into dictionary
            Dictionary<string, OptAlg> algDict = new Dictionary<string, OptAlg>();
            foreach (string path in dllPathsArr )
            {
                OptAlg alg = new OptAlg(path);
                algDict.Add(alg.name, alg);
            }

            ////// Wybranie funkcji i algorytmu; wywołanie //////
            
            MethodInfo fitFuncInfo = typeof(Program).GetMethod("HimmelblauFunction"); // Gathers method info about
            var fitnessDelegate = Delegate.CreateDelegate(algDict["GWO"].fitFuncType, null, fitFuncInfo); // Creates instance of a delegate using obtained mathod (fitness fucntion) info

            // Choose 'Solve' method and invoke it
            MethodInfo currentSovle = algDict["GWO"].optAlgMethods["Solve"];
            Console.WriteLine("Using method: " + currentSovle.Name);

            ////// TODO: Get parameters from the user (through presenter/controller) //////
            // Create array of parameters to be sent to Solve
            //double[,] multiDimensionalArray = { { -100, -100, -100 }, { 100, 100, 100 } }; // 
            double[,] multiDimensionalArray = { { -5, -5 }, { 5, 5 } }; // 
            double[] parameters = {100, 100};
            object[] allParameters = { fitnessDelegate, multiDimensionalArray, parameters };

            // Invoke 'Solve' method
            currentSovle.Invoke(algDict["GWO"].optAlgObj,
                allParameters
            );

            Console.Read();
        }
    }
}
