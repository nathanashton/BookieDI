namespace Bookie.Common.Plugin
{
    public class SupportedFormatPlugin
    {
        public ISupportedFormatPlugin Plugin { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public SupportedFormatPlugin(ISupportedFormatPlugin plugin, string name, string description)
        {
            Plugin = plugin;
            Name = name;
            Description = description;
        }
    }
}