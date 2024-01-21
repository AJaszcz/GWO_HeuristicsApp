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

            _view.AddFitFunc += _AddFitFunc;
            _model.AddFitFunc += _AddFitFuncToBox;

            _view.SelectAlgorithm += _SelectAlgorithm;
            _view.SelectFitFunc += _SelectFitFunc;

            _view.SaveAlgParams += _SaveAlgParams;
            // TODO: SaveFitFuncParams

            _view.RunAlgTest += _RunAlgTest;
        }
        // idk jak to inczaej zrobic - workaround
        public string prevSelected;

        private void _AddAlgorithm(string path)
        {
            this._model.AddAlgToDict(path);
        }
        private void _AddAlgorithmToBox(string algName)
        {
            this._view.AddAlgorithmToBox(algName);
        }
        private void _AddFitFunc(string path)
        {
            this._model.AddFitFuncToDict(path);
        }
        private void _AddFitFuncToBox(string fitFuncName)
        {
            this._view.AddFitFuncToBox(fitFuncName);
        }
        private void _SelectAlgorithm(string algName)
        {
            // Jesli jest w fitFuncBoxie jest wybrana jakas funkcja kosztu
            string selectedFitFunc = this._view.GetSelectedFitFuncName();
            if (selectedFitFunc != null)
            {
                // jesli przelaczamy miedzy fitBoxem a algBoxem
                //    _SaveFitFuncParams(selectedFitFunc); TODO: implementacja
                this._view.ClearLayoutPanel();
                this._view.ClearFitFuncSelect();
            }
            else
            {
                // jesli przelaczamy w algBox
                _SaveAlgParams(prevSelected);
            }

            // refresh layout
            this.prevSelected = algName;
            this._view.ClearLayoutPanel();
            this._view.AddToLayoutPanel(this._model.GetAlgInfo(algName), this._model.GetAlgParams(algName));
        }
        private void _SelectFitFunc(string fitFuncName)
        {
            // Jesli jest w algBoxie jest wybrany jakis algorytm
            string selectedAlg = this._view.GetSelectedAlgName();
            if (selectedAlg != null)
            {
                // jesli przelaczamy miedzy algBoxem a fitBoxem
                _SaveAlgParams(selectedAlg);
                this._view.ClearLayoutPanel();
                this._view.ClearAlgSelect();
            }
            else
            {
                // jesli przelaczamy w fitBox
                //    _SaveFitFuncParams(selectedFitFunc); TODO: implementacja
            }

            // refresh layout
            this.prevSelected = fitFuncName;
            this._view.ClearLayoutPanel();
            // TODO: Wyswietlic paraemtry funkcji
            //this._view.ShowMessage("TODO: wyswietlic paraemtry funkcji");
            this._view.AddFitFuncToLayoutPanel(this._model.GetFitFuncInfo(fitFuncName));
        }
        private void _SaveAlgParams(string algName)
        {
            double[] t = this._view.GetLayoutPanelParameters();
            //this._view.ShowMessage("Lost focus: SAVING NOW");
            if (t.Length > 0)
            {
                this._model.SaveAlgParameters(algName, t);
            }

        }
        private void _RunAlgTest(string algName, string[] fitFuncNames)
        {
            this._model.runAlgTest(algName, fitFuncNames);
        }
    }
}
