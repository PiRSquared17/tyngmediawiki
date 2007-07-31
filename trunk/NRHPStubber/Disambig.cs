using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Globalization;
using System.Text;
using Tyng.MediaWiki;

namespace NRHPStubber
{
    public static class Disambig
    {
        public static void CreateDisambigs()
        {
            const int low = 10;
            const int high = 20000;

            List<string> toCreate = new List<string>();

            using (DbConnection conn = new SqlConnection(Properties.Settings.Default.NRHP20070628))
            {
                conn.Open();

                using (DbCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select isnull(cleanname, resname), count(*) from propmain where certcd in ('LI', 'UN', 'NL') group by isnull(cleanname, resname) having count(*) between " + low.ToString() + " and " + high.ToString() + " order by 2 desc";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;

                    using (IDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (dr.Read())
                            toCreate.Add(dr.GetString(0));
                    }
                }
            }

            NrhpDatabase db = new NrhpDatabase();

            //master tables
            NrhpDatabaseTableAdapters.STATEMTableAdapter smta = new NRHPStubber.NrhpDatabaseTableAdapters.STATEMTableAdapter();
            smta.Fill(db.STATEM);

            NrhpDatabaseTableAdapters.GEOCODEMTableAdapter gmta = new NRHPStubber.NrhpDatabaseTableAdapters.GEOCODEMTableAdapter();
            gmta.Fill(db.GEOCODEM);

            foreach (string name in toCreate)
                CreateSingleDisambig(db, name);
        }

        private static void CreateSingleDisambig(NrhpDatabase db, string name)
        {
            TextInfo ti = CultureInfo.CreateSpecificCulture("en").TextInfo;
            Page p = Page.GetPage(MediaWikiNamespace.Main, name);

            NrhpDatabaseTableAdapters.PROPMAINTableAdapter pmta = new NRHPStubber.NrhpDatabaseTableAdapters.PROPMAINTableAdapter();
            pmta.FillByName(db.PROPMAIN, name);

            List<string> items = new List<string>(db.PROPMAIN.Rows.Count);

            foreach (NrhpDatabase.PROPMAINRow r in db.PROPMAIN)
            {
                string qualifier;
                string city = ti.ToTitleCase(ti.ToLower(r.PrimaryCity));
                string county = ti.ToTitleCase(ti.ToLower(r.PrimaryCounty)) + " County";
                string state = ti.ToTitleCase(ti.ToLower(r.PrimaryState));

                if (r.PrimaryVicinity)
                    qualifier = county;
                else
                    qualifier = city;

                items.Add(ContentHelper.GetLink(MediaWikiNamespace.Main, string.Format("{0} ({1}, {2})", name, qualifier, state)));
            }

            if (p.IsMissing)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("'''{0}''' may refer to:\n", name);
                sb.Append(ContentHelper.ToUnorderedList(items.ToArray()));
                sb.Append("\n\n{{geodis}}");

                p.NewRevision.AppendContent(sb.ToString());
                p.NewRevision.Comment = "Creating new disambiguation page for Nrhp data";
                p = p.Save();

                Page talk = Page.GetPage(MediaWikiNamespace.MainTalk, name);
                talk.NewRevision.AppendContent("{{WikiProject National Register of Historic Places|class=Dab}}");
                talk.NewRevision.Comment = "tagging for wikiproject";
                talk = talk.Save();

                Stubber.WriteLog(string.Format("Created disambiguation page [[{0}]] for {1} items.", p.Title, items.Count), DateTime.Now);
            }
            else
            {
                Page dis = Page.GetPage(MediaWikiNamespace.Main, p.Title + " (disambiguation)");
                if (!dis.IsMissing)
                {
                    Page talk = Page.GetPage(MediaWikiNamespace.MainTalk, dis.Title);

                    if (talk.IsMissing || (!talk.LastRevision.Categories.Contains(Stubber.CategoryAfR) && !talk.LastRevision.Categories.Contains("Dab-Class National Register of Historic Places articles")))
                    {
                        talk.NewRevision.Categories.Add(Stubber.CategoryAfR);
                        talk.NewRevision.Sections.Add("Nrhp Review", 1, "This page should be an Nrhp disambiguation page, please confirm that it is. It should contain the following links:\n");
                        talk.NewRevision.AppendContent(ContentHelper.ToUnorderedList(items.ToArray()));
                        talk.NewRevision.Comment = "Tagging for review";
                        talk = talk.Save();

                        Stubber.WriteLog(string.Format("Page already exists at [[{0}]] flagged for review.", p.Title), DateTime.Now);
                    }
                }
                else
                {
                    Page talk = Page.GetPage(MediaWikiNamespace.MainTalk, p.Title);

                    if (talk.IsMissing || (!talk.LastRevision.Categories.Contains(Stubber.CategoryAfR) && !talk.LastRevision.Categories.Contains("Dab-Class National Register of Historic Places articles")))
                    {
                        talk.NewRevision.Categories.Add(Stubber.CategoryAfR);
                        talk.NewRevision.Sections.Add("Nrhp Review", 1, "This page should be an Nrhp disambiguation page, please confirm that it is. It should contain the following links:\n");
                        talk.NewRevision.AppendContent(ContentHelper.ToUnorderedList(items.ToArray()));
                        talk.NewRevision.Comment = "Tagging for review";
                        talk = talk.Save();

                        Stubber.WriteLog(string.Format("Page already exists at [[{0}]] flagged for review.", p.Title), DateTime.Now);
                    }
                }
            }
        }
    }
}
