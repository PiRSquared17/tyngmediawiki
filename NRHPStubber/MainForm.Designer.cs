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
            System.Windows.Forms.StatusStrip status;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.bwFindMatches = new System.ComponentModel.BackgroundWorker();
            this.tsbPossibleArticles = new System.Windows.Forms.ToolStripButton();
            this.tsbFindMatches = new System.Windows.Forms.ToolStripButton();
            this.tstbCounty = new System.Windows.Forms.ToolStripTextBox();
            this.tsbStubCounty = new System.Windows.Forms.ToolStripButton();
            this.pROPMAINBindingNavigator = new System.Windows.Forms.ToolStrip();
            this.tsbDisambig = new System.Windows.Forms.ToolStripButton();
            status = new System.Windows.Forms.StatusStrip();
            status.SuspendLayout();
            this.pROPMAINBindingNavigator.SuspendLayout();
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
            this.tstbCounty.Text = "OH%";
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
            // pROPMAINBindingNavigator
            // 
            this.pROPMAINBindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbPossibleArticles,
            this.tsbFindMatches,
            this.tstbCounty,
            this.tsbStubCounty,
            this.tsbDisambig});
            this.pROPMAINBindingNavigator.Location = new System.Drawing.Point(0, 0);
            this.pROPMAINBindingNavigator.Name = "pROPMAINBindingNavigator";
            this.pROPMAINBindingNavigator.Size = new System.Drawing.Size(757, 25);
            this.pROPMAINBindingNavigator.TabIndex = 5;
            this.pROPMAINBindingNavigator.Text = "bindingNavigator1";
            // 
            // tsbDisambig
            // 
            this.tsbDisambig.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbDisambig.Image = ((System.Drawing.Image)(resources.GetObject("tsbDisambig.Image")));
            this.tsbDisambig.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDisambig.Name = "tsbDisambig";
            this.tsbDisambig.Size = new System.Drawing.Size(53, 22);
            this.tsbDisambig.Text = "Disambig";
            this.tsbDisambig.Click += new System.EventHandler(this.tsbDisambig_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(757, 155);
            this.Controls.Add(this.pROPMAINBindingNavigator);
            this.Controls.Add(status);
            this.Name = "MainForm";
            this.Text = "NRHP Stubber";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            status.ResumeLayout(false);
            status.PerformLayout();
            this.pROPMAINBindingNavigator.ResumeLayout(false);
            this.pROPMAINBindingNavigator.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker bwFindMatches;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.ToolStripButton tsbPossibleArticles;
        private System.Windows.Forms.ToolStripButton tsbFindMatches;
        private System.Windows.Forms.ToolStripTextBox tstbCounty;
        private System.Windows.Forms.ToolStripButton tsbStubCounty;
        private System.Windows.Forms.ToolStrip pROPMAINBindingNavigator;
        private System.Windows.Forms.ToolStripButton tsbDisambig;
    }
}

