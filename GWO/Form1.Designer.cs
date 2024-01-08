namespace HeurestyczneFront
{
    partial class Form1
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.algorytm_button = new System.Windows.Forms.Button();
            this.funkcja_button = new System.Windows.Forms.Button();
            this.rozwiaz_button = new System.Windows.Forms.Button();
            this.pdf_button = new System.Windows.Forms.Button();
            this.stop_button = new System.Windows.Forms.Button();
            this.wznow_button = new System.Windows.Forms.Button();
            this.save_button = new System.Windows.Forms.Button();
            this.algorytmy_list = new System.Windows.Forms.CheckedListBox();
            this.funkcje_lista = new System.Windows.Forms.CheckedListBox();
            this.info_panel = new System.Windows.Forms.TableLayoutPanel();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // algorytm_button
            // 
            this.algorytm_button.Location = new System.Drawing.Point(323, 29);
            this.algorytm_button.Name = "algorytm_button";
            this.algorytm_button.Size = new System.Drawing.Size(143, 60);
            this.algorytm_button.TabIndex = 0;
            this.algorytm_button.Text = "Dodaj algorytm";
            this.algorytm_button.UseVisualStyleBackColor = true;
            this.algorytm_button.Click += new System.EventHandler(this.button1_Click);
            // 
            // funkcja_button
            // 
            this.funkcja_button.Location = new System.Drawing.Point(701, 31);
            this.funkcja_button.Name = "funkcja_button";
            this.funkcja_button.Size = new System.Drawing.Size(115, 58);
            this.funkcja_button.TabIndex = 1;
            this.funkcja_button.Text = "Dodaj funkcje";
            this.funkcja_button.UseVisualStyleBackColor = true;
            this.funkcja_button.Click += new System.EventHandler(this.button2_Click);
            // 
            // rozwiaz_button
            // 
            this.rozwiaz_button.Location = new System.Drawing.Point(1151, 59);
            this.rozwiaz_button.Name = "rozwiaz_button";
            this.rozwiaz_button.Size = new System.Drawing.Size(95, 45);
            this.rozwiaz_button.TabIndex = 2;
            this.rozwiaz_button.Text = "Rozwiąż";
            this.rozwiaz_button.UseVisualStyleBackColor = true;
            // 
            // pdf_button
            // 
            this.pdf_button.Location = new System.Drawing.Point(1151, 140);
            this.pdf_button.Name = "pdf_button";
            this.pdf_button.Size = new System.Drawing.Size(95, 57);
            this.pdf_button.TabIndex = 3;
            this.pdf_button.Text = "PDF";
            this.pdf_button.UseVisualStyleBackColor = true;
            // 
            // stop_button
            // 
            this.stop_button.Location = new System.Drawing.Point(1151, 236);
            this.stop_button.Name = "stop_button";
            this.stop_button.Size = new System.Drawing.Size(115, 41);
            this.stop_button.TabIndex = 4;
            this.stop_button.Text = "Wstrzymaj";
            this.stop_button.UseVisualStyleBackColor = true;
            // 
            // wznow_button
            // 
            this.wznow_button.Location = new System.Drawing.Point(1151, 294);
            this.wznow_button.Name = "wznow_button";
            this.wznow_button.Size = new System.Drawing.Size(115, 44);
            this.wznow_button.TabIndex = 5;
            this.wznow_button.Text = "Wznów";
            this.wznow_button.UseVisualStyleBackColor = true;
            // 
            // save_button
            // 
            this.save_button.Location = new System.Drawing.Point(701, 414);
            this.save_button.Name = "save_button";
            this.save_button.Size = new System.Drawing.Size(99, 55);
            this.save_button.TabIndex = 6;
            this.save_button.Text = "Zapisz";
            this.save_button.UseVisualStyleBackColor = true;
            // 
            // algorytmy_list
            // 
            this.algorytmy_list.FormattingEnabled = true;
            this.algorytmy_list.Items.AddRange(new object[] {
            "list",
            "box",
            "X"});
            this.algorytmy_list.Location = new System.Drawing.Point(363, 109);
            this.algorytmy_list.Name = "algorytmy_list";
            this.algorytmy_list.Size = new System.Drawing.Size(218, 225);
            this.algorytmy_list.TabIndex = 7;
            this.algorytmy_list.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.algorytmy_list_ItemCheck);
            this.algorytmy_list.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.algorytmy_list_MouseDoubleClick);
            // 
            // funkcje_lista
            // 
            this.funkcje_lista.FormattingEnabled = true;
            this.funkcje_lista.Location = new System.Drawing.Point(677, 109);
            this.funkcje_lista.Name = "funkcje_lista";
            this.funkcje_lista.Size = new System.Drawing.Size(201, 208);
            this.funkcje_lista.TabIndex = 8;
            // 
            // info_panel
            // 
            this.info_panel.AutoScroll = true;
            this.info_panel.ColumnCount = 2;
            this.info_panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.info_panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.info_panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.info_panel.Location = new System.Drawing.Point(40, 92);
            this.info_panel.Name = "info_panel";
            this.info_panel.RowCount = 1;
            this.info_panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.info_panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.info_panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.info_panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.info_panel.Size = new System.Drawing.Size(258, 377);
            this.info_panel.TabIndex = 9;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.DecimalPlaces = 5;
            this.numericUpDown1.Location = new System.Drawing.Point(943, 109);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            8000,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            8000,
            0,
            0,
            -2147483648});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(120, 22);
            this.numericUpDown1.TabIndex = 10;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1442, 515);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.info_panel);
            this.Controls.Add(this.funkcje_lista);
            this.Controls.Add(this.algorytmy_list);
            this.Controls.Add(this.save_button);
            this.Controls.Add(this.wznow_button);
            this.Controls.Add(this.stop_button);
            this.Controls.Add(this.pdf_button);
            this.Controls.Add(this.rozwiaz_button);
            this.Controls.Add(this.funkcja_button);
            this.Controls.Add(this.algorytm_button);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button algorytm_button;
        private System.Windows.Forms.Button funkcja_button;
        private System.Windows.Forms.Button rozwiaz_button;
        private System.Windows.Forms.Button pdf_button;
        private System.Windows.Forms.Button stop_button;
        private System.Windows.Forms.Button wznow_button;
        private System.Windows.Forms.Button save_button;
        private System.Windows.Forms.CheckedListBox algorytmy_list;
        private System.Windows.Forms.CheckedListBox funkcje_lista;
        private System.Windows.Forms.TableLayoutPanel info_panel;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
    }
}

