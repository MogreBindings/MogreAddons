using System;
using System.Collections.Generic;
using System.Text;

namespace AutoWrap.Meta
{
    //class CppNativeProtectedTypesProxy : NativeProtectedTypesProxy
    //{
    //    public CppNativeProtectedTypesProxy(Wrapper wrapper, DefClass t, IndentStringBuilder sb)
    //        : base(wrapper, t, sb)
    //    {
    //    }

    //    protected string ProxyName
    //    {
    //        get
    //        {
    //            return NativeProtectedTypesProxy.GetProtectedTypesProxyName(_t);
    //        }
    //    }

    //    protected override void AddNestedType(DefType nested)
    //    {
    //    }

    //    protected override void AddStaticField(DefField field)
    //    {
    //        _sb.AppendIndent("");
    //        _sb.Append(field.NativeTypeName + "& " + ProxyName + "::ref_" + field.Name + " = " + field.Class.FullNativeName + "::" + field.Name + ";\n");
    //    }

    //    protected override void AddMethod(DefFunction f)
    //    {
    //        // method is supposed to be static

    //        _sb.AppendIndent("");
    //        _sb.Append(f.NativeTypeName + " " + ProxyName + "::base_" + f.Name + "(");
    //        AddNativeMethodParams(f);
    //        _sb.Append(" )");
    //        if (f.IsConstFunctionCall)
    //            _sb.Append(" const");
    //        _sb.Append("\n");
    //        _sb.AppendLine("{");
    //        _sb.IncreaseIndent();

    //        _sb.AppendIndent("");
    //        if (!f.IsVoid)
    //            _sb.Append("return ");
    //        _sb.Append(_t.FullNativeName + "::" + f.Name + "(");

    //        for (int i = 0; i < f.Parameters.Count; i++)
    //        {
    //            DefParam param = f.Parameters[i];
    //            _sb.Append(" " + param.Name);
    //            if (i < f.Parameters.Count - 1) _sb.Append(",");
    //        }
    //        _sb.Append(" );\n");

    //        _sb.DecreaseIndent();
    //        _sb.AppendLine("}");
    //    }
    //}
}
