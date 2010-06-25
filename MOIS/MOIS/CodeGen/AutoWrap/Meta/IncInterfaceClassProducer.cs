using System;
using System.Collections.Generic;
using System.Text;

namespace AutoWrap.Meta
{
    class IncInterfaceClassProducer : IncClassProducer
    {
        protected override string GetTopBaseClassName()
        {
            return null;
        }

        protected override bool AllowMethodOverloads
        {
            get
            {
                return false;
            }
        }

        protected override void AddPreDeclarations()
        {
            if (!_t.IsNested)
            {
                _wrapper.AddPreDeclaration("interface class " + _t.CLRName + ";");
                _wrapper.AddPragmaMakePublicForType(_t);
            }
        }

        public IncInterfaceClassProducer(Wrapper wrapper, DefClass t, IndentStringBuilder sb)
            : base(wrapper, t, sb)
        {
        }

        protected override void AddPublicDeclarations()
        {
            _sb.AppendLine("DEFINE_MANAGED_NATIVE_CONVERSIONS_FOR_INTERFACE( " + _t.CLRName + ", " + _t.FullNativeName + " )\n");
            _sb.AppendLine("virtual " + _t.FullNativeName + "* _GetNativePtr();\n");
            base.AddPublicDeclarations();
        }

        protected override void AddPublicConstructors()
        {
        }

        protected override void AddPrivateDeclarations()
        {
        }

        protected override void AddInternalDeclarations()
        {
        }

        protected override void AddDefinition()
        {
            _sb.AppendIndent("");
            if (!_t.IsNested)
                _sb.Append("public ");
            else
                _sb.Append(GetProtectionString(_t.ProtectionType) + ": ");
            _sb.Append("interface class " + _t.CLRName + "\n");
        }

        protected override void AddProtectedDeclarations()
        {
        }

        protected override void AddMethod(DefFunction f)
        {
            if (f.IsVirtual)
                base.AddMethod(f);
        }

        protected override void AddProperty(DefProperty p)
        {
            if (p.Function.IsVirtual)
                base.AddProperty(p);
        }
    }
}
