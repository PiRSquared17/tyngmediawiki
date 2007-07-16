namespace NRHPStubber
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.StatusStrip status;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.bwFindMatches = new System.ComponentModel.BackgroundWorker();
            this.pROPMAINBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.nrhpDatabase = new NRHPStubber.NrhpDatabase();
            this.pROPMAINTableAdapter = new NRHPStubber.NrhpDatabaseTableAdapters.PROPMAINTableAdapter();
            this.pROPMAINBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.pROPMAINBindingNavigatorSaveItem = new System.Windows.Forms.ToolStripButton();
            this.tsbPossibleArticles = new System.Windows.Forms.ToolStripButton();
            this.tsbFindMatches = new System.Windows.Forms.ToolStripButton();
            this.tstbCounty = new System.Windows.Forms.ToolStripTextBox();
            this.tsbStubCounty = new System.Windows.Forms.ToolStripButton();
            this.pROPMAINDataGridView = new System.Windows.Forms.DataGridView();
            this.refnumDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.resnameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.addressDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.restrictDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.retypecdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numcbldgDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numcsiteDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numcstrcDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numcobjDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numnbldgDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numnsiteDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numnstrcDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numnobjDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.parknmcdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.certcdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.certdateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descothrDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.acreDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.multnameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.articleIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cleanNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PrimaryState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PrimaryLocation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            status = new System.Windows.Forms.StatusStrip();
            status.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pROPMAINBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nrhpDatabase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pROPMAINBindingNavigator)).BeginInit();
            this.pROPMAINBindingNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pROPMAINDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // status
            // 
            status.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            status.Location = new System.Drawing.Point(0, 133);
            status.Name = "status";
            status.Size = new System.Drawing.Size(757, 22);
            status.TabIndex = 4;
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(78, 17);
            this.statusLabel.Text = "Status Label...";
            // 
            // bwFindMatches
            // 
            this.bwFindMatches.WorkerReportsProgress = true;
            this.bwFindMatches.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwFindMatches_DoWork);
            this.bwFindMatches.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.FindMatches_RunWorkerCompleted);
            this.bwFindMatches.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.FindMatches_ProgressChanged);
            // 
            // pROPMAINBindingSource
            // 
            this.pROPMAINBindingSource.AllowNew = false;
            this.pROPMAINBindingSource.DataMember = "PROPMAIN";
            this.pROPMAINBindingSource.DataSource = this.nrhpDatabase;
            // 
            // nrhpDatabase
            // 
            this.nrhpDatabase.DataSetName = "NrhpDatabase";
            this.nrhpDatabase.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // pROPMAINTableAdapter
            // 
            this.pROPMAINTableAdapter.ClearBeforeFill = true;
            // 
            // pROPMAINBindingNavigator
            // 
            this.pROPMAINBindingNavigator.AddNewItem = null;
            this.pROPMAINBindingNavigator.BindingSource = this.pROPMAINBindingSource;
            this.pROPMAINBindingNavigator.CountItem = this.bindingNavigatorCountItem;
            this.pROPMAINBindingNavigator.DeleteItem = null;
            this.pROPMAINBindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.pROPMAINBindingNavigatorSaveItem,
            this.tsbPossibleArticles,
            this.tsbFindMatches,
            this.tstbCounty,
            this.tsbStubCounty});
            this.pROPMAINBindingNavigator.Location = new System.Drawing.Point(0, 0);
            this.pROPMAINBindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.pROPMAINBindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.pROPMAINBindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.pROPMAINBindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.pROPMAINBindingNavigator.Name = "pROPMAINBindingNavigator";
            this.pROPMAINBindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.pROPMAINBindingNavigator.Size = new System.Drawing.Size(757, 25);
            this.pROPMAINBindingNavigator.TabIndex = 5;
            this.pROPMAINBindingNavigator.Text = "bindingNavigator1";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(36, 22);
            this.bindingNavigatorCountItem.Text = "of {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Total number of items";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveFirstItem.Text = "Move first";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMovePreviousItem.Text = "Move previous";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "Position";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 21);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "Current position";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveNextItem.Text = "Move next";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveLastItem.Text = "Move last";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // pROPMAINBindingNavigatorSaveItem
            // 
            this.pROPMAINBindingNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.pROPMAINBindingNavigatorSaveItem.Image = ((System.Drawing.Image)(resources.GetObject("pROPMAINBindingNavigatorSaveItem.Image")));
            this.pROPMAINBindingNavigatorSaveItem.Name = "pROPMAINBindingNavigatorSaveItem";
            this.pROPMAINBindingNavigatorSaveItem.Size = new System.Drawing.Size(23, 22);
            this.pROPMAINBindingNavigatorSaveItem.Text = "Save Data";
            this.pROPMAINBindingNavigatorSaveItem.Click += new System.EventHandler(this.pROPMAINBindingNavigatorSaveItem_Click);
            // 
            // tsbPossibleArticles
            // 
            this.tsbPossibleArticles.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbPossibleArticles.Image = ((System.Drawing.Image)(resources.GetObject("tsbPossibleArticles.Image")));
            this.tsbPossibleArticles.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPossibleArticles.Name = "tsbPossibleArticles";
            this.tsbPossibleArticles.Size = new System.Drawing.Size(46, 22);
            this.tsbPossibleArticles.Text = "Articles";
            this.tsbPossibleArticles.ToolTipText = "Possible Articles";
            this.tsbPossibleArticles.Click += new System.EventHandler(this.tsbPossibleArticles_Click);
            // 
            // tsbFindMatches
            // 
            this.tsbFindMatches.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbFindMatches.Image = ((System.Drawing.Image)(resources.GetObject("tsbFindMatches.Image")));
            this.tsbFindMatches.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFindMatches.Name = "tsbFindMatches";
            this.tsbFindMatches.Size = new System.Drawing.Size(74, 22);
            this.tsbFindMatches.Text = "Find Matches";
            this.tsbFindMatches.Click += new System.EventHandler(this.tsbFindMatches_Click);
            // 
            // tstbCounty
            // 
            this.tstbCounty.Name = "tstbCounty";
            this.tstbCounty.Size = new System.Drawing.Size(50, 25);
            this.tstbCounty.Text = "OH0061";
            // 
            // tsbStubCounty
            // 
            this.tsbStubCounty.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbStubCounty.Image = ((System.Drawing.Image)(resources.GetObject("tsbStubCounty.Image")));
            this.tsbStubCounty.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbStubCounty.Name = "tsbStubCounty";
            this.tsbStubCounty.Size = new System.Drawing.Size(33, 22);
            this.tsbStubCounty.Text = "Stub";
            this.tsbStubCounty.Click += new System.EventHandler(this.tsbStubCounty_Click);
            // 
            // pROPMAINDataGridView
            // 
            this.pROPMAINDataGridView.AllowUserToAddRows = false;
            this.pROPMAINDataGridView.AllowUserToDeleteRows = false;
            this.pROPMAINDataGridView.AutoGenerateColumns = false;
            this.pROPMAINDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.refnumDataGridViewTextBoxColumn,
            this.resnameDataGridViewTextBoxColumn,
            this.addressDataGridViewTextBoxColumn,
            this.restrictDataGridViewTextBoxColumn,
            this.retypecdDataGridViewTextBoxColumn,
            this.numcbldgDataGridViewTextBoxColumn,
            this.numcsiteDataGridViewTextBoxColumn,
            this.numcstrcDataGridViewTextBoxColumn,
            this.numcobjDataGridViewTextBoxColumn,
            this.numnbldgDataGridViewTextBoxColumn,
            this.numnsiteDataGridViewTextBoxColumn,
            this.numnstrcDataGridViewTextBoxColumn,
            this.numnobjDataGridViewTextBoxColumn,
            this.parknmcdDataGridViewTextBoxColumn,
            this.certcdDataGridViewTextBoxColumn,
            this.certdateDataGridViewTextBoxColumn,
            this.descothrDataGridViewTextBoxColumn,
            this.acreDataGridViewTextBoxColumn,
            this.multnameDataGridViewTextBoxColumn,
            this.articleIDDataGridViewTextBoxColumn,
            this.cleanNameDataGridViewTextBoxColumn,
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.PrimaryState,
            this.PrimaryLocation});
            this.pROPMAINDataGridView.DataSource = this.pROPMAINBindingSource;
            this.pROPMAINDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pROPMAINDataGridView.Location = new System.Drawing.Point(0, 25);
            this.pROPMAINDataGridView.Name = "pROPMAINDataGridView";
            this.pROPMAINDataGridView.Size = new System.Drawing.Size(757, 108);
            this.pROPMAINDataGridView.TabIndex = 5;
            this.pROPMAINDataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.pROPMAINDataGridView_CellDoubleClick);
            // 
            // refnumDataGridViewTextBoxColumn
            // 
            this.refnumDataGridViewTextBoxColumn.DataPropertyName = "refnum";
            this.refnumDataGridViewTextBoxColumn.HeaderText = "refnum";
            this.refnumDataGridViewTextBoxColumn.Name = "refnumDataGridViewTextBoxColumn";
            // 
            // resnameDataGridViewTextBoxColumn
            // 
            this.resnameDataGridViewTextBoxColumn.DataPropertyName = "resname";
            this.resnameDataGridViewTextBoxColumn.HeaderText = "resname";
            this.resnameDataGridViewTextBoxColumn.Name = "resnameDataGridViewTextBoxColumn";
            // 
            // addressDataGridViewTextBoxColumn
            // 
            this.addressDataGridViewTextBoxColumn.DataPropertyName = "address";
            this.addressDataGridViewTextBoxColumn.HeaderText = "address";
            this.addressDataGridViewTextBoxColumn.Name = "addressDataGridViewTextBoxColumn";
            // 
            // restrictDataGridViewTextBoxColumn
            // 
            this.restrictDataGridViewTextBoxColumn.DataPropertyName = "restrict";
            this.restrictDataGridViewTextBoxColumn.HeaderText = "restrict";
            this.restrictDataGridViewTextBoxColumn.Name = "restrictDataGridViewTextBoxColumn";
            // 
            // retypecdDataGridViewTextBoxColumn
            // 
            this.retypecdDataGridViewTextBoxColumn.DataPropertyName = "retypecd";
            this.retypecdDataGridViewTextBoxColumn.HeaderText = "retypecd";
            this.retypecdDataGridViewTextBoxColumn.Name = "retypecdDataGridViewTextBoxColumn";
            // 
            // numcbldgDataGridViewTextBoxColumn
            // 
            this.numcbldgDataGridViewTextBoxColumn.DataPropertyName = "numcbldg";
            this.numcbldgDataGridViewTextBoxColumn.HeaderText = "numcbldg";
            this.numcbldgDataGridViewTextBoxColumn.Name = "numcbldgDataGridViewTextBoxColumn";
            // 
            // numcsiteDataGridViewTextBoxColumn
            // 
            this.numcsiteDataGridViewTextBoxColumn.DataPropertyName = "numcsite";
            this.numcsiteDataGridViewTextBoxColumn.HeaderText = "numcsite";
            this.numcsiteDataGridViewTextBoxColumn.Name = "numcsiteDataGridViewTextBoxColumn";
            // 
            // numcstrcDataGridViewTextBoxColumn
            // 
            this.numcstrcDataGridViewTextBoxColumn.DataPropertyName = "numcstrc";
            this.numcstrcDataGridViewTextBoxColumn.HeaderText = "numcstrc";
            this.numcstrcDataGridViewTextBoxColumn.Name = "numcstrcDataGridViewTextBoxColumn";
            // 
            // numcobjDataGridViewTextBoxColumn
            // 
            this.numcobjDataGridViewTextBoxColumn.DataPropertyName = "numcobj";
            this.numcobjDataGridViewTextBoxColumn.HeaderText = "numcobj";
            this.numcobjDataGridViewTextBoxColumn.Name = "numcobjDataGridViewTextBoxColumn";
            // 
            // numnbldgDataGridViewTextBoxColumn
            // 
            this.numnbldgDataGridViewTextBoxColumn.DataPropertyName = "numnbldg";
            this.numnbldgDataGridViewTextBoxColumn.HeaderText = "numnbldg";
            this.numnbldgDataGridViewTextBoxColumn.Name = "numnbldgDataGridViewTextBoxColumn";
            // 
            // numnsiteDataGridViewTextBoxColumn
            // 
            this.numnsiteDataGridViewTextBoxColumn.DataPropertyName = "numnsite";
            this.numnsiteDataGridViewTextBoxColumn.HeaderText = "numnsite";
            this.numnsiteDataGridViewTextBoxColumn.Name = "numnsiteDataGridViewTextBoxColumn";
            // 
            // numnstrcDataGridViewTextBoxColumn
            // 
            this.numnstrcDataGridViewTextBoxColumn.DataPropertyName = "numnstrc";
            this.numnstrcDataGridViewTextBoxColumn.HeaderText = "numnstrc";
            this.numnstrcDataGridViewTextBoxColumn.Name = "numnstrcDataGridViewTextBoxColumn";
            // 
            // numnobjDataGridViewTextBoxColumn
            // 
            this.numnobjDataGridViewTextBoxColumn.DataPropertyName = "numnobj";
            this.numnobjDataGridViewTextBoxColumn.HeaderText = "numnobj";
            this.numnobjDataGridViewTextBoxColumn.Name = "numnobjDataGridViewTextBoxColumn";
            // 
            // parknmcdDataGridViewTextBoxColumn
            // 
            this.parknmcdDataGridViewTextBoxColumn.DataPropertyName = "parknmcd";
            this.parknmcdDataGridViewTextBoxColumn.HeaderText = "parknmcd";
            this.parknmcdDataGridViewTextBoxColumn.Name = "parknmcdDataGridViewTextBoxColumn";
            // 
            // certcdDataGridViewTextBoxColumn
            // 
            this.certcdDataGridViewTextBoxColumn.DataPropertyName = "certcd";
            this.certcdDataGridViewTextBoxColumn.HeaderText = "certcd";
            this.certcdDataGridViewTextBoxColumn.Name = "certcdDataGridViewTextBoxColumn";
            // 
            // certdateDataGridViewTextBoxColumn
            // 
            this.certdateDataGridViewTextBoxColumn.DataPropertyName = "certdate";
            this.certdateDataGridViewTextBoxColumn.HeaderText = "certdate";
            this.certdateDataGridViewTextBoxColumn.Name = "certdateDataGridViewTextBoxColumn";
            // 
            // descothrDataGridViewTextBoxColumn
            // 
            this.descothrDataGridViewTextBoxColumn.DataPropertyName = "descothr";
            this.descothrDataGridViewTextBoxColumn.HeaderText = "descothr";
            this.descothrDataGridViewTextBoxColumn.Name = "descothrDataGridViewTextBoxColumn";
            // 
            // acreDataGridViewTextBoxColumn
            // 
            this.acreDataGridViewTextBoxColumn.DataPropertyName = "acre";
            this.acreDataGridViewTextBoxColumn.HeaderText = "acre";
            this.acreDataGridViewTextBoxColumn.Name = "acreDataGridViewTextBoxColumn";
            // 
            // multnameDataGridViewTextBoxColumn
            // 
            this.multnameDataGridViewTextBoxColumn.DataPropertyName = "multname";
            this.multnameDataGridViewTextBoxColumn.HeaderText = "multname";
            this.multnameDataGridViewTextBoxColumn.Name = "multnameDataGridViewTextBoxColumn";
            // 
            // articleIDDataGridViewTextBoxColumn
            // 
            this.articleIDDataGridViewTextBoxColumn.DataPropertyName = "ArticleID";
            this.articleIDDataGridViewTextBoxColumn.HeaderText = "ArticleID";
            this.articleIDDataGridViewTextBoxColumn.Name = "articleIDDataGridViewTextBoxColumn";
            // 
            // cleanNameDataGridViewTextBoxColumn
            // 
            this.cleanNameDataGridViewTextBoxColumn.DataPropertyName = "CleanName";
            this.cleanNameDataGridViewTextBoxColumn.HeaderText = "CleanName";
            this.cleanNameDataGridViewTextBoxColumn.Name = "cleanNameDataGridViewTextBoxColumn";
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "PrimaryCity";
            this.dataGridViewTextBoxColumn1.HeaderText = "PrimaryCity";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "PrimaryCounty";
            this.dataGridViewTextBoxColumn2.HeaderText = "PrimaryCounty";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // PrimaryState
            // 
            this.PrimaryState.DataPropertyName = "PrimaryState";
            this.PrimaryState.HeaderText = "PrimaryState";
            this.PrimaryState.Name = "PrimaryState";
            this.PrimaryState.ReadOnly = true;
            // 
            // PrimaryLocation
            // 
            this.PrimaryLocation.DataPropertyName = "PrimaryLocation";
            this.PrimaryLocation.HeaderText = "PrimaryLocation";
            this.PrimaryLocation.Name = "PrimaryLocation";
            this.PrimaryLocation.ReadOnly = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(757, 155);
            this.Controls.Add(this.pROPMAINDataGridView);
            this.Controls.Add(this.pROPMAINBindingNavigator);
            this.Controls.Add(status);
            this.Name = "MainForm";
            this.Text = "NRHP Stubber";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            status.ResumeLayout(false);
            status.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pROPMAINBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nrhpDatabase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pROPMAINBindingNavigator)).EndInit();
            this.pROPMAINBindingNavigator.ResumeLayout(false);
            this.pROPMAINBindingNavigator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pROPMAINDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker bwFindMatches;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.BindingSource pROPMAINBindingSource;
        private NRHPStubber.NrhpDatabaseTableAdapters.PROPMAINTableAdapter pROPMAINTableAdapter;
        private System.Windows.Forms.BindingNavigator pROPMAINBindingNavigator;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.ToolStripButton pROPMAINBindingNavigatorSaveItem;
        private System.Windows.Forms.DataGridView pROPMAINDataGridView;
        private System.Windows.Forms.ToolStripButton tsbPossibleArticles;
        private NrhpDatabase nrhpDatabase;
        private System.Windows.Forms.ToolStripButton tsbFindMatches;
        private System.Windows.Forms.ToolStripTextBox tstbCounty;
        private System.Windows.Forms.DataGridViewTextBoxColumn refnumDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn resnameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn addressDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn restrictDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn retypecdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn numcbldgDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn numcsiteDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn numcstrcDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn numcobjDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn numnbldgDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn numnsiteDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn numnstrcDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn numnobjDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn parknmcdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn certcdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn certdateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn descothrDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn acreDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn multnameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn articleIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cleanNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn PrimaryState;
        private System.Windows.Forms.DataGridViewTextBoxColumn PrimaryLocation;
        private System.Windows.Forms.ToolStripButton tsbStubCounty;
    }
}

