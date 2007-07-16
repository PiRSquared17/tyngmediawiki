using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using Tyng.Google;
using Tyng.MediaWiki;

namespace NRHPStubber
{
    public partial class MainForm : Form
    {
        private DbProviderFactory _factory;
        private string _connString;

        public MainForm()
        {
            InitializeComponent();

            ConnectionStringSettings connection = ConfigurationManager.ConnectionStrings["NRHP20070628"];

            _factory = DbProviderFactories.GetFactory(connection.ProviderName);
            _connString = connection.ConnectionString;
        }

        #region Find Matches
                
        private void FindMatches_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            statusLabel.Text = (string) e.UserState;
        }

        private void FindMatches_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            tsbFindMatches.Enabled = true;

            if (e.Error != null)
            {
                throw new Exception("Error finding matches", e.Error);
            }
        }
        #endregion

        private void pROPMAINBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.pROPMAINBindingSource.EndEdit();
            this.pROPMAINTableAdapter.Update(this.nrhpDatabase.PROPMAIN);

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'nrhpDatabase.PROPMAIN' table. You can move, or remove it, as needed.
            this.pROPMAINTableAdapter.FillByCountyToCreate(this.nrhpDatabase.PROPMAIN, tstbCounty.Text);

            NrhpDatabaseTableAdapters.RETYPEMTableAdapter rtmta = new NRHPStubber.NrhpDatabaseTableAdapters.RETYPEMTableAdapter();
            rtmta.Fill(this.nrhpDatabase.RETYPEM);

            NrhpDatabaseTableAdapters.FUNCMTableAdapter fmta = new NRHPStubber.NrhpDatabaseTableAdapters.FUNCMTableAdapter();
            fmta.Fill(this.nrhpDatabase.FUNCM);

            NrhpDatabaseTableAdapters.HSFUNCDTableAdapter hsfdta = new NRHPStubber.NrhpDatabaseTableAdapters.HSFUNCDTableAdapter();
            hsfdta.Fill(this.nrhpDatabase.HSFUNCD);

            NrhpDatabaseTableAdapters.CSFUNCDTableAdapter csfdta = new NRHPStubber.NrhpDatabaseTableAdapters.CSFUNCDTableAdapter();
            csfdta.Fill(this.nrhpDatabase.CSFUNCD);

            NrhpDatabaseTableAdapters.ARCHTECDTableAdapter adta = new NRHPStubber.NrhpDatabaseTableAdapters.ARCHTECDTableAdapter();
            adta.Fill(this.nrhpDatabase.ARCHTECD);

            NrhpDatabaseTableAdapters.ARSTYLDTableAdapter asdta = new NRHPStubber.NrhpDatabaseTableAdapters.ARSTYLDTableAdapter();
            asdta.Fill(this.nrhpDatabase.ARSTYLD);

            NrhpDatabaseTableAdapters.ARSTYLMTableAdapter asmta = new NRHPStubber.NrhpDatabaseTableAdapters.ARSTYLMTableAdapter();
            asmta.Fill(this.nrhpDatabase.ARSTYLM);
        }

        private void tsbPossibleArticles_Click(object sender, EventArgs e)
        {
            PossibleArticlesForm f = new PossibleArticlesForm();
            f.CurrentRefNum = (int)((DataRowView)pROPMAINBindingSource.Current)["refnum"];
            f.ShowDialog();
        }

        private void tsbFindMatches_Click(object sender, EventArgs e)
        {
            tsbFindMatches.Enabled = false;
            bwFindMatches.RunWorkerAsync();
        }

        private void pROPMAINDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            NrhpDatabase.PROPMAINRow r = (NrhpDatabase.PROPMAINRow)((DataRowView)pROPMAINBindingSource.Current).Row;
            NrhpDatabase db = (NrhpDatabase) r.Table.DataSet;

            string article = Stubber.StubSingleArticle(db, r.refnum);

            PageSection[] sections = PageSection.ParseSections(article);

            Clipboard.SetText(article);
        }

        private void tsbStubCounty_Click(object sender, EventArgs e)
        {
            string passwordText = null;
            if (PasswordForm.Show(out passwordText) == DialogResult.OK)
            {
                Stubber.Stub(passwordText, tstbCounty.Text);
            }
        }

        private void bwFindMatches_DoWork(object sender, DoWorkEventArgs e)
        {
            MatchFinder.FindMatches(bwFindMatches);
        }
    }
}