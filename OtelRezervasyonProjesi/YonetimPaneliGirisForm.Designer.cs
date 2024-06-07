namespace OtelRezervasyonProjesi
{
    partial class YonetimPaneliGirisForm
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblAdminKullaniciAdi = new System.Windows.Forms.Label();
            this.tbxKullaniciAdi = new System.Windows.Forms.TextBox();
            this.lblAdminSifre = new System.Windows.Forms.Label();
            this.gbxYoneticiGirisPaneli = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnGiris = new System.Windows.Forms.Button();
            this.tbxSifre = new System.Windows.Forms.TextBox();
            this.gbxYoneticiGirisPaneli.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblAdminKullaniciAdi
            // 
            this.lblAdminKullaniciAdi.AutoSize = true;
            this.lblAdminKullaniciAdi.Font = new System.Drawing.Font("Franklin Gothic Book", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblAdminKullaniciAdi.Location = new System.Drawing.Point(59, 391);
            this.lblAdminKullaniciAdi.Name = "lblAdminKullaniciAdi";
            this.lblAdminKullaniciAdi.Size = new System.Drawing.Size(106, 21);
            this.lblAdminKullaniciAdi.TabIndex = 0;
            this.lblAdminKullaniciAdi.Text = "Kullanıcı Adı :";
            // 
            // tbxKullaniciAdi
            // 
            this.tbxKullaniciAdi.Font = new System.Drawing.Font("Franklin Gothic Book", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.tbxKullaniciAdi.Location = new System.Drawing.Point(181, 388);
            this.tbxKullaniciAdi.Name = "tbxKullaniciAdi";
            this.tbxKullaniciAdi.Size = new System.Drawing.Size(232, 27);
            this.tbxKullaniciAdi.TabIndex = 1;
            // 
            // lblAdminSifre
            // 
            this.lblAdminSifre.AutoSize = true;
            this.lblAdminSifre.Font = new System.Drawing.Font("Franklin Gothic Book", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblAdminSifre.Location = new System.Drawing.Point(114, 436);
            this.lblAdminSifre.Name = "lblAdminSifre";
            this.lblAdminSifre.Size = new System.Drawing.Size(52, 21);
            this.lblAdminSifre.TabIndex = 3;
            this.lblAdminSifre.Text = "Şifre :";
            // 
            // gbxYoneticiGirisPaneli
            // 
            this.gbxYoneticiGirisPaneli.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.gbxYoneticiGirisPaneli.Controls.Add(this.pictureBox1);
            this.gbxYoneticiGirisPaneli.Controls.Add(this.btnGiris);
            this.gbxYoneticiGirisPaneli.Controls.Add(this.tbxSifre);
            this.gbxYoneticiGirisPaneli.Controls.Add(this.lblAdminSifre);
            this.gbxYoneticiGirisPaneli.Controls.Add(this.lblAdminKullaniciAdi);
            this.gbxYoneticiGirisPaneli.Controls.Add(this.tbxKullaniciAdi);
            this.gbxYoneticiGirisPaneli.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.gbxYoneticiGirisPaneli.ForeColor = System.Drawing.SystemColors.Desktop;
            this.gbxYoneticiGirisPaneli.Location = new System.Drawing.Point(181, 35);
            this.gbxYoneticiGirisPaneli.Name = "gbxYoneticiGirisPaneli";
            this.gbxYoneticiGirisPaneli.Size = new System.Drawing.Size(482, 628);
            this.gbxYoneticiGirisPaneli.TabIndex = 4;
            this.gbxYoneticiGirisPaneli.TabStop = false;
            this.gbxYoneticiGirisPaneli.Text = "Otel Yetkili Girişi";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::OtelRezervasyonProjesi.Properties.Resources.account;
            this.pictureBox1.Location = new System.Drawing.Point(131, 85);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(226, 220);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // btnGiris
            // 
            this.btnGiris.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnGiris.Font = new System.Drawing.Font("Franklin Gothic Book", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnGiris.ForeColor = System.Drawing.SystemColors.Desktop;
            this.btnGiris.Location = new System.Drawing.Point(295, 543);
            this.btnGiris.Name = "btnGiris";
            this.btnGiris.Size = new System.Drawing.Size(172, 43);
            this.btnGiris.TabIndex = 4;
            this.btnGiris.Text = "Giriş";
            this.btnGiris.UseVisualStyleBackColor = true;
            this.btnGiris.Click += new System.EventHandler(this.btnAdminGiris_Click);
            // 
            // tbxSifre
            // 
            this.tbxSifre.Font = new System.Drawing.Font("Franklin Gothic Book", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.tbxSifre.Location = new System.Drawing.Point(181, 433);
            this.tbxSifre.Name = "tbxSifre";
            this.tbxSifre.PasswordChar = '*';
            this.tbxSifre.Size = new System.Drawing.Size(232, 27);
            this.tbxSifre.TabIndex = 2;
            // 
            // YonetimPaneliGirisForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(233)))), ((int)(((byte)(238)))));
            this.ClientSize = new System.Drawing.Size(800, 675);
            this.Controls.Add(this.gbxYoneticiGirisPaneli);
            this.ForeColor = System.Drawing.SystemColors.Desktop;
            this.Name = "YonetimPaneliGirisForm";
            this.Text = "Yönetim Paneli Giriş Sayfası";
            this.Load += new System.EventHandler(this.YonetimPaneliGirisForm_Load);
            this.gbxYoneticiGirisPaneli.ResumeLayout(false);
            this.gbxYoneticiGirisPaneli.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblAdminKullaniciAdi;
        private System.Windows.Forms.TextBox tbxKullaniciAdi;
        private System.Windows.Forms.Label lblAdminSifre;
        private System.Windows.Forms.GroupBox gbxYoneticiGirisPaneli;
        private System.Windows.Forms.TextBox tbxSifre;
        private System.Windows.Forms.Button btnGiris;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

