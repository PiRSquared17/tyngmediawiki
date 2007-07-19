using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using Tyng.Google;
using Tyng.MediaWiki;

namespace NRHPStubber
{
    public static class MatchFinder
    {
        public static void FindMatches(BackgroundWorker bwFindMatches)
        {
            TextInfo ti = CultureInfo.CreateSpecificCulture("en").TextInfo;
            NrhpDatabase db = new NrhpDatabase();

            NrhpDatabaseTableAdapters.PROPMAINTableAdapter pmta = new NRHPStubber.NrhpDatabaseTableAdapters.PROPMAINTableAdapter();
            pmta.FillByCountyToCreate(db.PROPMAIN, "OH0061");

            NrhpDatabaseTableAdapters.OTHNAMEDTableAdapter onta = new NRHPStubber.NrhpDatabaseTableAdapters.OTHNAMEDTableAdapter();
            onta.Fill(db.OTHNAMED);

            NrhpDatabaseTableAdapters.SIGNAMEDTableAdapter snta = new NRHPStubber.NrhpDatabaseTableAdapters.SIGNAMEDTableAdapter();
            snta.Fill(db.SIGNAMED);

            NrhpDatabaseTableAdapters.STATEMTableAdapter sta = new NRHPStubber.NrhpDatabaseTableAdapters.STATEMTableAdapter();
            sta.Fill(db.STATEM);

            NrhpDatabaseTableAdapters.PossibleArticlesTableAdapter pata = new NRHPStubber.NrhpDatabaseTableAdapters.PossibleArticlesTableAdapter();
            pata.Fill(db.PossibleArticles);

            NrhpDatabase.PROPMAINDataTable pmdt = db.PROPMAIN;

            for (int i = 0; i < pmdt.Rows.Count; i++)
            {
                NrhpDatabase.PROPMAINRow dr = pmdt[i];

                int percent = (i / pmdt.Rows.Count) * 100;
                
                //bwFindMatches.ReportProgress(percent, string.Format("Checking \"{0}\", google search \"{1}\"", dr.resname, dr.refnum));
                //GoogleSearch("REFNUM", "nrhp " + dr.refnum.ToString(), dr.refnum);

                if (!dr.IsCleanNameNull())
                {
                    CheckTitle(db, "Clean Name", bwFindMatches, dr.resname, dr.CleanName, ti.ToTitleCase(ti.ToLower(dr.PrimaryCity)), ti.ToTitleCase(ti.ToLower(dr.PrimaryState)), dr.refnum);
                }
                else
                {
                    CheckTitle(db, "Original Name", bwFindMatches, dr.resname, dr.resname, ti.ToTitleCase(ti.ToLower(dr.PrimaryCity)), ti.ToTitleCase(ti.ToLower(dr.PrimaryState)), dr.refnum);
                }

                //NrhpDatabase.OTHNAMEDRow[] otherNames = dr.GetOTHNAMEDRows();

                //foreach (NrhpDatabase.OTHNAMEDRow otherName in otherNames)
                //{
                //    CheckTitle("Other Name", bwFindMatches, dr.resname, otherName.othname, ti.ToTitleCase(ti.ToLower(dr.PrimaryCity)), ti.ToTitleCase(ti.ToLower(dr.PrimaryState)), dr.refnum);
                //}

                //NrhpDatabase.SIGNAMEDRow[] sigNames = dr.GetSIGNAMEDRows();

                //foreach (NrhpDatabase.SIGNAMEDRow sigName in sigNames)
                //{
                //    if (sigName.IsCleanNameNull())
                //    {
                //        //first + last
                //        string firstAndLast = sigName.FirstName + " " + sigName.LastName;
                //        bwFindMatches.ReportProgress(percent, string.Format("Checking \"{0}\", title \"{1}\"", dr.resname, firstAndLast));
                //        CheckArticleTitle("SIGNAMED - First + ' ' + Last", firstAndLast, dr.refnum);
                //    }
                //    else
                //    {
                //        bwFindMatches.ReportProgress(percent, string.Format("Checking \"{0}\", title \"{1}\"", dr.resname, sigName.CleanName));
                //        CheckArticleTitle("SIGNAMED - CleanName", sigName.CleanName, dr.refnum);
                //    }
                //}

            }

            pata.Update(db.PossibleArticles);
        }

