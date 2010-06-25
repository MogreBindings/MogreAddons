using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace AutoWrap.Meta
{
    class CppNonOverridableClassProducer : CppClassProducer
    {
        protected override string GetTopBaseClassName()
        {
            return "Wrapper";
        }

        protected override void AddPostBody()
        {
            base.AddPostBody();
            AddDefaultImplementationClass();
        }

        protected override void AddConstructorBody()
        {
            base.AddConstructorBody();
            _sb.AppendLine();
            _sb.AppendLine("*_native = this;");
        }

        protected virtual void AddDefaultImplementationClass()
        {
            if (IsAbstractClass)
            {
                string className = GetClassName() + "_Default";
                foreach (DefFunction f in _abstractFunctions)
                {
                    _sb.AppendIndent(GetCLRTypeName(f) + " " + className + "::" + f.CLRName);
                    AddMethodParameters(f, f.Parameters.Count);
                    _sb.Append("\n");
                    _sb.AppendLine("{");
                    _sb.IncreaseIndent();
                    AddMethodBody(f, f.Parameters.Count);
                    _sb.DecreaseIndent();
                    _sb.AppendLine("}\n");
                }

                foreach (DefProperty p in _abstractProperties)
                {
                    string ptype = GetCLRTypeName(p);
                    string pname = className + "::" + p.Name;
                    if (p.CanRead)
                    {
                        string managedType = GetMethodNativeCall(p.GetterFunction, 0);

                        _sb.AppendLine(ptype + " " + pname + "::get()");
                        _sb.AppendLine("{");
                        if (_cachedMembers.Contains(p.GetterFunction))
                        {
                            string priv = NameToPrivate(p.Name);
                            _sb.AppendLine("\treturn ( CLR_NULL == " + priv + " ) ? (" + priv + " = " + managedType + ") : " + priv + ";");
                        }
                        else
                        {
                            _sb.AppendLine("\treturn " + managedType + ";");
                        }
                        _sb.AppendLine("}\n");
                    }

                    if (p.CanWrite)
                    {
                        _sb.AppendLine("void " + pname + "::set( " + ptype + " " + p.SetterFunction.Parameters[0].Name + " )");
                        _sb.AppendLine("{");
                        _sb.IncreaseIndent();

                        AddMethodBody(p.SetterFunction, 1);

                        _sb.DecreaseIndent();
                        _sb.AppendLine("}\n");
                    }
                }

                _sb.AppendLine();
            }
        }

        public CppNonOverridableClassProducer(Wrapper wrapper, DefClass t, IndentStringBuilder sb)
            : base(wrapper, t, sb)
        {
        }
    }
}
