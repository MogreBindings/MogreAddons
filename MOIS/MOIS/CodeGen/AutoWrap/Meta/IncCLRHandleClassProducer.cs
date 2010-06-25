using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace AutoWrap.Meta
{
    class IncCLRHandleClassProducer : IncPlainWrapperClassProducer
    {
        protected override string GetTopBaseClassName()
        {
            return "INativePointer";
        }

        protected override bool DoCleanupInFinalizer
        {
            get { return _t.HasAttribute<DoCleanupInFinalizerAttribute>(); }
        }

        protected override void AddPrivateDeclarations()
        {
            base.AddPrivateDeclarations();
            _sb.AppendLine();
            _sb.AppendLine("virtual void ClearNativePtr() = INativePointer::ClearNativePtr");
            _sb.AppendLine("{");
            _sb.AppendLine("\t_native = 0;");
            _sb.AppendLine("}");
        }

        protected override void AddManagedNativeConversionsDefinition()
        {
            _sb.AppendFormatIndent("DEFINE_MANAGED_NATIVE_CONVERSIONS_FOR_CLRHANDLE( {0} )\n", GetClassName());
        }

        public IncCLRHandleClassProducer(Wrapper wrapper, DefClass t, IndentStringBuilder sb)
            : base(wrapper, t, sb)
        {
        }
    }
}
