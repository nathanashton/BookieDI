using System;
using System.IO;

namespace Bookie.Format.Mobi.Metadata
{
    public class PalmDOCHead : BaseHeader
    {
        private byte[] compression = new byte[2];
        private byte[] unused0 = new byte[2];
        private byte[] textLength = new byte[4];
        private byte[] recordCount = new byte[2];
        private byte[] recordSize = new byte[2];
        private byte[] encryptionType = new byte[2];
        private byte[] unused1 = new byte[2];

        public PalmDOCHead()
        {
            PopulateFieldList(true);
        }

        public PalmDOCHead(FileStream fs)
        {
            fs.Read(this.compression, 0, this.compression.Length);
            fs.Read(this.unused0, 0, this.unused0.Length);
            fs.Read(this.textLength, 0, this.textLength.Length);
            fs.Read(this.recordCount, 0, this.recordCount.Length);

            fs.Read(this.recordSize, 0, this.recordSize.Length);
            fs.Read(this.encryptionType, 0, this.encryptionType.Length);
            fs.Read(this.unused1, 0, this.unused1.Length);

            PopulateFieldList();
        }

        //Properties
        public ushort Compression
        {
            get { return Converter.ToUInt16(this.compression); }
        }

        public string CompressionAsString
        {
            get
            {
                switch (this.Compression)
                {
                    case 1: return "None";
                    case 2: return "PalmDOC";
                    case 17480: return "HUFF/CDIC";
                    default:
                        return String.Format("Unknown (0)", this.Compression);
                }
            }
        }

        public uint TextLength
        {
            get { return Converter.ToUInt32(this.textLength); }
        }

        public ushort RecordCount
        {
            get { return Converter.ToUInt16(this.recordCount); }
        }

        public ushort RecordSize
        {
            get { return Converter.ToUInt16(this.recordSize); }
        }

        public ushort EncryptionType
        {
            get { return Converter.ToUInt16(this.encryptionType); }
        }

        public string EncryptionTypeAsString
        {
            get
            {
                switch (this.EncryptionType)
                {
                    case 0: return "None";
                    case 1: return "Old Mobipocket";
                    case 2: return "Mobipocket"; ;
                    default:
                        return String.Format("Unknown (0)", this.EncryptionType);
                }
            }
        }
    }
}
