using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWO
{
    public class GenerateTextReport : IGenerateTextReport
    {
        public string ReportString { get; set; }
        public GenerateTextReport() 
        {
            ReportString = "Nothing solved yet";
        }

        public GenerateTextReport(double FBest, double[] XBest)
        {
            string res = "";
            res += "Best value found: " + FBest.ToString() + "\n";
            res += "Position of this value: \n";
            int i = 1;
            foreach(double x in XBest)
            {
                res += "x" + i.ToString() + ": " + x.ToString() + "\n";
                i++;
            }
            ReportString = res;
        }
    }
}
