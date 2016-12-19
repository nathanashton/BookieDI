using Bookie.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bookie.Format.Mobi.Metadata
{
    public class BaseHeader
    {
        private readonly List<string> _fieldListExclude = new List<string>
        {
            "FieldList",
            "FieldListNoBlankRows",
            "EmptyFieldList",
            "EXTHHeader"
        };

        public SortedDictionary<string, object> FieldList { get; } = new SortedDictionary<string, object>();

        public SortedDictionary<string, object> FieldListNoBlankRows { get; } = new SortedDictionary<string, object>();

        public SortedDictionary<string, object> EmptyFieldList { get; } = new SortedDictionary<string, object>();

        public override string ToString()
        {
            return ToString(false);
        }

        public string ToString(bool showBlankRows)
        {
            var sb = new StringBuilder();

            if (showBlankRows)
            {
                foreach (var kp in FieldList)
                {
                    sb.AppendLine($"{kp.Key}: {kp.Value}");
                }
            }
            else
            {
                foreach (var kp in FieldListNoBlankRows)
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
                FieldList.Clear();
                EmptyFieldList.Clear();
                foreach (var propinfo in GetType().GetProperties())
                {
                    if (_fieldListExclude.Contains(propinfo.Name) == false)
                    {
                        if (!blankOnly)
                        {
                            FieldList.Add(propinfo.Name, propinfo.GetValue(this, null));
                            if (propinfo.GetValue(this, null).ToString() != string.Empty)
                            {
                                FieldListNoBlankRows.Add(propinfo.Name, propinfo.GetValue(this, null));
                            }
                        }
                        EmptyFieldList.Add(propinfo.Name, null);
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