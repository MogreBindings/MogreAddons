using System;
using System.Collections.Generic;
using System.Text;

namespace AutoWrap.Meta
{
    class AttributeHolder : Producer
    {
        public List<AutoWrapAttribute> Attributes = new List<AutoWrapAttribute>();

        public virtual bool HasAttribute<T>() where T : AutoWrapAttribute
        {
            foreach (AutoWrapAttribute attr in Attributes)
            {
                if (attr is T)
                    return true;
            }

            return false;
        }

        public virtual T GetAttribute<T>()
        {
            foreach (AutoWrapAttribute attr in Attributes)
            {
                if (attr is T)
                    return (T)(object)attr;
            }

            return default(T);
        }
    }
}