        private static void GoogleSearch(NrhpDatabase db, string testType, string search, int refnum)
        {
            AjaxSearchResult[] searchResults = AjaxSearch.Search(AjaxSearchType.Web, "site:en.wikipedia.org " + search);

            foreach (AjaxSearchResult result in searchResults)
            {
                Uri uri = new Uri(result.Url);

                string[] segments = uri.Segments;
                string pageTitle;

                if (segments.Length >= 3 && segments[1] == "wiki/")
                {
                    pageTitle = string.Join("", segments, 2, segments.Length - 2);
                    pageTitle = Uri.UnescapeDataString(pageTitle);
                }
                else if (segments.Length == 2 && Uri.UnescapeDataString(segments[1]).StartsWith("?title="))
                {
                    pageTitle = Uri.UnescapeDataString(segments[1]).Substring(7);
                }
                else
                    throw new Exception("Bad search result url \"" + uri.ToString() + "\"");

                Page p = Page.GetPage(pageTitle);

                //don't record non main namespace hits
                if (p.Namespace == MediaWikiNamespace.Main)
                    AddPossibleArticle(db, refnum, testType, p);
            }
        }

        private static void AddPossibleArticle(NrhpDatabase db, int refnum, string testType, Page p)
        {
            //TODO: just write to the ds, no need to hit the db each time...

            if (!p.IsMissing)
            {
                Page redirectTarget = p.FollowRuntimeRedirects();
                
                NrhpDatabase.PossibleArticlesDataTable pa = db.PossibleArticles;

                if (pa.FindByrefnumArticleID(refnum, p.Id.Value) == null)
                {
                    NrhpDatabase.PossibleArticlesRow newRow = pa.NewPossibleArticlesRow();
                    newRow.PROPMAINRow = db.PROPMAIN.FindByrefnum(refnum);
                    newRow.ArticleID = p.Id.Value;
                    newRow.ArticleContent = p.LastRevision.GetContent();
                    newRow.FoundOn = DateTime.Now;
                    newRow.FoundReason = testType;

                    pa.AddPossibleArticlesRow(newRow);
                }
            }
        }

        private static void CheckTitle(NrhpDatabase db, string testType, BackgroundWorker bwFindMatches, string original, string checkTitle, string city, string state, int refnum)
        {
            bwFindMatches.ReportProgress(0, string.Format("Checking \"{0}\", title \"{1}\"", original, checkTitle));
            CheckPage(db, testType, checkTitle, refnum);

            if (!string.IsNullOrEmpty(state))
            {
                string testName = checkTitle + " (" + state + ")";
                bwFindMatches.ReportProgress(0, string.Format("Checking \"{0}\", title \"{1}\"", original, testName));
                CheckPage(db, testType + " (State)", testName, refnum);

                testName = checkTitle + ", " + state;
                bwFindMatches.ReportProgress(0, string.Format("Checking \"{0}\", title \"{1}\"", original, testName));
                CheckPage(db, testType + ", State", testName, refnum);

                if (!string.IsNullOrEmpty(city))
                {
                    testName = checkTitle + " (" + city + ")";
                    bwFindMatches.ReportProgress(0, string.Format("Checking \"{0}\", title \"{1}\"", original, testName));
                    CheckPage(db, testType + " (City)", testName, refnum);

                    testName = checkTitle + ", " + city;
                    bwFindMatches.ReportProgress(0, string.Format("Checking \"{0}\", title \"{1}\"", original, testName));
                    CheckPage(db, testType + ", City", testName, refnum);

                    testName = checkTitle + " (" + city + ", " + state + ")";
                    bwFindMatches.ReportProgress(0, string.Format("Checking \"{0}\", title \"{1}\"", original, testName));
                    CheckPage(db, testType + " (City, State)", testName, refnum);

                    testName = checkTitle + ", " + city + ", " + state;
                    bwFindMatches.ReportProgress(0, string.Format("Checking \"{0}\", title \"{1}\"", original, testName));
                    CheckPage(db, testType + ", City, State", testName, refnum);
                }
            }
        }

        private static void CheckPage(NrhpDatabase db, string testType, string title, int refnum)
        {
            Page p = Page.GetPage(title);

            AddPossibleArticle(db, refnum, testType, p);
        }

    }
}
