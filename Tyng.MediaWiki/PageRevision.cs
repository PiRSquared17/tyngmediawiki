using System;
using System.Xml;

namespace Tyng.MediaWiki
{
    public class PageRevision
    {
        DateTime _timestamp;
        string _user;
        string _comment;
        string _content;

        internal PageRevision(XmlElement revision)
        {
            _content = revision.InnerText;
            _user = revision.Attributes["user"].Value;
            _timestamp = DateTime.Parse(revision.Attributes["timestamp"].Value);

            XmlAttribute commentAtt = revision.Attributes["comment"];

            if (commentAtt != null)
                _comment = revision.Attributes["comment"].Value;
        }

        public DateTime Timestamp { get { return _timestamp; } }
        public string Content { get { return _content; } }

        internal static PageRevision FetchLatestRevision(MediaWikiApi api, int pageId)
        {
            XmlDocument xml = api.RequestApi("query", "prop=revisions", "rvprop=timestamp|user|comment|content", "rvlimit=1", "pageids=" + pageId.ToString());
            XmlNode rev = xml.SelectSingleNode("/api/query/pages/page/revisions/rev");

            return new PageRevision((XmlElement)rev);
        }
    }
}
