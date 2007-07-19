using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Tyng.MediaWiki
{
    [Serializable]
    public sealed class Category : IPage, ICloneable
    {
        string _category;

        private Category() { }

        public Category(string name)
        {
            _category = NamespaceUtility.StripNamespace(MediaWikiNamespace.Category, name);
        }

        #region IPage Members

        public string Title
        {
            get { return _category; }
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

        #region ICloneable Members

        public Category Clone()
        {
            Category clone = new Category();
            clone._category = _category;
            return clone;
        }

        object ICloneable.Clone()
        {
            return Clone();
        }

        #endregion

        internal void RenderContent(System.Text.StringBuilder sb)
        {
            sb.AppendFormat("[[{0}]]\n", this.FullTitle);
        }
    }

    [Serializable]
    public sealed class CategoryCollection : BindingList<Category>, ICloneable
    {
        public Category Add(string categoryName)
        {
            Category newCategory = new Category(categoryName);
            Add(newCategory);
            return newCategory;
        }

        public void AddRange(IEnumerable<string> categories)
        {
            foreach (string s in categories)
                Add(s);
        }

        #region ICloneable Members

        public CategoryCollection Clone()
        {
            CategoryCollection clone = new CategoryCollection();
            foreach (Category c in this)
                clone.Add(c.Clone());

            return clone;
        }

        object ICloneable.Clone()
        {
            return Clone();
        }

        #endregion

        internal void RenderContent(System.Text.StringBuilder sb)
        {
            foreach (Category c in this)
                c.RenderContent(sb);
        }
    }
}
