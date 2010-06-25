using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace AutoWrap.Meta
{
    class DefNameSpace
    {
        XmlElement _elem;
        string _managedNamespace;

        public DefNameSpace ParentNameSpace = null;
        public List<DefNameSpace> ChildNameSpaces = new List<DefNameSpace>();

        public XmlElement Element
        {
            get { return _elem; }
        }

        public string NativeName
        {
            get { return DefNameSpace.GetFullName(_elem); }
        }

        public string CLRName;

        public List<DefType> Types = new List<DefType>();

        public T FindType<T>(string name)
        {
            return FindType<T>(name, true);
        }
        public T FindType<T>(string name, bool raiseException)
        {
            if (name.EndsWith(" std::string"))
                name = "std::string";

            if (name == "std::string")
                return (T)(object)new DefString();

            if (name.StartsWith(Globals.NativeNamespace + "::"))
                name = name.Substring(name.IndexOf("::") + 2);

            T type = FindTypeInList<T>(name, Types, false);
            if (type == null)
            {
                if (ParentNameSpace == null)
                {
                    if (raiseException)
                        throw new Exception("Could not find type");
                    else
                        return (T)(object)new DefInternal(name);
                }
                else
                    return ParentNameSpace.FindType<T>(name, raiseException);
            }

            return (T)(object)DefType.CreateExplicitType((DefType)(object)type);
        }

        protected virtual T FindTypeInList<T>(string name, List<DefType> types, bool raiseException)
        {
            List<DefType> list = new List<DefType>();

            string topname = name;
            string nextnames = null;

            if (name.Contains("::"))
            {
                topname = name.Substring(0, name.IndexOf("::"));
                nextnames = name.Substring(name.IndexOf("::") + 2);
            }

            foreach (DefType t in types)
            {
                if (t is T && t.Name == topname)
                {
                    list.Add(t);
                }
            }

            if (list.Count == 0)
            {
                if (raiseException)
                    throw new Exception("Could not find type");
                else
                    return default(T);
            }
            else if (list.Count > 1)
                throw new Exception("Found more than one type");

            T type = (T)(object)list[0];

            if (nextnames == null)
                return type;
            else
                return FindTypeInList<T>(nextnames, ((DefClass)(object)type).NestedTypes, raiseException);
        }

        public DefNameSpace(XmlElement elem, string managedNamespace)
        {
            this._elem = elem;
            this._managedNamespace = managedNamespace;

            string second = elem.GetAttribute("second");
            string third = elem.GetAttribute("third");

            this.CLRName = managedNamespace;

            if (second != "")
                this.CLRName += "::" + second;

            if (third != "")
                this.CLRName += "::" + third;

            foreach (XmlElement child in elem.ChildNodes)
            {
                DefType type = DefType.CreateType(child);
                if (type != null)
                {
                    type.NameSpace = this;
                    this.Types.Add(type);
                }
            }
        }

        public DefType GetDefType(string name)
        {
            DefType type = null;
            foreach (DefType t in Types)
            {
                if (t.Name == name)
                {
                    type = t;
                    break;
                }
            }

            if (type == null)
                throw new Exception("DefType not found");

            return type;
        }

        public static string GetFullName(XmlElement elem)
        {
            if (elem.Name != "namespace")
                throw new Exception("Wrong element; expected 'namespace'.");

            string first, second, third;
            first = elem.GetAttribute("name");
            second = elem.GetAttribute("second");
            third = elem.GetAttribute("third");

            string name = first;
            if (second != "")
                name += "::" + second;

            if (third != "")
                name += "::" + third;

            return name;
        }
    }
}
