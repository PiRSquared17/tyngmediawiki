using System;
using System.Collections.Generic;
using System.ComponentModel;
using Tyng.ComponentModel;

namespace Tyng.MediaWiki
{
    [Serializable]
    public sealed class Category : BusinessObject<Category>, IPage
    {
        string _category;

        private Category() { }

        public static Category NewCategory()
        {
            Category o = new Category();
            o.MarkDirty();
            return o;
        }

        public Category GetCategory(string name)
        {
            name = NamespaceUtility.StripNamespace(MediaWikiNamespace.Category, name);
            Category o = new Category();
            o.Title = name;
            o.MarkClean();
            return o;
        }

        #region IPage Members

        public string Title
        {
            get { return _category; }
            set
            {
                CanWriteProperty();
                _category = NamespaceUtility.StripNamespace(MediaWikiNamespace.Category, value);
                MarkDirty();
            }
        }

        MediaWikiNamespace IPage.Namespace
        {
            get { return MediaWikiNamespace.Category; }
        }

        public string FullTitle
        {
            get { return NamespaceUtility.NamespaceToPrefix(MediaWikiNamespace.Category) + Title; }
        }

        #endregion

        internal void RenderContent(System.Text.StringBuilder sb)
        {
            sb.AppendFormat("[[{0}]]\n", this.FullTitle);
        }
    }

    
}
