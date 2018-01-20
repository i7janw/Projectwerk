namespace Laagspanningsnet
{
    partial class Form1
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
            this.tableLayoutPanel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLaagspanningsnet)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.menuStrip1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dgvLaagspanningsnet, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
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
            this.dgvLaagspanningsnet.Location = new System.Drawing.Point(3, 130);
            this.dgvLaagspanningsnet.MultiSelect = false;
            this.dgvLaagspanningsnet.Name = "dgvLaagspanningsnet";
            this.dgvLaagspanningsnet.RowHeadersVisible = false;
            this.dgvLaagspanningsnet.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLaagspanningsnet.Size = new System.Drawing.Size(1188, 566);
            this.dgvLaagspanningsnet.TabIndex = 1;
            this.dgvLaagspanningsnet.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLaagspanningsnet_CellContentClick);
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
    }
}

