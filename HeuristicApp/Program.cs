using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HeuristicApp
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var view = new View.Form1();
            var model = new Model.MainModel();
            var presenter = new Presenter.MainPresenter(view, model);
            model.LoadBaseAlgorithms();
            model.LoadBaseFitFuncs();
            Application.Run(view);
        }
    }
}
