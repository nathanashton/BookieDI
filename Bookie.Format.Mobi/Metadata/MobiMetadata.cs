using System.IO;

namespace Bookie.Format.Mobi.Metadata
{
    public class MobiMetadata
    {
        public MobiMetadata(FileStream fs)
        {
            SetUpData(fs);
        }

        public MobiMetadata(string filePath)
        {
            var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            SetUpData(fs);
            fs.Close();
        }

        public PdbHead PdbHeader { get; private set; }

        public PalmDocHead PalmDocHeader { get; private set; }

        public MobiHead MobiHeader { get; private set; }

        private void SetUpData(FileStream fs)
        {
            PdbHeader = new PdbHead(fs);
            PalmDocHeader = new PalmDocHead(fs);
            MobiHeader = new MobiHead(fs, PdbHeader.MobiHeaderSize);
        }
    }
}