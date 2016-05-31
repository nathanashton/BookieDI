using System;
using System.Collections.Generic;
using System.Text;

namespace Bookie.Format.Mobi.Metadata
{
    public class BaseHeader
    {
        protected SortedDictionary<string, object> fieldList = new SortedDictionary<string, object>();
        protected SortedDictionary<string, object> fieldListNoBlankRows = new SortedDictionary<string, object>();
        protected SortedDictionary<string, object> emptyFieldList = new SortedDictionary<string, object>(); //Used to get properties for a blank record

        private readonly List<string> _fieldListExclude = new List<string>() { "FieldList", "FieldListNoBlankRows", "EmptyFieldList", "EXTHHeader" };

        public SortedDictionary<string, object> FieldList => fieldList;

        public SortedDictionary<string, object> FieldListNoBlankRows => fieldListNoBlankRows;

        public SortedDictionary<string, object> EmptyFieldList => emptyFieldList;

        public override string ToString()
        {
            return ToString(false);
        }

        public string ToString(bool showBlankRows)
        {
            StringBuilder sb = new StringBuilder();

            if (showBlankRows)
            {
                foreach (KeyValuePair<string, object> kp in fieldList)
                {
                    sb.AppendLine($"{kp.Key}: {kp.Value}");
                }
            }
            else
            {
                foreach (KeyValuePair<string, object> kp in fieldListNoBlankRows)
                {
                    sb.AppendLine($"{kp.Key}: {kp.Value}");
                }
            }

            return sb.ToString();
        }

        protected void PopulateFieldList()
        {
            PopulateFieldList(false);
        }

        protected void PopulateFieldList(bool blankOnly)
        {
            fieldList.Clear();
            emptyFieldList.Clear();
            foreach (System.Reflection.PropertyInfo propinfo in GetType().GetProperties())
            {
                if (_fieldListExclude.Contains(propinfo.Name) == false)
                {
                    if (!blankOnly)
                    {
                        fieldList.Add(propinfo.Name, propinfo.GetValue(this, null));
                        if (propinfo.GetValue(this, null).ToString() != String.Empty)
                        {
                            fieldListNoBlankRows.Add(propinfo.Name, propinfo.GetValue(this, null));
                        }
                    }
                    emptyFieldList.Add(propinfo.Name, null);
                }
            }
        }
    }
}