﻿namespace Laagspanningsnet
{
    partial class MachineNieuw
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MachineNieuw));
            this.lblTitel = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtbxMachine = new System.Windows.Forms.TextBox();
            this.txtbxOmschrijving = new System.Windows.Forms.TextBox();
            this.txtbxLocatie = new System.Windows.Forms.TextBox();
            this.lblMachine = new System.Windows.Forms.Label();
            this.lblOmschrijving = new System.Windows.Forms.Label();
            this.lblLocatie = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTitel
            // 
            this.lblTitel.AutoSize = true;
            this.lblTitel.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.69811F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitel.Location = new System.Drawing.Point(12, 9);
            this.lblTitel.Name = "lblTitel";
            this.lblTitel.Size = new System.Drawing.Size(245, 33);
            this.lblTitel.TabIndex = 0;
            this.lblTitel.Text = "Nieuwe machine";
            this.lblTitel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(151, 179);
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
            this.btnCancel.Location = new System.Drawing.Point(232, 179);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Annuleren";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancelClick);
            // 
            // txtbxMachine
            // 
            this.txtbxMachine.Location = new System.Drawing.Point(99, 78);
            this.txtbxMachine.Name = "txtbxMachine";
            this.txtbxMachine.Size = new System.Drawing.Size(208, 20);
            this.txtbxMachine.TabIndex = 15;
            this.txtbxMachine.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtbxMachineKeyPress);
            // 
            // txtbxOmschrijving
            // 
            this.txtbxOmschrijving.Location = new System.Drawing.Point(99, 104);
            this.txtbxOmschrijving.Name = "txtbxOmschrijving";
            this.txtbxOmschrijving.Size = new System.Drawing.Size(208, 20);
            this.txtbxOmschrijving.TabIndex = 14;
            // 
            // txtbxLocatie
            // 
            this.txtbxLocatie.Location = new System.Drawing.Point(99, 130);
            this.txtbxLocatie.Name = "txtbxLocatie";
            this.txtbxLocatie.Size = new System.Drawing.Size(208, 20);
            this.txtbxLocatie.TabIndex = 16;
            // 
            // lblMachine
            // 
            this.lblMachine.AutoSize = true;
            this.lblMachine.Location = new System.Drawing.Point(23, 80);
            this.lblMachine.Name = "lblMachine";
            this.lblMachine.Size = new System.Drawing.Size(70, 15);
            this.lblMachine.TabIndex = 0;
            this.lblMachine.Text = "Machine ID";
            this.lblMachine.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblOmschrijving
            // 
            this.lblOmschrijving.AutoSize = true;
            this.lblOmschrijving.Location = new System.Drawing.Point(15, 106);
            this.lblOmschrijving.Name = "lblOmschrijving";
            this.lblOmschrijving.Size = new System.Drawing.Size(78, 15);
            this.lblOmschrijving.TabIndex = 9;
            this.lblOmschrijving.Text = "Omschrijving";
            this.lblOmschrijving.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblLocatie
            // 
            this.lblLocatie.AutoSize = true;
            this.lblLocatie.Location = new System.Drawing.Point(46, 132);
            this.lblLocatie.Name = "lblLocatie";
            this.lblLocatie.Size = new System.Drawing.Size(47, 15);
            this.lblLocatie.TabIndex = 17;
            this.lblLocatie.Text = "Locatie";
            this.lblLocatie.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // MachineNieuw
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(342, 225);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblTitel);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblLocatie);
            this.Controls.Add(this.lblOmschrijving);
            this.Controls.Add(this.lblMachine);
            this.Controls.Add(this.txtbxLocatie);
            this.Controls.Add(this.txtbxOmschrijving);
            this.Controls.Add(this.txtbxMachine);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MachineNieuw";
            this.Text = "Nieuwe Machine";
            this.Load += new System.EventHandler(this.MachineNieuwLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblTitel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblMachine;
        private System.Windows.Forms.Label lblOmschrijving;
        private System.Windows.Forms.TextBox txtbxOmschrijving;
        private System.Windows.Forms.TextBox txtbxMachine;
        private System.Windows.Forms.TextBox txtbxLocatie;
        private System.Windows.Forms.Label lblLocatie;
    }
}