using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace AutoWrap.Meta
{
    class MetaDefinition
    {
        XmlDocument _doc = new XmlDocument();
        string _managedNamespace;
        List<KeyValuePair<AttributeHolder, AutoWrapAttribute>> _holders = new List<KeyValuePair<AttributeHolder, AutoWrapAttribute>>();

        public List<DefNameSpace> NameSpaces = new List<DefNameSpace>();

        public MetaDefinition(string file, string managedNamespace)
        {
            _doc.Load(file);
            this._managedNamespace = managedNamespace;

            XmlElement root = (XmlElement)_doc.GetElementsByTagName("meta")[0];

            foreach (XmlNode elem in root.ChildNodes)
            {
                if (elem is XmlElement)
                    AddNamespace(elem as XmlElement);
            }
        }

        public void AddAttributes(string file)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(file);

            XmlElement root = (XmlElement)doc.GetElementsByTagName("meta")[0];

            foreach (XmlNode elem in root.ChildNodes)
            {
                if (elem is XmlElement)
                    AddAttributesInNamespace(GetNameSpace((elem as XmlElement).GetAttribute("name")), elem as XmlElement);
            }

            foreach (KeyValuePair<AttributeHolder, AutoWrapAttribute> pair in _holders)
            {
                pair.Value.ProcessHolder(pair.Key);
            }
        }

        private void AddAttributesInNamespace(DefNameSpace nameSpace, XmlElement elem)
        {
            foreach (XmlNode child in elem.ChildNodes)
            {
                if (child is XmlElement)
                    AddAttributesInType(nameSpace.GetDefType((child as XmlElement).GetAttribute("name")), child as XmlElement);
            }
        }

        private void AddAttributesInType(DefType type, XmlElement elem)
        {
            foreach (XmlAttribute attr in elem.Attributes)
            {
                if (attr.Name != "name")
                    AddAttributeInHolder(type, CreateAttribute(attr));
            }

            foreach (XmlNode child in elem.ChildNodes)
            {
                if (!(child is XmlElement))
                    continue;

                if (child.Name[0] == '_')
                {
                    AddAttributeInHolder(type, CreateAttribute(child as XmlElement));
                    continue;
                }

                switch (child.Name)
                {
                    case "class":
                    case "struct":
                    case "enumeration":
                    case "typedef":
                        AddAttributesInType((type as DefClass).GetNestedType((child as XmlElement).GetAttribute("name")), child as XmlElement);
                        break;
                    case "function":
                    case "variable":
                        foreach (DefMember m in (type as DefClass).GetMembers((child as XmlElement).GetAttribute("name")))
                            AddAttributesInMember(m, child as XmlElement);
                        break;
                    default:
                        throw new Exception("Unexpected");
                }
            }
        }

        private void AddAttributesInMember(DefMember member, XmlElement elem)
        {
            foreach (XmlAttribute attr in elem.Attributes)
            {
                if (attr.Name != "name")
                    AddAttributeInHolder(member, CreateAttribute(attr));
            }

            foreach (XmlNode child in elem.ChildNodes)
            {
                if (!(child is XmlElement))
                    continue;

                if (child.Name[0] == '_')
                {
                    AddAttributeInHolder(member, CreateAttribute(child as XmlElement));
                    continue;
                }

                switch (child.Name)
                {
                    case "param":
                        if (!(member is DefFunction))
                            throw new Exception("Unexpected");

                        string name = (child as XmlElement).GetAttribute("name");
                        DefParam param = null;
                        foreach (DefParam p in (member as DefFunction).Parameters)
                        {
                            if (p.Name == name)
                            {
                                param = p;
                                break;
                            }
                        }
                        if (param == null)
                            throw new Exception("Wrong param name");

                        foreach (XmlAttribute attr in child.Attributes)
                        {
                            if (attr.Name != "name")
                                AddAttributeInHolder(param, CreateAttribute(attr));
                        }
                        break;

                    default:
                        throw new Exception("Unexpected");
                }
            }
        }

        private AutoWrapAttribute CreateAttribute(XmlElement elem)
        {
            string typename = elem.Name.Substring(1);
            string nameSpace = typeof(WrapTypeAttribute).Namespace;

            Type type = Assembly.GetExecutingAssembly().GetType( nameSpace + "."+typename + "Attribute", true, true);
            return (AutoWrapAttribute)type.GetMethod("FromElement").Invoke(null, new object[] { elem });
        }

        private AutoWrapAttribute CreateAttribute(XmlAttribute attr)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement elem = doc.CreateElement("_" + attr.Name);
            elem.InnerText = attr.Value;
            return CreateAttribute(elem);
        }

        private void AddAttributeInHolder(AttributeHolder holder, AutoWrapAttribute attr)
        {
            holder.Attributes.Add(attr);
            _holders.Add(new KeyValuePair<AttributeHolder, AutoWrapAttribute>(holder, attr));
        }

        public DefNameSpace GetNameSpace(string name)
        {
            DefNameSpace spc = null;
            foreach (DefNameSpace ns in NameSpaces)
            {
                if (ns.NativeName == name)
                {
                    spc = ns;
                    break;
                }
            }

            if (spc == null)
                throw new Exception("couldn't find namespace");

            return spc;
        }

        private void AddNamespace(XmlElement elem)
        {
            if (elem.Name != "namespace")
                throw new Exception("Wrong element; expected 'namespace'.");

            DefNameSpace spc = new DefNameSpace(elem, _managedNamespace);

            if (spc.NativeName.Contains("::"))
            {
                string pname = spc.NativeName.Substring(0, spc.NativeName.LastIndexOf("::"));
                foreach (DefNameSpace fns in NameSpaces)
                {
                    if (fns.NativeName == pname)
                    {
                        spc.ParentNameSpace = fns;
                        fns.ChildNameSpaces.Add(spc);
                        break;
                    }
                }
            }

            NameSpaces.Add(spc);
        }
    }
}
