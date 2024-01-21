using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWO
{
    public interface IStateWriter
    {
        // Metoda zapisuj ąca do pliku tekstowego stan algorytmu (w odpowiednim formacie
        void SaveToFileStateOfAlghoritm(string path);
        // Stan algorytmu : numer iteracji , liczba wywo łań funkcji celu ,
        // populacja wraz z warto ścią funkcji dopasowania
    }
}
