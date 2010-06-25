using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace AutoWrap.Meta
{
    class CppCLRHandleClassProducer : CppPlainWrapperClassProducer
    {
        protected override void AddConstructorBody()
        {
            base.AddConstructorBody();
            _sb.AppendLine();
            _sb.AppendLine("_native->_CLRHandle = this;");
        }

        public CppCLRHandleClassProducer(Wrapper wrapper, DefClass t, IndentStringBuilder sb)
            : base(wrapper, t, sb)
        {
        }
    }
}
