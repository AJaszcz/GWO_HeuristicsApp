using GWO;

GWO.GWO Alg = new GWO.GWO();
Sphere func = new Sphere();
double[,] domain = new double[2, 5];
for (int i = 0; i < 5; i++)
{
    domain[0, i] = -1000000;
    domain[1, i] = 1000000;
}
for (int i = 0; i < 5; i++)
{
    GWO.GWO alg = new GWO.GWO();
    double[] parameters = new double[] { 50, 5, 70, 0, 1, 0, 1 };
    alg.Solve(Sphere.SphereFunction, domain, parameters);
    Console.WriteLine(String.Join(" ; ", alg.XBest));
    Console.WriteLine(alg.FBest);
}

public interface IFitnessFunction
{
    double CalculateFitnesse(double[] position);
}
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