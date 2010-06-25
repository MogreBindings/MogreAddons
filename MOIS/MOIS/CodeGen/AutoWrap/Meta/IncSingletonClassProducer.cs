using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace AutoWrap.Meta
{
    class IncSingletonClassProducer : IncClassProducer
    {
        protected override string GetTopBaseClassName()
        {
            return null;
        }

        protected override bool DoCleanupInFinalizer
        {
            get
            {
                return true;
            }
        }

        protected override bool AllowProperty(DefProperty p)
        {
            if (base.AllowProperty(p))
            {
                if (p.Name == "Singleton" || p.Name == "SingletonPtr")
                    return false;
                else
                    return true;
            }
            else
                return false;
        }

        protected override void AddDisposerBody()
        {
            _sb.AppendLine("_native = " + _t.FullNativeName + "::getSingletonPtr();");

            base.AddDisposerBody();

            _sb.AppendLine("if (_createdByCLR && _native) { delete _native; _native = 0; }");
            _sb.AppendLine("_singleton = nullptr;");
        }

        protected override void AddPrivateDeclarations()
        {
            base.AddPrivateDeclarations();
            _sb.AppendLine("static " + _t.CLRName + "^ _singleton;");

            if (_t.BaseClass == null)
            {
                _sb.AppendLine(_t.FullNativeName + "* _native;");
                _sb.AppendLine("bool _createdByCLR;");
            }
        }

        protected override void AddInternalConstructors()
        {
            base.AddInternalConstructors();

            if (_t.BaseClass == null)
                _sb.AppendLine(_t.CLRName + "( " + _t.FullNativeName + "* obj ) : _native(obj)");
            else
                _sb.AppendLine(_t.CLRName + "( " + _t.FullNativeName + "* obj ) : " + _t.BaseClass.CLRName + "(obj)");

            _sb.AppendLine("{");
            _sb.IncreaseIndent();
            base.AddConstructorBody();
            _sb.DecreaseIndent();
            _sb.AppendLine("}\n");
        }

        protected override void AddPublicFields()
        {
            _sb.AppendLine("static property " + _t.CLRName + "^ Singleton");
            _sb.AppendLine("{");
            _sb.IncreaseIndent();
            _sb.AppendLine(_t.CLRName + "^ get()");
            _sb.AppendLine("{");
            _sb.IncreaseIndent();
            _sb.AppendLine("if (_singleton == CLR_NULL)");
            _sb.AppendLine("{");
            _sb.IncreaseIndent();
            _sb.AppendLine(_t.FullNativeName + "* ptr = " + _t.FullNativeName + "::getSingletonPtr();");
            _sb.AppendLine("if ( ptr ) _singleton = gcnew " + _t.CLRName + "( ptr );");
            _sb.DecreaseIndent();
            _sb.AppendLine("}");
            _sb.AppendLine("return _singleton;");
            _sb.DecreaseIndent();
            _sb.AppendLine("}");
            _sb.DecreaseIndent();
            _sb.AppendLine("}");

            base.AddPublicFields();
        }

        public IncSingletonClassProducer(Wrapper wrapper, DefClass t, IndentStringBuilder sb)
            : base(wrapper, t, sb)
        {
        }
    }
}
