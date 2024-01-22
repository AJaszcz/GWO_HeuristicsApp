using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWO
{
    public interface IStateReader
    {
        // Metoda wczytuj ąca z pliku stan algorytmu (w odpowiednim formacie ).
        // Stan algorytmu : numer iteracji , liczba wywo łań funkcji celu ,
        // populacja wraz z warto ścią funkcji dopasowania
        StateReader LoadFromFileStateOfAlghoritm(string path);
    }
}
