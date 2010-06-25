using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace AutoWrap.Meta
{
    class CppNativeDirectorClassProducer : CppClassProducer
    {
        public override bool IsNativeClass
        {
            get { return true; }
        }

        public CppNativeDirectorClassProducer(Wrapper wrapper, DefClass t, IndentStringBuilder sb)
            : base(wrapper, t, sb)
        {
        }

        protected override void AddPublicConstructor(DefFunction f)
        {
        }

        protected override string GetClassName()
        {
            string full = _t.FullCLRName;
            int index = full.IndexOf("::");
            string name = full.Substring(index + 2);

            index = name.LastIndexOf("::");
            if (index == -1)
                return GetNativeDirectorName(_t);

            if (!_t.IsNested)
            {
                return name.Substring(0, index + 2) + GetNativeDirectorName(_t);
            }
            else
            {
                name = name.Substring(0, index);
                index = name.LastIndexOf("::");
                if (index == -1)
                    return GetNativeDirectorName(_t);
                else
                    return name.Substring(0, index + 2) + GetNativeDirectorName(_t);
            }
        }

        protected override void AddMethod(DefFunction f)
        {
            string def = f.Definition.Replace(f.Class.FullNativeName, GetClassName()) + "(";
            if (def.StartsWith("virtual "))
                def = def.Substring("virtual ".Length);
            _sb.AppendIndent(def);
            for (int i = 0; i < f.Parameters.Count; i++)
            {
                DefParam param = f.Parameters[i];
                _sb.Append(" ");
                AddNativeMethodParam(param);
                if (i < f.Parameters.Count - 1) _sb.Append(",");
            }
            _sb.Append(" )\n");
            _sb.AppendLine("{");
            _sb.IncreaseIndent();

            _sb.AppendLine("if (doCallFor" + f.CLRName + ")");
            _sb.AppendLine("{");
            _sb.IncreaseIndent();

            CppNativeProxyClassProducer.AddNativeProxyMethodBody(f, "_receiver", _sb);

            _sb.DecreaseIndent();
            _sb.AppendLine("}");
            if (!f.IsVoid)
            {
                _sb.AppendLine("else");
                string ret = null;
                if (f.HasAttribute<DefaultReturnValueAttribute>())
                    ret = f.GetAttribute<DefaultReturnValueAttribute>().Name;
                else
                    throw new Exception("Default return value not set.");
                _sb.AppendLine("\treturn " + ret + ";");
            }

            _sb.DecreaseIndent();
            _sb.AppendLine("}");
        }

        protected virtual void AddNativeMethodParam(DefParam param)
        {
            _sb.Append(param.NativeTypeName + " " + param.Name);
        }
    }
}
