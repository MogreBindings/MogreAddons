using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace AutoWrap.Meta
{
    class IncPlainWrapperClassProducer : IncClassProducer
    {
        protected override string GetTopBaseClassName()
        {
            return null;
        }

        protected override bool RequiresCleanUp
        {
            get { return _t.BaseClass == null || base.RequiresCleanUp;  }
        }

        protected override bool DoCleanupInFinalizer
        {
            get { return !_t.HasAttribute<NoFinalizerAttribute>(); }
        }

        protected override void AddInternalDeclarations()
        {
            base.AddInternalDeclarations();

            if (_t.BaseClass == null)
            {
                _sb.AppendLine(_t.FullNativeName + "* _native;");
                _sb.AppendLine("bool _createdByCLR;");
                _sb.AppendLine();
            }
        }

        protected override void AddPublicDeclarations()
        {
            base.AddPublicDeclarations();
            AddManagedNativeConversionsDefinition();
        }

        protected virtual void AddManagedNativeConversionsDefinition()
        {
            if (_t.HasAttribute<ConvertToCorrectSubclassAttribute>())
            {
                _sb.AppendLine("inline static operator " + _t.CLRName + "^ (const " + _t.FullNativeName + "* t)");
                _sb.AppendLine("{");
                _sb.AppendLine("\treturn (t) ? _ToCorrectSubclass(const_cast<" + _t.FullNativeName + "*>(t)) : nullptr;");
                _sb.AppendLine("}");

                _sb.AppendLine("inline static operator " + _t.CLRName + "^ (const " + _t.FullNativeName + "& t)");
                _sb.AppendLine("{");
                _sb.AppendLine("\treturn _ToCorrectSubclass(&const_cast<" + _t.FullNativeName + "&>(t));");
                _sb.AppendLine("}");

                _sb.AppendLine("inline static operator " + _t.FullNativeName + "* (" + _t.CLRName + "^ t) {");
                _sb.AppendLine("\treturn (t == CLR_NULL) ? 0 : static_cast<" + _t.FullNativeName + "*>(t->_native);");
                _sb.AppendLine("}");

                _sb.AppendLine("inline static operator " + _t.FullNativeName + "& (" + _t.CLRName + "^ t) {");
                _sb.AppendLine("\treturn *static_cast<" + _t.FullNativeName + "*>(t->_native);");
                _sb.AppendLine("}");

                _sb.AppendLine();
                AddToCorrectSubclassMethod();
            }
            else
            {
                if (_t.Name == _t.CLRName)
                    _sb.AppendFormatIndent("DEFINE_MANAGED_NATIVE_CONVERSIONS_FOR_PLAINWRAPPER( {0} )\n", GetClassName());
                else
                {
                    string clrName = _t.FullCLRName.Substring(_t.FullCLRName.IndexOf("::") + 2);
                    string nativeName = _t.FullNativeName.Substring(_t.FullNativeName.IndexOf("::") + 2);
                    _sb.AppendFormatIndent("DEFINE_MANAGED_NATIVE_CONVERSIONS_FOR_PLAINWRAPPER_EXPLICIT( {0}, {1} )\n", clrName, nativeName);
                }
            }
        }

        protected void AddToCorrectSubclassMethod()
        {
            _sb.AppendLine("internal: static " + _t.CLRName + "^ _ToCorrectSubclass(" + _t.FullNativeName + "* t);");
            _sb.AppendLine("public:");
        }

        protected override void AddInternalConstructors()
        {
            base.AddInternalConstructors();

            if (_t.BaseClass == null)
            {
                _sb.AppendFormatIndent("{0}( " + _t.FullNativeName + "* obj ) : _native(obj), _createdByCLR(false)\n", _t.CLRName);
            }
            else
            {
                DefClass topclass = GetTopClass(_t);
                _sb.AppendFormatIndent("{0}( " + topclass.FullNativeName + "* obj ) : " + topclass.CLRName + "(obj)\n", _t.CLRName);
            }
            _sb.AppendLine("{");
            _sb.IncreaseIndent();

            //NOTE: SuppressFinalize should not be called when the class is 'wrapped' by a SharedPtr class, (i.e DataStreamPtr -> DataStream)
            //so that the SharedPtr class gets a chance to clean up. Look for a way to have SuppressFinalize without this kind of problems.
            //_sb.AppendLine("System::GC::SuppressFinalize(this);");

            base.AddConstructorBody();
            _sb.DecreaseIndent();
            _sb.AppendLine("}\n");
        }

        protected override void AddDisposerBody()
        {
            base.AddDisposerBody();
            _sb.AppendLine("if (_createdByCLR &&_native)");
            _sb.AppendLine("{");
            if (!_t.HasAttribute<NotConstructableAttribute>())
                _sb.AppendLine("\tdelete _native;");
            _sb.AppendLine("\t_native = 0;");
            _sb.AppendLine("}");
        }

        public IncPlainWrapperClassProducer(Wrapper wrapper, DefClass t, IndentStringBuilder sb)
            : base(wrapper, t, sb)
        {
        }
    }
}
