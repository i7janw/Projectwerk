namespace Laagspanningsnet
{
    partial class Afdrukken
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Afdrukken));
            this.lblTitel = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblSelectie = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbPrinter = new System.Windows.Forms.ComboBox();
            this.lblAantal = new System.Windows.Forms.Label();
            this.cmbAantal = new System.Windows.Forms.ComboBox();
            this.cmbSelectie = new System.Windows.Forms.ComboBox();
            this.cbxInclusief = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // lblTitel
            // 
            this.lblTitel.AutoSize = true;
            this.lblTitel.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.69811F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitel.Location = new System.Drawing.Point(12, 9);
            this.lblTitel.Name = "lblTitel";
            this.lblTitel.Size = new System.Drawing.Size(155, 33);
            this.lblTitel.TabIndex = 0;
            this.lblTitel.Text = "Afdrukken";
            this.lblTitel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(404, 287);
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
            this.btnCancel.Location = new System.Drawing.Point(485, 287);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Annuleren";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancelClick);
            // 
            // lblSelectie
            // 
            this.lblSelectie.AutoSize = true;
            this.lblSelectie.Location = new System.Drawing.Point(15, 74);
            this.lblSelectie.Name = "lblSelectie";
            this.lblSelectie.Size = new System.Drawing.Size(51, 15);
            this.lblSelectie.TabIndex = 21;
            this.lblSelectie.Text = "Selectie";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(293, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 15);
            this.label1.TabIndex = 19;
            this.label1.Text = "Printer";
            // 
            // cmbPrinter
            // 
            this.cmbPrinter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cmbPrinter.FormattingEnabled = true;
            this.cmbPrinter.Location = new System.Drawing.Point(296, 92);
            this.cmbPrinter.Name = "cmbPrinter";
            this.cmbPrinter.Size = new System.Drawing.Size(264, 144);
            this.cmbPrinter.TabIndex = 18;
            // 
            // lblAantal
            // 
            this.lblAantal.AutoSize = true;
            this.lblAantal.Location = new System.Drawing.Point(293, 252);
            this.lblAantal.Name = "lblAantal";
            this.lblAantal.Size = new System.Drawing.Size(88, 15);
            this.lblAantal.TabIndex = 1;
            this.lblAantal.Text = "Aantal kopieën";
            this.lblAantal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbAantal
            // 
            this.cmbAantal.FormattingEnabled = true;
            this.cmbAantal.Location = new System.Drawing.Point(387, 250);
            this.cmbAantal.Name = "cmbAantal";
            this.cmbAantal.Size = new System.Drawing.Size(173, 21);
            this.cmbAantal.TabIndex = 0;
            // 
            // cmbSelectie
            // 
            this.cmbSelectie.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cmbSelectie.FormattingEnabled = true;
            this.cmbSelectie.Location = new System.Drawing.Point(18, 92);
            this.cmbSelectie.Name = "cmbSelectie";
            this.cmbSelectie.Size = new System.Drawing.Size(264, 202);
            this.cmbSelectie.TabIndex = 22;
            // 
            // cbxInclusief
            // 
            this.cbxInclusief.AutoSize = true;
            this.cbxInclusief.Location = new System.Drawing.Point(18, 291);
            this.cbxInclusief.Name = "cbxInclusief";
            this.cbxInclusief.Size = new System.Drawing.Size(155, 19);
            this.cbxInclusief.TabIndex = 23;
            this.cbxInclusief.Text = "Inclusief aansluitpunten";
            this.cbxInclusief.UseVisualStyleBackColor = true;
            // 
            // Afdrukken
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(578, 341);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.cbxInclusief);
            this.Controls.Add(this.cmbAantal);
            this.Controls.Add(this.lblAantal);
            this.Controls.Add(this.cmbPrinter);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblSelectie);
            this.Controls.Add(this.lblTitel);
            this.Controls.Add(this.cmbSelectie);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Afdrukken";
            this.Text = "Afdrukken";
            this.Load += new System.EventHandler(this.AfdrukkenLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblTitel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ComboBox cmbPrinter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblAantal;
        private System.Windows.Forms.ComboBox cmbAantal;
        private System.Windows.Forms.Label lblSelectie;
        private System.Windows.Forms.ComboBox cmbSelectie;
        private System.Windows.Forms.CheckBox cbxInclusief;
    }
}