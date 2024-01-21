using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace HeuristicApp.View
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.fitFuncBox = new System.Windows.Forms.CheckedListBox();
            this.runButton = new System.Windows.Forms.Button();
            this.RunAlgTestButton = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.algBox = new System.Windows.Forms.CheckedListBox();
            this.fitFuncLabel = new System.Windows.Forms.Label();
            this.algLabel = new System.Windows.Forms.Label();
            this.algAddButton = new System.Windows.Forms.Button();
            this.fitFuncAddButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.numericOnLayout = new System.Windows.Forms.NumericUpDown();
            this.SuspendLayout();
            // 
            // fitFuncBox
            // 
            this.fitFuncBox.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.fitFuncBox.FormattingEnabled = true;
            this.fitFuncBox.Location = new System.Drawing.Point(1048, 71);
            this.fitFuncBox.Name = "fitFuncBox";
            this.fitFuncBox.Size = new System.Drawing.Size(308, 395);
            this.fitFuncBox.TabIndex = 1;
            this.fitFuncBox.SelectedIndexChanged += new System.EventHandler(this.fitFuncBox_SelectedIndexChanged);
            // 
            // runButton
            // 
            this.runButton.Location = new System.Drawing.Point(1362, 71);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(122, 37);
            this.runButton.TabIndex = 2;
            this.runButton.Text = "Run";
            this.runButton.UseVisualStyleBackColor = true;
            // 
            // RunAlgTestButton
            // 
            this.RunAlgTestButton.Location = new System.Drawing.Point(1362, 114);
            this.RunAlgTestButton.Name = "RunAlgTestButton";
            this.RunAlgTestButton.Size = new System.Drawing.Size(122, 37);
            this.RunAlgTestButton.TabIndex = 3;
            this.RunAlgTestButton.Text = "Run Alg Test";
            this.RunAlgTestButton.UseVisualStyleBackColor = true;
            this.RunAlgTestButton.Click += new System.EventHandler(this.RunAlgTestButton_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(1362, 157);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(122, 37);
            this.button3.TabIndex = 4;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // algBox
            // 
            this.algBox.FormattingEnabled = true;
            this.algBox.Location = new System.Drawing.Point(734, 71);
            this.algBox.Name = "algBox";
            this.algBox.Size = new System.Drawing.Size(308, 395);
            this.algBox.TabIndex = 5;
            this.algBox.SelectedIndexChanged += new System.EventHandler(this.algBox_SelectedIndexChanged);
            // 
            // fitFuncLabel
            // 
            this.fitFuncLabel.AutoSize = true;
            this.fitFuncLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fitFuncLabel.Location = new System.Drawing.Point(1041, 29);
            this.fitFuncLabel.Name = "fitFuncLabel";
            this.fitFuncLabel.Size = new System.Drawing.Size(274, 38);
            this.fitFuncLabel.TabIndex = 6;
            this.fitFuncLabel.Text = "Fitness Functions";
            this.fitFuncLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // algLabel
            // 
            this.algLabel.AutoSize = true;
            this.algLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.algLabel.Location = new System.Drawing.Point(727, 29);
            this.algLabel.Name = "algLabel";
            this.algLabel.Size = new System.Drawing.Size(171, 38);
            this.algLabel.TabIndex = 7;
            this.algLabel.Text = "Algorithms";
            this.algLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // algAddButton
            // 
            this.algAddButton.Location = new System.Drawing.Point(734, 472);
            this.algAddButton.Name = "algAddButton";
            this.algAddButton.Size = new System.Drawing.Size(308, 39);
            this.algAddButton.TabIndex = 8;
            this.algAddButton.Text = "Add algorithm";
            this.algAddButton.UseVisualStyleBackColor = true;
            this.algAddButton.Click += new System.EventHandler(this.algAddButton_Click);
            // 
            // fitFuncAddButton
            // 
            this.fitFuncAddButton.Location = new System.Drawing.Point(1048, 472);
            this.fitFuncAddButton.Name = "fitFuncAddButton";
            this.fitFuncAddButton.Size = new System.Drawing.Size(308, 39);
            this.fitFuncAddButton.TabIndex = 9;
            this.fitFuncAddButton.Text = "Add fitness function";
            this.fitFuncAddButton.UseVisualStyleBackColor = true;
            this.fitFuncAddButton.Click += new System.EventHandler(this.fitFuncAddButton_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 71);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 10;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(716, 440);
            this.tableLayoutPanel1.TabIndex = 10;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1506, 531);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.fitFuncAddButton);
            this.Controls.Add(this.algAddButton);
            this.Controls.Add(this.algLabel);
            this.Controls.Add(this.fitFuncLabel);
            this.Controls.Add(this.algBox);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.RunAlgTestButton);
            this.Controls.Add(this.runButton);
            this.Controls.Add(this.fitFuncBox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();
            //
            // numericOnLayout
            //
            this.numericOnLayout.ValueChanged += new System.EventHandler(this.numericOnLayout_VlaueChanged);

        }

        #endregion
        private System.Windows.Forms.CheckedListBox fitFuncBox;
        private System.Windows.Forms.Button runButton;
        private System.Windows.Forms.Button RunAlgTestButton;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.CheckedListBox algBox;
        private System.Windows.Forms.Label fitFuncLabel;
        private System.Windows.Forms.Label algLabel;
        private System.Windows.Forms.Button algAddButton;
        private System.Windows.Forms.Button fitFuncAddButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.NumericUpDown numericOnLayout;

        public void AddAlgorithmToBox(string algName)
        {
            this.algBox.Items.Add(algName);
        }
        public void AddFitFuncToBox(string fitFuncName)
        {
            this.fitFuncBox.Items.Add(fitFuncName);
        }
        public void ClearLayoutPanel()
        {
            tableLayoutPanel1.Controls.Clear();
        }
        public void AddToLayoutPanel(object[,] arguments, double[] parameters)
        {
            ClearLayoutPanel();
            for (int i = 0; i < arguments.GetLength(0); i++)
            {
                tableLayoutPanel1.Controls.Add(new Label() { Name = (string)arguments[i, 0], Text = arguments[i,0].ToString() }, 0, i);
                tableLayoutPanel1.Controls.Add(new Label() { Text = arguments[i, 1].ToString() }, 1, i);
                tableLayoutPanel1.Controls.Add(new Label() { Text = System.String.Format("{0} - {1}", arguments[i, 2], arguments[i, 3]) }, 2, i);
                tableLayoutPanel1.Controls.Add(new NumericUpDown() {Value = (decimal)parameters[i]}, 3, i);
            }
        }
        public void AddFitFuncToLayoutPanel(object[] arguments)
        {
            ClearLayoutPanel();
            tableLayoutPanel1.Controls.Add(new Label() { Text = arguments[0].ToString() }, 0, 0);
            tableLayoutPanel1.Controls.Add(new Label() { Text = arguments[0].ToString() }, 1, 0);
            tableLayoutPanel1.Controls.Add(new Label() { Text = "dims:"}, 2, 0);
            //tableLayoutPanel1.Controls.Add(new NumericUpDown() { Name = "n", Value = (decimal)(int)arguments[2] , ReadOnly=(bool)arguments[3] }, 3, 0);

            tableLayoutPanel1.Controls.Add(this.numericOnLayout, 3, 0);
            this.numericOnLayout.ReadOnly = (bool)arguments[3];

            tableLayoutPanel1.Controls.Add(new Label() { Text = "params" }, 0, 1);
            tableLayoutPanel1.Controls.Add(new Label() { Text = "lb" }, 1, 1);
            tableLayoutPanel1.Controls.Add(new Label() { Text = "ub" }, 2, 1);

            this.numericOnLayout.Value = (decimal)(int)arguments[2];
            //for (int i = 0; i < (int)arguments[2]; i++)
            //{
            //    AddDomainToLayoutPanel(i);
            //}
        }
        //public void DimensionalityChanged(int i)
        //{
            
        //}
        public void AddDomainToLayoutPanel(int i)
        {
            tableLayoutPanel1.Controls.Add(new Label() { Text = String.Format("x{0}", i) }, 0, i + 2);
            tableLayoutPanel1.Controls.Add(new NumericUpDown() { Value = 0 }, 1, i + 2);
            tableLayoutPanel1.Controls.Add(new NumericUpDown() { Value = 0 }, 2, i + 2);
        }
        public void RemoveDomainFromLayoutPanel(int i)
        {
            tableLayoutPanel1.GetControlFromPosition(0, i+1).Dispose();
            tableLayoutPanel1.GetControlFromPosition(1, i+1).Dispose();
            tableLayoutPanel1.GetControlFromPosition(2, i+1).Dispose();
        }
        public double[] GetLayoutPanelParameters()
        {
            TableLayoutControlCollection controls = tableLayoutPanel1.Controls;
            List <double> objList = new List <double>();
            for (int i = 0; i < controls.Count; i++)
            {
                if (controls[i] is NumericUpDown)
                {
                    NumericUpDown numericUpDown = (NumericUpDown)controls[i];
                    objList.Add((double)numericUpDown.Value);
                }
            }
            return objList.ToArray();
        }
        public void ClearAlgSelect()
        {
            this.algBox.ClearSelected();
        }
        public void ClearFitFuncSelect()
        {
            this.fitFuncBox.ClearSelected();
        }
        public string GetSelectedAlgName()
        {
            return this.algBox.SelectedItem as string;
        }
        public string GetSelectedFitFuncName()
        {
            return this.fitFuncBox.SelectedItem as string;
        }
    }
}