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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblTitel = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
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
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.lblTitel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(671, 621);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lblTitel
            // 
            this.lblTitel.AutoSize = true;
            this.lblTitel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitel.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.69811F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitel.Location = new System.Drawing.Point(3, 0);
            this.lblTitel.Name = "lblTitel";
            this.lblTitel.Size = new System.Drawing.Size(665, 100);
            this.lblTitel.TabIndex = 0;
            this.lblTitel.Text = "Aansluitpunt";
            this.lblTitel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.btnOK, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.btnCancel, 2, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 544);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(665, 74);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // btnOK
            // 
            this.btnOK.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnOK.Location = new System.Drawing.Point(244, 23);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnCancel.Location = new System.Drawing.Point(345, 23);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Annuleren";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.lblMachine, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.lblAansluitpunt, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.cmbMachine, 2, 1);
            this.tableLayoutPanel3.Controls.Add(this.cmbAansluitpunt, 2, 2);
            this.tableLayoutPanel3.Controls.Add(this.lblOmschrijving, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.lblKabelType, 0, 4);
            this.tableLayoutPanel3.Controls.Add(this.lblKabelsectie, 0, 5);
            this.tableLayoutPanel3.Controls.Add(this.lblStroom, 0, 6);
            this.tableLayoutPanel3.Controls.Add(this.lblPolen, 0, 7);
            this.tableLayoutPanel3.Controls.Add(this.txtbxOmschrijving, 2, 3);
            this.tableLayoutPanel3.Controls.Add(this.txtbxKabeltype, 2, 4);
            this.tableLayoutPanel3.Controls.Add(this.txtbxKabelsectie, 2, 5);
            this.tableLayoutPanel3.Controls.Add(this.txtbxStroom, 2, 6);
            this.tableLayoutPanel3.Controls.Add(this.cmbPolen, 2, 7);
            this.tableLayoutPanel3.Controls.Add(this.lblKring, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.txtbxKring, 2, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 103);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 9;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.50328F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.49953F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.49953F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.49953F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.49953F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.49953F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.49953F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.49953F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.Size = new System.Drawing.Size(665, 435);
            this.tableLayoutPanel3.TabIndex = 2;
            // 
            // lblMachine
            // 
            this.lblMachine.AutoSize = true;
            this.lblMachine.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblMachine.Location = new System.Drawing.Point(249, 54);
            this.lblMachine.Name = "lblMachine";
            this.lblMachine.Size = new System.Drawing.Size(70, 54);
            this.lblMachine.TabIndex = 0;
            this.lblMachine.Text = "Machine ID";
            this.lblMachine.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblAansluitpunt
            // 
            this.lblAansluitpunt.AutoSize = true;
            this.lblAansluitpunt.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblAansluitpunt.Location = new System.Drawing.Point(183, 108);
            this.lblAansluitpunt.Name = "lblAansluitpunt";
            this.lblAansluitpunt.Size = new System.Drawing.Size(136, 54);
            this.lblAansluitpunt.TabIndex = 1;
            this.lblAansluitpunt.Text = "Aansluitpunt (T/K/VB) ID";
            this.lblAansluitpunt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbMachine
            // 
            this.cmbMachine.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbMachine.FormattingEnabled = true;
            this.cmbMachine.Location = new System.Drawing.Point(345, 70);
            this.cmbMachine.Name = "cmbMachine";
            this.cmbMachine.Size = new System.Drawing.Size(296, 21);
            this.cmbMachine.TabIndex = 7;
            this.cmbMachine.SelectedIndexChanged += new System.EventHandler(this.cmbMachine_SelectedIndexChanged);
            // 
            // cmbAansluitpunt
            // 
            this.cmbAansluitpunt.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbAansluitpunt.FormattingEnabled = true;
            this.cmbAansluitpunt.Location = new System.Drawing.Point(345, 124);
            this.cmbAansluitpunt.Name = "cmbAansluitpunt";
            this.cmbAansluitpunt.Size = new System.Drawing.Size(296, 21);
            this.cmbAansluitpunt.TabIndex = 8;
            this.cmbAansluitpunt.SelectedIndexChanged += new System.EventHandler(this.cmbAansluitpunt_SelectedIndexChanged);
            // 
            // lblOmschrijving
            // 
            this.lblOmschrijving.AutoSize = true;
            this.lblOmschrijving.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblOmschrijving.Location = new System.Drawing.Point(241, 162);
            this.lblOmschrijving.Name = "lblOmschrijving";
            this.lblOmschrijving.Size = new System.Drawing.Size(78, 54);
            this.lblOmschrijving.TabIndex = 9;
            this.lblOmschrijving.Text = "Omschrijving";
            this.lblOmschrijving.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblKabelType
            // 
            this.lblKabelType.AutoSize = true;
            this.lblKabelType.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblKabelType.Location = new System.Drawing.Point(258, 216);
            this.lblKabelType.Name = "lblKabelType";
            this.lblKabelType.Size = new System.Drawing.Size(61, 54);
            this.lblKabelType.TabIndex = 10;
            this.lblKabelType.Text = "Kabeltype";
            this.lblKabelType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblKabelsectie
            // 
            this.lblKabelsectie.AutoSize = true;
            this.lblKabelsectie.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblKabelsectie.Location = new System.Drawing.Point(248, 270);
            this.lblKabelsectie.Name = "lblKabelsectie";
            this.lblKabelsectie.Size = new System.Drawing.Size(71, 54);
            this.lblKabelsectie.TabIndex = 11;
            this.lblKabelsectie.Text = "Kabelsectie";
            this.lblKabelsectie.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblStroom
            // 
            this.lblStroom.AutoSize = true;
            this.lblStroom.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblStroom.Location = new System.Drawing.Point(254, 324);
            this.lblStroom.Name = "lblStroom";
            this.lblStroom.Size = new System.Drawing.Size(65, 54);
            this.lblStroom.TabIndex = 12;
            this.lblStroom.Text = "Stroom (A)";
            this.lblStroom.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPolen
            // 
            this.lblPolen.AutoSize = true;
            this.lblPolen.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblPolen.Location = new System.Drawing.Point(244, 378);
            this.lblPolen.Name = "lblPolen";
            this.lblPolen.Size = new System.Drawing.Size(75, 54);
            this.lblPolen.TabIndex = 13;
            this.lblPolen.Text = "Aantal polen";
            this.lblPolen.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtbxOmschrijving
            // 
            this.txtbxOmschrijving.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtbxOmschrijving.Location = new System.Drawing.Point(345, 179);
            this.txtbxOmschrijving.Name = "txtbxOmschrijving";
            this.txtbxOmschrijving.Size = new System.Drawing.Size(296, 20);
            this.txtbxOmschrijving.TabIndex = 14;
            // 
            // txtbxKabeltype
            // 
            this.txtbxKabeltype.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtbxKabeltype.Location = new System.Drawing.Point(345, 233);
            this.txtbxKabeltype.Name = "txtbxKabeltype";
            this.txtbxKabeltype.Size = new System.Drawing.Size(296, 20);
            this.txtbxKabeltype.TabIndex = 15;
            // 
            // txtbxKabelsectie
            // 
            this.txtbxKabelsectie.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtbxKabelsectie.Location = new System.Drawing.Point(345, 287);
            this.txtbxKabelsectie.Name = "txtbxKabelsectie";
            this.txtbxKabelsectie.Size = new System.Drawing.Size(296, 20);
            this.txtbxKabelsectie.TabIndex = 16;
            // 
            // txtbxStroom
            // 
            this.txtbxStroom.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtbxStroom.Location = new System.Drawing.Point(345, 341);
            this.txtbxStroom.Name = "txtbxStroom";
            this.txtbxStroom.Size = new System.Drawing.Size(296, 20);
            this.txtbxStroom.TabIndex = 17;
            this.txtbxStroom.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtbxStroom_KeyPress);
            // 
            // cmbPolen
            // 
            this.cmbPolen.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbPolen.FormattingEnabled = true;
            this.cmbPolen.Location = new System.Drawing.Point(345, 394);
            this.cmbPolen.Name = "cmbPolen";
            this.cmbPolen.Size = new System.Drawing.Size(296, 21);
            this.cmbPolen.TabIndex = 18;
            // 
            // lblKring
            // 
            this.lblKring.AutoSize = true;
            this.lblKring.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblKring.Location = new System.Drawing.Point(283, 0);
            this.lblKring.Name = "lblKring";
            this.lblKring.Size = new System.Drawing.Size(36, 54);
            this.lblKring.TabIndex = 19;
            this.lblKring.Text = "Kring";
            this.lblKring.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtbxKring
            // 
            this.txtbxKring.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtbxKring.Location = new System.Drawing.Point(345, 17);
            this.txtbxKring.Name = "txtbxKring";
            this.txtbxKring.Size = new System.Drawing.Size(296, 20);
            this.txtbxKring.TabIndex = 20;
            // 
            // AansluitingAanpassen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(671, 621);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AansluitingAanpassen";
            this.Text = "Aansluiting aanpassen";
            this.Load += new System.EventHandler(this.AansluitingAanpassen_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblTitel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
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