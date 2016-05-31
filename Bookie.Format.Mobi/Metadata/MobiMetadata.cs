using System.IO;

namespace Bookie.Format.Mobi.Metadata
{
    public class MobiMetadata
    {
        private PDBHead pdbHeader;
        private PalmDOCHead palmDocHeader;
        private MobiHead mobiHeader;

        public PDBHead PDBHeader
        {
            get { return pdbHeader; }
        }

        public PalmDOCHead PalmDocHeader
        {
            get { return palmDocHeader; }
        }

        public MobiHead MobiHeader
        {
            get { return mobiHeader; }
        }

        public MobiMetadata(FileStream fs)
        {
            SetUpData(fs);
        }

        public MobiMetadata(string filePath)
        {
            System.IO.FileStream fs = new System.IO.FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            SetUpData(fs);
            fs.Close();
        }

        private void SetUpData(FileStream fs)
        {
            pdbHeader = new PDBHead(fs);
            palmDocHeader = new PalmDOCHead(fs);
            mobiHeader = new MobiHead(fs, pdbHeader.MobiHeaderSize);


        }
    }
}
