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
        static void Main(string[] args)
        {
            // Specify the path to your DLL
            string dllPath = "C:\\Users\\antek\\source\\GWO_HeuristicsApp\\GWO\\obj\\Debug\\GWO.dll"; // Full path to dll file
            // Load the DLL
            Assembly assembly = Assembly.LoadFrom(dllPath);

            // Get all types in the assembly
            Type[] types = assembly.GetTypes();
            Dictionary<string, Type> dict = new Dictionary<string, Type>();

            // Prints all fields and methods for each type extracted
            foreach (Type type in types)
            {
                dict.Add(type.FullName, type);
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

            Type optAlg = dict["GWO.GWO"]; // GWO class Type
            Type fitFunc = dict["fitnessFunction"]; // Delegeate Type

            object classInstance = Activator.CreateInstance(optAlg); // Creates object instance of GWO class
            MethodInfo fitFuncInfo = typeof(Program).GetMethod("CalculateFitness"); // Gathers method info about 
            var fitnessDelegate = Delegate.CreateDelegate(fitFunc, null, fitFuncInfo); // Creates instance of a delegate



            MethodInfo[] methods = optAlg.GetMethods(); // Gather GWO class methods
            Console.WriteLine("\nUsing method: " + methods[16].Name); // wip: turn this into dictionary, instead of array
            double[,] multiDimensionalArray = { { -100, -100, -100 }, { 100, 100, 100 } }; // 
            double[] parameters = {100, 100};
            object[] allParameters = { fitnessDelegate, multiDimensionalArray, parameters };

            methods[16].Invoke(classInstance,
                allParameters
            );

            Console.Read();

        }
    }
}
