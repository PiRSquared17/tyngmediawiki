using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Tyng.ComponentModel
{
    [Serializable]
    public abstract class BusinessObjectCollection<T, C> : BindingList<C>, ICloneable
        where C : BusinessObject<C>
        where T : BusinessObjectCollection<T, C>
    {
        protected BusinessObjectCollection()
        {
        }

        protected BusinessObjectCollection(IList<C> list) : base(list)
        {
        }

        protected void MarkClean()
        {
            foreach (BusinessObject<C> child in this)
                child.MarkClean();
        }

        protected void MarkDirty()
        {
            foreach (BusinessObject<C> child in this)
                child.MarkDirty();
        }

        public void SetReadOnly()
        {
            foreach (BusinessObject<C> child in this)
                child.SetReadOnly();
        }

        public void SetEditable()
        {
            foreach (BusinessObject<C> child in this)
                child.SetEditable();
        }

        public bool IsDirty
        {
            get
            {
                foreach (BusinessObject<C> child in this)
                    if (child.IsDirty) return true;

                return false;
            }
        }

        protected T CloneInternal()
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, this);
                ms.Position = 0;
                return (T)bf.Deserialize(ms);
            }
        }

        public T Clone() { return CloneInternal(); }

        #region ICloneable Members

        object ICloneable.Clone()
        {
            return CloneInternal();
        }

        #endregion
    }
}
