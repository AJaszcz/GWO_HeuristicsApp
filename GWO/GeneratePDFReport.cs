using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWO
{
    public class GeneratePDFReport : IGeneratePDFReport
    {
        public string StringReport { get; set; }
        public GeneratePDFReport()
        {
            this.StringReport = "None";
        }
        public GeneratePDFReport(string ReportString) 
        {
            this.StringReport = ReportString;
            this.StringReport = this.StringReport.Replace("\n", "<br>");
        }
        public void GenerateReport(string path)
        {
            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Create))
                {
                    using (StreamWriter w = new StreamWriter(fs, Encoding.UTF8))
                    {
                        w.WriteLine("<!DOCTYPE html>\r\n<html lang=\"en\">\r\n<head>\r\n    <meta charset=\"UTF-8\">\r\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\r\n    <title>Wynik algorytmu GWO</title>\r\n</head>\r\n<body>\r\n    <p>");
                        w.WriteLine(StringReport);
                        w.WriteLine("</p>\r\n</body>\r\n</html>");
                    }
                }
                Console.WriteLine("HTML File Created succesfully");
            }
            catch(IOException e) {
                Console.WriteLine($"Error creating HTML file: {e.Message}");
            }
        }
    }
}
