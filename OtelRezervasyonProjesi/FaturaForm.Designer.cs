namespace OtelRezervasyonProjesi
{
    partial class FaturaForm
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
            this.lblRezervasyon = new System.Windows.Forms.Label();
            this.tbxMusteriNumarasi = new System.Windows.Forms.TextBox();
            this.btnFaturaIndır = new System.Windows.Forms.Button();
            this.gbxFaturaOlustur = new System.Windows.Forms.GroupBox();
            this.gbxFaturaOlustur.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblRezervasyon
            // 
            this.lblRezervasyon.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblRezervasyon.AutoSize = true;
            this.lblRezervasyon.Font = new System.Drawing.Font("Franklin Gothic Book", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblRezervasyon.Location = new System.Drawing.Point(116, 153);
            this.lblRezervasyon.Name = "lblRezervasyon";
            this.lblRezervasyon.Size = new System.Drawing.Size(130, 20);
            this.lblRezervasyon.TabIndex = 0;
            this.lblRezervasyon.Text = "Müşteri Numarası :";
            // 
            // tbxMusteriNumarasi
            // 
            this.tbxMusteriNumarasi.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tbxMusteriNumarasi.Location = new System.Drawing.Point(271, 153);
            this.tbxMusteriNumarasi.Name = "tbxMusteriNumarasi";
            this.tbxMusteriNumarasi.Size = new System.Drawing.Size(219, 22);
            this.tbxMusteriNumarasi.TabIndex = 1;
            // 
            // btnFaturaIndır
            // 
            this.btnFaturaIndır.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnFaturaIndır.Font = new System.Drawing.Font("Franklin Gothic Book", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnFaturaIndır.Location = new System.Drawing.Point(441, 268);
            this.btnFaturaIndır.Name = "btnFaturaIndır";
            this.btnFaturaIndır.Size = new System.Drawing.Size(136, 48);
            this.btnFaturaIndır.TabIndex = 2;
            this.btnFaturaIndır.Text = "Faturayı İndir";
            this.btnFaturaIndır.UseVisualStyleBackColor = true;
            this.btnFaturaIndır.Click += new System.EventHandler(this.btnFaturaIndır_Click);
            // 
            // gbxFaturaOlustur
            // 
            this.gbxFaturaOlustur.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.gbxFaturaOlustur.Controls.Add(this.tbxMusteriNumarasi);
            this.gbxFaturaOlustur.Controls.Add(this.btnFaturaIndır);
            this.gbxFaturaOlustur.Controls.Add(this.lblRezervasyon);
            this.gbxFaturaOlustur.Location = new System.Drawing.Point(88, 52);
            this.gbxFaturaOlustur.Name = "gbxFaturaOlustur";
            this.gbxFaturaOlustur.Size = new System.Drawing.Size(609, 341);
            this.gbxFaturaOlustur.TabIndex = 3;
            this.gbxFaturaOlustur.TabStop = false;
            this.gbxFaturaOlustur.Text = "Fatura Oluştur";
            // 
            // FaturaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(233)))), ((int)(((byte)(238)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.gbxFaturaOlustur);
            this.Name = "FaturaForm";
            this.Text = "Fatura Oluştur";
            this.Load += new System.EventHandler(this.FaturaForm_Load);
            this.gbxFaturaOlustur.ResumeLayout(false);
            this.gbxFaturaOlustur.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblRezervasyon;
        private System.Windows.Forms.TextBox tbxMusteriNumarasi;
        private System.Windows.Forms.Button btnFaturaIndır;
        private System.Windows.Forms.GroupBox gbxFaturaOlustur;
    }
}