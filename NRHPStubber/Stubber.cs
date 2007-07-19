using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Text;
using Tyng.MediaWiki;

namespace NRHPStubber
{
    public static class Stubber
    {
        public static void Stub(string county)
        {
            TextInfo ti = CultureInfo.CreateSpecificCulture("en").TextInfo;

            NrhpDatabase db = new NrhpDatabase();
            NrhpDatabaseTableAdapters.PROPMAINTableAdapter pmta = new NRHPStubber.NrhpDatabaseTableAdapters.PROPMAINTableAdapter();
            pmta.FillByCountyToCreate(db.PROPMAIN, county);

            NrhpDatabaseTableAdapters.RETYPEMTableAdapter rtmta = new NRHPStubber.NrhpDatabaseTableAdapters.RETYPEMTableAdapter();
            rtmta.Fill(db.RETYPEM);

            NrhpDatabaseTableAdapters.FUNCMTableAdapter fmta = new NRHPStubber.NrhpDatabaseTableAdapters.FUNCMTableAdapter();
            fmta.Fill(db.FUNCM);

            NrhpDatabaseTableAdapters.HSFUNCDTableAdapter hsfdta = new NRHPStubber.NrhpDatabaseTableAdapters.HSFUNCDTableAdapter();
            hsfdta.Fill(db.HSFUNCD);

            NrhpDatabaseTableAdapters.ARCHTECDTableAdapter adta = new NRHPStubber.NrhpDatabaseTableAdapters.ARCHTECDTableAdapter();
            adta.Fill(db.ARCHTECD);

            NrhpDatabaseTableAdapters.ARSTYLDTableAdapter asdta = new NRHPStubber.NrhpDatabaseTableAdapters.ARSTYLDTableAdapter();
            asdta.Fill(db.ARSTYLD);

            NrhpDatabaseTableAdapters.ARSTYLMTableAdapter asmta = new NRHPStubber.NrhpDatabaseTableAdapters.ARSTYLMTableAdapter();
            asmta.Fill(db.ARSTYLM);

            NrhpDatabaseTableAdapters.PossibleArticlesTableAdapter pata = new NRHPStubber.NrhpDatabaseTableAdapters.PossibleArticlesTableAdapter();
            pata.Fill(db.PossibleArticles);

            NrhpDatabaseTableAdapters.OWNERMTableAdapter omta = new NRHPStubber.NrhpDatabaseTableAdapters.OWNERMTableAdapter();
            omta.Fill(db.OWNERM);

            NrhpDatabaseTableAdapters.OWNERDTableAdapter odta = new NRHPStubber.NrhpDatabaseTableAdapters.OWNERDTableAdapter();
            odta.Fill(db.OWNERD);

            StringBuilder log = new StringBuilder();

            foreach (NrhpDatabase.PROPMAINRow pm in db.PROPMAIN.Rows)
            {

                string redirTitle;
                string fullTitle;

                if (pm.IsCleanNameNull())
                    redirTitle = pm.resname;
                else
                    redirTitle = pm.CleanName;

                if (pm.PrimaryVicinity)
                    fullTitle = redirTitle + " (" + ti.ToTitleCase(ti.ToLower(pm.PrimaryState)) + ")";
                else
                    fullTitle = redirTitle + " (" + ti.ToTitleCase(ti.ToLower(pm.PrimaryCity)) + ", " + ti.ToTitleCase(ti.ToLower(pm.PrimaryState)) + ")";

                Page p = Page.GetPage(fullTitle);
                if (!p.IsMissing) throw new Exception("Page exists in current location");

                StubSingleArticle(db, pm.refnum, p.NewRevision);

                p.NewRevision.Comment = "New article stub created from NRIS database";
                p.Save();

                log.AppendFormat("*{0:yyyy-MM-dd HH:mm:ss} - [[{1}]] ([[Special:Whatlinkshere/{1}|links]], [{{{{fullurl:{1}|action=history}}}} history]) stub created.  ", DateTime.Now, p.Title);

                pm.BeginEdit();
                pm.ArticleID = p.Id.Value;
                pm.EndEdit();

                pmta.Update(pm);

                p = Page.GetPage(redirTitle);
                if (p.IsMissing)
                {
                    p.NewRevision.RedirectTitle = fullTitle;
                    p.NewRevision.Comment = "Redirct to geo specific title by bot, contact [[User:Paultyng]] for info";
                    p.Save();
                    log.AppendFormat("[[{0}]] redirect added.", p.Title);
                }

                //write and reset log
                WriteLog(log.ToString(), DateTime.Now);
                log = new StringBuilder();
            }

            
        }

