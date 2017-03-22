namespace SNESTest3
{
    partial class metatileEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(metatileEditor));
            this.loMetatileBrowserPanel = new System.Windows.Forms.Panel();
            this.tileBrowserPanel = new System.Windows.Forms.Panel();
            this.tileBrowserPageSelector = new System.Windows.Forms.NumericUpDown();
            this.loMetatilePageSelector = new System.Windows.Forms.NumericUpDown();
            this.hiMetatileViewer = new System.Windows.Forms.Panel();
            this.groupTileBrowser = new System.Windows.Forms.GroupBox();
            this.tileBrowserHoverDisplay = new System.Windows.Forms.Label();
            this.labelPalette = new System.Windows.Forms.Label();
            this.tileBrowserPaletteSelector = new System.Windows.Forms.NumericUpDown();
            this.labelTileBrowserPage = new System.Windows.Forms.Label();
            this.groupBoxLoMetatileBrowser = new System.Windows.Forms.GroupBox();
            this.loMetatileBrowserHoverDisplay = new System.Windows.Forms.Label();
            this.labelLoMetatileBrowserPage = new System.Windows.Forms.Label();
            this.groupboxHiMetatile = new System.Windows.Forms.GroupBox();
            this.hiMetatileHoverDisplay = new System.Windows.Forms.Label();
            this.groupBoxLoMetatile = new System.Windows.Forms.GroupBox();
            this.checkTile3Priority = new System.Windows.Forms.CheckBox();
            this.checkTile2Priority = new System.Windows.Forms.CheckBox();
            this.checkTile1Priority = new System.Windows.Forms.CheckBox();
            this.checkTile0Priority = new System.Windows.Forms.CheckBox();
            this.labelTile3 = new System.Windows.Forms.Label();
            this.labelTile1 = new System.Windows.Forms.Label();
            this.tile3PaletteSelector = new System.Windows.Forms.NumericUpDown();
            this.tile1PaletteSelector = new System.Windows.Forms.NumericUpDown();
            this.tile2PaletteSelector = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.tile0PaletteSelector = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.labelTile0 = new System.Windows.Forms.Label();
            this.checkTile3FlipLR = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.checkTile1FlipLR = new System.Windows.Forms.CheckBox();
            this.checkTile2FlipLR = new System.Windows.Forms.CheckBox();
            this.labelTile2 = new System.Windows.Forms.Label();
            this.checkTile3FlipUD = new System.Windows.Forms.CheckBox();
            this.checkTile0FlipLR = new System.Windows.Forms.CheckBox();
            this.checkTile1FlipUD = new System.Windows.Forms.CheckBox();
            this.checkTile2FlipUD = new System.Windows.Forms.CheckBox();
            this.loMetatileViewerPanel = new System.Windows.Forms.Panel();
            this.checkTile0FlipUD = new System.Windows.Forms.CheckBox();
            this.checkSelectedTileFlipUD = new System.Windows.Forms.CheckBox();
            this.checkSelectedTileFlipLR = new System.Windows.Forms.CheckBox();
            this.selectedTilePaletteSelector = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxSelectedTile = new System.Windows.Forms.GroupBox();
            this.checkSelectedTilePriority = new System.Windows.Forms.CheckBox();
            this.tileViewer = new System.Windows.Forms.Panel();
            this.groupDebug = new System.Windows.Forms.GroupBox();
            this.labelDebugHiMetatileHover = new System.Windows.Forms.Label();
            this.labelDebugLoMetatileHover = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.metatilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.importLoMetatileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.tileBrowserPageSelector)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.loMetatilePageSelector)).BeginInit();
            this.groupTileBrowser.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tileBrowserPaletteSelector)).BeginInit();
            this.groupBoxLoMetatileBrowser.SuspendLayout();
            this.groupboxHiMetatile.SuspendLayout();
            this.groupBoxLoMetatile.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tile3PaletteSelector)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tile1PaletteSelector)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tile2PaletteSelector)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tile0PaletteSelector)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.selectedTilePaletteSelector)).BeginInit();
            this.groupBoxSelectedTile.SuspendLayout();
            this.groupDebug.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // loMetatileBrowserPanel
            // 
            this.loMetatileBrowserPanel.BackColor = System.Drawing.Color.Black;
            this.loMetatileBrowserPanel.Location = new System.Drawing.Point(6, 45);
            this.loMetatileBrowserPanel.Name = "loMetatileBrowserPanel";
            this.loMetatileBrowserPanel.Size = new System.Drawing.Size(512, 512);
            this.loMetatileBrowserPanel.TabIndex = 0;
            this.loMetatileBrowserPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.loMetatileBrowserPanel_Paint);
            this.loMetatileBrowserPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.loMetatileBrowserPanel_MouseClick);
            this.loMetatileBrowserPanel.MouseLeave += new System.EventHandler(this.loMetatileBrowserPanel_MouseLeave);
            this.loMetatileBrowserPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.metatileLoBrowser_MouseMove);
            // 
            // tileBrowserPanel
            // 
            this.tileBrowserPanel.BackColor = System.Drawing.Color.Black;
            this.tileBrowserPanel.Location = new System.Drawing.Point(7, 45);
            this.tileBrowserPanel.Name = "tileBrowserPanel";
            this.tileBrowserPanel.Size = new System.Drawing.Size(256, 512);
            this.tileBrowserPanel.TabIndex = 1;
            this.tileBrowserPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.tileBrowserPanel_Paint);
            this.tileBrowserPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tileBrowserPanel_MouseClick);
            this.tileBrowserPanel.MouseLeave += new System.EventHandler(this.tileBrowserPanel_MouseLeave);
            this.tileBrowserPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.tileBrowserPanel_MouseMove);
            // 
            // tileBrowserPageSelector
            // 
            this.tileBrowserPageSelector.Location = new System.Drawing.Point(6, 20);
            this.tileBrowserPageSelector.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.tileBrowserPageSelector.Name = "tileBrowserPageSelector";
            this.tileBrowserPageSelector.Size = new System.Drawing.Size(34, 20);
            this.tileBrowserPageSelector.TabIndex = 2;
            this.tileBrowserPageSelector.ValueChanged += new System.EventHandler(this.tileBrowserPageSelector_ValueChanged);
            // 
            // loMetatilePageSelector
            // 
            this.loMetatilePageSelector.Location = new System.Drawing.Point(6, 20);
            this.loMetatilePageSelector.Name = "loMetatilePageSelector";
            this.loMetatilePageSelector.Size = new System.Drawing.Size(56, 20);
            this.loMetatilePageSelector.TabIndex = 2;
            this.loMetatilePageSelector.ValueChanged += new System.EventHandler(this.loMetatilePageSelector_ValueChanged);
            // 
            // hiMetatileViewer
            // 
            this.hiMetatileViewer.BackColor = System.Drawing.Color.Black;
            this.hiMetatileViewer.Location = new System.Drawing.Point(15, 25);
            this.hiMetatileViewer.Name = "hiMetatileViewer";
            this.hiMetatileViewer.Size = new System.Drawing.Size(128, 128);
            this.hiMetatileViewer.TabIndex = 3;
            this.hiMetatileViewer.Paint += new System.Windows.Forms.PaintEventHandler(this.hiMetatileViewer_Paint);
            this.hiMetatileViewer.MouseClick += new System.Windows.Forms.MouseEventHandler(this.hiMetatileViewer_MouseClick);
            this.hiMetatileViewer.MouseLeave += new System.EventHandler(this.hiMetatileViewer_MouseLeave);
            this.hiMetatileViewer.MouseMove += new System.Windows.Forms.MouseEventHandler(this.hiMetatileViewer_MouseMove);
            // 
            // groupTileBrowser
            // 
            this.groupTileBrowser.Controls.Add(this.tileBrowserHoverDisplay);
            this.groupTileBrowser.Controls.Add(this.labelPalette);
            this.groupTileBrowser.Controls.Add(this.tileBrowserPaletteSelector);
            this.groupTileBrowser.Controls.Add(this.labelTileBrowserPage);
            this.groupTileBrowser.Controls.Add(this.tileBrowserPageSelector);
            this.groupTileBrowser.Controls.Add(this.tileBrowserPanel);
            this.groupTileBrowser.Location = new System.Drawing.Point(695, 32);
            this.groupTileBrowser.Name = "groupTileBrowser";
            this.groupTileBrowser.Size = new System.Drawing.Size(270, 564);
            this.groupTileBrowser.TabIndex = 4;
            this.groupTileBrowser.TabStop = false;
            this.groupTileBrowser.Text = "Tile Browser";
            // 
            // tileBrowserHoverDisplay
            // 
            this.tileBrowserHoverDisplay.AutoSize = true;
            this.tileBrowserHoverDisplay.Location = new System.Drawing.Point(216, 24);
            this.tileBrowserHoverDisplay.Name = "tileBrowserHoverDisplay";
            this.tileBrowserHoverDisplay.Size = new System.Drawing.Size(31, 13);
            this.tileBrowserHoverDisplay.TabIndex = 6;
            this.tileBrowserHoverDisplay.Text = "0000";
            // 
            // labelPalette
            // 
            this.labelPalette.AutoSize = true;
            this.labelPalette.Location = new System.Drawing.Point(106, 24);
            this.labelPalette.Name = "labelPalette";
            this.labelPalette.Size = new System.Drawing.Size(40, 13);
            this.labelPalette.TabIndex = 5;
            this.labelPalette.Text = "Palette";
            // 
            // tileBrowserPaletteSelector
            // 
            this.tileBrowserPaletteSelector.Location = new System.Drawing.Point(72, 20);
            this.tileBrowserPaletteSelector.Name = "tileBrowserPaletteSelector";
            this.tileBrowserPaletteSelector.Size = new System.Drawing.Size(34, 20);
            this.tileBrowserPaletteSelector.TabIndex = 4;
            this.tileBrowserPaletteSelector.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.tileBrowserPaletteSelector.ValueChanged += new System.EventHandler(this.tileBrowserPaletteSelector_ValueChanged);
            // 
            // labelTileBrowserPage
            // 
            this.labelTileBrowserPage.AutoSize = true;
            this.labelTileBrowserPage.Location = new System.Drawing.Point(40, 24);
            this.labelTileBrowserPage.Name = "labelTileBrowserPage";
            this.labelTileBrowserPage.Size = new System.Drawing.Size(32, 13);
            this.labelTileBrowserPage.TabIndex = 3;
            this.labelTileBrowserPage.Text = "Page";
            // 
            // groupBoxLoMetatileBrowser
            // 
            this.groupBoxLoMetatileBrowser.Controls.Add(this.loMetatileBrowserHoverDisplay);
            this.groupBoxLoMetatileBrowser.Controls.Add(this.loMetatilePageSelector);
            this.groupBoxLoMetatileBrowser.Controls.Add(this.loMetatileBrowserPanel);
            this.groupBoxLoMetatileBrowser.Controls.Add(this.labelLoMetatileBrowserPage);
            this.groupBoxLoMetatileBrowser.Location = new System.Drawing.Point(2, 32);
            this.groupBoxLoMetatileBrowser.Name = "groupBoxLoMetatileBrowser";
            this.groupBoxLoMetatileBrowser.Size = new System.Drawing.Size(525, 564);
            this.groupBoxLoMetatileBrowser.TabIndex = 5;
            this.groupBoxLoMetatileBrowser.TabStop = false;
            this.groupBoxLoMetatileBrowser.Text = "LoMetatile Browser";
            // 
            // loMetatileBrowserHoverDisplay
            // 
            this.loMetatileBrowserHoverDisplay.AutoSize = true;
            this.loMetatileBrowserHoverDisplay.Location = new System.Drawing.Point(476, 22);
            this.loMetatileBrowserHoverDisplay.Name = "loMetatileBrowserHoverDisplay";
            this.loMetatileBrowserHoverDisplay.Size = new System.Drawing.Size(31, 13);
            this.loMetatileBrowserHoverDisplay.TabIndex = 4;
            this.loMetatileBrowserHoverDisplay.Text = "0000";
            // 
            // labelLoMetatileBrowserPage
            // 
            this.labelLoMetatileBrowserPage.AutoSize = true;
            this.labelLoMetatileBrowserPage.Location = new System.Drawing.Point(68, 24);
            this.labelLoMetatileBrowserPage.Name = "labelLoMetatileBrowserPage";
            this.labelLoMetatileBrowserPage.Size = new System.Drawing.Size(32, 13);
            this.labelLoMetatileBrowserPage.TabIndex = 3;
            this.labelLoMetatileBrowserPage.Text = "Page";
            // 
            // groupboxHiMetatile
            // 
            this.groupboxHiMetatile.Controls.Add(this.hiMetatileHoverDisplay);
            this.groupboxHiMetatile.Controls.Add(this.hiMetatileViewer);
            this.groupboxHiMetatile.Location = new System.Drawing.Point(532, 32);
            this.groupboxHiMetatile.Name = "groupboxHiMetatile";
            this.groupboxHiMetatile.Size = new System.Drawing.Size(158, 158);
            this.groupboxHiMetatile.TabIndex = 6;
            this.groupboxHiMetatile.TabStop = false;
            this.groupboxHiMetatile.Text = "HiMetatile";
            // 
            // hiMetatileHoverDisplay
            // 
            this.hiMetatileHoverDisplay.AutoSize = true;
            this.hiMetatileHoverDisplay.Location = new System.Drawing.Point(111, 11);
            this.hiMetatileHoverDisplay.Name = "hiMetatileHoverDisplay";
            this.hiMetatileHoverDisplay.Size = new System.Drawing.Size(31, 13);
            this.hiMetatileHoverDisplay.TabIndex = 4;
            this.hiMetatileHoverDisplay.Text = "0000";
            // 
            // groupBoxLoMetatile
            // 
            this.groupBoxLoMetatile.Controls.Add(this.checkTile3Priority);
            this.groupBoxLoMetatile.Controls.Add(this.checkTile2Priority);
            this.groupBoxLoMetatile.Controls.Add(this.checkTile1Priority);
            this.groupBoxLoMetatile.Controls.Add(this.checkTile0Priority);
            this.groupBoxLoMetatile.Controls.Add(this.labelTile3);
            this.groupBoxLoMetatile.Controls.Add(this.labelTile1);
            this.groupBoxLoMetatile.Controls.Add(this.tile3PaletteSelector);
            this.groupBoxLoMetatile.Controls.Add(this.tile1PaletteSelector);
            this.groupBoxLoMetatile.Controls.Add(this.tile2PaletteSelector);
            this.groupBoxLoMetatile.Controls.Add(this.label6);
            this.groupBoxLoMetatile.Controls.Add(this.tile0PaletteSelector);
            this.groupBoxLoMetatile.Controls.Add(this.label4);
            this.groupBoxLoMetatile.Controls.Add(this.label5);
            this.groupBoxLoMetatile.Controls.Add(this.labelTile0);
            this.groupBoxLoMetatile.Controls.Add(this.checkTile3FlipLR);
            this.groupBoxLoMetatile.Controls.Add(this.label3);
            this.groupBoxLoMetatile.Controls.Add(this.checkTile1FlipLR);
            this.groupBoxLoMetatile.Controls.Add(this.checkTile2FlipLR);
            this.groupBoxLoMetatile.Controls.Add(this.labelTile2);
            this.groupBoxLoMetatile.Controls.Add(this.checkTile3FlipUD);
            this.groupBoxLoMetatile.Controls.Add(this.checkTile0FlipLR);
            this.groupBoxLoMetatile.Controls.Add(this.checkTile1FlipUD);
            this.groupBoxLoMetatile.Controls.Add(this.checkTile2FlipUD);
            this.groupBoxLoMetatile.Controls.Add(this.loMetatileViewerPanel);
            this.groupBoxLoMetatile.Controls.Add(this.checkTile0FlipUD);
            this.groupBoxLoMetatile.Location = new System.Drawing.Point(532, 190);
            this.groupBoxLoMetatile.Name = "groupBoxLoMetatile";
            this.groupBoxLoMetatile.Size = new System.Drawing.Size(158, 319);
            this.groupBoxLoMetatile.TabIndex = 7;
            this.groupBoxLoMetatile.TabStop = false;
            this.groupBoxLoMetatile.Text = "LoMetatile";
            // 
            // checkTile3Priority
            // 
            this.checkTile3Priority.AutoSize = true;
            this.checkTile3Priority.Location = new System.Drawing.Point(83, 267);
            this.checkTile3Priority.Name = "checkTile3Priority";
            this.checkTile3Priority.Size = new System.Drawing.Size(57, 17);
            this.checkTile3Priority.TabIndex = 8;
            this.checkTile3Priority.Text = "Priority";
            this.checkTile3Priority.UseVisualStyleBackColor = true;
            this.checkTile3Priority.CheckedChanged += new System.EventHandler(this.checkTile3Priority_CheckedChanged);
            this.checkTile3Priority.Click += new System.EventHandler(this.checkTile3Priority_Click);
            // 
            // checkTile2Priority
            // 
            this.checkTile2Priority.AutoSize = true;
            this.checkTile2Priority.Location = new System.Drawing.Point(6, 267);
            this.checkTile2Priority.Name = "checkTile2Priority";
            this.checkTile2Priority.Size = new System.Drawing.Size(57, 17);
            this.checkTile2Priority.TabIndex = 7;
            this.checkTile2Priority.Text = "Priority";
            this.checkTile2Priority.UseVisualStyleBackColor = true;
            this.checkTile2Priority.CheckedChanged += new System.EventHandler(this.checkTile2Priority_CheckedChanged);
            this.checkTile2Priority.Click += new System.EventHandler(this.checkTile2Priority_Click);
            // 
            // checkTile1Priority
            // 
            this.checkTile1Priority.AutoSize = true;
            this.checkTile1Priority.Location = new System.Drawing.Point(83, 37);
            this.checkTile1Priority.Name = "checkTile1Priority";
            this.checkTile1Priority.Size = new System.Drawing.Size(57, 17);
            this.checkTile1Priority.TabIndex = 6;
            this.checkTile1Priority.Text = "Priority";
            this.checkTile1Priority.UseVisualStyleBackColor = true;
            this.checkTile1Priority.CheckedChanged += new System.EventHandler(this.checkTile1Priority_CheckedChanged);
            this.checkTile1Priority.Click += new System.EventHandler(this.checkTile1Priority_Click);
            // 
            // checkTile0Priority
            // 
            this.checkTile0Priority.AutoSize = true;
            this.checkTile0Priority.Location = new System.Drawing.Point(6, 37);
            this.checkTile0Priority.Name = "checkTile0Priority";
            this.checkTile0Priority.Size = new System.Drawing.Size(57, 17);
            this.checkTile0Priority.TabIndex = 5;
            this.checkTile0Priority.Text = "Priority";
            this.checkTile0Priority.UseVisualStyleBackColor = true;
            this.checkTile0Priority.CheckedChanged += new System.EventHandler(this.checkTile0Priority_CheckedChanged);
            this.checkTile0Priority.Click += new System.EventHandler(this.checkTile0Priority_Click);
            // 
            // labelTile3
            // 
            this.labelTile3.AutoSize = true;
            this.labelTile3.Location = new System.Drawing.Point(112, 231);
            this.labelTile3.Name = "labelTile3";
            this.labelTile3.Size = new System.Drawing.Size(26, 13);
            this.labelTile3.TabIndex = 4;
            this.labelTile3.Text = "tile3";
            // 
            // labelTile1
            // 
            this.labelTile1.AutoSize = true;
            this.labelTile1.Location = new System.Drawing.Point(112, 84);
            this.labelTile1.Name = "labelTile1";
            this.labelTile1.Size = new System.Drawing.Size(26, 13);
            this.labelTile1.TabIndex = 4;
            this.labelTile1.Text = "tile1";
            // 
            // tile3PaletteSelector
            // 
            this.tile3PaletteSelector.Location = new System.Drawing.Point(83, 246);
            this.tile3PaletteSelector.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.tile3PaletteSelector.Name = "tile3PaletteSelector";
            this.tile3PaletteSelector.Size = new System.Drawing.Size(32, 20);
            this.tile3PaletteSelector.TabIndex = 2;
            this.tile3PaletteSelector.ValueChanged += new System.EventHandler(this.tile3PaletteSelector_ValueChanged);
            // 
            // tile1PaletteSelector
            // 
            this.tile1PaletteSelector.Location = new System.Drawing.Point(83, 16);
            this.tile1PaletteSelector.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.tile1PaletteSelector.Name = "tile1PaletteSelector";
            this.tile1PaletteSelector.Size = new System.Drawing.Size(32, 20);
            this.tile1PaletteSelector.TabIndex = 2;
            this.tile1PaletteSelector.ValueChanged += new System.EventHandler(this.tile1PaletteSelector_ValueChanged);
            // 
            // tile2PaletteSelector
            // 
            this.tile2PaletteSelector.Location = new System.Drawing.Point(6, 246);
            this.tile2PaletteSelector.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.tile2PaletteSelector.Name = "tile2PaletteSelector";
            this.tile2PaletteSelector.Size = new System.Drawing.Size(32, 20);
            this.tile2PaletteSelector.TabIndex = 2;
            this.tile2PaletteSelector.ValueChanged += new System.EventHandler(this.tile2PaletteSelector_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(116, 250);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "Palette";
            // 
            // tile0PaletteSelector
            // 
            this.tile0PaletteSelector.Location = new System.Drawing.Point(6, 16);
            this.tile0PaletteSelector.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.tile0PaletteSelector.Name = "tile0PaletteSelector";
            this.tile0PaletteSelector.Size = new System.Drawing.Size(32, 20);
            this.tile0PaletteSelector.TabIndex = 2;
            this.tile0PaletteSelector.ValueChanged += new System.EventHandler(this.tile0PaletteSelector_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(116, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Palette";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(39, 250);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Palette";
            // 
            // labelTile0
            // 
            this.labelTile0.AutoSize = true;
            this.labelTile0.Location = new System.Drawing.Point(15, 84);
            this.labelTile0.Name = "labelTile0";
            this.labelTile0.Size = new System.Drawing.Size(26, 13);
            this.labelTile0.TabIndex = 4;
            this.labelTile0.Text = "tile0";
            // 
            // checkTile3FlipLR
            // 
            this.checkTile3FlipLR.AutoSize = true;
            this.checkTile3FlipLR.Location = new System.Drawing.Point(83, 299);
            this.checkTile3FlipLR.Name = "checkTile3FlipLR";
            this.checkTile3FlipLR.Size = new System.Drawing.Size(64, 17);
            this.checkTile3FlipLR.TabIndex = 1;
            this.checkTile3FlipLR.Text = "L/R Flip";
            this.checkTile3FlipLR.UseVisualStyleBackColor = true;
            this.checkTile3FlipLR.CheckedChanged += new System.EventHandler(this.checkTile3FlipLR_CheckedChanged);
            this.checkTile3FlipLR.Click += new System.EventHandler(this.checkTile3FlipLR_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(39, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Palette";
            // 
            // checkTile1FlipLR
            // 
            this.checkTile1FlipLR.AutoSize = true;
            this.checkTile1FlipLR.Location = new System.Drawing.Point(83, 69);
            this.checkTile1FlipLR.Name = "checkTile1FlipLR";
            this.checkTile1FlipLR.Size = new System.Drawing.Size(64, 17);
            this.checkTile1FlipLR.TabIndex = 1;
            this.checkTile1FlipLR.Text = "L/R Flip";
            this.checkTile1FlipLR.UseVisualStyleBackColor = true;
            this.checkTile1FlipLR.CheckedChanged += new System.EventHandler(this.checkTile1FlipLR_CheckedChanged);
            this.checkTile1FlipLR.Click += new System.EventHandler(this.checkTile1FlipLR_Click);
            // 
            // checkTile2FlipLR
            // 
            this.checkTile2FlipLR.AutoSize = true;
            this.checkTile2FlipLR.Location = new System.Drawing.Point(6, 299);
            this.checkTile2FlipLR.Name = "checkTile2FlipLR";
            this.checkTile2FlipLR.Size = new System.Drawing.Size(64, 17);
            this.checkTile2FlipLR.TabIndex = 1;
            this.checkTile2FlipLR.Text = "L/R Flip";
            this.checkTile2FlipLR.UseVisualStyleBackColor = true;
            this.checkTile2FlipLR.CheckedChanged += new System.EventHandler(this.checkTile2FlipLR_CheckedChanged);
            this.checkTile2FlipLR.Click += new System.EventHandler(this.checkTile2FlipLR_Click);
            // 
            // labelTile2
            // 
            this.labelTile2.AutoSize = true;
            this.labelTile2.Location = new System.Drawing.Point(15, 231);
            this.labelTile2.Name = "labelTile2";
            this.labelTile2.Size = new System.Drawing.Size(26, 13);
            this.labelTile2.TabIndex = 4;
            this.labelTile2.Text = "tile2";
            // 
            // checkTile3FlipUD
            // 
            this.checkTile3FlipUD.AutoSize = true;
            this.checkTile3FlipUD.Location = new System.Drawing.Point(83, 283);
            this.checkTile3FlipUD.Name = "checkTile3FlipUD";
            this.checkTile3FlipUD.Size = new System.Drawing.Size(66, 17);
            this.checkTile3FlipUD.TabIndex = 1;
            this.checkTile3FlipUD.Text = "U/D Flip";
            this.checkTile3FlipUD.UseVisualStyleBackColor = true;
            this.checkTile3FlipUD.CheckedChanged += new System.EventHandler(this.checkTile3FlipUD_CheckedChanged);
            this.checkTile3FlipUD.Click += new System.EventHandler(this.checkTile3FlipUD_Click);
            // 
            // checkTile0FlipLR
            // 
            this.checkTile0FlipLR.AutoSize = true;
            this.checkTile0FlipLR.Location = new System.Drawing.Point(6, 69);
            this.checkTile0FlipLR.Name = "checkTile0FlipLR";
            this.checkTile0FlipLR.Size = new System.Drawing.Size(64, 17);
            this.checkTile0FlipLR.TabIndex = 1;
            this.checkTile0FlipLR.Text = "L/R Flip";
            this.checkTile0FlipLR.UseVisualStyleBackColor = true;
            this.checkTile0FlipLR.CheckedChanged += new System.EventHandler(this.checkTile0FlipLR_CheckedChanged);
            this.checkTile0FlipLR.Click += new System.EventHandler(this.checkTile0FlipLR_Click);
            // 
            // checkTile1FlipUD
            // 
            this.checkTile1FlipUD.AutoSize = true;
            this.checkTile1FlipUD.Location = new System.Drawing.Point(83, 53);
            this.checkTile1FlipUD.Name = "checkTile1FlipUD";
            this.checkTile1FlipUD.Size = new System.Drawing.Size(66, 17);
            this.checkTile1FlipUD.TabIndex = 1;
            this.checkTile1FlipUD.Text = "U/D Flip";
            this.checkTile1FlipUD.UseVisualStyleBackColor = true;
            this.checkTile1FlipUD.CheckedChanged += new System.EventHandler(this.checkTile1FlipUD_CheckedChanged);
            this.checkTile1FlipUD.Click += new System.EventHandler(this.checkTile1FlipUD_Click);
            // 
            // checkTile2FlipUD
            // 
            this.checkTile2FlipUD.AutoSize = true;
            this.checkTile2FlipUD.Location = new System.Drawing.Point(6, 283);
            this.checkTile2FlipUD.Name = "checkTile2FlipUD";
            this.checkTile2FlipUD.Size = new System.Drawing.Size(66, 17);
            this.checkTile2FlipUD.TabIndex = 1;
            this.checkTile2FlipUD.Text = "U/D Flip";
            this.checkTile2FlipUD.UseVisualStyleBackColor = true;
            this.checkTile2FlipUD.CheckedChanged += new System.EventHandler(this.checkTile2FlipUD_CheckedChanged);
            this.checkTile2FlipUD.Click += new System.EventHandler(this.checkTile2FlipUD_Click);
            // 
            // loMetatileViewerPanel
            // 
            this.loMetatileViewerPanel.BackColor = System.Drawing.Color.Black;
            this.loMetatileViewerPanel.Location = new System.Drawing.Point(14, 100);
            this.loMetatileViewerPanel.Name = "loMetatileViewerPanel";
            this.loMetatileViewerPanel.Size = new System.Drawing.Size(128, 128);
            this.loMetatileViewerPanel.TabIndex = 0;
            this.loMetatileViewerPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.loMetatileViewerPanel_Paint);
            this.loMetatileViewerPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.loMetatileViewerPanel_MouseClick);
            this.loMetatileViewerPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.loMetatileViewerPanel_MouseMove);
            // 
            // checkTile0FlipUD
            // 
            this.checkTile0FlipUD.AutoSize = true;
            this.checkTile0FlipUD.Location = new System.Drawing.Point(6, 53);
            this.checkTile0FlipUD.Name = "checkTile0FlipUD";
            this.checkTile0FlipUD.Size = new System.Drawing.Size(66, 17);
            this.checkTile0FlipUD.TabIndex = 1;
            this.checkTile0FlipUD.Text = "U/D Flip";
            this.checkTile0FlipUD.UseVisualStyleBackColor = true;
            this.checkTile0FlipUD.CheckedChanged += new System.EventHandler(this.checkTile0FlipUD_CheckedChanged);
            this.checkTile0FlipUD.Click += new System.EventHandler(this.checkTile0FlipUD_Click);
            // 
            // checkSelectedTileFlipUD
            // 
            this.checkSelectedTileFlipUD.AutoSize = true;
            this.checkSelectedTileFlipUD.Location = new System.Drawing.Point(79, 52);
            this.checkSelectedTileFlipUD.Name = "checkSelectedTileFlipUD";
            this.checkSelectedTileFlipUD.Size = new System.Drawing.Size(66, 17);
            this.checkSelectedTileFlipUD.TabIndex = 1;
            this.checkSelectedTileFlipUD.Text = "U/D Flip";
            this.checkSelectedTileFlipUD.UseVisualStyleBackColor = true;
            this.checkSelectedTileFlipUD.Click += new System.EventHandler(this.checkSelectedTileFlipUD_Click);
            // 
            // checkSelectedTileFlipLR
            // 
            this.checkSelectedTileFlipLR.AutoSize = true;
            this.checkSelectedTileFlipLR.Location = new System.Drawing.Point(79, 68);
            this.checkSelectedTileFlipLR.Name = "checkSelectedTileFlipLR";
            this.checkSelectedTileFlipLR.Size = new System.Drawing.Size(64, 17);
            this.checkSelectedTileFlipLR.TabIndex = 1;
            this.checkSelectedTileFlipLR.Text = "L/R Flip";
            this.checkSelectedTileFlipLR.UseVisualStyleBackColor = true;
            this.checkSelectedTileFlipLR.Click += new System.EventHandler(this.checkSelectedTileFlipLR_Click);
            // 
            // selectedTilePaletteSelector
            // 
            this.selectedTilePaletteSelector.Location = new System.Drawing.Point(79, 15);
            this.selectedTilePaletteSelector.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.selectedTilePaletteSelector.Name = "selectedTilePaletteSelector";
            this.selectedTilePaletteSelector.Size = new System.Drawing.Size(32, 20);
            this.selectedTilePaletteSelector.TabIndex = 2;
            this.selectedTilePaletteSelector.ValueChanged += new System.EventHandler(this.selectedTilePaletteSelector_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(112, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Palette";
            // 
            // groupBoxSelectedTile
            // 
            this.groupBoxSelectedTile.Controls.Add(this.checkSelectedTilePriority);
            this.groupBoxSelectedTile.Controls.Add(this.tileViewer);
            this.groupBoxSelectedTile.Controls.Add(this.selectedTilePaletteSelector);
            this.groupBoxSelectedTile.Controls.Add(this.label1);
            this.groupBoxSelectedTile.Controls.Add(this.checkSelectedTileFlipLR);
            this.groupBoxSelectedTile.Controls.Add(this.checkSelectedTileFlipUD);
            this.groupBoxSelectedTile.Location = new System.Drawing.Point(532, 509);
            this.groupBoxSelectedTile.Name = "groupBoxSelectedTile";
            this.groupBoxSelectedTile.Size = new System.Drawing.Size(158, 87);
            this.groupBoxSelectedTile.TabIndex = 8;
            this.groupBoxSelectedTile.TabStop = false;
            this.groupBoxSelectedTile.Text = "Selected Tile";
            // 
            // checkSelectedTilePriority
            // 
            this.checkSelectedTilePriority.AutoSize = true;
            this.checkSelectedTilePriority.Location = new System.Drawing.Point(79, 36);
            this.checkSelectedTilePriority.Name = "checkSelectedTilePriority";
            this.checkSelectedTilePriority.Size = new System.Drawing.Size(57, 17);
            this.checkSelectedTilePriority.TabIndex = 9;
            this.checkSelectedTilePriority.Text = "Priority";
            this.checkSelectedTilePriority.UseVisualStyleBackColor = true;
            this.checkSelectedTilePriority.Click += new System.EventHandler(this.checkSelectedTilePriority_Click);
            // 
            // tileViewer
            // 
            this.tileViewer.BackColor = System.Drawing.Color.Black;
            this.tileViewer.Location = new System.Drawing.Point(9, 16);
            this.tileViewer.Name = "tileViewer";
            this.tileViewer.Size = new System.Drawing.Size(64, 64);
            this.tileViewer.TabIndex = 0;
            this.tileViewer.Paint += new System.Windows.Forms.PaintEventHandler(this.tileViewer_Paint);
            // 
            // groupDebug
            // 
            this.groupDebug.Controls.Add(this.labelDebugHiMetatileHover);
            this.groupDebug.Controls.Add(this.labelDebugLoMetatileHover);
            this.groupDebug.Location = new System.Drawing.Point(1, 613);
            this.groupDebug.Name = "groupDebug";
            this.groupDebug.Size = new System.Drawing.Size(963, 100);
            this.groupDebug.TabIndex = 9;
            this.groupDebug.TabStop = false;
            this.groupDebug.Text = "Debug Shit";
            // 
            // labelDebugHiMetatileHover
            // 
            this.labelDebugHiMetatileHover.AutoSize = true;
            this.labelDebugHiMetatileHover.Location = new System.Drawing.Point(12, 37);
            this.labelDebugHiMetatileHover.Name = "labelDebugHiMetatileHover";
            this.labelDebugHiMetatileHover.Size = new System.Drawing.Size(90, 13);
            this.labelDebugHiMetatileHover.TabIndex = 1;
            this.labelDebugHiMetatileHover.Text = "hiMetatileHover =";
            // 
            // labelDebugLoMetatileHover
            // 
            this.labelDebugLoMetatileHover.AutoSize = true;
            this.labelDebugLoMetatileHover.Location = new System.Drawing.Point(12, 20);
            this.labelDebugLoMetatileHover.Name = "labelDebugLoMetatileHover";
            this.labelDebugLoMetatileHover.Size = new System.Drawing.Size(93, 13);
            this.labelDebugLoMetatileHover.TabIndex = 0;
            this.labelDebugLoMetatileHover.Text = "loMetatileHover = ";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.metatilesToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuStrip1.Size = new System.Drawing.Size(967, 24);
            this.menuStrip1.TabIndex = 10;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // metatilesToolStripMenuItem
            // 
            this.metatilesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importLoMetatileToolStripMenuItem});
            this.metatilesToolStripMenuItem.Name = "metatilesToolStripMenuItem";
            this.metatilesToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.metatilesToolStripMenuItem.Text = "Metatiles";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem1});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // helpToolStripMenuItem1
            // 
            this.helpToolStripMenuItem1.Name = "helpToolStripMenuItem1";
            this.helpToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.helpToolStripMenuItem1.Text = "Help";
            this.helpToolStripMenuItem1.Click += new System.EventHandler(this.helpToolStripMenuItem1_Click);
            // 
            // importLoMetatileToolStripMenuItem
            // 
            this.importLoMetatileToolStripMenuItem.Name = "importLoMetatileToolStripMenuItem";
            this.importLoMetatileToolStripMenuItem.Size = new System.Drawing.Size(247, 22);
            this.importLoMetatileToolStripMenuItem.Text = "Import loMetatile from another bank";
            // 
            // metatileEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(967, 598);
            this.Controls.Add(this.groupDebug);
            this.Controls.Add(this.groupBoxSelectedTile);
            this.Controls.Add(this.groupBoxLoMetatile);
            this.Controls.Add(this.groupboxHiMetatile);
            this.Controls.Add(this.groupBoxLoMetatileBrowser);
            this.Controls.Add(this.groupTileBrowser);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "metatileEditor";
            this.ShowIcon = false;
            this.Text = "Metatile Editor";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.metatileEditor_FormClosed);
            this.Load += new System.EventHandler(this.metatileEditor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tileBrowserPageSelector)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.loMetatilePageSelector)).EndInit();
            this.groupTileBrowser.ResumeLayout(false);
            this.groupTileBrowser.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tileBrowserPaletteSelector)).EndInit();
            this.groupBoxLoMetatileBrowser.ResumeLayout(false);
            this.groupBoxLoMetatileBrowser.PerformLayout();
            this.groupboxHiMetatile.ResumeLayout(false);
            this.groupboxHiMetatile.PerformLayout();
            this.groupBoxLoMetatile.ResumeLayout(false);
            this.groupBoxLoMetatile.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tile3PaletteSelector)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tile1PaletteSelector)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tile2PaletteSelector)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tile0PaletteSelector)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.selectedTilePaletteSelector)).EndInit();
            this.groupBoxSelectedTile.ResumeLayout(false);
            this.groupBoxSelectedTile.PerformLayout();
            this.groupDebug.ResumeLayout(false);
            this.groupDebug.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel loMetatileBrowserPanel;
        private System.Windows.Forms.Panel tileBrowserPanel;
        private System.Windows.Forms.NumericUpDown tileBrowserPageSelector;
        private System.Windows.Forms.NumericUpDown loMetatilePageSelector;
        private System.Windows.Forms.Panel hiMetatileViewer;
        private System.Windows.Forms.GroupBox groupTileBrowser;
        private System.Windows.Forms.Label labelPalette;
        private System.Windows.Forms.NumericUpDown tileBrowserPaletteSelector;
        private System.Windows.Forms.Label labelTileBrowserPage;
        private System.Windows.Forms.GroupBox groupBoxLoMetatileBrowser;
        private System.Windows.Forms.Label labelLoMetatileBrowserPage;
        private System.Windows.Forms.GroupBox groupboxHiMetatile;
        private System.Windows.Forms.GroupBox groupBoxLoMetatile;
        private System.Windows.Forms.Panel loMetatileViewerPanel;
        private System.Windows.Forms.Label labelTile3;
        private System.Windows.Forms.Label labelTile1;
        private System.Windows.Forms.NumericUpDown tile3PaletteSelector;
        private System.Windows.Forms.NumericUpDown tile1PaletteSelector;
        private System.Windows.Forms.NumericUpDown tile2PaletteSelector;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown tile0PaletteSelector;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labelTile0;
        private System.Windows.Forms.CheckBox checkTile3FlipLR;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkTile1FlipLR;
        private System.Windows.Forms.CheckBox checkTile2FlipLR;
        private System.Windows.Forms.Label labelTile2;
        private System.Windows.Forms.CheckBox checkTile3FlipUD;
        private System.Windows.Forms.CheckBox checkTile0FlipLR;
        private System.Windows.Forms.CheckBox checkTile1FlipUD;
        private System.Windows.Forms.CheckBox checkTile2FlipUD;
        private System.Windows.Forms.CheckBox checkTile0FlipUD;
        private System.Windows.Forms.CheckBox checkSelectedTileFlipUD;
        private System.Windows.Forms.CheckBox checkSelectedTileFlipLR;
        private System.Windows.Forms.NumericUpDown selectedTilePaletteSelector;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBoxSelectedTile;
        private System.Windows.Forms.Panel tileViewer;
        private System.Windows.Forms.Label loMetatileBrowserHoverDisplay;
        private System.Windows.Forms.GroupBox groupDebug;
        private System.Windows.Forms.Label tileBrowserHoverDisplay;
        private System.Windows.Forms.Label hiMetatileHoverDisplay;
        private System.Windows.Forms.CheckBox checkTile3Priority;
        private System.Windows.Forms.CheckBox checkTile2Priority;
        private System.Windows.Forms.CheckBox checkTile1Priority;
        private System.Windows.Forms.CheckBox checkTile0Priority;
        private System.Windows.Forms.CheckBox checkSelectedTilePriority;
        private System.Windows.Forms.Label labelDebugLoMetatileHover;
        private System.Windows.Forms.Label labelDebugHiMetatileHover;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem metatilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem importLoMetatileToolStripMenuItem;
    }
}