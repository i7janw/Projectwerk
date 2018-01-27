namespace Laagspanningsnet
{
    partial class Hoofdscherm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aansluitpuntToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.machineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.afdrukkenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvLaagspanningsnet = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnUndo = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblLayout = new System.Windows.Forms.Label();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.lblVoeding = new System.Windows.Forms.Label();
            this.btnDynVoeding = new System.Windows.Forms.Button();
            this.lblLocatie = new System.Windows.Forms.Label();
            this.lblDynLocatie = new System.Windows.Forms.Label();
            this.lblKabel = new System.Windows.Forms.Label();
            this.lblDynKabel = new System.Windows.Forms.Label();
            this.lblStroom = new System.Windows.Forms.Label();
            this.lblDynStroom = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLaagspanningsnet)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.menuStrip1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dgvLaagspanningsnet, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1194, 699);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(18, 18);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem,
            this.aansluitpuntToolStripMenuItem,
            this.machineToolStripMenuItem,
            this.afdrukkenToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1194, 27);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(50, 23);
            this.startToolStripMenuItem.Text = "Start";
            // 
            // aansluitpuntToolStripMenuItem
            // 
            this.aansluitpuntToolStripMenuItem.Name = "aansluitpuntToolStripMenuItem";
            this.aansluitpuntToolStripMenuItem.Size = new System.Drawing.Size(99, 23);
            this.aansluitpuntToolStripMenuItem.Text = "Aansluitpunt";
            // 
            // machineToolStripMenuItem
            // 
            this.machineToolStripMenuItem.Name = "machineToolStripMenuItem";
            this.machineToolStripMenuItem.Size = new System.Drawing.Size(73, 23);
            this.machineToolStripMenuItem.Text = "Machine";
            // 
            // afdrukkenToolStripMenuItem
            // 
            this.afdrukkenToolStripMenuItem.Name = "afdrukkenToolStripMenuItem";
            this.afdrukkenToolStripMenuItem.Size = new System.Drawing.Size(84, 23);
            this.afdrukkenToolStripMenuItem.Text = "Afdrukken";
            // 
            // dgvLaagspanningsnet
            // 
            this.dgvLaagspanningsnet.AllowUserToAddRows = false;
            this.dgvLaagspanningsnet.AllowUserToDeleteRows = false;
            this.dgvLaagspanningsnet.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLaagspanningsnet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLaagspanningsnet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLaagspanningsnet.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvLaagspanningsnet.Location = new System.Drawing.Point(3, 180);
            this.dgvLaagspanningsnet.MultiSelect = false;
            this.dgvLaagspanningsnet.Name = "dgvLaagspanningsnet";
            this.dgvLaagspanningsnet.RowHeadersVisible = false;
            this.dgvLaagspanningsnet.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLaagspanningsnet.Size = new System.Drawing.Size(1188, 566);
            this.dgvLaagspanningsnet.TabIndex = 1;
            this.dgvLaagspanningsnet.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLaagspanningsnet_CellContentClick);
            this.dgvLaagspanningsnet.SelectionChanged += new System.EventHandler(this.dgvLaagspanningsnet_SelectionChanged);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.btnUndo, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnSave, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.lblLayout, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 30);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1188, 144);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // btnUndo
            // 
            this.btnUndo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnUndo.Location = new System.Drawing.Point(497, 3);
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.Size = new System.Drawing.Size(194, 66);
            this.btnUndo.TabIndex = 0;
            this.btnUndo.Text = "Ongedaan Maken";
            this.btnUndo.UseVisualStyleBackColor = true;
            this.btnUndo.Click += new System.EventHandler(this.btnUndo_Click);
            // 
            // btnSave
            // 
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSave.Location = new System.Drawing.Point(497, 75);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(194, 66);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Opslaan";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblLayout
            // 
            this.lblLayout.AutoSize = true;
            this.lblLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLayout.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.73585F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLayout.Location = new System.Drawing.Point(3, 0);
            this.lblLayout.Name = "lblLayout";
            this.lblLayout.Size = new System.Drawing.Size(488, 72);
            this.lblLayout.TabIndex = 3;
            this.lblLayout.Text = "Layout van K810a";
            this.lblLayout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 4;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 62F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.lblVoeding, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnDynVoeding, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.lblLocatie, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.lblDynLocatie, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.lblKabel, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.lblDynKabel, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this.lblStroom, 2, 1);
            this.tableLayoutPanel3.Controls.Add(this.lblDynStroom, 3, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 75);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(488, 66);
            this.tableLayoutPanel3.TabIndex = 4;
            // 
            // lblVoeding
            // 
            this.lblVoeding.AutoSize = true;
            this.lblVoeding.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblVoeding.Location = new System.Drawing.Point(3, 0);
            this.lblVoeding.Name = "lblVoeding";
            this.lblVoeding.Size = new System.Drawing.Size(64, 33);
            this.lblVoeding.TabIndex = 0;
            this.lblVoeding.Text = "Voeding :";
            this.lblVoeding.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnDynVoeding
            // 
            this.btnDynVoeding.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnDynVoeding.Location = new System.Drawing.Point(73, 3);
            this.btnDynVoeding.Name = "btnDynVoeding";
            this.btnDynVoeding.Size = new System.Drawing.Size(118, 27);
            this.btnDynVoeding.TabIndex = 1;
            this.btnDynVoeding.Text = "?";
            this.btnDynVoeding.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDynVoeding.UseVisualStyleBackColor = true;
            this.btnDynVoeding.Click += new System.EventHandler(this.btnDynVoeding_Click);
            // 
            // lblLocatie
            // 
            this.lblLocatie.AutoSize = true;
            this.lblLocatie.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLocatie.Location = new System.Drawing.Point(3, 33);
            this.lblLocatie.Name = "lblLocatie";
            this.lblLocatie.Size = new System.Drawing.Size(64, 33);
            this.lblLocatie.TabIndex = 2;
            this.lblLocatie.Text = "Locatie :";
            this.lblLocatie.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDynLocatie
            // 
            this.lblDynLocatie.AutoSize = true;
            this.lblDynLocatie.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblDynLocatie.Location = new System.Drawing.Point(73, 33);
            this.lblDynLocatie.Name = "lblDynLocatie";
            this.lblDynLocatie.Size = new System.Drawing.Size(14, 33);
            this.lblDynLocatie.TabIndex = 3;
            this.lblDynLocatie.Text = "?";
            this.lblDynLocatie.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblKabel
            // 
            this.lblKabel.AutoSize = true;
            this.lblKabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblKabel.Location = new System.Drawing.Point(251, 0);
            this.lblKabel.Name = "lblKabel";
            this.lblKabel.Size = new System.Drawing.Size(56, 33);
            this.lblKabel.TabIndex = 4;
            this.lblKabel.Text = "Kabel :";
            this.lblKabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDynKabel
            // 
            this.lblDynKabel.AutoSize = true;
            this.lblDynKabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblDynKabel.Location = new System.Drawing.Point(313, 0);
            this.lblDynKabel.Name = "lblDynKabel";
            this.lblDynKabel.Size = new System.Drawing.Size(14, 33);
            this.lblDynKabel.TabIndex = 5;
            this.lblDynKabel.Text = "?";
            this.lblDynKabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStroom
            // 
            this.lblStroom.AutoSize = true;
            this.lblStroom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStroom.Location = new System.Drawing.Point(251, 33);
            this.lblStroom.Name = "lblStroom";
            this.lblStroom.Size = new System.Drawing.Size(56, 33);
            this.lblStroom.TabIndex = 6;
            this.lblStroom.Text = "Stroom :";
            this.lblStroom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDynStroom
            // 
            this.lblDynStroom.AutoSize = true;
            this.lblDynStroom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDynStroom.Location = new System.Drawing.Point(313, 33);
            this.lblDynStroom.Name = "lblDynStroom";
            this.lblDynStroom.Size = new System.Drawing.Size(172, 33);
            this.lblDynStroom.TabIndex = 7;
            this.lblDynStroom.Text = "?";
            this.lblDynStroom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1194, 699);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLaagspanningsnet)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aansluitpuntToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem machineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem afdrukkenToolStripMenuItem;
        private System.Windows.Forms.DataGridView dgvLaagspanningsnet;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btnUndo;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblLayout;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label lblVoeding;
        private System.Windows.Forms.Button btnDynVoeding;
        private System.Windows.Forms.Label lblLocatie;
        private System.Windows.Forms.Label lblDynLocatie;
        private System.Windows.Forms.Label lblKabel;
        private System.Windows.Forms.Label lblDynKabel;
        private System.Windows.Forms.Label lblStroom;
        private System.Windows.Forms.Label lblDynStroom;
    }
}

