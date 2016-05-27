namespace Bookie.Common.Plugin
{
    public class FormatPlugin
    {
        public IFormatPlugin Plugin { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public FormatPlugin(IFormatPlugin plugin, string name, string description)
        {
            Plugin = plugin;
            Name = name;
            Description = description;
        }
    }
}