namespace OtelRezervasyonProjesi
{
    partial class TemizlikHizmetiForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgwTemizlikDurumu = new System.Windows.Forms.DataGridView();
            this.btnTemizlikHizmeti = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbxOdaNumarasi = new System.Windows.Forms.TextBox();
            this.gbxTemizlikIslemleri = new System.Windows.Forms.GroupBox();
            this.OdaID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OdaTemizlikDurumu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OdaDurumu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgwTemizlikDurumu)).BeginInit();
            this.gbxTemizlikIslemleri.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgwTemizlikDurumu
            // 
            this.dgwTemizlikDurumu.AllowUserToResizeColumns = false;
            this.dgwTemizlikDurumu.AllowUserToResizeRows = false;
            this.dgwTemizlikDurumu.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgwTemizlikDurumu.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgwTemizlikDurumu.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Franklin Gothic Book", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(153)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgwTemizlikDurumu.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgwTemizlikDurumu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwTemizlikDurumu.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.OdaID,
            this.OdaTemizlikDurumu,
            this.OdaDurumu});
            this.dgwTemizlikDurumu.GridColor = System.Drawing.SystemColors.ControlLight;
            this.dgwTemizlikDurumu.Location = new System.Drawing.Point(199, 53);
            this.dgwTemizlikDurumu.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgwTemizlikDurumu.MultiSelect = false;
            this.dgwTemizlikDurumu.Name = "dgwTemizlikDurumu";
            this.dgwTemizlikDurumu.RowHeadersVisible = false;
            this.dgwTemizlikDurumu.RowHeadersWidth = 51;
            this.dgwTemizlikDurumu.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Franklin Gothic Book", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.dgwTemizlikDurumu.RowTemplate.Height = 24;
            this.dgwTemizlikDurumu.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgwTemizlikDurumu.Size = new System.Drawing.Size(908, 541);
            this.dgwTemizlikDurumu.TabIndex = 0;
            this.dgwTemizlikDurumu.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgwTemizlikDurumu_CellFormatting);
            // 
            // btnTemizlikHizmeti
            // 
            this.btnTemizlikHizmeti.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTemizlikHizmeti.BackColor = System.Drawing.Color.Snow;
            this.btnTemizlikHizmeti.Font = new System.Drawing.Font("Franklin Gothic Book", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnTemizlikHizmeti.ForeColor = System.Drawing.SystemColors.Desktop;
            this.btnTemizlikHizmeti.Location = new System.Drawing.Point(698, 29);
            this.btnTemizlikHizmeti.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnTemizlikHizmeti.Name = "btnTemizlikHizmeti";
            this.btnTemizlikHizmeti.Size = new System.Drawing.Size(183, 51);
            this.btnTemizlikHizmeti.TabIndex = 9;
            this.btnTemizlikHizmeti.Text = "Onayla";
            this.btnTemizlikHizmeti.UseVisualStyleBackColor = false;
            this.btnTemizlikHizmeti.Click += new System.EventHandler(this.btnTemizlikHizmeti_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Franklin Gothic Book", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(61, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(145, 25);
            this.label1.TabIndex = 10;
            this.label1.Text = "Oda Numarası :";
            // 
            // tbxOdaNumarasi
            // 
            this.tbxOdaNumarasi.Font = new System.Drawing.Font("Franklin Gothic Book", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.tbxOdaNumarasi.Location = new System.Drawing.Point(224, 38);
            this.tbxOdaNumarasi.Name = "tbxOdaNumarasi";
            this.tbxOdaNumarasi.Size = new System.Drawing.Size(232, 30);
            this.tbxOdaNumarasi.TabIndex = 11;
            // 
            // gbxTemizlikIslemleri
            // 
            this.gbxTemizlikIslemleri.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbxTemizlikIslemleri.Controls.Add(this.btnTemizlikHizmeti);
            this.gbxTemizlikIslemleri.Controls.Add(this.tbxOdaNumarasi);
            this.gbxTemizlikIslemleri.Controls.Add(this.label1);
            this.gbxTemizlikIslemleri.Location = new System.Drawing.Point(199, 665);
            this.gbxTemizlikIslemleri.Name = "gbxTemizlikIslemleri";
            this.gbxTemizlikIslemleri.Size = new System.Drawing.Size(908, 100);
            this.gbxTemizlikIslemleri.TabIndex = 12;
            this.gbxTemizlikIslemleri.TabStop = false;
            this.gbxTemizlikIslemleri.Text = "Temizlik İşlemi";
            // 
            // OdaID
            // 
            this.OdaID.DataPropertyName = "OdaID";
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(254)))), ((int)(((byte)(254)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Franklin Gothic Book", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.IndianRed;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(153)))), ((int)(((byte)(17)))));
            this.OdaID.DefaultCellStyle = dataGridViewCellStyle2;
            this.OdaID.HeaderText = "Oda Numarası";
            this.OdaID.MinimumWidth = 6;
            this.OdaID.Name = "OdaID";
            this.OdaID.ReadOnly = true;
            // 
            // OdaTemizlikDurumu
            // 
            this.OdaTemizlikDurumu.DataPropertyName = "OdaTemizlikDurumu";
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Franklin Gothic Book", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.DarkSlateGray;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(153)))), ((int)(((byte)(17)))));
            this.OdaTemizlikDurumu.DefaultCellStyle = dataGridViewCellStyle3;
            this.OdaTemizlikDurumu.HeaderText = "Temizlik Durumu";
            this.OdaTemizlikDurumu.MinimumWidth = 6;
            this.OdaTemizlikDurumu.Name = "OdaTemizlikDurumu";
            this.OdaTemizlikDurumu.ReadOnly = true;
            // 
            // OdaDurumu
            // 
            this.OdaDurumu.DataPropertyName = "OdaDurumu";
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(254)))), ((int)(((byte)(254)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Franklin Gothic Book", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.SlateBlue;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(153)))), ((int)(((byte)(17)))));
            this.OdaDurumu.DefaultCellStyle = dataGridViewCellStyle4;
            this.OdaDurumu.HeaderText = "Oda Durumu";
            this.OdaDurumu.MinimumWidth = 6;
            this.OdaDurumu.Name = "OdaDurumu";
            this.OdaDurumu.ReadOnly = true;
            // 
            // TemizlikHizmetiForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(233)))), ((int)(((byte)(238)))));
            this.ClientSize = new System.Drawing.Size(1313, 851);
            this.Controls.Add(this.dgwTemizlikDurumu);
            this.Controls.Add(this.gbxTemizlikIslemleri);
            this.Font = new System.Drawing.Font("Franklin Gothic Book", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "TemizlikHizmetiForm";
            this.Text = "Temizlik Hizmeti";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.TemizlikHizmetiForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgwTemizlikDurumu)).EndInit();
            this.gbxTemizlikIslemleri.ResumeLayout(false);
            this.gbxTemizlikIslemleri.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgwTemizlikDurumu;
        private System.Windows.Forms.Button btnTemizlikHizmeti;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxOdaNumarasi;
        private System.Windows.Forms.GroupBox gbxTemizlikIslemleri;
        private System.Windows.Forms.DataGridViewTextBoxColumn OdaID;
        private System.Windows.Forms.DataGridViewTextBoxColumn OdaTemizlikDurumu;
        private System.Windows.Forms.DataGridViewTextBoxColumn OdaDurumu;
    }
}