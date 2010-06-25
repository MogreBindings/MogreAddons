using System;
using System.Collections.Generic;
using System.Text;

namespace AutoWrap.Meta
{
    class IncValueClassProducer : IncClassProducer
    {
        protected override string GetTopBaseClassName()
        {
            return null;
        }

        protected override void AddDefinition()
        {
            _sb.AppendIndent("");
            if (!_t.IsNested)
                _sb.Append("public ");
            else
                _sb.Append(GetProtectionString(_t.ProtectionType) + ": ");
            _sb.AppendFormat("value class {0}\n", _t.CLRName);
        }

        protected override void AddPreDeclarations()
        {
            if (!_t.IsNested)
                _wrapper.AddPragmaMakePublicForType(_t);
        }

        protected override void AddInternalDeclarations()
        {
            base.AddInternalDeclarations();

            if (IsReadOnly)
            {
                foreach (DefField field in _t.PublicFields)
                {
                    _sb.AppendLine(field.Type.FullCLRName + " " + NameToPrivate(field) + ";");
                }
                _sb.AppendLine();
            }
        }

        protected override void AddPublicDeclarations()
        {
            base.AddPublicDeclarations();
            _sb.AppendLine("DEFINE_MANAGED_NATIVE_CONVERSIONS_FOR_VALUECLASS( " + GetClassName() + " )");
        }

        protected override void AddPublicConstructors()
        {
        }

        protected override void AddPropertyField(DefField field)
        {
            //TODO comments for fields
            //AddComments(field);

            if (IsReadOnly)
            {
                string ptype = GetCLRTypeName(field);
                _sb.AppendFormatIndent("property {0} {1}\n{{\n", ptype, ToCamelCase(field.Name));
                _sb.IncreaseIndent();
                _sb.AppendLine(ptype + " get()\n{");
                _sb.AppendLine("\treturn " + NameToPrivate(field) + ";");
                _sb.AppendLine("}");
                _sb.DecreaseIndent();
                _sb.AppendLine("}");
            }
            else
            {
                _sb.AppendLine(field.Type.FullCLRName + " " + field.Name + ";");
            }
        }

        public IncValueClassProducer(Wrapper wrapper, DefClass t, IndentStringBuilder sb)
            : base(wrapper, t, sb)
        {
        }
    }
}
