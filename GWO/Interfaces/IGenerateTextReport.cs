using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWO
{
    public interface IGenerateTextReport
    {
        // Tworzy raport w postaci łań cucha znak ów
        // w raporcie powinny znale źć się informacje o:
        // najlepszym osobniku wraz z warto ścią funkcji celu ,
        // liczbie wywo łań funkcji celu ,
        // parametrach algorytmu
        string ReportString { get; set; }   // Od wersji 8.0
    }
}