        private static void WriteLog(string log, DateTime date)
        {
            Page logPage = Page.GetPage(string.Format("User:NrhpBot/Logs/{0:yyyy}/{0:MMMM}/{0:dd}", date));

            logPage.NewRevision.AppendContent("\n" + log);
            logPage.NewRevision.Comment = "Adding logs for new run";

            logPage.Save();
        }

        public static void StubSingleArticle(NrhpDatabase db, int refnum, PageRevision newRevision)
        {
            TextInfo ti = CultureInfo.CreateSpecificCulture("en").TextInfo;

            NrhpDatabase.PROPMAINRow r = db.PROPMAIN.FindByrefnum(refnum);
            Dictionary<string, string> cites = new Dictionary<string, string>();
            bool historicDistrict = r.retypecd == "D"; //D = District
            bool landmark = r.certcd == "NL"; //NL = Designated National Landmark

            StringBuilder sb = new StringBuilder("<!-- This article was automatically created by [[User:NrhpBot]] from the [http://www.nr.nps.gov/ NRIS Database]. The prose may be stilted, and there may be grammatical and Wikification errors. Please improve in any way you see fit.  Contact [[User:Paultyng]] if you want to enhance the stub template. -->");
            sb.AppendLine();

            if (!r.IsCleanNameNull())
            {
                sb.AppendFormat("<!-- \"{0}\" original name -->\n", r.resname);
            }

            sb.Append("{{Infobox nrhp\n  | name = ");

            string name;

            if (r.IsCleanNameNull())
                name = r.resname;
            else
                name = r.CleanName;

            name = name.Trim();

            sb.AppendLine(name);

            if (historicDistrict)
                sb.Append("  | nrhp_type = hd\n");
            else if (landmark)
                sb.Append("  | nrhp_type = nhl\n");

            if (historicDistrict && !r.IsacreNull()) sb.AppendFormat("  | area = {0} acres\n", r.acre);

            sb.Append("  | image =\n  | caption =\n  | ");

            if (r.PrimaryVicinity)
                sb.Append("nearest_city = ");
            else
                sb.Append("location = ");

            sb.AppendFormat("[[{0}, {1}|{0}]], [[{1}]]\n  | architect = ", ti.ToTitleCase(ti.ToLower(r.PrimaryCity)), ti.ToTitleCase(ti.ToLower(r.PrimaryState)));

            NrhpDatabase.ARCHTECDRow[] architects = r.GetARCHTECDRows();

            if (architects.Length > 0)
            {
                for (int i = 0; i < architects.Length; i++)
                {
                    if (i > 0 && i < architects.Length - 1) sb.Append(", ");
                    if (i == architects.Length - 1 && i > 0) sb.Append(" and ");

                    if (architects[i].IsCleanNameNull())
                        sb.Append(architects[i].architect);
                    else
                        sb.AppendFormat("{0} <!-- \"{1}\" in data -->", architects[i].CleanName, architects[i].architect);
                }

                NrisCite(cites, sb);
            }

            sb.Append("\n  | architecture = ");

            NrhpDatabase.ARSTYLDRow[] styles = r.GetARSTYLDRows();

            if (styles.Length > 0)
            {
                for (int i = 0; i < styles.Length; i++)
                {
                    if (i > 0 && i < styles.Length - 1) sb.Append(", ");
                    if (i == styles.Length - 1 && i > 0) sb.Append(" and ");

                    sb.Append(styles[i].ARSTYLMRow.arstyl);
                }

                NrisCite(cites, sb);
            }

            sb.AppendFormat("\n  | added = [[{0:MMMM} {0:%d}]], [[{0:yyyy}]]", r.certdate);
            NrisCite(cites, sb);

            sb.Append("\n  | governing_body = ");

            NrhpDatabase.OWNERDRow[] owners = r.GetOWNERDRows();

            if (owners.Length > 0)
            {
                for (int i = 0; i < owners.Length; i++)
                {
                    if(i > 0 && i < owners.Length - 1) sb.Append(", ");
                    if(i == owners.Length - 1 && i > 0) sb.Append(" and ");

                    sb.Append(ti.ToTitleCase(ti.ToLower(owners[i].OWNERMRow.owner)));
                }

                NrisCite(cites, sb);
            }

            sb.AppendFormat("\n  | refnum = {0}", r.refnum);
            NrisCite(cites, sb);

            sb.AppendFormat("\n}}}}\n'''{0}''' is a registered historic {1} {2}[[{3}, {4}|{3}]], [[{4}]], listed in the [[National Register of Historic Places|National Register]] on [[{5:MMMM} {5:%d}]], [[{5:yyyy}]].  ", name, ti.ToLower(r.RETYPEMRow.retype), (r.PrimaryVicinity ? "near " : "in "), r.PrimaryCity, ti.ToTitleCase(ti.ToLower(r.PrimaryState)), r.certdate);
            if (historicDistrict && !r.IsnumcbldgNull()) sb.AppendFormat("It contains {0} contributing buildings.  ", r.numcbldg);

            newRevision.Sections.Add(sb.ToString()); //add overview
            sb = new StringBuilder();

            NrhpDatabase.HSFUNCDRow[] historicFunctions = r.GetHSFUNCDRows();

            if (historicFunctions.Length > 0)
            {
                foreach (NrhpDatabase.HSFUNCDRow historicFunction in historicFunctions)
                {
                    sb.AppendFormat("*{0}\n", ti.ToTitleCase(ti.ToLower(historicFunction.FUNCMRow.func)));
                }

                newRevision.Sections.Add("Historic uses", 1, sb.ToString());
                sb = new StringBuilder();
            }

            NrhpDatabase.PossibleArticlesRow[] possibleArticles = r.GetPossibleArticlesRows();
            foreach (NrhpDatabase.PossibleArticlesRow possibleArticle in possibleArticles)
            {
                if (!possibleArticle.IsSeeAlsoNull())
                {
                    Page seeAlsoPage = Page.GetPage(possibleArticle.ArticleID);

                    sb.AppendFormat("* [[{0}]]\n", seeAlsoPage.FullTitle);
                }

                if(sb.Length > 0)
                {
                    newRevision.Sections.Add("See also", 1, sb.ToString());
                    sb = new StringBuilder();
                }
            }

            sb.Append("{{reflist}}\n\n");

            if(!historicDistrict)
                sb.AppendFormat("{{{{{0}-{1}-NRHP-struct-stub}}}}\n", ti.ToTitleCase(ti.ToLower(r.PrimaryState)), ti.ToTitleCase(ti.ToLower(r.PrimaryCounty)));
            else
                sb.AppendFormat("{{{{{0}-{1}-NRHP-dist-stub}}}}\n", ti.ToTitleCase(ti.ToLower(r.PrimaryState)), ti.ToTitleCase(ti.ToLower(r.PrimaryCounty)));

            sb.AppendFormat("{{{{Registered Historic Places}}}}", ti.ToTitleCase(ti.ToLower(r.PrimaryState)), ti.ToTitleCase(ti.ToLower(r.PrimaryCounty)));

            newRevision.Categories.Add(string.Format("Registered Historic Places in {0}", ti.ToTitleCase(ti.ToLower(r.PrimaryState))));

            if (historicDistrict) newRevision.Categories.Add("[[Category:Historic districts in the United States]]");

            newRevision.Sections.Add("Notes", 1, sb.ToString());
        }

        private static void NrisCite(Dictionary<string, string> cites, StringBuilder sb)
        {
            Cite(cites, "nris", "{{cite web|url=http://www.nr.nps.gov/|title=National Register Information System|date=[[2007-06-30]]|work=National Register of Historic Places|publisher=National Park Service}}", sb);
        }


        private static void Cite(Dictionary<string, string> cites, string name, string cite, StringBuilder sb)
        {
            if (cites.ContainsKey(name))
                sb.AppendFormat("<ref name=\"{0}\"/>", name);
            else
            {
                sb.AppendFormat("<ref name=\"{0}\">{1}</ref>", name, cite);
                cites.Add(name, cite);
            }
        }
    }
}
