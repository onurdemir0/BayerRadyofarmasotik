
namespace BayerRadyofarmasotik.UI
{
    partial class UserControlIptalHeader
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlIptalHeader = new System.Windows.Forms.Panel();
            this.btnIptalEkle = new BayerRadyofarmasotik.ButtonControls.RJButton();
            this.btnIptalTemizle = new BayerRadyofarmasotik.ButtonControls.RJButton();
            this.btnIptalGonder = new BayerRadyofarmasotik.ButtonControls.RJButton();
            this.pnlIptalHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlIptalHeader
            // 
            this.pnlIptalHeader.Controls.Add(this.btnIptalEkle);
            this.pnlIptalHeader.Controls.Add(this.btnIptalTemizle);
            this.pnlIptalHeader.Controls.Add(this.btnIptalGonder);
            this.pnlIptalHeader.Location = new System.Drawing.Point(3, 3);
            this.pnlIptalHeader.Name = "pnlIptalHeader";
            this.pnlIptalHeader.Size = new System.Drawing.Size(1577, 94);
            this.pnlIptalHeader.TabIndex = 9;
            // 
            // btnIptalEkle
            // 
            this.btnIptalEkle.BackColor = System.Drawing.Color.ForestGreen;
            this.btnIptalEkle.BackgroundColor = System.Drawing.Color.ForestGreen;
            this.btnIptalEkle.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnIptalEkle.BorderRadius = 5;
            this.btnIptalEkle.BorderSize = 0;
            this.btnIptalEkle.FlatAppearance.BorderSize = 0;
            this.btnIptalEkle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIptalEkle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnIptalEkle.ForeColor = System.Drawing.Color.White;
            this.btnIptalEkle.Location = new System.Drawing.Point(10, 22);
            this.btnIptalEkle.Name = "btnIptalEkle";
            this.btnIptalEkle.Size = new System.Drawing.Size(467, 52);
            this.btnIptalEkle.TabIndex = 2;
            this.btnIptalEkle.Text = "Yeni Satır Ekle";
            this.btnIptalEkle.TextColor = System.Drawing.Color.White;
            this.btnIptalEkle.UseVisualStyleBackColor = false;
            this.btnIptalEkle.Click += new System.EventHandler(this.btnIptalEkle_Click);
            // 
            // btnIptalTemizle
            // 
            this.btnIptalTemizle.BackColor = System.Drawing.Color.Firebrick;
            this.btnIptalTemizle.BackgroundColor = System.Drawing.Color.Firebrick;
            this.btnIptalTemizle.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnIptalTemizle.BorderRadius = 5;
            this.btnIptalTemizle.BorderSize = 0;
            this.btnIptalTemizle.FlatAppearance.BorderSize = 0;
            this.btnIptalTemizle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIptalTemizle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnIptalTemizle.ForeColor = System.Drawing.Color.White;
            this.btnIptalTemizle.Location = new System.Drawing.Point(1015, 22);
            this.btnIptalTemizle.Name = "btnIptalTemizle";
            this.btnIptalTemizle.Size = new System.Drawing.Size(467, 52);
            this.btnIptalTemizle.TabIndex = 5;
            this.btnIptalTemizle.Text = "Tümünü Temizle";
            this.btnIptalTemizle.TextColor = System.Drawing.Color.White;
            this.btnIptalTemizle.UseVisualStyleBackColor = false;
            // 
            // btnIptalGonder
            // 
            this.btnIptalGonder.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnIptalGonder.BackgroundColor = System.Drawing.Color.DodgerBlue;
            this.btnIptalGonder.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnIptalGonder.BorderRadius = 5;
            this.btnIptalGonder.BorderSize = 0;
            this.btnIptalGonder.FlatAppearance.BorderSize = 0;
            this.btnIptalGonder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIptalGonder.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnIptalGonder.ForeColor = System.Drawing.Color.White;
            this.btnIptalGonder.Location = new System.Drawing.Point(515, 22);
            this.btnIptalGonder.Name = "btnIptalGonder";
            this.btnIptalGonder.Size = new System.Drawing.Size(467, 52);
            this.btnIptalGonder.TabIndex = 3;
            this.btnIptalGonder.Text = "Gönder";
            this.btnIptalGonder.TextColor = System.Drawing.Color.White;
            this.btnIptalGonder.UseVisualStyleBackColor = false;
            // 
            // UserControlIptalHeader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlIptalHeader);
            this.Name = "UserControlIptalHeader";
            this.Size = new System.Drawing.Size(1600, 108);
            this.pnlIptalHeader.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlIptalHeader;
        private ButtonControls.RJButton btnIptalEkle;
        private ButtonControls.RJButton btnIptalTemizle;
        private ButtonControls.RJButton btnIptalGonder;
    }
}
