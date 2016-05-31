using System.IO;

namespace Bookie.Format.Mobi.Metadata
{
    public class MobiMetadata
    {
        private PdbHead _pdbHeader;
        private PalmDocHead _palmDocHeader;
        private MobiHead _mobiHeader;

        public PdbHead PdbHeader => _pdbHeader;

        public PalmDocHead PalmDocHeader => _palmDocHeader;

        public MobiHead MobiHeader => _mobiHeader;

        public MobiMetadata(FileStream fs)
        {
            SetUpData(fs);
        }

        public MobiMetadata(string filePath)
        {
            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            SetUpData(fs);
            fs.Close();
        }

        private void SetUpData(FileStream fs)
        {
            _pdbHeader = new PdbHead(fs);
            _palmDocHeader = new PalmDocHead(fs);
            _mobiHeader = new MobiHead(fs, _pdbHeader.MobiHeaderSize);
        }
    }
}