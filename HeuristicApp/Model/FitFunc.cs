using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HeuristicApp.Model
{
    internal class FitFunc
    {
        public string name;
        public string describtion;
        public double[,] domain; // { {lb, lb, lb}, {ub, ub, ub} }
        public int n;
        public bool fixedDimensionality;
        public MethodInfo fitFuncMethod;
        public object fitFuncObj;
        public Type fitFuncType;
        public FitFunc(string dllPath)
        {
            GetFitnessesFromDll(dllPath);
        }
        public void GetFitnessesFromDll(string dllPath)
        {
            Assembly assembly = Assembly.LoadFrom(dllPath);

            // powinien byc tylko 1 typ (jesli nie, kolejne zostaja nadpisane)
            //foreach (Type type in assembly.GetTypes())
            //{
            //    LoadFitFunc(type);
            //}
            LoadFitFunc(assembly.GetTypes()[0]); // tylko pierwszy (czyli cala klasa) zostaje wczytana
        }
        public void LoadFitFunc(Type type)
        {
            this.fitFuncType = type;
            this.fitFuncObj = Activator.CreateInstance(type);
            foreach (FieldInfo f in type.GetFields())
            {
                if (f.Name == "name")
                    this.name = (string)f.GetValue(fitFuncObj);
                if (f.Name == "describtion")
                    this.describtion = (string)f.GetValue(fitFuncObj);
                if (f.Name == "fixedDimensionality")
                    this.fixedDimensionality = (bool)f.GetValue(fitFuncObj);
                if (f.Name == "n")
                {
                    // idk
                    object n = f.GetValue(fitFuncObj);
                    if (n == null)
                    {
                        this.n = 1;
                    }
                    else
                    {
                        this.n = (int)n;
                    }

                }
                if (f.Name == "domain")
                {
                    object domain = f.GetValue(fitFuncObj);
                    if(domain == null)
                    {
                        this.domain = new double[2, this.n];
                    }
                    else
                    {
                        this.domain = (double[,])domain;
                    }
                }

            }
            foreach (MethodInfo m in type.GetMethods())
            {
                this.AddFitFuncMethod(m);
            }
        }
        public void AddFitFuncMethod(MethodInfo m)
        {
            // troche na okolo ale dziala
            string[] standard = { "Equals", "GetHashCode", "GetType", "ToString" };
            if (!standard.Contains(m.Name) && m.Name.Contains("Function"))
            {
                this.fitFuncMethod = m;
            }
        }
    }
}
