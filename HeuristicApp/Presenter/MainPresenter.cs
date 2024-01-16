using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeuristicApp.Presenter
{
    internal class MainPresenter
    {
        private View.Form1 _view;
        private Model.MainModel _model;

        //// events
        //public event Action<string> _AddAlgorithmAction;

        public MainPresenter(View.Form1 view, Model.MainModel model)
        {
            _view = view;
            _model = model;

            _view.AddAlgorithm += _AddAlgorithm;
            _model.AddAlgorithm += _AddAlgorithmToBox;

            _view.AddFitFunc += _GetFitnessesFromDll;
            _model.AddFitFunc += _AddFitFuncToBox;

            _view.SelectAlgorithm += _SelectAlgorithm;
            _view.SelectFitFunc += _SelectFitFunc;
            //_AddAlgorithmAction += model.AddAlgToDict;
        }

        private void _AddAlgorithm(string path)
        {
            this._model.AddAlgToDict(path);
            //this._view.ShowMessage(":)");
            //this._AddAlgorithmToBox(algName);
        }
        private void _AddAlgorithmToBox(string algName)
        {
            this._view.AddAlgorithmToBox(algName);
        }
        private void _GetFitnessesFromDll(string path)
        {
            this._model.GetFitnessesFromDll(path);
        }
        private void _AddFitFuncToBox(string fitFuncName)
        {
            this._view.AddFitFuncToBox(fitFuncName);
        }
        private void _SelectAlgorithm(string algName)
        {
            this._view.AddToLayoutPanel(this._model.GetAlgInfo(algName));

        }
        private void _SelectFitFunc(string fitFuncName)
        {
            this._view.ShowMessage("TODO: wyswietlic paraemtry funkcji");
        }
    }
}
