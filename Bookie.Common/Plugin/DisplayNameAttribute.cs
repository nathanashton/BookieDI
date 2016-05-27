using System;

namespace Bookie.Common.Plugin
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DisplayNameAttribute : Attribute
    {
        private readonly string _displayName;

        public DisplayNameAttribute(string displayName)
        {
            _displayName = displayName;
        }

        public override string ToString()
        {
            return _displayName;
        }
    }
}