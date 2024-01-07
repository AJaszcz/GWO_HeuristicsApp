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

namespace ReflectionTest
{
    internal class Program
    {
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
        static void Main(string[] args)
        {
            // Specify the path to your DLL
            ///// TODO: implement defeault path to find dlls in and create logic for loading from many different assemblies //////
            string dllPath = "C:\\Users\\antek\\source\\GWO_HeuristicsApp\\GWO\\obj\\Debug\\GWO.dll"; // Full path to dll file
            // Load the DLL
            Assembly assembly = Assembly.LoadFrom(dllPath);

            
            // Get all types in the assembly
            Type[] types = assembly.GetTypes();
            // Create dictionary for the extracted Types
            Dictionary<string, Type> typesDict = new Dictionary<string, Type>();
            foreach (Type type in types)
            {
                // Adds to dict 
                typesDict.Add(type.FullName, type);

                // Prints all fields and methods for each type extracted
                Console.WriteLine("Type: " + type.FullName);
                FieldInfo[] field_arr = type.GetFields();
                foreach (FieldInfo field in field_arr)
                {
                    Console.WriteLine($"  Field: {field.Name}");
                }
                // Get all methods in the type
                MethodInfo[] methods_arr = type.GetMethods();
                foreach (MethodInfo m in methods_arr)
                {
                    Console.WriteLine("  Method: " + m.Name);
                }
            }

            // Create instances of specific types (example, to be changed later)
            Type optAlg = typesDict["GWO.GWO"]; // GWO class Type
            Type fitFunc = typesDict["fitnessFunction"]; // Delegeate Type

            // Create delegate with fitness function
            object classInstance = Activator.CreateInstance(optAlg); // Creates object instance of GWO class

            // Choose fit function and create delegate
            ////// TODO: implement loading functions from another dll and forwarding them to presenter/controller //////

            // Examples:
            // MethodInfo fitFuncInfo = typeof(Program).GetMethod("CalculateFitness"); // Gathers method info about 
            MethodInfo fitFuncInfo = typeof(Program).GetMethod("HimmelblauFunction"); // Gathers method info about

            var fitnessDelegate = Delegate.CreateDelegate(fitFunc, null, fitFuncInfo); // Creates instance of a delegate using obtained mathod (fitness fucntion) info

            // Gather methods from the algorithm class and put into dictionary
            MethodInfo[] methods = optAlg.GetMethods(); // Gather GWO class methods
            Dictionary<string, MethodInfo> optAlgMethodsDict = new Dictionary<string, MethodInfo>();
            foreach (MethodInfo method in methods)
            {
                optAlgMethodsDict.Add(method.Name, method);
            }

            // Choose 'Solve' method and invoke it
            MethodInfo currentSovle = optAlgMethodsDict["Solve"];
            Console.WriteLine("\nUsing method: " + currentSovle.Name);

            ////// TODO: Get parameters from the user (through presenter/controller) //////
            // Create array of parameters to be sent to Solve
            //double[,] multiDimensionalArray = { { -100, -100, -100 }, { 100, 100, 100 } }; // 
            double[,] multiDimensionalArray = { { -5, -5 }, { 5, 5 } }; // 
            double[] parameters = {100, 100};
            object[] allParameters = { fitnessDelegate, multiDimensionalArray, parameters };

            // Ivoke 'Solve' method
            currentSovle.Invoke(classInstance,
                allParameters
            );

            Console.Read();
        }
    }
}
