using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HeurestyczneFront
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            /*CheckBox a = new CheckBox();
            a.Text = "Jedeeen";
            algorytmyPanel.SetCellPosition(new CheckBox(), new TableLayoutPanelCellPosition(0, 0));
            algorytmyPanel.Controls.Add(a);
            algorytmyPanel.SetCellPosition(new CheckBox(), new TableLayoutPanelCellPosition(0, 1));
            a.Text = "Dwa";
            algorytmyPanel.Controls.Add(a);*/
        }

        private void algorytmy_list_SelectedIndexChanged(object sender, EventArgs e)
        {

            // info_panel.SetCellPosition(new Te,new TableLayoutPanelCellPosition(0, 0));


        }

        private void testy(object sender, EventArgs e)
        {
            TextBox pomoc = new TextBox();
            info_panel.SetCellPosition(pomoc, new TableLayoutPanelCellPosition(0, 0));

        }

        private void algorytmy_list_ItemCheck(object sender, ItemCheckEventArgs e)
        {

            
        }

        private void algorytmy_list_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            // Pobierz indeks zaznaczonego elementu
            int selectedIndex = algorytmy_list.IndexFromPoint(e.Location);

            if (selectedIndex != ListBox.NoMatches)
            {
                string selectedText = algorytmy_list.Items[selectedIndex].ToString();
                TextBox nazwa = new TextBox();
                info_panel.SetCellPosition(new TextBox(), new TableLayoutPanelCellPosition(0, info_panel.RowCount));
                nazwa.Text = selectedText;
                info_panel.SetCellPosition(new TextBox(), new TableLayoutPanelCellPosition(1, info_panel.RowCount));
                TextBox wartosci = new TextBox();
                wartosci.Text = "Wartosci";
                info_panel.Controls.Add(wartosci);
            }
        }

        private void algorytmyPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
