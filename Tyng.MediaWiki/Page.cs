using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Tyng.MediaWiki
{
    
    public sealed class Page
    {
        bool _infoFetched = false;
        int? _id;
        string _title;
        MediaWikiNamespace _ns;
        DateTime _lastTouched;
        int _lastRevisionId;
        int _size;
        int _counter;
        bool _isRedirect;
        Page _redirect;
        bool _new;
        string[] _categories;
        PageRevision _lastRevision;

        internal Page(XmlElement pageElement)
        {
            ParsePage(pageElement);
            ParsePageInfo(pageElement);
            ParseCategories(pageElement);
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
        }

        public void Revise(string newContent, string editSummary)
        {
            Revise(newContent, editSummary, false);
        }

        public void Revise(string newContent, string editSummary, bool minorEdit)
        {
            Revise(MediaWikiApi.Anonymous, newContent, editSummary);
        }

        public void Revise(MediaWikiApi api, string newContent, string editSummary)
        {
            Revise(api, newContent, editSummary, false);
        }

        public void Revise(MediaWikiApi api, string newContent, string editSummary, bool minorEdit)
        {
            XmlDocument xml = api.EditPage(FullTitle, newContent, editSummary, minorEdit);

            _id = null;
            _categories = null;
            _infoFetched = false;
            _lastRevision = null;
            _redirect = null;

            XmlElement page = (XmlElement)xml.SelectSingleNode("/api/query/pages/page");

            ParsePage(page);
            ParsePageInfo(page);
            CheckMissing();
        }

        #region Content
        private void FetchContent(MediaWikiApi api)
        {
            if (_lastRevision != null) return;
            CheckMissing();

            _lastRevision = PageRevision.FetchLatestRevision(api, Id.Value);
        }
        #endregion

        #region Categories

        private void FetchCategories(MediaWikiApi api)
        {
            if (_categories != null) return;
            CheckMissing();

            XmlDocument xml = api.RequestApi("query", "prop=categories", "pageids=" + Id.ToString());
            XmlNode page = xml.SelectSingleNode("/api/query/pages/page");
            ParseCategories((XmlElement)page);
        }


        private void ParseCategories(XmlElement page)
        {
            _categories = null;

            if (!IsMissing)
            {
                XmlNode categories = page.SelectSingleNode("categories");

                if (categories != null)
                {
                    List<string> foundCategories = new List<string>(categories.ChildNodes.Count);

                    foreach (XmlNode category in categories.ChildNodes)
                    {
                        foundCategories.Add(NamespaceUtility.StripNamespace(MediaWikiNamespace.Category, category.Attributes["title"].Value));
                    }

                    _categories = foundCategories.ToArray();
                }
            }
        }

        #endregion

        #region Info

        private void ParsePageInfo(XmlElement page)
        {
            _infoFetched = false;

            if (! IsMissing)
            {
                XmlAttribute touched = page.Attributes["touched"];
                if (touched != null)
                {
                    _lastTouched = DateTime.Parse(touched.Value);
                    _counter = int.Parse(page.Attributes["counter"].Value);
                    _lastRevisionId = int.Parse(page.Attributes["lastrevid"].Value);
                    _size = int.Parse(page.Attributes["length"].Value);
                    _new = (page.Attributes["new"] != null);
                    _isRedirect = (page.Attributes["redirect"] != null);

                    _infoFetched = true;
                }
            }
        }

        private void FetchInfo(MediaWikiApi api)
        {
            if (_infoFetched) return;
            CheckMissing();

            XmlDocument xml = api.RequestApi("query", "prop=info", "pageids=" + Id.ToString());
            XmlNode page = xml.SelectSingleNode("/api/query/pages/page");
            ParseCategories((XmlElement)page);
        }

        #endregion

        #region Redirect
        private void ParseRedirect(XmlElement page)
        {
            _redirect = null;

            if (IsRedirect)
            {
                _redirect = new Page(page);
            }
        }

        private void FetchRedirect(MediaWikiApi api)
        {
            if (!IsRedirect || _redirect != null) return;
            CheckMissing();

            XmlDocument xml = api.RequestApi("query", "redirects", "pageids=" + Id.ToString());
            XmlNode page = xml.SelectSingleNode("/api/query/pages/page");
            ParseRedirect((XmlElement)page);
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

        public int? EndPointId
        {
            get
            {
                if (Redirect == null)
                    return Id;
                return Redirect.EndPointId;
            }
        }

        public string EndPointCurrentContent
        {
            get
            {
                if (Redirect == null)
                    return CurrentContent;
                return Redirect.EndPointCurrentContent;
            }
        }

        public string CurrentContent
        {
            get
            {
                FetchContent(MediaWikiApi.Anonymous);

                return _lastRevision.Content;
            }
        }

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
                FetchInfo(MediaWikiApi.Anonymous);

                return _counter;
            }
        }

        public bool IsRedirect
        {
            get
            {
                FetchInfo(MediaWikiApi.Anonymous);

                return _isRedirect;
            }
        }

        public bool IsNew
        {
            get
            {
                FetchInfo(MediaWikiApi.Anonymous);

                return _new;
            }
        }

        public DateTime LastTouched
        {
            get
            {
                FetchInfo(MediaWikiApi.Anonymous);

                return _lastTouched;
            }
        }

        public int LastRevisionId
        {
            get
            {
                FetchInfo(MediaWikiApi.Anonymous);

                return _lastRevisionId;
            }
        }

        public int Size
        {
            get
            {
                FetchInfo(MediaWikiApi.Anonymous);

                return _size;
            }
        }

        public string[] Categories
        {
            get
            {
                FetchCategories(MediaWikiApi.Anonymous);

                return _categories;
            }
        }

        public Page Redirect
        {
            get
            {
                FetchRedirect(MediaWikiApi.Anonymous);

                return _redirect;
            }
        }
        #endregion
    }
}
