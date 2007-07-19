using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Tyng.MediaWiki
{
    [Serializable]
    public sealed class Page : IPage
    {
        int? _id;
        string _title;
        MediaWikiNamespace _ns;
        DateTime _lastTouched;
        int _lastRevisionId;
        int _counter;
        bool _isNewlyCreated;
        PageRevision _lastRevision;
        PageRevision _newRevision;

        private Page()
        {
        }

        #region Fetch / Factory

        public static Page GetPage(string fullTitle)
        {
            return GetPage(MediaWikiApi.GetDefault(), fullTitle);
        }

        public static Page GetPage(int id)
        {
            return GetPage(MediaWikiApi.GetDefault(), id);
        }

        public static Page GetPage(string login, string fullTitle)
        {
            return GetPage(MediaWikiApi.GetMediaWikiApi(login), fullTitle);
        }

        public static Page GetPage(string login, int id)
        {
            return GetPage(MediaWikiApi.GetMediaWikiApi(login), id);
        }

        private static Page GetPage(MediaWikiApi api, string fullTitle)
        {
            Page p = new Page();
            fullTitle = Uri.EscapeDataString(fullTitle);
            XmlElement pageElement = (XmlElement)FetchPages(api, "titles=" + fullTitle)[0];
            p.ParsePage(pageElement);
            return p;
        }

        private static Page GetPage(MediaWikiApi api, int id)
        {
            Page p = new Page();
            XmlElement pageElement = (XmlElement)FetchPages(api, "pageids=" + id)[0];
            p.ParsePage(pageElement);
            return p;
        }

        private static XmlNodeList FetchPages(MediaWikiApi api, string param)
        {
            XmlDocument xml = api.RequestApi("query", "prop=revisions|info", "rvprop=timestamp|user|comment|content", "rvlimit=1", "redirects", param);
            return xml.SelectNodes("/api/query/pages/page");
        }
        
        private void ParsePage(XmlElement page)
        {
            _id = null;

            if (page.Attributes["missing"] == null)
            {
                _id = int.Parse(page.Attributes["pageid"].Value);
            }

            _ns = MediaWikiNamespace.Main;
            XmlAttribute nsAtt = page.Attributes["ns"];
            if (nsAtt != null) _ns = (MediaWikiNamespace)Enum.ToObject(typeof(MediaWikiNamespace), int.Parse(nsAtt.Value));

            _title = NamespaceUtility.StripNamespace(_ns, page.Attributes["title"].Value);

            if (!IsMissing)
            {
                XmlAttribute touched = page.Attributes["touched"];
                if (touched != null)
                {
                    _lastTouched = DateTime.Parse(touched.Value);
                    _counter = int.Parse(page.Attributes["counter"].Value);
                    _lastRevisionId = int.Parse(page.Attributes["lastrevid"].Value);
                    _isNewlyCreated = (page.Attributes["new"] != null);
                }

                _lastRevision = new PageRevision((XmlElement) page.SelectSingleNode("revisions/rev"), (page.Attributes["redirect"] != null));
            }
        }
        #endregion

        public Page FollowRuntimeRedirects(string login)
        {
            return FollowRuntimeRedirects(MediaWikiApi.GetMediaWikiApi(login));
        }

        public Page FollowRuntimeRedirects()
        {
            return FollowRuntimeRedirects(MediaWikiApi.GetDefault());
        }

        private Page FollowRuntimeRedirects(MediaWikiApi api)
        {
            return FollowRuntimeRedirectsInternal(api, new Stack<string>());
        }

        private Page FollowRuntimeRedirectsInternal(MediaWikiApi api, Stack<string> redirects)
        {
            if (redirects.Contains(FullTitle)) throw new InvalidOperationException("Circular redirect detected.");

            if (Config.MaxRedirectFollow == 0 || !LastRevision.IsRedirect) return this;

            if (Config.MaxRedirectFollow == redirects.Count) throw new InvalidOperationException("Long redirect chain detected.");

            redirects.Push(FullTitle);
            Page redirectTo = Page.GetPage(api, LastRevision.RedirectTitle);
            
            return redirectTo.FollowRuntimeRedirectsInternal(api, redirects);
        }

        //#region Revise Overloads
        //public void Revise(PageSectionCollection newSections, string editSummary)
        //{
        //    Revise(MediaWikiApi.Anonymous, newSections, editSummary, false);
        //}

        //public void Revise(PageSectionCollection newSections, string editSummary, bool minorEdit)
        //{
        //    Revise(MediaWikiApi.Anonymous, newSections, editSummary, minorEdit);
        //}

        //public void Revise(MediaWikiApi api, PageSectionCollection newSections, string editSummary)
        //{
        //    Revise(api, newSections, editSummary, false);
        //}

        //public void Revise(MediaWikiApi api, PageSectionCollection newSections, string editSummary, bool minorEdit)
        //{
        //    Revise(api, newSections.ConcatSections(), editSummary, minorEdit);
        //}

        //public void Revise(int sectionIndex, PageSection section, string editSummary)
        //{
        //    Revise(MediaWikiApi.Anonymous, sectionIndex, section, editSummary, false);
        //}

        //public void Revise(int sectionIndex, PageSection section, string editSummary, bool minorEdit)
        //{
        //    Revise(MediaWikiApi.Anonymous, sectionIndex, section, editSummary, minorEdit);
        //}

        //public void Revise(MediaWikiApi api, int sectionIndex, PageSection section, string editSummary)
        //{
        //    Revise(api, sectionIndex, section, editSummary, false);
        //}

        //public void Revise(MediaWikiApi api, int sectionIndex, PageSection section, string editSummary, bool minorEdit)
        //{
        //    throw new NotImplementedException();
        //}

        //public void Revise(string newContent, string editSummary)
        //{
        //    Revise(newContent, editSummary, false);
        //}

        //public void Revise(string newContent, string editSummary, bool minorEdit)
        //{
        //    Revise(MediaWikiApi.Anonymous, newContent, editSummary);
        //}

        //public void Revise(MediaWikiApi api, string newContent, string editSummary)
        //{
        //    Revise(api, newContent, editSummary, false);
        //}

        //public void Revise(MediaWikiApi api, string newContent, string editSummary, bool minorEdit)
        //{
        //    XmlDocument xml = api.EditPage(FullTitle, newContent, editSummary, minorEdit);

        //    _id = null;
        //    _categories = null;
        //    _infoFetched = false;
        //    _lastRevision = null;
        //    _redirect = null;

        //    XmlElement page = (XmlElement)xml.SelectSingleNode("/api/query/pages/page");

        //    ParsePage(page);
        //    ParsePageInfo(page);
        //    CheckMissing();
        //}
        //#endregion

        #region Save
        public Page Save()
        {
            return Save(MediaWikiApi.GetDefault());
        }

        public Page Save(string login)
        {
            return Save(MediaWikiApi.GetMediaWikiApi(login));
        }

        private Page Save(MediaWikiApi api)
        {
            throw new NotImplementedException();
        }
        #endregion

        private void CheckMissing()
        {
            if (IsMissing) throw new NotSupportedException("This action is not supported on a missing page.");
        }

        #region Properties

        public bool IsMissing { get { return _id == null; } }
        public string Title { get { return _title; } }
        public MediaWikiNamespace Namespace { get { return _ns; } }
        public int? Id { get { return _id; } }

        public string FullTitle
        {
            get
            {
                return NamespaceUtility.NamespaceToPrefix(Namespace) + Title;
            }
        }

        public int Counter
        {
            get
            {
                CheckMissing();
                return _counter;
            }
        }

        public DateTime LastTouched
        {
            get
            {
                CheckMissing();
                return _lastTouched;
            }
        }

        public int LastRevisionId
        {
            get
            {
                CheckMissing();
                return _lastRevisionId;
            }
        }

        //TODO: make this read only...
        public PageRevision LastRevision
        {
            get
            {
                CheckMissing();
                return _lastRevision;
            }
        }

        public PageRevision NewRevision
        {
            get
            {
                if (_newRevision == null)
                {
                    _newRevision = LastRevision.Clone();
                }

                return _newRevision;
            }
        }

        public bool IsNewlyCreated()
        {
            CheckMissing();
            return _isNewlyCreated;
        }

        #endregion
    }
}
