using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NRHPStubber
{
    public partial class PossibleArticlesForm : Form
    {
        public PossibleArticlesForm()
        {
            InitializeComponent();

            // TODO: This line of code loads data into the 'nrhpDatabase.PossibleArticles' table. You can move, or remove it, as needed.
            this.possibleArticlesTableAdapter.FillNeedingEvaluation(this.nrhpDatabase.PossibleArticles);
            // TODO: This line of code loads data into the 'nrhpDatabase.PROPMAIN' table. You can move, or remove it, as needed.
            this.pROPMAINTableAdapter.FillArticlesNeedingChecking(this.nrhpDatabase.PROPMAIN);
        }

        public int CurrentRefNum
        {
            get
            {
                return (int)((DataRowView)pROPMAINBindingSource.Current)["refnum"];
            }
            set
            {
                int index = pROPMAINBindingSource.Find("refnum", value);
                pROPMAINBindingSource.Position = index;
            }
        }

        private void pROPMAINBindingNavigatorSaveItem_Click_1(object sender, EventArgs e)
        {
            this.Validate();
            this.possibleArticlesBindingSource.EndEdit();
            this.pROPMAINBindingSource.EndEdit();

            this.possibleArticlesTableAdapter.Update(this.nrhpDatabase.PossibleArticles);
            this.pROPMAINTableAdapter.Update(this.nrhpDatabase.PROPMAIN);
        }

        private void possibleArticlesDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (possibleArticlesDataGridView.Columns[e.ColumnIndex].HeaderText == "Match")
            {
                DataRowView source = ((DataRowView)possibleArticlesBindingSource.Current);
                int refnum = (int)source["refnum"];
                int articleId = (int)source["ArticleID"];
                NrhpDatabase.PROPMAINRow r = nrhpDatabase.PROPMAIN.FindByrefnum(refnum);
                r.BeginEdit();
                r.ArticleID = articleId;
                r.EndEdit();
            }
            else if (possibleArticlesDataGridView.Columns[e.ColumnIndex].HeaderText == "Not Match")
            {
                DataRowView source = ((DataRowView)possibleArticlesBindingSource.Current);
                source.BeginEdit();
                source["NotAMatch"] = DateTime.Now;
                source.EndEdit();
            }
            else if (possibleArticlesDataGridView.Columns[e.ColumnIndex].HeaderText == "See Also")
            {
                DataRowView source = ((DataRowView)possibleArticlesBindingSource.Current);
                source.BeginEdit();
                source["SeeAlso"] = DateTime.Now;
                source.EndEdit();
            }
            
        }

        private void possibleArticlesDataGridView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataRowView source = ((DataRowView)possibleArticlesBindingSource.Current);

            string url = string.Format("http://en.wikipedia.org/w/index.php?curid={0}&title=Doesn't%20matter", source["ArticleID"]);

            System.Diagnostics.Process.Start(url);            
        }

   }
}