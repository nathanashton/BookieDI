using System;

namespace Bookie.Common.Plugin
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DescriptionAttribute : Attribute
    {
        private readonly string _description;

        public DescriptionAttribute(string description)
        {
            _description = description;
        }

        public override string ToString()
        {
            return _description;
        }
    }
}