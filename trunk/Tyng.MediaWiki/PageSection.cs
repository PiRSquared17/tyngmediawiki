using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Tyng.MediaWiki
{
    public sealed class PageSection
    {
        const int MinHeadingLevel = 1;
        const int MaxHeadingLevel = 5;

        int _headingLevel = 1;
        string _heading;
        string _content;

        private PageSection(string heading, int headingLevel, string content)
        {
            if (headingLevel < MinHeadingLevel || headingLevel > MaxHeadingLevel) throw new ArgumentOutOfRangeException("headingLevel");

            _heading = heading;
            _headingLevel = headingLevel;
            _content = content;
        }

        public string Heading { get { return _heading; } }
        public string Content { get { return _content; } }
        public int HeadingLevel { get { return _headingLevel; } }

        public override string ToString()
        {
            return string.Format("{0} {1} {0}\n{2}", new string('=', HeadingLevel + 1), Heading, Content);
        }

        public static PageSection[] ParseSections(string content)
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
                                sections.Add(new PageSection(sectionHeader, headingLevel, sectionContent));
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
                    sections.Add(new PageSection(sectionHeader, headingLevel, sectionContent));
                    sectionHeader = null;
                    sectionContent = null;
                    headingLevel = 1;
                }
            }

            return sections.ToArray();
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
    }
}
