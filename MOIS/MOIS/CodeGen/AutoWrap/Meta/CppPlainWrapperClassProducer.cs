using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace AutoWrap.Meta
{
    class CppPlainWrapperClassProducer : CppClassProducer
    {
        protected override void AddPublicDeclarations()
        {
            base.AddPublicDeclarations();
            if (_t.HasAttribute<ConvertToCorrectSubclassAttribute>())
                AddToCorrectSubclassMethod();
        }

        protected void AddToCorrectSubclassMethod()
        {
            _sb.AppendLine(_t.CLRName + "^ " + _t.CLRName + "::_ToCorrectSubclass(" + _t.FullNativeName + "* t)");
            _sb.AppendLine("{");
            _sb.IncreaseIndent();
            _sb.AppendLine(_t.FullNativeName + "* subptr;");

            if (_t.Derives != null)
            {
                foreach (string substr in _t.Derives)
                {
                    DefClass sub = _t.FindType<DefClass>(substr, false);
                    if (sub != null && _wrapper.TypeIsWrappable(sub))
                    {
                        _wrapper.AddTypeDependancy(sub);
                        _sb.AppendLine("subptr = dynamic_cast<" + sub.FullNativeName + "*>(t);");
                        _sb.AppendLine("if (subptr)");
                        if (sub.HasAttribute<ConvertToCorrectSubclassAttribute>())
                            _sb.AppendLine("\treturn " + sub.CLRName + "::_ToCorrectSubclass(t);");
                        else
                            _sb.AppendLine("\treturn gcnew " + sub.CLRName + "(subptr);");
                        _sb.AppendLine();
                    }
                }
            }

            _sb.AppendLine("return gcnew " + _t.CLRName + "(t);");
            _sb.DecreaseIndent();
            _sb.AppendLine("}");
        }

        public CppPlainWrapperClassProducer(Wrapper wrapper, DefClass t, IndentStringBuilder sb)
            : base(wrapper, t, sb)
        {
        }
    }
}
