using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace AutoWrap.Meta
{
    class CppNativePtrValueClassProducer : CppClassProducer
    {
        protected override string GetNativeInvokationTarget(bool isConst)
        {
            return "_native";
        }

        protected override string GetNativeInvokationTargetObject()
        {
            return "*_native";
        }

        protected override void AddPublicDeclarations()
        {
            base.AddPublicDeclarations();

            if (!IsReadOnly && IsConstructable)
            {
                _sb.AppendLine();
                AddCreators();
            }
        }

        protected override void AddPublicConstructor(DefFunction f)
        {
        }

        protected virtual void AddCreators()
        {
            if (!_t.IsNativeAbstractClass)
            {
                if (_t.Constructors.Length > 0)
                {
                    foreach (DefFunction func in _t.Constructors)
                        if (func.ProtectionType == ProtectionType.Public)
                            AddCreator(func);
                }
                else
                    AddCreator(null);

                _sb.AppendLine();
            }
        }

        protected virtual void AddCreator(DefFunction f)
        {
            if (f == null)
                AddCreatorOverload(f, 0);
            else
            {
                int defcount = 0;

                if (!f.HasAttribute<NoDefaultParamOverloadsAttribute>())
                {
                    foreach (DefParam param in f.Parameters)
                        if (param.DefaultValue != null)
                            defcount++;
                }

                // The overloads (because of default values)
                for (int dc = 0; dc <= defcount; dc++)
                {
                    if (dc < defcount && f.HasAttribute<HideParamsWithDefaultValuesAttribute>())
                        continue;

                    AddCreatorOverload(f, f.Parameters.Count - dc);
                }

            }
        }

        protected virtual void AddCreatorOverload(DefFunction f, int count)
        {
            _sb.AppendIndent(_t.FullCLRName + " " + GetClassName() + "::Create");
            if (f == null)
                _sb.Append("()");
            else
                AddMethodParameters(f, count);

            _sb.Append("\n");
            _sb.AppendLine("{");
            _sb.IncreaseIndent();

            string preCall = null, postCall = null;

            if (f != null)
            {
                preCall = GetMethodPreNativeCall(f, count);
                postCall = GetMethodPostNativeCall(f, count);

                if (!String.IsNullOrEmpty(preCall))
                    _sb.AppendLine(preCall);
            }

            _sb.AppendLine(_t.CLRName + " ptr;");
            _sb.AppendIndent("ptr._native = new " + _t.FullNativeName + "(");

            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    DefParam p = f.Parameters[i];
                    string newname;
                    p.Type.GetPreCallParamConversion(p, out newname);
                    _sb.Append(" " + newname);
                    if (i < count - 1) _sb.Append(",");
                }
            }

            _sb.Append(");\n");

            if (!String.IsNullOrEmpty(postCall))
            {
                _sb.AppendLine();
                _sb.AppendLine(postCall);
                _sb.AppendLine();
            }

            _sb.AppendLine("return ptr;");

            _sb.DecreaseIndent();
            _sb.AppendLine("}");
        }

        public CppNativePtrValueClassProducer(Wrapper wrapper, DefClass t, IndentStringBuilder sb)
            : base(wrapper, t, sb)
        {
        }
    }
}
