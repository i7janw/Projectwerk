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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Hoofdscherm));
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.laagspanningsnetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.overzichtTransfosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.afdrukkenToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.afsluitenToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.aansluitpuntToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nieuwToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.aanpassenToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.verwijderenToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.hernoemenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.machineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nieuwToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aanpassenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.verwijderenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvLaagspanningsnet = new Laagspanningsnet.LaagspanningGridView();
            this.panel = new System.Windows.Forms.Panel();
            this.lblDynStroom = new System.Windows.Forms.Label();
            this.lblDynKabel = new System.Windows.Forms.Label();
            this.lblKabel = new System.Windows.Forms.Label();
            this.lblStroom = new System.Windows.Forms.Label();
            this.lblDynLocatie = new System.Windows.Forms.Label();
            this.btnDynVoeding = new System.Windows.Forms.Button();
            this.lblVoeding = new System.Windows.Forms.Label();
            this.lblLocatie = new System.Windows.Forms.Label();
            this.lblLayout = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtbxSearch = new System.Windows.Forms.TextBox();
            this.lblDynKruimelpad = new System.Windows.Forms.Label();
            this.lblKruimelpad = new System.Windows.Forms.Label();
            this.logo = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel.SuspendLayout();
            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLaagspanningsnet)).BeginInit();
            this.panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logo)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 1;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Controls.Add(this.dgvLaagspanningsnet, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.panel, 0, 0);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 2;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 175F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(1247, 727);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // menuStrip
            // 
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(18, 18);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.laagspanningsnetToolStripMenuItem,
            this.aansluitpuntToolStripMenuItem,
            this.machineToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1241, 27);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
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
            // afdrukkenToolStripMenuItem1
            // 
            this.afdrukkenToolStripMenuItem1.Name = "afdrukkenToolStripMenuItem1";
            this.afdrukkenToolStripMenuItem1.Size = new System.Drawing.Size(195, 24);
            this.afdrukkenToolStripMenuItem1.Text = "Afdrukken";
            this.afdrukkenToolStripMenuItem1.Click += new System.EventHandler(this.MenuAfdrukkenClick);
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
            this.nieuwToolStripMenuItem1.Size = new System.Drawing.Size(152, 24);
            this.nieuwToolStripMenuItem1.Text = "Nieuw";
            this.nieuwToolStripMenuItem1.Click += new System.EventHandler(this.MenuAansluitpuntNieuwClick);
            // 
            // aanpassenToolStripMenuItem1
            // 
            this.aanpassenToolStripMenuItem1.Name = "aanpassenToolStripMenuItem1";
            this.aanpassenToolStripMenuItem1.Size = new System.Drawing.Size(152, 24);
            this.aanpassenToolStripMenuItem1.Text = "Aanpassen";
            this.aanpassenToolStripMenuItem1.Click += new System.EventHandler(this.MenuAansluitpuntAanpassenClick);
            // 
            // verwijderenToolStripMenuItem1
            // 
            this.verwijderenToolStripMenuItem1.Name = "verwijderenToolStripMenuItem1";
            this.verwijderenToolStripMenuItem1.Size = new System.Drawing.Size(152, 24);
            this.verwijderenToolStripMenuItem1.Text = "Verwijderen";
            this.verwijderenToolStripMenuItem1.Click += new System.EventHandler(this.MenuAansluitpuntVerwijderenClick);
            // 
            // hernoemenToolStripMenuItem
            // 
            this.hernoemenToolStripMenuItem.Name = "hernoemenToolStripMenuItem";
            this.hernoemenToolStripMenuItem.Size = new System.Drawing.Size(152, 24);
            this.hernoemenToolStripMenuItem.Text = "Hernoemen";
            this.hernoemenToolStripMenuItem.Click += new System.EventHandler(this.MenuAansluitpuntHernoemenClick);
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
            this.dgvLaagspanningsnet.Location = new System.Drawing.Point(3, 178);
            this.dgvLaagspanningsnet.MultiSelect = false;
            this.dgvLaagspanningsnet.Name = "dgvLaagspanningsnet";
            this.dgvLaagspanningsnet.RowHeadersVisible = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Ivory;
            this.dgvLaagspanningsnet.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvLaagspanningsnet.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvLaagspanningsnet.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLaagspanningsnet.Size = new System.Drawing.Size(1241, 546);
            this.dgvLaagspanningsnet.TabIndex = 1;
            this.dgvLaagspanningsnet.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvLaagspanningsnetCellContentClick);
            this.dgvLaagspanningsnet.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvLaagspanningsnetCellValueChanged);
            this.dgvLaagspanningsnet.SelectionChanged += new System.EventHandler(this.DgvLaagspanningsnetSelectionChanged);
            // 
            // panel
            // 
            this.panel.Controls.Add(this.logo);
            this.panel.Controls.Add(this.lblKruimelpad);
            this.panel.Controls.Add(this.lblDynKruimelpad);
            this.panel.Controls.Add(this.menuStrip);
            this.panel.Controls.Add(this.lblDynStroom);
            this.panel.Controls.Add(this.lblDynKabel);
            this.panel.Controls.Add(this.lblKabel);
            this.panel.Controls.Add(this.lblStroom);
            this.panel.Controls.Add(this.lblDynLocatie);
            this.panel.Controls.Add(this.btnDynVoeding);
            this.panel.Controls.Add(this.lblVoeding);
            this.panel.Controls.Add(this.lblLocatie);
            this.panel.Controls.Add(this.lblLayout);
            this.panel.Controls.Add(this.btnSearch);
            this.panel.Controls.Add(this.txtbxSearch);
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(3, 3);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(1241, 169);
            this.panel.TabIndex = 3;
            // 
            // lblDynStroom
            // 
            this.lblDynStroom.AutoSize = true;
            this.lblDynStroom.Location = new System.Drawing.Point(286, 113);
            this.lblDynStroom.Name = "lblDynStroom";
            this.lblDynStroom.Size = new System.Drawing.Size(14, 15);
            this.lblDynStroom.TabIndex = 7;
            this.lblDynStroom.Text = "?";
            this.lblDynStroom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDynKabel
            // 
            this.lblDynKabel.AutoSize = true;
            this.lblDynKabel.Location = new System.Drawing.Point(286, 86);
            this.lblDynKabel.Name = "lblDynKabel";
            this.lblDynKabel.Size = new System.Drawing.Size(14, 15);
            this.lblDynKabel.TabIndex = 5;
            this.lblDynKabel.Text = "?";
            this.lblDynKabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblKabel
            // 
            this.lblKabel.AutoSize = true;
            this.lblKabel.Location = new System.Drawing.Point(235, 86);
            this.lblKabel.Name = "lblKabel";
            this.lblKabel.Size = new System.Drawing.Size(45, 15);
            this.lblKabel.TabIndex = 4;
            this.lblKabel.Text = "Kabel :";
            this.lblKabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStroom
            // 
            this.lblStroom.AutoSize = true;
            this.lblStroom.Location = new System.Drawing.Point(227, 113);
            this.lblStroom.Name = "lblStroom";
            this.lblStroom.Size = new System.Drawing.Size(53, 15);
            this.lblStroom.TabIndex = 6;
            this.lblStroom.Text = "Stroom :";
            this.lblStroom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDynLocatie
            // 
            this.lblDynLocatie.AutoSize = true;
            this.lblDynLocatie.Location = new System.Drawing.Point(76, 113);
            this.lblDynLocatie.Name = "lblDynLocatie";
            this.lblDynLocatie.Size = new System.Drawing.Size(14, 15);
            this.lblDynLocatie.TabIndex = 3;
            this.lblDynLocatie.Text = "?";
            this.lblDynLocatie.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnDynVoeding
            // 
            this.btnDynVoeding.Location = new System.Drawing.Point(76, 82);
            this.btnDynVoeding.Name = "btnDynVoeding";
            this.btnDynVoeding.Size = new System.Drawing.Size(118, 23);
            this.btnDynVoeding.TabIndex = 1;
            this.btnDynVoeding.Text = "?";
            this.btnDynVoeding.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDynVoeding.UseVisualStyleBackColor = true;
            this.btnDynVoeding.Click += new System.EventHandler(this.BtnDynVoedingClick);
            // 
            // lblVoeding
            // 
            this.lblVoeding.AutoSize = true;
            this.lblVoeding.Location = new System.Drawing.Point(12, 86);
            this.lblVoeding.Name = "lblVoeding";
            this.lblVoeding.Size = new System.Drawing.Size(58, 15);
            this.lblVoeding.TabIndex = 0;
            this.lblVoeding.Text = "Voeding :";
            this.lblVoeding.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblLocatie
            // 
            this.lblLocatie.AutoSize = true;
            this.lblLocatie.Location = new System.Drawing.Point(17, 113);
            this.lblLocatie.Name = "lblLocatie";
            this.lblLocatie.Size = new System.Drawing.Size(53, 15);
            this.lblLocatie.TabIndex = 2;
            this.lblLocatie.Text = "Locatie :";
            this.lblLocatie.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblLayout
            // 
            this.lblLayout.AutoSize = true;
            this.lblLayout.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.73585F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLayout.Location = new System.Drawing.Point(8, 30);
            this.lblLayout.Name = "lblLayout";
            this.lblLayout.Size = new System.Drawing.Size(296, 38);
            this.lblLayout.TabIndex = 3;
            this.lblLayout.Text = "Layout van K810a";
            this.lblLayout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSearch.Location = new System.Drawing.Point(745, 82);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(44, 23);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "Zoek";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.BtnSearchClick);
            // 
            // txtbxSearch
            // 
            this.txtbxSearch.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtbxSearch.Location = new System.Drawing.Point(545, 84);
            this.txtbxSearch.Name = "txtbxSearch";
            this.txtbxSearch.Size = new System.Drawing.Size(194, 20);
            this.txtbxSearch.TabIndex = 0;
            this.txtbxSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtbxSearchKeyDown);
            // 
            // lblDynKruimelpad
            // 
            this.lblDynKruimelpad.AutoSize = true;
            this.lblDynKruimelpad.Location = new System.Drawing.Point(76, 137);
            this.lblDynKruimelpad.Name = "lblDynKruimelpad";
            this.lblDynKruimelpad.Size = new System.Drawing.Size(71, 15);
            this.lblDynKruimelpad.TabIndex = 8;
            this.lblDynKruimelpad.Text = "Kruimelpad";
            // 
            // lblKruimelpad
            // 
            this.lblKruimelpad.AutoSize = true;
            this.lblKruimelpad.Location = new System.Drawing.Point(35, 137);
            this.lblKruimelpad.Name = "lblKruimelpad";
            this.lblKruimelpad.Size = new System.Drawing.Size(35, 15);
            this.lblKruimelpad.TabIndex = 9;
            this.lblKruimelpad.Text = "Pad :";
            // 
            // logo
            // 
            this.logo.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.logo.Image = global::Laagspanningsnet.Properties.Resources.logo_hansen_sumitomo;
            this.logo.Location = new System.Drawing.Point(977, 30);
            this.logo.Name = "logo";
            this.logo.Size = new System.Drawing.Size(261, 53);
            this.logo.TabIndex = 10;
            this.logo.TabStop = false;
            // 
            // Hoofdscherm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1247, 727);
            this.Controls.Add(this.tableLayoutPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.MinimumSize = new System.Drawing.Size(1024, 768);
            this.Name = "Hoofdscherm";
            this.Text = "Laagspanningsnet : Hansen Industrial Transmissions";
            this.Load += new System.EventHandler(this.Hoofdscherm_Load);
            this.tableLayoutPanel.ResumeLayout(false);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLaagspanningsnet)).EndInit();
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.MenuStrip menuStrip;
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
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.Label lblDynKruimelpad;
        private System.Windows.Forms.ToolStripMenuItem hernoemenToolStripMenuItem;
        private System.Windows.Forms.Label lblKruimelpad;
        private System.Windows.Forms.PictureBox logo;
    }
}

