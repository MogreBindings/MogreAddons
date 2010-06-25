using System;
using System.Collections.Generic;
using System.Text;

namespace AutoWrap.Meta
{
    class NativeProxyClassProducer : ClassProducer
    {
        public override bool IsNativeClass
        {
            get { return true; }
        }

        public static string GetProxyName(DefClass type)
        {
            string name = type.FullNativeName;
            name = name.Substring(name.IndexOf("::") + 2);
            return name.Replace("::", "_") + "_Proxy";
        }

        //protected List<DefFunction> _protectedFunctions = new List<DefFunction>();

        public NativeProxyClassProducer(Wrapper wrapper, DefClass t, IndentStringBuilder sb)
            : base(wrapper, t, sb)
        {
            //SearchProtectedFunctions(_t);
        }

        //protected virtual void SearchProtectedFunctions(DefClass type)
        //{
        //    foreach (DefFunction func in type.Functions)
        //    {
        //        if (func.IsDeclarableFunction)
        //        {
        //            if (func.ProtectionType == ProtectionType.Protected
        //                && !func.IsStatic
        //                && !func.IsAbstract)
        //            {
        //                if (!ContainsFunction(func, _protectedFunctions))
        //                    _protectedFunctions.Add(func);
        //            }
        //        }
        //    }

        //    foreach (DefClass iface in type.GetInterfaces())
        //        SearchProtectedFunctions(iface);

        //    if (type.BaseClass != null)
        //        SearchProtectedFunctions(type.BaseClass);
        //}

        private string _proxyName;
        protected virtual string ProxyName
        {
            get
            {
                if (_proxyName == null)
                    _proxyName = GetProxyName(_t);

                return _proxyName;
            }
        }

        protected virtual void AddNativeMethodParams(DefFunction f)
        {
            for (int i = 0; i < f.Parameters.Count; i++)
            {
                DefParam param = f.Parameters[i];
                _sb.Append(" ");

                _sb.Append(param.NativeTypeName);
                _sb.Append(" " + param.Name);

                if (i < f.Parameters.Count - 1) _sb.Append(",");
            }
        }

        protected override void AddPreBody()
        {
            _sb.AppendLine("//################################################################");
            _sb.AppendLine("//" + ProxyName);
            _sb.AppendLine("//################################################################\n");
        }

        protected override void AddBody()
        {
            AddFields();

            _sb.AppendLine();
            if (_t.Constructors.Length > 0)
            {
                foreach (DefFunction func in _t.Constructors)
                    AddConstructor(func);
            }
            else
                AddConstructor(null);

            foreach (DefFunction func in _overridableFunctions)
            {
                _sb.AppendLine();
                AddOverridableFunction(func);
            }

            //foreach (DefFunction func in _protectedFunctions)
            //{
            //    _sb.AppendLine();
            //    AddProtectedFunction(func);
            //}
        }

        protected virtual void AddFields()
        {
        }

        protected virtual void AddConstructor(DefFunction f)
        {
        }

        protected virtual void AddOverridableFunction(DefFunction f)
        {
        }

        //protected virtual void AddProtectedFunction(DefFunction f)
        //{
        //}
    }
}
