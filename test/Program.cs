using GWO;

GWO.GWO gwo = new GWO.GWO();
gwo.SetDummyVal();
string text = gwo.StringReportGenerator.ReportString;
Console.WriteLine(text);
gwo.PdfReportGenerator.GenerateReport(@"C:\temp\text.html");