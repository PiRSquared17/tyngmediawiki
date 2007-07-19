using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace Tyng.MediaWiki
{
    [Serializable]
    public class PageRevision : ICloneable
    {
        DateTime? _timestamp;
        string _user;
        string _comment;
        PageSectionCollection _sections = PageSectionCollection.NewPageSectionCollection();
        string _redirectTitle;
        CategoryCollection _explicitCategories;

        private PageRevision() { }

        internal PageRevision(XmlElement revision, bool isRedirect)
        {
            string text = revision.InnerText;

            Match redirectMatch = Regex.Match(text, MediaWikiApi.RedirectRegex);
            if (isRedirect)
            {
                _redirectTitle = redirectMatch.Groups["title"].Value;
                text = text.Replace(redirectMatch.Value, string.Empty);
            }

            _explicitCategories = new CategoryCollection();
            MatchCollection categoryMatches = Regex.Matches(text, MediaWikiApi.CategoryRegex);
            foreach (Match m in categoryMatches)
            {
                Category newCategory = _explicitCategories.Add(m.Groups["title"].Value);
                text = text.Replace(m.Value, string.Empty);
            }

            _sections = PageSectionCollection.GetPageSectionCollection(text);
            _user = revision.Attributes["user"].Value;
            _timestamp = DateTime.Parse(revision.Attributes["timestamp"].Value);

            XmlAttribute commentAtt = revision.Attributes["comment"];

            if (commentAtt != null)
                _comment = revision.Attributes["comment"].Value;
        }

        public DateTime? Timestamp { get { return _timestamp; } }
        public PageSectionCollection Sections { get { return _sections; } }

        public bool IsRedirect { get { return !string.IsNullOrEmpty(_redirectTitle); } }

        public string RedirectTitle 
        { 
            get 
            { 
                return _redirectTitle; 
            }
            set
            {
                _redirectTitle = value;
            }
        }

        public string Comment
        {
            get
            {
                return _comment;
            }
            set
            {
                _comment = value;
            }
        }

        public CategoryCollection Categories 
        { 
            get 
            { 
                return _explicitCategories; 
            } 
        }

        public void AppendContent(string newContent)
        {
            if (_sections.Count == 0)
                _sections.Add(newContent);
            else
                _sections[_sections.Count - 1].AppendContent(newContent);
        }

        public string GetContent()
        {
            StringBuilder sb = new StringBuilder();

            _sections.RenderContent(sb);
            sb.AppendLine();
            _explicitCategories.RenderContent(sb);

            return sb.ToString();
        }

        #region ICloneable Members

        public PageRevision Clone()
        {
            PageRevision clone = new PageRevision();
            clone._timestamp = _timestamp;
            clone._user = null;
            clone._comment = null;
            clone._sections = _sections.Clone();
            clone._redirectTitle = _redirectTitle;
            clone._explicitCategories = _explicitCategories.Clone();

            return clone;
        }

        object ICloneable.Clone()
        {
            return Clone();
        }

        #endregion
    }
}
