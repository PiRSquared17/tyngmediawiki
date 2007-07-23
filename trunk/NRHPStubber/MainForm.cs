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
using Tyng.MediaWiki.Configuration;

namespace NRHPStubber
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

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

        private void tsbPossibleArticles_Click(object sender, EventArgs e)
        {
            PossibleArticlesForm f = new PossibleArticlesForm();
            f.ShowDialog();
        }

        private void tsbFindMatches_Click(object sender, EventArgs e)
        {
            tsbFindMatches.Enabled = false;
            bwFindMatches.RunWorkerAsync();
        }

        private void tsbStubCounty_Click(object sender, EventArgs e)
        {
            Stubber.Stub(tstbCounty.Text);
        }

        private void bwFindMatches_DoWork(object sender, DoWorkEventArgs e)
        {
            MatchFinder.FindMatches(bwFindMatches);
        }

        private void tsbDisambig_Click(object sender, EventArgs e)
        {
            Disambig.CreateDisambigs();
        }

    }
}