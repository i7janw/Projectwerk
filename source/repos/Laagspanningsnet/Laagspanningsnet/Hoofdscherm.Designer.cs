﻿namespace Laagspanningsnet
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Hoofdscherm));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.laagspanningsnetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.overzichtTransfosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.afsluitenToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.aansluitpuntToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nieuwToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.aanpassenToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.verwijderenToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.machineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nieuwToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aanpassenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.verwijderenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblLayout = new System.Windows.Forms.Label();
            this.lblVoeding = new System.Windows.Forms.Label();
            this.btnDynVoeding = new System.Windows.Forms.Button();
            this.lblLocatie = new System.Windows.Forms.Label();
            this.lblDynLocatie = new System.Windows.Forms.Label();
            this.lblKabel = new System.Windows.Forms.Label();
            this.lblDynKabel = new System.Windows.Forms.Label();
            this.lblStroom = new System.Windows.Forms.Label();
            this.lblDynStroom = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtbxSearch = new System.Windows.Forms.TextBox();
            this.afdrukkenToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblKruimelpad = new System.Windows.Forms.Label();
            this.dgvLaagspanningsnet = new Laagspanningsnet.LaagspanningGridView();
            this.hernoemenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLaagspanningsnet)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.menuStrip1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dgvLaagspanningsnet, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1194, 699);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(18, 18);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.laagspanningsnetToolStripMenuItem,
            this.aansluitpuntToolStripMenuItem,
            this.machineToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1194, 27);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // laagspanningsnetToolStripMenuItem
            // 
            this.laagspanningsnetToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.overzichtTransfosToolStripMenuItem,
            this.afdrukkenToolStripMenuItem1,
            this.afsluitenToolStripMenuItem1});
            this.laagspanningsnetToolStripMenuItem.Name = "laagspanningsnetToolStripMenuItem";
            this.laagspanningsnetToolStripMenuItem.Size = new System.Drawing.Size(132, 23);
            this.laagspanningsnetToolStripMenuItem.Text = "Laagspanningsnet";
            // 
            // overzichtTransfosToolStripMenuItem
            // 
            this.overzichtTransfosToolStripMenuItem.Name = "overzichtTransfosToolStripMenuItem";
            this.overzichtTransfosToolStripMenuItem.Size = new System.Drawing.Size(195, 24);
            this.overzichtTransfosToolStripMenuItem.Text = "Overzicht Transfo\'s";
            this.overzichtTransfosToolStripMenuItem.Click += new System.EventHandler(this.MenuTransfoClick);
            // 
            // afsluitenToolStripMenuItem1
            // 
            this.afsluitenToolStripMenuItem1.Name = "afsluitenToolStripMenuItem1";
            this.afsluitenToolStripMenuItem1.Size = new System.Drawing.Size(195, 24);
            this.afsluitenToolStripMenuItem1.Text = "Afsluiten";
            this.afsluitenToolStripMenuItem1.Click += new System.EventHandler(this.MenuAfsluitenClick);
            // 
            // aansluitpuntToolStripMenuItem
            // 
            this.aansluitpuntToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nieuwToolStripMenuItem1,
            this.aanpassenToolStripMenuItem1,
            this.verwijderenToolStripMenuItem1,
            this.hernoemenToolStripMenuItem});
            this.aansluitpuntToolStripMenuItem.Name = "aansluitpuntToolStripMenuItem";
            this.aansluitpuntToolStripMenuItem.Size = new System.Drawing.Size(99, 23);
            this.aansluitpuntToolStripMenuItem.Text = "Aansluitpunt";
            // 
            // nieuwToolStripMenuItem1
            // 
            this.nieuwToolStripMenuItem1.Name = "nieuwToolStripMenuItem1";
            this.nieuwToolStripMenuItem1.Size = new System.Drawing.Size(194, 24);
            this.nieuwToolStripMenuItem1.Text = "Nieuw";
            this.nieuwToolStripMenuItem1.Click += new System.EventHandler(this.MenuAansluitpuntNieuwClick);
            // 
            // aanpassenToolStripMenuItem1
            // 
            this.aanpassenToolStripMenuItem1.Name = "aanpassenToolStripMenuItem1";
            this.aanpassenToolStripMenuItem1.Size = new System.Drawing.Size(194, 24);
            this.aanpassenToolStripMenuItem1.Text = "Aanpassen";
            this.aanpassenToolStripMenuItem1.Click += new System.EventHandler(this.MenuAansluitpuntAanpassenClick);
            // 
            // verwijderenToolStripMenuItem1
            // 
            this.verwijderenToolStripMenuItem1.Name = "verwijderenToolStripMenuItem1";
            this.verwijderenToolStripMenuItem1.Size = new System.Drawing.Size(194, 24);
            this.verwijderenToolStripMenuItem1.Text = "Verwijderen";
            this.verwijderenToolStripMenuItem1.Click += new System.EventHandler(this.MenuAansluitpuntVerwijderenClick);
            // 
            // machineToolStripMenuItem
            // 
            this.machineToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nieuwToolStripMenuItem,
            this.aanpassenToolStripMenuItem,
            this.verwijderenToolStripMenuItem});
            this.machineToolStripMenuItem.Name = "machineToolStripMenuItem";
            this.machineToolStripMenuItem.Size = new System.Drawing.Size(73, 23);
            this.machineToolStripMenuItem.Text = "Machine";
            // 
            // nieuwToolStripMenuItem
            // 
            this.nieuwToolStripMenuItem.Name = "nieuwToolStripMenuItem";
            this.nieuwToolStripMenuItem.Size = new System.Drawing.Size(151, 24);
            this.nieuwToolStripMenuItem.Text = "Nieuw";
            this.nieuwToolStripMenuItem.Click += new System.EventHandler(this.MenuMachineNieuwClick);
            // 
            // aanpassenToolStripMenuItem
            // 
            this.aanpassenToolStripMenuItem.Name = "aanpassenToolStripMenuItem";
            this.aanpassenToolStripMenuItem.Size = new System.Drawing.Size(151, 24);
            this.aanpassenToolStripMenuItem.Text = "Aanpassen";
            this.aanpassenToolStripMenuItem.Click += new System.EventHandler(this.MenuMachineAanpassenClick);
            // 
            // verwijderenToolStripMenuItem
            // 
            this.verwijderenToolStripMenuItem.Name = "verwijderenToolStripMenuItem";
            this.verwijderenToolStripMenuItem.Size = new System.Drawing.Size(151, 24);
            this.verwijderenToolStripMenuItem.Text = "Verwijderen";
            this.verwijderenToolStripMenuItem.Click += new System.EventHandler(this.MenuMachineVerwijderenClick);
            // 
            // lblLayout
            // 
            this.lblLayout.AutoSize = true;
            this.lblLayout.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.73585F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLayout.Location = new System.Drawing.Point(9, 12);
            this.lblLayout.Name = "lblLayout";
            this.lblLayout.Size = new System.Drawing.Size(296, 38);
            this.lblLayout.TabIndex = 3;
            this.lblLayout.Text = "Layout van K810a";
            this.lblLayout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblVoeding
            // 
            this.lblVoeding.AutoSize = true;
            this.lblVoeding.Location = new System.Drawing.Point(12, 67);
            this.lblVoeding.Name = "lblVoeding";
            this.lblVoeding.Size = new System.Drawing.Size(58, 15);
            this.lblVoeding.TabIndex = 0;
            this.lblVoeding.Text = "Voeding :";
            this.lblVoeding.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnDynVoeding
            // 
            this.btnDynVoeding.Location = new System.Drawing.Point(76, 63);
            this.btnDynVoeding.Name = "btnDynVoeding";
            this.btnDynVoeding.Size = new System.Drawing.Size(118, 23);
            this.btnDynVoeding.TabIndex = 1;
            this.btnDynVoeding.Text = "?";
            this.btnDynVoeding.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDynVoeding.UseVisualStyleBackColor = true;
            this.btnDynVoeding.Click += new System.EventHandler(this.BtnDynVoedingClick);
            // 
            // lblLocatie
            // 
            this.lblLocatie.AutoSize = true;
            this.lblLocatie.Location = new System.Drawing.Point(17, 97);
            this.lblLocatie.Name = "lblLocatie";
            this.lblLocatie.Size = new System.Drawing.Size(53, 15);
            this.lblLocatie.TabIndex = 2;
            this.lblLocatie.Text = "Locatie :";
            this.lblLocatie.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDynLocatie
            // 
            this.lblDynLocatie.AutoSize = true;
            this.lblDynLocatie.Location = new System.Drawing.Point(76, 97);
            this.lblDynLocatie.Name = "lblDynLocatie";
            this.lblDynLocatie.Size = new System.Drawing.Size(14, 15);
            this.lblDynLocatie.TabIndex = 3;
            this.lblDynLocatie.Text = "?";
            this.lblDynLocatie.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblKabel
            // 
            this.lblKabel.AutoSize = true;
            this.lblKabel.Location = new System.Drawing.Point(235, 67);
            this.lblKabel.Name = "lblKabel";
            this.lblKabel.Size = new System.Drawing.Size(45, 15);
            this.lblKabel.TabIndex = 4;
            this.lblKabel.Text = "Kabel :";
            this.lblKabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDynKabel
            // 
            this.lblDynKabel.AutoSize = true;
            this.lblDynKabel.Location = new System.Drawing.Point(286, 69);
            this.lblDynKabel.Name = "lblDynKabel";
            this.lblDynKabel.Size = new System.Drawing.Size(14, 15);
            this.lblDynKabel.TabIndex = 5;
            this.lblDynKabel.Text = "?";
            this.lblDynKabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStroom
            // 
            this.lblStroom.AutoSize = true;
            this.lblStroom.Location = new System.Drawing.Point(227, 97);
            this.lblStroom.Name = "lblStroom";
            this.lblStroom.Size = new System.Drawing.Size(53, 15);
            this.lblStroom.TabIndex = 6;
            this.lblStroom.Text = "Stroom :";
            this.lblStroom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDynStroom
            // 
            this.lblDynStroom.AutoSize = true;
            this.lblDynStroom.Location = new System.Drawing.Point(286, 97);
            this.lblDynStroom.Name = "lblDynStroom";
            this.lblDynStroom.Size = new System.Drawing.Size(14, 15);
            this.lblDynStroom.TabIndex = 7;
            this.lblDynStroom.Text = "?";
            this.lblDynStroom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnSearch.Location = new System.Drawing.Point(803, 61);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(44, 23);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "Zoek";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.BtnSearchClick);
            // 
            // txtbxSearch
            // 
            this.txtbxSearch.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtbxSearch.Location = new System.Drawing.Point(603, 63);
            this.txtbxSearch.Name = "txtbxSearch";
            this.txtbxSearch.Size = new System.Drawing.Size(194, 20);
            this.txtbxSearch.TabIndex = 0;
            this.txtbxSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtbxSearchKeyDown);
            // 
            // afdrukkenToolStripMenuItem1
            // 
            this.afdrukkenToolStripMenuItem1.Name = "afdrukkenToolStripMenuItem1";
            this.afdrukkenToolStripMenuItem1.Size = new System.Drawing.Size(195, 24);
            this.afdrukkenToolStripMenuItem1.Text = "Afdrukken";
            this.afdrukkenToolStripMenuItem1.Click += new System.EventHandler(this.MenuAfdrukkenClick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblDynStroom);
            this.panel1.Controls.Add(this.lblDynKabel);
            this.panel1.Controls.Add(this.lblKabel);
            this.panel1.Controls.Add(this.lblStroom);
            this.panel1.Controls.Add(this.lblDynLocatie);
            this.panel1.Controls.Add(this.btnDynVoeding);
            this.panel1.Controls.Add(this.lblVoeding);
            this.panel1.Controls.Add(this.lblLocatie);
            this.panel1.Controls.Add(this.lblLayout);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.txtbxSearch);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 30);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1188, 144);
            this.panel1.TabIndex = 3;
            // 
            // lblKruimelpad
            // 
            this.lblKruimelpad.AutoSize = true;
            this.lblKruimelpad.ForeColor = System.Drawing.Color.Green;
            this.lblKruimelpad.Location = new System.Drawing.Point(6, 162);
            this.lblKruimelpad.Name = "lblKruimelpad";
            this.lblKruimelpad.Size = new System.Drawing.Size(71, 15);
            this.lblKruimelpad.TabIndex = 8;
            this.lblKruimelpad.Text = "Kruimelpad";
            // 
            // dgvLaagspanningsnet
            // 
            this.dgvLaagspanningsnet.AllowUserToAddRows = false;
            this.dgvLaagspanningsnet.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dgvLaagspanningsnet.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvLaagspanningsnet.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLaagspanningsnet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLaagspanningsnet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLaagspanningsnet.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvLaagspanningsnet.Location = new System.Drawing.Point(3, 180);
            this.dgvLaagspanningsnet.MultiSelect = false;
            this.dgvLaagspanningsnet.Name = "dgvLaagspanningsnet";
            this.dgvLaagspanningsnet.RowHeadersVisible = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Ivory;
            this.dgvLaagspanningsnet.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvLaagspanningsnet.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvLaagspanningsnet.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLaagspanningsnet.Size = new System.Drawing.Size(1188, 516);
            this.dgvLaagspanningsnet.TabIndex = 1;
            this.dgvLaagspanningsnet.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvLaagspanningsnetCellContentClick);
            this.dgvLaagspanningsnet.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvLaagspanningsnetCellValueChanged);
            this.dgvLaagspanningsnet.SelectionChanged += new System.EventHandler(this.DgvLaagspanningsnetSelectionChanged);
            // 
            // hernoemenToolStripMenuItem
            // 
            this.hernoemenToolStripMenuItem.Name = "hernoemenToolStripMenuItem";
            this.hernoemenToolStripMenuItem.Size = new System.Drawing.Size(194, 24);
            this.hernoemenToolStripMenuItem.Text = "Hernoemen";
            this.hernoemenToolStripMenuItem.Click += new System.EventHandler(this.MenuAansluitpuntHernoemenClick);
            // 
            // Hoofdscherm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1194, 699);
            this.Controls.Add(this.lblKruimelpad);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Hoofdscherm";
            this.Text = "Laagspanningsnet : Hansen Industrial Transmissions";
            this.Load += new System.EventHandler(this.Hoofdscherm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLaagspanningsnet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem aansluitpuntToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem machineToolStripMenuItem;
        private LaagspanningGridView dgvLaagspanningsnet;
        private System.Windows.Forms.Label lblLayout;
        private System.Windows.Forms.Label lblVoeding;
        private System.Windows.Forms.Button btnDynVoeding;
        private System.Windows.Forms.Label lblLocatie;
        private System.Windows.Forms.Label lblDynLocatie;
        private System.Windows.Forms.Label lblKabel;
        private System.Windows.Forms.Label lblDynKabel;
        private System.Windows.Forms.Label lblStroom;
        private System.Windows.Forms.Label lblDynStroom;
        private System.Windows.Forms.TextBox txtbxSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ToolStripMenuItem nieuwToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aanpassenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem verwijderenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nieuwToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem aanpassenToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem verwijderenToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem laagspanningsnetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem overzichtTransfosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem afsluitenToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem afdrukkenToolStripMenuItem1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblKruimelpad;
        private System.Windows.Forms.ToolStripMenuItem hernoemenToolStripMenuItem;
    }
}

