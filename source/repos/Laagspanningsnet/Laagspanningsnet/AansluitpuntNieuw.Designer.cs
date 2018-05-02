namespace Laagspanningsnet
{
    partial class AansluitpuntNieuw
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AansluitpuntNieuw));
            this.lblTitel = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtbxAansluitpunt = new System.Windows.Forms.TextBox();
            this.lblAansluitpunt = new System.Windows.Forms.Label();
            this.txtbxLocatie = new System.Windows.Forms.TextBox();
            this.lblLocatie = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTitel
            // 
            this.lblTitel.AutoSize = true;
            this.lblTitel.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.69811F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitel.Location = new System.Drawing.Point(12, 9);
            this.lblTitel.Name = "lblTitel";
            this.lblTitel.Size = new System.Drawing.Size(282, 33);
            this.lblTitel.TabIndex = 0;
            this.lblTitel.Text = "Nieuw Aansluitpunt";
            this.lblTitel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(186, 152);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.BtnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(267, 152);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Annuleren";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // txtbxAansluitpunt
            // 
            this.txtbxAansluitpunt.Location = new System.Drawing.Point(110, 73);
            this.txtbxAansluitpunt.Name = "txtbxAansluitpunt";
            this.txtbxAansluitpunt.Size = new System.Drawing.Size(232, 20);
            this.txtbxAansluitpunt.TabIndex = 15;
            this.txtbxAansluitpunt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtbxAansluitpunt_KeyPress);
            // 
            // lblAansluitpunt
            // 
            this.lblAansluitpunt.AutoSize = true;
            this.lblAansluitpunt.Location = new System.Drawing.Point(15, 73);
            this.lblAansluitpunt.Name = "lblAansluitpunt";
            this.lblAansluitpunt.Size = new System.Drawing.Size(89, 15);
            this.lblAansluitpunt.TabIndex = 0;
            this.lblAansluitpunt.Text = "Aansluitpunt ID";
            this.lblAansluitpunt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtbxLocatie
            // 
            this.txtbxLocatie.Location = new System.Drawing.Point(110, 99);
            this.txtbxLocatie.Name = "txtbxLocatie";
            this.txtbxLocatie.Size = new System.Drawing.Size(232, 20);
            this.txtbxLocatie.TabIndex = 16;
            // 
            // lblLocatie
            // 
            this.lblLocatie.AutoSize = true;
            this.lblLocatie.Location = new System.Drawing.Point(57, 99);
            this.lblLocatie.Name = "lblLocatie";
            this.lblLocatie.Size = new System.Drawing.Size(47, 15);
            this.lblLocatie.TabIndex = 17;
            this.lblLocatie.Text = "Locatie";
            this.lblLocatie.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // AansluitpuntNieuw
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(386, 209);
            this.Controls.Add(this.lblTitel);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblLocatie);
            this.Controls.Add(this.lblAansluitpunt);
            this.Controls.Add(this.txtbxAansluitpunt);
            this.Controls.Add(this.txtbxLocatie);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AansluitpuntNieuw";
            this.Text = "Nieuw Aansluitpunt";
            this.Load += new System.EventHandler(this.AansluitpuntNieuw_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblTitel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblAansluitpunt;
        private System.Windows.Forms.TextBox txtbxAansluitpunt;
        private System.Windows.Forms.TextBox txtbxLocatie;
        private System.Windows.Forms.Label lblLocatie;
    }
}