namespace NRHPStubber
{
    partial class PossibleArticlesForm
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
            System.Windows.Forms.Label refnumLabel;
            System.Windows.Forms.Label resnameLabel;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PossibleArticlesForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.nrhpDatabase = new NRHPStubber.NrhpDatabase();
            this.pROPMAINBindingSource = new System.Windows.Forms.BindingSource(this.components);
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
            this.refnumLabel1 = new System.Windows.Forms.Label();
            this.resnameLabel1 = new System.Windows.Forms.Label();
            this.possibleArticlesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.possibleArticlesTableAdapter = new NRHPStubber.NrhpDatabaseTableAdapters.PossibleArticlesTableAdapter();
            this.possibleArticlesDataGridView = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NotAMatchButton = new System.Windows.Forms.DataGridViewButtonColumn();
            this.MatchButton = new System.Windows.Forms.DataGridViewButtonColumn();
            this.SeeAlsoButtonColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            refnumLabel = new System.Windows.Forms.Label();
            resnameLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nrhpDatabase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pROPMAINBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pROPMAINBindingNavigator)).BeginInit();
            this.pROPMAINBindingNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.possibleArticlesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.possibleArticlesDataGridView)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // refnumLabel
            // 
            refnumLabel.AutoSize = true;
            refnumLabel.Location = new System.Drawing.Point(17, 23);
            refnumLabel.Name = "refnumLabel";
            refnumLabel.Size = new System.Drawing.Size(42, 13);
            refnumLabel.TabIndex = 1;
            refnumLabel.Text = "refnum:";
            // 
            // resnameLabel
            // 
            resnameLabel.AutoSize = true;
            resnameLabel.Location = new System.Drawing.Point(17, 46);
            resnameLabel.Name = "resnameLabel";
            resnameLabel.Size = new System.Drawing.Size(50, 13);
            resnameLabel.TabIndex = 3;
            resnameLabel.Text = "resname:";
            // 
            // nrhpDatabase
            // 
            this.nrhpDatabase.DataSetName = "NrhpDatabase";
            this.nrhpDatabase.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // pROPMAINBindingSource
            // 
            this.pROPMAINBindingSource.AllowNew = false;
            this.pROPMAINBindingSource.DataMember = "PROPMAIN";
            this.pROPMAINBindingSource.DataSource = this.nrhpDatabase;
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
            this.pROPMAINBindingNavigatorSaveItem});
            this.pROPMAINBindingNavigator.Location = new System.Drawing.Point(0, 0);
            this.pROPMAINBindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.pROPMAINBindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.pROPMAINBindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.pROPMAINBindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.pROPMAINBindingNavigator.Name = "pROPMAINBindingNavigator";
            this.pROPMAINBindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.pROPMAINBindingNavigator.Size = new System.Drawing.Size(583, 25);
            this.pROPMAINBindingNavigator.TabIndex = 0;
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
            this.pROPMAINBindingNavigatorSaveItem.Click += new System.EventHandler(this.pROPMAINBindingNavigatorSaveItem_Click_1);
            // 
            // refnumLabel1
            // 
            this.refnumLabel1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.pROPMAINBindingSource, "refnum", true));
            this.refnumLabel1.Location = new System.Drawing.Point(73, 23);
            this.refnumLabel1.Name = "refnumLabel1";
            this.refnumLabel1.Size = new System.Drawing.Size(123, 23);
            this.refnumLabel1.TabIndex = 2;
            // 
            // resnameLabel1
            // 
            this.resnameLabel1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.pROPMAINBindingSource, "resname", true));
            this.resnameLabel1.Location = new System.Drawing.Point(73, 46);
            this.resnameLabel1.Name = "resnameLabel1";
            this.resnameLabel1.Size = new System.Drawing.Size(408, 23);
            this.resnameLabel1.TabIndex = 4;
            // 
            // possibleArticlesBindingSource
            // 
            this.possibleArticlesBindingSource.AllowNew = false;
            this.possibleArticlesBindingSource.DataMember = "FK_PossibleArticles_PROPMAIN";
            this.possibleArticlesBindingSource.DataSource = this.pROPMAINBindingSource;
            // 
            // possibleArticlesTableAdapter
            // 
            this.possibleArticlesTableAdapter.ClearBeforeFill = true;
            // 
            // possibleArticlesDataGridView
            // 
            this.possibleArticlesDataGridView.AutoGenerateColumns = false;
            this.possibleArticlesDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn3,
            this.NotAMatchButton,
            this.MatchButton,
            this.SeeAlsoButtonColumn,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn6});
            this.possibleArticlesDataGridView.DataSource = this.possibleArticlesBindingSource;
            this.possibleArticlesDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.possibleArticlesDataGridView.Location = new System.Drawing.Point(0, 107);
            this.possibleArticlesDataGridView.Name = "possibleArticlesDataGridView";
            this.possibleArticlesDataGridView.ReadOnly = true;
            this.possibleArticlesDataGridView.RowTemplate.Height = 500;
            this.possibleArticlesDataGridView.Size = new System.Drawing.Size(583, 319);
            this.possibleArticlesDataGridView.TabIndex = 5;
            this.possibleArticlesDataGridView.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.possibleArticlesDataGridView_CellContentDoubleClick);
            this.possibleArticlesDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.possibleArticlesDataGridView_CellContentClick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(refnumLabel);
            this.panel1.Controls.Add(this.refnumLabel1);
            this.panel1.Controls.Add(resnameLabel);
            this.panel1.Controls.Add(this.resnameLabel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(583, 82);
            this.panel1.TabIndex = 6;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "FoundReason";
            this.dataGridViewTextBoxColumn5.HeaderText = "FoundReason";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "ArticleContent";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewTextBoxColumn3.HeaderText = "ArticleContent";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn3.Width = 500;
            // 
            // NotAMatchButton
            // 
            this.NotAMatchButton.HeaderText = "Not Match";
            this.NotAMatchButton.Name = "NotAMatchButton";
            this.NotAMatchButton.ReadOnly = true;
            this.NotAMatchButton.Text = "Not Match";
            this.NotAMatchButton.UseColumnTextForButtonValue = true;
            // 
            // MatchButton
            // 
            this.MatchButton.HeaderText = "Match";
            this.MatchButton.Name = "MatchButton";
            this.MatchButton.ReadOnly = true;
            this.MatchButton.Text = "Match";
            this.MatchButton.UseColumnTextForButtonValue = true;
            // 
            // SeeAlsoButtonColumn
            // 
            this.SeeAlsoButtonColumn.HeaderText = "See Also";
            this.SeeAlsoButtonColumn.Name = "SeeAlsoButtonColumn";
            this.SeeAlsoButtonColumn.ReadOnly = true;
            this.SeeAlsoButtonColumn.Text = "See Also";
            this.SeeAlsoButtonColumn.UseColumnTextForButtonValue = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "FoundOn";
            this.dataGridViewTextBoxColumn4.HeaderText = "FoundOn";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "ArticleID";
            this.dataGridViewTextBoxColumn2.HeaderText = "ArticleID";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "NotAMatch";
            this.dataGridViewTextBoxColumn6.HeaderText = "NotAMatch";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            // 
            // PossibleArticlesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(583, 426);
            this.Controls.Add(this.possibleArticlesDataGridView);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pROPMAINBindingNavigator);
            this.Name = "PossibleArticlesForm";
            this.Text = "Possible Articles";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.nrhpDatabase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pROPMAINBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pROPMAINBindingNavigator)).EndInit();
            this.pROPMAINBindingNavigator.ResumeLayout(false);
            this.pROPMAINBindingNavigator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.possibleArticlesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.possibleArticlesDataGridView)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private NrhpDatabase nrhpDatabase;
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
        private System.Windows.Forms.Label refnumLabel1;
        private System.Windows.Forms.Label resnameLabel1;
        private System.Windows.Forms.BindingSource possibleArticlesBindingSource;
        private NRHPStubber.NrhpDatabaseTableAdapters.PossibleArticlesTableAdapter possibleArticlesTableAdapter;
        private System.Windows.Forms.DataGridView possibleArticlesDataGridView;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewButtonColumn NotAMatchButton;
        private System.Windows.Forms.DataGridViewButtonColumn MatchButton;
        private System.Windows.Forms.DataGridViewButtonColumn SeeAlsoButtonColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;




    }
}