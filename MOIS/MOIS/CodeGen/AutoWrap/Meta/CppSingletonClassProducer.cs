using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace AutoWrap.Meta
{
    class CppSingletonClassProducer : CppClassProducer
    {
        public CppSingletonClassProducer(Wrapper wrapper, DefClass t, IndentStringBuilder sb)
            : base(wrapper, t, sb)
        {
        }
    }
}
