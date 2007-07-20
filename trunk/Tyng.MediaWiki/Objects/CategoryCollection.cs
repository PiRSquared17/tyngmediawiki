using System;
using System.Collections.Generic;
using System.Text;
using Tyng.ComponentModel;

namespace Tyng.MediaWiki
{
    [Serializable]
    public sealed class CategoryCollection : BusinessObjectCollection<CategoryCollection, Category>
    {
        public Category Add(string categoryName)
        {
            Category newCategory = Category.NewCategory();
            newCategory.Title = categoryName;
            Add(newCategory);
            return newCategory;
        }

        public void AddRange(IEnumerable<string> categories)
        {
            foreach (string s in categories)
                Add(s);
        }

        internal void RenderContent(System.Text.StringBuilder sb)
        {
            foreach (Category c in this)
                c.RenderContent(sb);
        }

        internal static CategoryCollection NewCategoryCollection()
        {
            return new CategoryCollection();
        }

        private CategoryCollection() { }
    }
}
