namespace Laagspanningsnet
{
    partial class AansluitpuntHernoemen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AansluitpuntHernoemen));
            this.lblTitel = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblAansluitpuntOud = new System.Windows.Forms.Label();
            this.cmbAansluitpunt = new System.Windows.Forms.ComboBox();
            this.lblAansluitpuntNieuw = new System.Windows.Forms.Label();
            this.txtbxAansluitpunt = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lblTitel
            // 
            this.lblTitel.AutoSize = true;
            this.lblTitel.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.69811F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitel.Location = new System.Drawing.Point(12, 9);
            this.lblTitel.Name = "lblTitel";
            this.lblTitel.Size = new System.Drawing.Size(357, 33);
            this.lblTitel.TabIndex = 0;
            this.lblTitel.Text = "Aansluitpunt Hernoemen";
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
            this.btnOK.Click += new System.EventHandler(this.BtnOK_Click);
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
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // lblAansluitpuntOud
            // 
            this.lblAansluitpuntOud.AutoSize = true;
            this.lblAansluitpuntOud.Location = new System.Drawing.Point(27, 75);
            this.lblAansluitpuntOud.Name = "lblAansluitpuntOud";
            this.lblAansluitpuntOud.Size = new System.Drawing.Size(115, 15);
            this.lblAansluitpuntOud.TabIndex = 0;
            this.lblAansluitpuntOud.Text = "Oud aansluitpunt ID";
            this.lblAansluitpuntOud.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbAansluitpunt
            // 
            this.cmbAansluitpunt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAansluitpunt.FormattingEnabled = true;
            this.cmbAansluitpunt.Location = new System.Drawing.Point(149, 73);
            this.cmbAansluitpunt.Name = "cmbAansluitpunt";
            this.cmbAansluitpunt.Size = new System.Drawing.Size(213, 21);
            this.cmbAansluitpunt.TabIndex = 18;
            // 
            // lblAansluitpuntNieuw
            // 
            this.lblAansluitpuntNieuw.AutoSize = true;
            this.lblAansluitpuntNieuw.Location = new System.Drawing.Point(15, 102);
            this.lblAansluitpuntNieuw.Name = "lblAansluitpuntNieuw";
            this.lblAansluitpuntNieuw.Size = new System.Drawing.Size(127, 15);
            this.lblAansluitpuntNieuw.TabIndex = 19;
            this.lblAansluitpuntNieuw.Text = "Nieuw aansluitpunt ID";
            this.lblAansluitpuntNieuw.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtbxAansluitpunt
            // 
            this.txtbxAansluitpunt.Location = new System.Drawing.Point(149, 101);
            this.txtbxAansluitpunt.Name = "txtbxAansluitpunt";
            this.txtbxAansluitpunt.Size = new System.Drawing.Size(213, 20);
            this.txtbxAansluitpunt.TabIndex = 20;
            // 
            // AansluitpuntHernoemen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 217);
            this.Controls.Add(this.txtbxAansluitpunt);
            this.Controls.Add(this.lblAansluitpuntNieuw);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblTitel);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblAansluitpuntOud);
            this.Controls.Add(this.cmbAansluitpunt);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AansluitpuntHernoemen";
            this.Text = "Aansluitpunt Hernoemen";
            this.Load += new System.EventHandler(this.AansluitpuntHernoemen_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblTitel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblAansluitpuntOud;
        private System.Windows.Forms.ComboBox cmbAansluitpunt;
        private System.Windows.Forms.Label lblAansluitpuntNieuw;
        private System.Windows.Forms.TextBox txtbxAansluitpunt;
    }
}