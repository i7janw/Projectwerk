namespace Laagspanningsnet
{
    partial class AansluitpuntAanpassen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AansluitpuntAanpassen));
            this.lblTitel = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblAansluitpunt = new System.Windows.Forms.Label();
            this.cmbAansluitpunt = new System.Windows.Forms.ComboBox();
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
            this.lblTitel.Size = new System.Drawing.Size(350, 33);
            this.lblTitel.TabIndex = 0;
            this.lblTitel.Text = "Aansluitpunt Aanpassen";
            this.lblTitel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(206, 153);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.BtnOkClick);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(287, 153);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Annuleren";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancelClick);
            // 
            // lblAansluitpunt
            // 
            this.lblAansluitpunt.AutoSize = true;
            this.lblAansluitpunt.Location = new System.Drawing.Point(15, 75);
            this.lblAansluitpunt.Name = "lblAansluitpunt";
            this.lblAansluitpunt.Size = new System.Drawing.Size(89, 15);
            this.lblAansluitpunt.TabIndex = 0;
            this.lblAansluitpunt.Text = "Aansluitpunt ID";
            this.lblAansluitpunt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbAansluitpunt
            // 
            this.cmbAansluitpunt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAansluitpunt.FormattingEnabled = true;
            this.cmbAansluitpunt.Location = new System.Drawing.Point(110, 73);
            this.cmbAansluitpunt.Name = "cmbAansluitpunt";
            this.cmbAansluitpunt.Size = new System.Drawing.Size(252, 21);
            this.cmbAansluitpunt.TabIndex = 18;
            this.cmbAansluitpunt.SelectedIndexChanged += new System.EventHandler(this.cmbAansluitpunt_SelectedIndexChanged);
            // 
            // txtbxLocatie
            // 
            this.txtbxLocatie.Location = new System.Drawing.Point(110, 100);
            this.txtbxLocatie.Name = "txtbxLocatie";
            this.txtbxLocatie.Size = new System.Drawing.Size(252, 20);
            this.txtbxLocatie.TabIndex = 16;
            // 
            // lblLocatie
            // 
            this.lblLocatie.AutoSize = true;
            this.lblLocatie.Location = new System.Drawing.Point(57, 102);
            this.lblLocatie.Name = "lblLocatie";
            this.lblLocatie.Size = new System.Drawing.Size(47, 15);
            this.lblLocatie.TabIndex = 17;
            this.lblLocatie.Text = "Locatie";
            this.lblLocatie.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // AansluitpuntAanpassen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 217);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblTitel);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblLocatie);
            this.Controls.Add(this.lblAansluitpunt);
            this.Controls.Add(this.txtbxLocatie);
            this.Controls.Add(this.cmbAansluitpunt);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AansluitpuntAanpassen";
            this.Text = "Aansluitpunt Aanpassen";
            this.Load += new System.EventHandler(this.AansluitpuntAanpassenLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblTitel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblAansluitpunt;
        private System.Windows.Forms.TextBox txtbxLocatie;
        private System.Windows.Forms.Label lblLocatie;
        private System.Windows.Forms.ComboBox cmbAansluitpunt;
    }
}