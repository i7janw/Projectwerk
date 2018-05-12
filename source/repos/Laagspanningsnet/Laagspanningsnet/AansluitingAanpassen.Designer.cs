namespace Laagspanningsnet
{
    partial class AansluitingAanpassen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AansluitingAanpassen));
            this.lblTitel = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblMachine = new System.Windows.Forms.Label();
            this.lblAansluitpunt = new System.Windows.Forms.Label();
            this.cmbMachine = new System.Windows.Forms.ComboBox();
            this.cmbAansluitpunt = new System.Windows.Forms.ComboBox();
            this.lblOmschrijving = new System.Windows.Forms.Label();
            this.lblKabelType = new System.Windows.Forms.Label();
            this.lblKabelsectie = new System.Windows.Forms.Label();
            this.lblStroom = new System.Windows.Forms.Label();
            this.lblPolen = new System.Windows.Forms.Label();
            this.txtbxOmschrijving = new System.Windows.Forms.TextBox();
            this.txtbxKabeltype = new System.Windows.Forms.TextBox();
            this.txtbxKabelsectie = new System.Windows.Forms.TextBox();
            this.txtbxStroom = new System.Windows.Forms.TextBox();
            this.cmbPolen = new System.Windows.Forms.ComboBox();
            this.lblKring = new System.Windows.Forms.Label();
            this.txtbxKring = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lblTitel
            // 
            this.lblTitel.AutoSize = true;
            this.lblTitel.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.69811F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitel.Location = new System.Drawing.Point(12, 9);
            this.lblTitel.Name = "lblTitel";
            this.lblTitel.Size = new System.Drawing.Size(187, 33);
            this.lblTitel.TabIndex = 0;
            this.lblTitel.Text = "Aansluitpunt";
            this.lblTitel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(273, 327);
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
            this.btnCancel.Location = new System.Drawing.Point(354, 327);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Annuleren";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancelClick);
            // 
            // lblMachine
            // 
            this.lblMachine.AutoSize = true;
            this.lblMachine.Location = new System.Drawing.Point(75, 106);
            this.lblMachine.Name = "lblMachine";
            this.lblMachine.Size = new System.Drawing.Size(70, 15);
            this.lblMachine.TabIndex = 0;
            this.lblMachine.Text = "Machine ID";
            this.lblMachine.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblAansluitpunt
            // 
            this.lblAansluitpunt.AutoSize = true;
            this.lblAansluitpunt.Location = new System.Drawing.Point(9, 131);
            this.lblAansluitpunt.Name = "lblAansluitpunt";
            this.lblAansluitpunt.Size = new System.Drawing.Size(136, 15);
            this.lblAansluitpunt.TabIndex = 1;
            this.lblAansluitpunt.Text = "Aansluitpunt (T/K/VB) ID";
            this.lblAansluitpunt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbMachine
            // 
            this.cmbMachine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMachine.FormattingEnabled = true;
            this.cmbMachine.Location = new System.Drawing.Point(151, 102);
            this.cmbMachine.Name = "cmbMachine";
            this.cmbMachine.Size = new System.Drawing.Size(278, 21);
            this.cmbMachine.TabIndex = 7;
            this.cmbMachine.SelectedIndexChanged += new System.EventHandler(this.CmbMachineSelectedIndexChanged);
            // 
            // cmbAansluitpunt
            // 
            this.cmbAansluitpunt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAansluitpunt.FormattingEnabled = true;
            this.cmbAansluitpunt.Location = new System.Drawing.Point(151, 129);
            this.cmbAansluitpunt.Name = "cmbAansluitpunt";
            this.cmbAansluitpunt.Size = new System.Drawing.Size(278, 21);
            this.cmbAansluitpunt.TabIndex = 8;
            this.cmbAansluitpunt.SelectedIndexChanged += new System.EventHandler(this.CmbAansluitpuntSelectedIndexChanged);
            // 
            // lblOmschrijving
            // 
            this.lblOmschrijving.AutoSize = true;
            this.lblOmschrijving.Location = new System.Drawing.Point(67, 158);
            this.lblOmschrijving.Name = "lblOmschrijving";
            this.lblOmschrijving.Size = new System.Drawing.Size(78, 15);
            this.lblOmschrijving.TabIndex = 9;
            this.lblOmschrijving.Text = "Omschrijving";
            this.lblOmschrijving.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblKabelType
            // 
            this.lblKabelType.AutoSize = true;
            this.lblKabelType.Location = new System.Drawing.Point(84, 184);
            this.lblKabelType.Name = "lblKabelType";
            this.lblKabelType.Size = new System.Drawing.Size(61, 15);
            this.lblKabelType.TabIndex = 10;
            this.lblKabelType.Text = "Kabeltype";
            this.lblKabelType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblKabelsectie
            // 
            this.lblKabelsectie.AutoSize = true;
            this.lblKabelsectie.Location = new System.Drawing.Point(74, 210);
            this.lblKabelsectie.Name = "lblKabelsectie";
            this.lblKabelsectie.Size = new System.Drawing.Size(71, 15);
            this.lblKabelsectie.TabIndex = 11;
            this.lblKabelsectie.Text = "Kabelsectie";
            this.lblKabelsectie.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblStroom
            // 
            this.lblStroom.AutoSize = true;
            this.lblStroom.Location = new System.Drawing.Point(80, 236);
            this.lblStroom.Name = "lblStroom";
            this.lblStroom.Size = new System.Drawing.Size(65, 15);
            this.lblStroom.TabIndex = 12;
            this.lblStroom.Text = "Stroom (A)";
            this.lblStroom.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPolen
            // 
            this.lblPolen.AutoSize = true;
            this.lblPolen.Location = new System.Drawing.Point(70, 262);
            this.lblPolen.Name = "lblPolen";
            this.lblPolen.Size = new System.Drawing.Size(75, 15);
            this.lblPolen.TabIndex = 13;
            this.lblPolen.Text = "Aantal polen";
            this.lblPolen.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtbxOmschrijving
            // 
            this.txtbxOmschrijving.Location = new System.Drawing.Point(151, 156);
            this.txtbxOmschrijving.Name = "txtbxOmschrijving";
            this.txtbxOmschrijving.Size = new System.Drawing.Size(278, 20);
            this.txtbxOmschrijving.TabIndex = 14;
            // 
            // txtbxKabeltype
            // 
            this.txtbxKabeltype.Location = new System.Drawing.Point(151, 182);
            this.txtbxKabeltype.Name = "txtbxKabeltype";
            this.txtbxKabeltype.Size = new System.Drawing.Size(278, 20);
            this.txtbxKabeltype.TabIndex = 15;
            // 
            // txtbxKabelsectie
            // 
            this.txtbxKabelsectie.Location = new System.Drawing.Point(151, 208);
            this.txtbxKabelsectie.Name = "txtbxKabelsectie";
            this.txtbxKabelsectie.Size = new System.Drawing.Size(278, 20);
            this.txtbxKabelsectie.TabIndex = 16;
            this.txtbxKabelsectie.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.KabelsectieKeyPress);
            // 
            // txtbxStroom
            // 
            this.txtbxStroom.Location = new System.Drawing.Point(151, 234);
            this.txtbxStroom.Name = "txtbxStroom";
            this.txtbxStroom.Size = new System.Drawing.Size(278, 20);
            this.txtbxStroom.TabIndex = 17;
            this.txtbxStroom.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtbxStroomKeyPress);
            // 
            // cmbPolen
            // 
            this.cmbPolen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPolen.FormattingEnabled = true;
            this.cmbPolen.Location = new System.Drawing.Point(151, 260);
            this.cmbPolen.Name = "cmbPolen";
            this.cmbPolen.Size = new System.Drawing.Size(278, 21);
            this.cmbPolen.TabIndex = 18;
            // 
            // lblKring
            // 
            this.lblKring.AutoSize = true;
            this.lblKring.Location = new System.Drawing.Point(109, 78);
            this.lblKring.Name = "lblKring";
            this.lblKring.Size = new System.Drawing.Size(36, 15);
            this.lblKring.TabIndex = 19;
            this.lblKring.Text = "Kring";
            this.lblKring.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtbxKring
            // 
            this.txtbxKring.Location = new System.Drawing.Point(151, 76);
            this.txtbxKring.Name = "txtbxKring";
            this.txtbxKring.Size = new System.Drawing.Size(278, 20);
            this.txtbxKring.TabIndex = 20;
            this.txtbxKring.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtbxKringKeyPress);
            // 
            // AansluitingAanpassen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(447, 380);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.cmbPolen);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.txtbxStroom);
            this.Controls.Add(this.txtbxKabelsectie);
            this.Controls.Add(this.txtbxKabeltype);
            this.Controls.Add(this.txtbxOmschrijving);
            this.Controls.Add(this.cmbAansluitpunt);
            this.Controls.Add(this.cmbMachine);
            this.Controls.Add(this.lblAansluitpunt);
            this.Controls.Add(this.lblMachine);
            this.Controls.Add(this.lblPolen);
            this.Controls.Add(this.lblStroom);
            this.Controls.Add(this.lblKabelsectie);
            this.Controls.Add(this.lblKabelType);
            this.Controls.Add(this.lblOmschrijving);
            this.Controls.Add(this.txtbxKring);
            this.Controls.Add(this.lblTitel);
            this.Controls.Add(this.lblKring);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AansluitingAanpassen";
            this.Text = "Aansluiting aanpassen";
            this.Load += new System.EventHandler(this.AansluitingAanpassenLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblTitel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblMachine;
        private System.Windows.Forms.Label lblAansluitpunt;
        private System.Windows.Forms.ComboBox cmbMachine;
        private System.Windows.Forms.ComboBox cmbAansluitpunt;
        private System.Windows.Forms.Label lblOmschrijving;
        private System.Windows.Forms.Label lblKabelType;
        private System.Windows.Forms.Label lblKabelsectie;
        private System.Windows.Forms.Label lblStroom;
        private System.Windows.Forms.Label lblPolen;
        private System.Windows.Forms.TextBox txtbxOmschrijving;
        private System.Windows.Forms.TextBox txtbxKabeltype;
        private System.Windows.Forms.TextBox txtbxKabelsectie;
        private System.Windows.Forms.TextBox txtbxStroom;
        private System.Windows.Forms.ComboBox cmbPolen;
        private System.Windows.Forms.Label lblKring;
        private System.Windows.Forms.TextBox txtbxKring;
    }
}