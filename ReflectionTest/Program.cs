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
            //string dllPath = "C:\\Users\\antek\\source\\repos\\ReflectionTest\\algorithms\\TestLibrary2.dll";
            //string dllPath = "C:\\Users\\antek\\source\\repos\\TestLibrary2\\bin\\Debug\\TestLibrary2.dll";
            string dllPath = "C:\\Users\\antek\\source\\repos\\GWO\\obj\\Debug\\GWO.dll";
            // Load the DLL
            Assembly assembly = Assembly.LoadFrom(dllPath);

            // Get all types in the assembly
            Type[] types = assembly.GetTypes();
            Dictionary<string, Type> dict = new Dictionary<string, Type>();


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
            // Assuming you know the type name
            //string typeName = types[0].Name.ToString();

            //// Get the type
            //Type classType = types[0];  //typ klasy (?)

            //// Create an instance of the class
            //object classInstance = Activator.CreateInstance(classType);

            //MethodInfo[] methods = types[0].GetMethods(); //metody naszej klasy Class1

            ////// Assuming you know the method name
            ////string methodName = methods[0].Name;

            ////// Get the method
            ////MethodInfo method = classType.GetMethod(methodName);

            //MethodInfo method = methods[0];


            //// Invoke the method on the class instance
            //method.Invoke(classInstance, null);
            Type optAlg = dict["GWO.GWO"];
            Type fitFunc = dict["fitnessFunction"];
            object classInstance = Activator.CreateInstance(optAlg);
            MethodInfo fitFuncInfo = typeof(Program).GetMethod("CalculateFitness"); //Info o metodzie (konkretnej funkcji kosztu)
            var fitnessDelegate = Delegate.CreateDelegate(fitFunc, null, fitFuncInfo);



            MethodInfo[] methods = optAlg.GetMethods();
            Console.WriteLine("\nUsing method: " + methods[16].Name);
            double[,] multiDimensionalArray = { { -100, -100, -100 }, { 100, 100, 100 } };
            double[] parameters = {100, 100};
            object[] allParameters = { fitnessDelegate, multiDimensionalArray, parameters };

            methods[16].Invoke(classInstance,
                allParameters
            );

            Console.Read();

        }
    }
}
