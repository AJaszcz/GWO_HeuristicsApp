using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HeuristicApp.View
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        // events
        public event Action<string> AddAlgorithm;
        public event Action<string> AddFitFunc;

        public event Action<string> SelectAlgorithm;
        public event Action<string> SelectFitFunc;
        //public Func<string, string[]> AddAlgorithms;

        public void ShowMessage(string message) => MessageBox.Show(message);

        private void algAddButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select dll with algorithm";
            openFileDialog.Filter = "Dll files (*.dll)|*.dll";

            // If file was selected
            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Let presenter check if path is valid, load algorithm in model and refresh list in view
                AddAlgorithm?.Invoke(openFileDialog.FileName);
            }
        }

        private void fitFuncAddButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select dll with fitness functions";
            openFileDialog.Filter = "Dll files (*.dll)|*.dll";

            // If file was selected
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Let presenter check if path is valid, load algorithm in model and refresh list in view
                AddFitFunc?.Invoke(openFileDialog.FileName);
            }
        }

        private void algBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string name = this.algBox.SelectedItem as string;
            SelectAlgorithm?.Invoke(name);
        }

        private void fitFuncBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string name = this.fitFuncBox.SelectedItem as string;
            SelectFitFunc?.Invoke(name);
        }
    }
}
