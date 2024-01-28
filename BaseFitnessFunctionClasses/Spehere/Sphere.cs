using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spehere
{
    public class Sphere
    {
        public string name;
        public string description;
        public int n;
        public bool fixedDimensionality;
        public double[,] domain;
        public static double SphereFunction(params double[] args)
        {
            // Your fitness calculation logic here
            double result = 0;
            foreach (var arg in args)
            {
                result += arg * arg;
            }
            return result;
        }
        public Sphere()
        {
            name = "Sphere";
            description = "This is Sphere function";
            n = 0;
            fixedDimensionality = false;
            //domain = new double[,] { { -5.0, -5.0 }, { 5.0, 5.0 } };
        }
    }
}
