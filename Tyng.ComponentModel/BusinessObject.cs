using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Tyng.ComponentModel
{
    [Serializable]
    public abstract class BusinessObject<T> : ICloneable
        where T : BusinessObject<T>
    {
        bool _isDirty = false;
        bool _readOnly = false;

        protected internal void MarkClean()
        {
            _isDirty = false;
        }

        protected internal void MarkDirty()
        {
            if (_readOnly) throw new ReadOnlyException();

            _isDirty = true;
        }

        protected void CanWriteProperty()
        {
            if (_readOnly) throw new ReadOnlyException();
        }

        public virtual void SetReadOnly()
        {
            _readOnly = true;
        }

        public virtual void SetEditable()
        {
            _readOnly = false;
        }

        public virtual bool IsDirty { get { return _isDirty; } }

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
