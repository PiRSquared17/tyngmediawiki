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

            //master tables
            NrhpDatabaseTableAdapters.STATEMTableAdapter smta = new NRHPStubber.NrhpDatabaseTableAdapters.STATEMTableAdapter();
            smta.Fill(db.STATEM);

            NrhpDatabaseTableAdapters.RETYPEMTableAdapter rtmta = new NRHPStubber.NrhpDatabaseTableAdapters.RETYPEMTableAdapter();
            rtmta.Fill(db.RETYPEM);

            NrhpDatabaseTableAdapters.FUNCMTableAdapter fmta = new NRHPStubber.NrhpDatabaseTableAdapters.FUNCMTableAdapter();
            fmta.Fill(db.FUNCM);

            NrhpDatabaseTableAdapters.ARSTYLMTableAdapter asmta = new NRHPStubber.NrhpDatabaseTableAdapters.ARSTYLMTableAdapter();
            asmta.Fill(db.ARSTYLM);

            NrhpDatabaseTableAdapters.OWNERMTableAdapter omta = new NRHPStubber.NrhpDatabaseTableAdapters.OWNERMTableAdapter();
            omta.Fill(db.OWNERM);

            //main data table
            NrhpDatabaseTableAdapters.PROPMAINTableAdapter pmta = new NRHPStubber.NrhpDatabaseTableAdapters.PROPMAINTableAdapter();
            pmta.FillByCountyToCreate(db.PROPMAIN, county);

            //other data tables
            NrhpDatabaseTableAdapters.HSFUNCDTableAdapter hsfdta = new NRHPStubber.NrhpDatabaseTableAdapters.HSFUNCDTableAdapter();
            hsfdta.Fill(db.HSFUNCD);

            NrhpDatabaseTableAdapters.ARCHTECDTableAdapter adta = new NRHPStubber.NrhpDatabaseTableAdapters.ARCHTECDTableAdapter();
            adta.Fill(db.ARCHTECD);

            NrhpDatabaseTableAdapters.ARSTYLDTableAdapter asdta = new NRHPStubber.NrhpDatabaseTableAdapters.ARSTYLDTableAdapter();
            asdta.Fill(db.ARSTYLD);

            NrhpDatabaseTableAdapters.PossibleArticlesTableAdapter pata = new NRHPStubber.NrhpDatabaseTableAdapters.PossibleArticlesTableAdapter();
            pata.Fill(db.PossibleArticles);

            NrhpDatabaseTableAdapters.OWNERDTableAdapter odta = new NRHPStubber.NrhpDatabaseTableAdapters.OWNERDTableAdapter();
            odta.Fill(db.OWNERD);

            NrhpDatabaseTableAdapters.GEOCODEMTableAdapter gmta = new NRHPStubber.NrhpDatabaseTableAdapters.GEOCODEMTableAdapter();
            gmta.Fill(db.GEOCODEM);

            StringBuilder log = new StringBuilder();

            foreach (NrhpDatabase.PROPMAINRow pm in db.PROPMAIN.Rows)
            {

                string redirTitle;
                string fullTitle;

                if (pm.IsCleanNameNull())
                    redirTitle = pm.resname.Trim();
                else
                    redirTitle = pm.CleanName.Trim();

                if (redirTitle.IndexOf('(') >= 0)
                {
                    fullTitle = redirTitle;
                }
                else
                {
                    string inStateLocation;

                    if (pm.PrimaryVicinity)
                        inStateLocation = ti.ToTitleCase(ti.ToLower(pm.PrimaryCounty)) + " County";
                    else
                        inStateLocation = ti.ToTitleCase(ti.ToLower(pm.PrimaryCity));

                    if (redirTitle.StartsWith(inStateLocation + " "))
                    {
                        fullTitle = redirTitle;
                    }
                    else
                        fullTitle = string.Format("{0} ({1}, {2})", redirTitle, inStateLocation, ti.ToTitleCase(ti.ToLower(pm.PrimaryState)));
                }

                Page talk;
                Page p = Page.GetPage(fullTitle);
                if (!p.IsMissing)
                {
                    talk = Page.GetPage(MediaWikiNamespace.MainTalk, p.Title);

                    talk.NewRevision.Categories.Add("NrhpBot Articles for Review");
                    talk.NewRevision.Comment = "tagging for review";
                    PageSection newSection = talk.NewRevision.Sections.Add("Bot Infobox", 1, "Below is the bot generated infobox in case someone wants to use it to clean up this page:\n\n");
                    newSection.AppendContent(GetInfobox(db, pm.refnum, new Dictionary<string, string>(), redirTitle));

                    talk.Save();

                    log.AppendFormat("Page [[{0}]] already exists, tagging talk for review.", fullTitle);
                }
                else
                {

                    StubSingleArticle(db, pm.refnum, p.NewRevision);

                    p.NewRevision.Comment = "New article stub created from NRIS database";
                    p = p.Save();

                    //req photo
                    talk = Page.GetPage(MediaWikiNamespace.MainTalk, p.Title);
                    if (talk.IsMissing)
                    {
                        if (pm.PrimaryVicinity)
                            talk.NewRevision.AppendContent(string.Format("{{{{reqphotoin|{0}}}}}", ti.ToTitleCase(ti.ToLower(pm.PrimaryState))));
                        else
                            talk.NewRevision.AppendContent(string.Format("{{{{reqphotoin|{0}, {1}|{1}}}}}", ti.ToTitleCase(ti.ToLower(pm.PrimaryCity)), ti.ToTitleCase(ti.ToLower(pm.PrimaryState))));

                        talk.NewRevision.AppendContent("{{WikiProject National Register of Historic Places|class=stub}}");

                        talk.NewRevision.Comment = "Adding reqphotoin and wikiproject templates";
                        talk = talk.Save();
                    }

                    log.AppendFormat("[[{1}]] ([[Special:Whatlinkshere/{1}|links]], [{{{{fullurl:{1}|action=history}}}} history]) stub created.  ", DateTime.Now, p.Title);

                    pm.BeginEdit();
                    pm.ArticleID = p.Id.Value;
                    pm.EndEdit();

                    pmta.Update(pm);

                    if (fullTitle == redirTitle)
                    {
                        LinkCollection wlh = p.WhatLinksHere();

                        if (wlh.Count != 1 || !wlh[0].Title.StartsWith("List of Registered Historic Places in "))
                        {
                            talk = Page.GetPage(MediaWikiNamespace.MainTalk, p.Title);

                            talk.NewRevision.Categories.Add("NrhpBot Articles for Review");
                            talk.NewRevision.Comment = "Tagging for NRHP review";
                            talk.NewRevision.Sections.Add("Nrhp Review", 1, "This page appears to either have multiple or 0 links from pages whose names are like 'List of Registered Historic Places in *' ~~~~");
                            talk = talk.Save();
                        }
                    }
                    else
                    {
                        p = Page.GetPage(redirTitle);

                        if (p.IsMissing)
                        {
                            LinkCollection wlh = p.WhatLinksHere();

                            p.NewRevision.RedirectTitle = fullTitle;
                            p.NewRevision.Comment = "Redirct to geo specific title by bot, contact [[User:Paultyng]] for info";
                            p = p.Save();

                            talk = Page.GetPage(MediaWikiNamespace.MainTalk, p.Title);

                            talk.NewRevision.AppendContent("{{WikiProject National Register of Historic Places|class=redirect}}");
                            if (wlh.Count != 1 || !wlh[0].Title.StartsWith("List of Registered Historic Places in "))
                            {
                                talk.NewRevision.Categories.Add("NrhpBot Articles for Review");
                                talk.NewRevision.Sections.Add("Nrhp Review", 1, "This page appears to either have multiple or 0 links from pages whose names are like 'List of Registered Historic Places in *' ~~~~");
                            }
                            talk.NewRevision.Comment = "Adding wikiproject template";
                            talk = talk.Save();

                            log.AppendFormat("[[{0}]] redirect added.", p.Title);

                        }
                        else
                        {
                            talk = Page.GetPage(MediaWikiNamespace.MainTalk, p.Title);

                            talk.NewRevision.Categories.Add("NrhpBot Articles for Review");
                            talk.NewRevision.Comment = "Tagging for NRHP review";
                            talk.NewRevision.Sections.Add("Nrhp Review", 1, "This page was preexisting before the bot run, it may need to be redirected or disambiguated with '" + fullTitle + "'. ~~~~");
                            talk = talk.Save();
                        }
                    }
                }

                //write and reset log
                WriteLog(log.ToString(), DateTime.Now);
                log = new StringBuilder();
            }


        }

        private static void WriteLog(string log, DateTime date)
        {
            Page logPage = Page.GetPage(string.Format("User:NrhpBot/Logs/{0:yyyy}/{0:MMMM}/{0:dd}", date));

            logPage.NewRevision.AppendContent(string.Format("\n* {0:" + ContentHelper.DateFormat + " HH:mm:ss} - {1}", date, log));
            logPage.NewRevision.Comment = "Adding logs for new run";

            logPage.Save();

            Page transcludePage = Page.GetPage("User:NrhpBot/Logs");
            PageSectionCollection sections = transcludePage.NewRevision.Sections;
            PageSection yearSection = sections.Find(string.Format("{0:yyyy}", date));
            PageSection monthSection = sections.Find(string.Format("{0:yyyy}/{0:MMMM}", date));
            PageSection daySection = sections.Find(string.Format("{0:yyyy}/{0:MMMM}/{0:dd}", date));
            int index;

            if (daySection != null)
                return;
            else if (monthSection != null)
            {
                index = sections.IndexOf(monthSection);
                PageSection[] days = sections.GetChildren(index);
                index++;

                if (days.Length > 0)
                {
                    index = sections.IndexOf(days[days.Length - 1]) + 1;

                    for (int i = 0; i < days.Length; i++)
                    {
                        if (int.Parse(days[i].Heading) == date.Day - 1)
                        {
                            index = sections.IndexOf(days[i]) + 1;
                            break;
                        }
                        else if (int.Parse(days[i].Heading) > date.Day)
                        {
                            index = sections.IndexOf(days[i]);
                            break;
                        }
                    }
                }

                PageSection section = PageSection.NewPageSection();
                section.Heading = date.Day.ToString();
                section.HeadingLevel = 3;
                section.Content = string.Format("{{{{/{0:yyyy}/{0:MMMM}/{0:dd}}}}}", date);

                sections.Insert(index, section);
            }
            else if (yearSection != null)
            {
                DateTimeFormatInfo dtfi = DateTimeFormatInfo.CurrentInfo;

                index = sections.IndexOf(yearSection);
                PageSection[] months = sections.GetChildren(index);
                index++;

                if (months.Length > 0)
                {
                    index = sections.IndexOf(months[months.Length - 1]) + 1;

                    for (int i = 0; i < months.Length; i++)
                    {
                        if (Array.IndexOf<string>(dtfi.MonthNames, months[i].Heading) + 1 == date.Month - 1)
                        {
                            index = sections.IndexOf(months[i]) + 1;
                            break;
                        }
                        else if (Array.IndexOf<string>(dtfi.MonthNames, months[i].Heading) + 1 > date.Month)
                        {
                            index = sections.IndexOf(months[i]);
                            break;
                        }
                    }
                }

                PageSection section = PageSection.NewPageSection();
                section.Heading = date.ToString("MMMM");
                section.HeadingLevel = 2;
                sections.Insert(index, section);

                section = PageSection.NewPageSection();
                section.Heading = date.Day.ToString();
                section.HeadingLevel = 3;
                section.Content = string.Format("{{{{/{0:yyyy}/{0:MMMM}/{0:dd}}}}}", date);
                sections.Insert(index + 1, section);
            }
            else
            {
                index = 0;

                PageSection[] years = sections.GetTopLevel();

                if (years.Length > 0)
                {
                    index = sections.IndexOf(years[years.Length - 1]) + 1;

                    for (int i = 0; i < years.Length; i++)
                    {
                        if (int.Parse(years[i].Heading) == date.Year - 1)
                        {
                            index = sections.IndexOf(years[i]) + 1;
                            break;
                        }
                        else if (int.Parse(years[i].Heading) > date.Year)
                        {
                            index = sections.IndexOf(years[i]);
                            break;
                        }
                    }
                }

                PageSection section = PageSection.NewPageSection();
                section.Heading = date.Year.ToString();
                section.HeadingLevel = 1;
                sections.Insert(index, section);

                section = PageSection.NewPageSection();
                section.Heading = date.ToString("MMMM");
                section.HeadingLevel = 2;
                sections.Insert(index + 1, section);

                section = PageSection.NewPageSection();
                section.Heading = date.Day.ToString();
                section.HeadingLevel = 3;
                section.Content = string.Format("{{{{/{0:yyyy}/{0:MMMM}/{0:dd}}}}}", date);
                sections.Insert(index + 2, section);
            }

            transcludePage.NewRevision.Comment = "Adding transclude to log summary";
            transcludePage.Save();
        }

        private static string GetInfobox(NrhpDatabase db, int refnum, Dictionary<string, string> cites, string name)
        {
            TextInfo ti = CultureInfo.CreateSpecificCulture("en").TextInfo;

            NrhpDatabase.PROPMAINRow r = db.PROPMAIN.FindByrefnum(refnum);
            bool historicDistrict = r.retypecd == "D"; //D = District
            bool landmark = r.certcd == "NL"; //NL = Designated National Landmark
            NrhpDatabase.GEOCODEMRow geocode = r.GEOCODEMRow;

            StringBuilder sb = new StringBuilder("{{Infobox nrhp\n  | name = ");

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

            sb.AppendFormat("\n  | added = {0:" + ContentHelper.DateFormat + "}", r.certdate);
            NrisCite(cites, sb);

            sb.Append("\n  | governing_body = ");

            NrhpDatabase.OWNERDRow[] owners = r.GetOWNERDRows();

            if (owners.Length > 0)
            {
                for (int i = 0; i < owners.Length; i++)
                {
                    if (i > 0 && i < owners.Length - 1) sb.Append(", ");
                    if (i == owners.Length - 1 && i > 0) sb.Append(" and ");

                    sb.Append(ti.ToTitleCase(ti.ToLower(owners[i].OWNERMRow.owner)));
                }

                NrisCite(cites, sb);
            }

            if (!r.IsmultnameNull() && !string.IsNullOrEmpty(r.multname)) sb.AppendFormat("\n  | mpsub = {0}", r.multname);

            if (geocode != null && !geocode.IsgdtlatNull() && !geocode.IsgdtlongNull())
            {
                decimal lat = r.GEOCODEMRow.gdtlat;
                decimal lon = r.GEOCODEMRow.gdtlong;

                int latDeg = Math.Abs((int)lat);
                int lonDeg = Math.Abs((int)lon);
                int latMin = Math.Abs((int)((lat % 1) * 60));
                int lonMin = Math.Abs((int)((lon % 1) * 60));
                decimal latSec = Math.Abs((((lat % 1) * 60) % 1) * 60);
                decimal lonSec = Math.Abs((((lon % 1) * 60) % 1) * 60);
                char latDir = lat > 0 ? 'N' : 'S';
                char lonDir = lon > 0 ? 'E' : 'W';

                sb.AppendFormat("\n  | lat_degrees = {0} | lat_minutes = {1} | lat_seconds = {2:0.00} | lat_direction = {3}\n  | long_degrees = {4} | long_minutes = {5} | long_seconds = {6:0.00} | long_direction = {7}\n<!-- lat/long data is approximate based on data from the NRHP database, it may not be correct -->", latDeg, latMin, latSec, latDir, lonDeg, lonMin, lonSec, lonDir);
            }

            sb.AppendFormat("\n  | refnum = {0}", r.refnum);
            NrisCite(cites, sb);

            sb.Append("\n}}");

            return sb.ToString();
        }

        public static void StubSingleArticle(NrhpDatabase db, int refnum, PageRevision newRevision)
        {
            TextInfo ti = CultureInfo.CreateSpecificCulture("en").TextInfo;

            NrhpDatabase.PROPMAINRow r = db.PROPMAIN.FindByrefnum(refnum);
            bool historicDistrict = r.retypecd == "D"; //D = District
            bool landmark = r.certcd == "NL"; //NL = Designated National Landmark
            NrhpDatabase.GEOCODEMRow geocode = r.GEOCODEMRow;
            Dictionary<string, string> cites = new Dictionary<string, string>();

            string name;

            if (r.IsCleanNameNull())
                name = r.resname;
            else
                name = r.CleanName;

            name = name.Trim();

            StringBuilder sb = new StringBuilder("<!-- This article was automatically created by [[User:NrhpBot]] from the [http://www.nr.nps.gov/ NRIS Database]. The prose may be stilted, and there may be grammatical and Wikification errors. Please improve in any way you see fit.  Contact [[User:Paultyng]] if you want to enhance the stub template. -->\n");

            sb.Append("<!-- \n");
            if (!r.IsCleanNameNull())
            {
                sb.AppendFormat("\tOriginal Name:\t\"{0}\"\n", r.resname);
            }

            sb.AppendFormat("\tMain Address:\t\"{0}\"\n", r.address);
            if (geocode != null)
            {

                sb.AppendFormat("\tGeocode Data:\n\t\tAddress:\t\"{0}\" from \"{1}\"\n\t\tCity:\t\"{2}\" from \"{3}\"\n\t\tState:\t\"{4}\" from \"{5}\"\n\t\tZip:\t\"{6}-{7}\" from \"{8}\"\n", geocode.IsgdtsadNull() ? "" : geocode.gdtsad, geocode.address, geocode.IsgdtcityNull() ? "" : geocode.gdtcity, geocode.city, geocode.IsgdtstatecdNull() ? "" : geocode.gdtstatecd, geocode.IsstatecdNull() ? "" : geocode.statecd, geocode.IsgdtpcodeNull() ? "" : geocode.gdtpcode, geocode.Isgdtplus4Null() ? "" : geocode.gdtplus4, geocode.IszipNull() ? "" : geocode.zip);
            }

            sb.Append("-->\n");

            sb.Append(GetInfobox(db, refnum, cites, name));

            sb.AppendFormat("\n'''{0}''' is a registered historic {1} {2}[[{3}, {4}|{3}]], [[{4}]], listed in the [[National Register of Historic Places|National Register]] on {5:" + ContentHelper.DateFormat + "}.  ", name, ti.ToLower(r.RETYPEMRow.retype), (r.PrimaryVicinity ? "near " : "in "), r.PrimaryCity, ti.ToTitleCase(ti.ToLower(r.PrimaryState)), r.certdate);
            if (historicDistrict && !r.IsnumcbldgNull() && r.numcbldg > 0) sb.AppendFormat("It contains {0} contributing buildings.  ", r.numcbldg);

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

                    sb.AppendFormat("* {0}\n", ContentHelper.GetLink(seeAlsoPage));
                }

                if (sb.Length > 0)
                {
                    newRevision.Sections.Add("See also", 1, sb.ToString());
                    sb = new StringBuilder();
                }
            }

            sb.Append("{{reflist}}\n\n");

            string stubTemplateName;

            if (!historicDistrict)
                stubTemplateName = string.Format("{0}-{1}-NRHP-struct-stub", ti.ToTitleCase(ti.ToLower(r.PrimaryState)), ti.ToTitleCase(ti.ToLower(r.PrimaryCounty)));
            else
                stubTemplateName = string.Format("{0}-{1}-NRHP-dist-stub", ti.ToTitleCase(ti.ToLower(r.PrimaryState)), ti.ToTitleCase(ti.ToLower(r.PrimaryCounty)));

            Page stubTemplate = Page.GetPage(MediaWikiNamespace.Template, stubTemplateName);

            if (stubTemplate.IsMissing)
            {
                stubTemplate.NewRevision.AppendContent(string.Format("{{{{{0}-NRHP-struct-stub}}}}", ti.ToTitleCase(ti.ToLower(r.PrimaryState))));
                stubTemplate.NewRevision.Comment = "Creating upmerged stub template.";

                stubTemplate.Save();

                WriteLog(string.Format("Creating upmerge stub template {0}", ContentHelper.GetLink(stubTemplate)), DateTime.Now);
            }

            sb.AppendFormat("{{{{{0}}}}}\n", stubTemplate.Title);

            sb.AppendFormat("{{{{Registered Historic Places}}}}", ti.ToTitleCase(ti.ToLower(r.PrimaryState)), ti.ToTitleCase(ti.ToLower(r.PrimaryCounty)));

            newRevision.Categories.Add(string.Format("Registered Historic Places in {0}", ti.ToTitleCase(ti.ToLower(r.PrimaryState))));

            if (historicDistrict) newRevision.Categories.Add("Historic districts in the United States");

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
