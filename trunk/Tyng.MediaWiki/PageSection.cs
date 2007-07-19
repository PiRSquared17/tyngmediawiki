using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Tyng.MediaWiki
{
    public sealed class PageSectionCollection : BindingList<PageSection>, ICloneable
    {
        #region Constructors and Factory
        private PageSectionCollection() { }

        private PageSectionCollection(IList<PageSection> list) : base(list) { }

        public static PageSectionCollection NewPageSectionCollection()
        {
            return new PageSectionCollection();
        }

        public static PageSectionCollection GetPageSectionCollection(string content)
        {
            return new PageSectionCollection(ParseSections(content));
        }
        #endregion

        public PageSection Add(string content)
        {
            PageSection newSection = PageSection.NewPageSection();
            newSection.Content = content;
            Add(newSection);
            return newSection;
        }

        public PageSection Add(string heading, int headingLevel, string content)
        {
            PageSection newSection = PageSection.NewPageSection();
            newSection.Heading = heading;
            newSection.HeadingLevel = headingLevel;
            newSection.Content = content;
            Add(newSection);
            return newSection;
        }

        internal void RenderContent(StringBuilder sb)
        {
            if (this.Count == 0) return;

            foreach (PageSection section in this)
            {
                section.RenderContent(sb);
            }
        }

        #region Section parsing
        private static IList<PageSection> ParseSections(string content)
        {
            if (string.IsNullOrEmpty(content)) throw new ArgumentNullException("content");

            List<PageSection> sections = new List<PageSection>();

            using (StringReader sr = new StringReader(content))
            {
                string sectionHeader = null;
                string sectionContent = null;
                int headingLevel = 1;

                string line = sr.ReadLine();

                while (line != null)
                {
                    if (line.StartsWith("="))
                    {
                        int testLevel = CountChar(line, '=', true) - 1;
                        string testHead = CleanHeader(line, testLevel);

                        if (testHead != null)
                        {
                            //end last section, start new section
                            if (sectionContent != null)
                            {
                                //add section and reset
                                sections.Add(PageSection.GetPageSection(sectionHeader, headingLevel, sectionContent));
                                sectionHeader = null;
                                sectionContent = null;
                                headingLevel = 1;
                            }

                            headingLevel = testLevel;
                            sectionHeader = testHead;

                            if (headingLevel == 0)
                            {
                                //bad heading, auto clean to level 1:
                                headingLevel = 1;
                            }

                            line = null;
                        }
                    }

                    if (line != null)
                    {
                        if (sectionContent == null) sectionContent = string.Empty;

                        sectionContent += line;
                    }

                    line = sr.ReadLine();
                }

                if (sectionContent != null)
                {
                    //add section and reset
                    sections.Add(PageSection.GetPageSection(sectionHeader, headingLevel, sectionContent));
                    sectionHeader = null;
                    sectionContent = null;
                    headingLevel = 1;
                }
            }

            return sections;
        }

        private static string CleanHeader(string header, int level)
        {
            header = header.Substring(level + 1);
            header = header.Trim();

            if (!header.EndsWith(new string('=', level + 1))) return null;

            header = header.Substring(0, header.Length - (level + 1));

            return header;
        }

        private static int CountChar(string toSearch, char toCount, bool consecutive)
        {
            int total = 0;
            for (int i = 0; i < toSearch.Length; i++)
            {
                if (consecutive && toSearch[i] != toCount) return total;

                if (toSearch[i] == toCount) total++;
            }

            return total;
        }
        #endregion

        #region ICloneable Members

        public PageSectionCollection Clone()
        {
            PageSectionCollection clone = new PageSectionCollection();
            foreach (PageSection section in this)
            {
                clone.Add(section.Clone());
            }
            return clone;
        }

        object ICloneable.Clone()
        {
            return Clone();
        }

        #endregion
    }

    [Serializable]
    public sealed class PageSection : ICloneable
    {
        const int MinHeadingLevel = 1;
        const int MaxHeadingLevel = 5;

        int _headingLevel = 1;
        string _heading;
        string _content;

        #region Constructors and Factory
        private PageSection()
        {
            _headingLevel = 1;
            _heading = null;
            _content = string.Empty;
        }

        private PageSection(string content)
        {
            _headingLevel = 1;
            _heading = null;
            _content = content;
        }
        private PageSection(string heading, int headingLevel, string content)
        {
            if (headingLevel < MinHeadingLevel || headingLevel > MaxHeadingLevel) throw new ArgumentOutOfRangeException("headingLevel");

            _heading = heading;
            _headingLevel = headingLevel;
            _content = content;
        }

        public static PageSection NewPageSection()
        {
            return new PageSection();
        }

        public static PageSection GetPageSection(string content)
        {
            return new PageSection(content);
        }

        public static PageSection GetPageSection(string heading, int headingLevel, string content)
        {
            return new PageSection(heading, headingLevel, content);
        }
        #endregion

        public void AppendContent(string newContent)
        {
            if (string.IsNullOrEmpty(_content)) 
                _content = newContent;
            else
                _content += newContent;
        }

        public string Heading 
        { 
            get 
            { 
                return _heading; 
            }
            set
            {
                _heading = value;
            }
        }
        public string Content 
        { 
            get 
            { return _content; }
            set
            {
                _content = value;
            }
        }
        public int HeadingLevel
        { 
            get 
            { 
                return _headingLevel; 
            }
            set
            {
                if (value < MinHeadingLevel || value > MaxHeadingLevel) throw new ArgumentOutOfRangeException("value");

                _headingLevel = value;
            }
        }

        #region ICloneable Members

        public PageSection Clone()
        {
            PageSection o = (PageSection)Activator.CreateInstance(typeof(PageSection), true);
            o._heading = _heading;
            o._headingLevel = _headingLevel;
            o._content = _content;
            return o;
        }

        object ICloneable.Clone()
        {
            return Clone();
        }

        #endregion

        internal void RenderContent(StringBuilder sb)
        {
            if (string.IsNullOrEmpty(Heading))
            {
                sb.Append(Content);
                return;
            }

            sb.AppendFormat("{0} {1} {0}\n{2}", new string('=', HeadingLevel + 1), Heading, Content);
        }
    }
}
