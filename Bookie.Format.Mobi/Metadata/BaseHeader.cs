using Bookie.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bookie.Format.Mobi.Metadata
{
    public class BaseHeader
    {
        private readonly SortedDictionary<string, object> _fieldList = new SortedDictionary<string, object>();
        private readonly SortedDictionary<string, object> _fieldListNoBlankRows = new SortedDictionary<string, object>();
        private readonly SortedDictionary<string, object> _emptyFieldList = new SortedDictionary<string, object>(); //Used to get properties for a blank record

        private readonly List<string> _fieldListExclude = new List<string>() { "FieldList", "FieldListNoBlankRows", "EmptyFieldList", "EXTHHeader" };

        public SortedDictionary<string, object> FieldList => _fieldList;

        public SortedDictionary<string, object> FieldListNoBlankRows => _fieldListNoBlankRows;

        public SortedDictionary<string, object> EmptyFieldList => _emptyFieldList;

        public override string ToString()
        {
            return ToString(false);
        }

        public string ToString(bool showBlankRows)
        {
            StringBuilder sb = new StringBuilder();

            if (showBlankRows)
            {
                foreach (KeyValuePair<string, object> kp in _fieldList)
                {
                    sb.AppendLine($"{kp.Key}: {kp.Value}");
                }
            }
            else
            {
                foreach (KeyValuePair<string, object> kp in _fieldListNoBlankRows)
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
            try
            {
                _fieldList.Clear();
                _emptyFieldList.Clear();
                foreach (System.Reflection.PropertyInfo propinfo in GetType().GetProperties())
                {
                    if (_fieldListExclude.Contains(propinfo.Name) == false)
                    {
                        if (!blankOnly)
                        {
                            _fieldList.Add(propinfo.Name, propinfo.GetValue(this, null));
                            if (propinfo.GetValue(this, null).ToString() != String.Empty)
                            {
                                _fieldListNoBlankRows.Add(propinfo.Name, propinfo.GetValue(this, null));
                            }
                        }
                        _emptyFieldList.Add(propinfo.Name, null);
                    }
                }
            }
            catch (NullReferenceException ex)
            {
                throw new BookieException("Known issue with Mobi");
            }
        }
    }
}