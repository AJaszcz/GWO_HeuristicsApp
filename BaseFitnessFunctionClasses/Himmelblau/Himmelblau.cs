using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Himmelblau
{
    public class Himmelblau
    {
        //public string name = "Himmelblau";
        //public string description = "This is Himmelblau function";
        //public int n = 2;
        //public bool fixedDimensionality = true;
        //public double[,] domain = { { -5.0, -5.0 }, { 5.0, 5.0 } };
        public string name;
        public string description;
        public int n;
        public bool fixedDimensionality;
        public double[,] domain;
        public static double HimmelblauFunction(params double[] args)
        {
            if (args.Length > 2)
            {
                throw new Exception("args length > 2");
            }
            double x = args[0];
            double y = args[1];
            return Math.Pow((x * x + y - 11), 2) + Math.Pow((x + y * y - 7), 2);
        }
        public Himmelblau(){
            name = "Himmelblau";
            description = "This is Himmelblau function";
            n = 2;
            fixedDimensionality = true;
            domain = new double[,] { { -5.0, -5.0 }, { 5.0, 5.0 } };
        }
    }
}
