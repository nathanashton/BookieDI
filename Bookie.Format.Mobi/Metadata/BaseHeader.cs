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

        private List<string> fieldListExclude = new List<string>() { "FieldList", "FieldListNoBlankRows", "EmptyFieldList", "EXTHHeader" };

        public SortedDictionary<string, object> FieldList
        {
            get { return this.fieldList; }
        }

        public SortedDictionary<string, object> FieldListNoBlankRows
        {
            get { return this.fieldListNoBlankRows; }
        }

        public SortedDictionary<string, object> EmptyFieldList
        {
            get { return this.emptyFieldList; }
        }

        public override string ToString()
        {
            return ToString(false);
        }

        public string ToString(bool showBlankRows)
        {
            StringBuilder sb = new StringBuilder();

            if (showBlankRows)
            {
                foreach (KeyValuePair<string, object> kp in this.fieldList)
                {
                    sb.AppendLine(String.Format("{0}: {1}", kp.Key, kp.Value));
                }
            }
            else
            {
                foreach (KeyValuePair<string, object> kp in this.fieldListNoBlankRows)
                {
                    sb.AppendLine(String.Format("{0}: {1}", kp.Key, kp.Value));
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
            foreach (System.Reflection.PropertyInfo propinfo in this.GetType().GetProperties())
            {
                if (fieldListExclude.Contains(propinfo.Name)==false)
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
