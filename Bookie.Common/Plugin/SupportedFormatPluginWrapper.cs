namespace Bookie.Common.Plugin
{
    public class SupportedFormatPluginWrapper
    {
        public ISupportedFormatPlugin Plugin { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public SupportedFormatPluginWrapper(ISupportedFormatPlugin plugin, string name, string description)
        {
            Plugin = plugin;
            Name = name;
            Description = description;
        }
    }
}