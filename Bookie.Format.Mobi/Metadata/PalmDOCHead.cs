using System.IO;

namespace Bookie.Format.Mobi.Metadata
{
    public class PalmDocHead : BaseHeader
    {
        private readonly byte[] _compression = new byte[2];
        private readonly byte[] _encryptionType = new byte[2];
        private readonly byte[] _recordCount = new byte[2];
        private readonly byte[] _recordSize = new byte[2];
        private readonly byte[] _textLength = new byte[4];
        private readonly byte[] _unused0 = new byte[2];
        private readonly byte[] _unused1 = new byte[2];

        public PalmDocHead()
        {
            PopulateFieldList(true);
        }

        public PalmDocHead(FileStream fs)
        {
            fs.Read(_compression, 0, _compression.Length);
            fs.Read(_unused0, 0, _unused0.Length);
            fs.Read(_textLength, 0, _textLength.Length);
            fs.Read(_recordCount, 0, _recordCount.Length);

            fs.Read(_recordSize, 0, _recordSize.Length);
            fs.Read(_encryptionType, 0, _encryptionType.Length);
            fs.Read(_unused1, 0, _unused1.Length);

            PopulateFieldList();
        }

        //Properties
        public ushort Compression => Converter.ToUInt16(_compression);

        public string CompressionAsString
        {
            get
            {
                switch (Compression)
                {
                    case 1:
                        return "None";

                    case 2:
                        return "PalmDOC";

                    case 17480:
                        return "HUFF/CDIC";

                    default:
                        return $"Unknown {Compression}";
                }
            }
        }

        public uint TextLength => Converter.ToUInt32(_textLength);

        public ushort RecordCount => Converter.ToUInt16(_recordCount);

        public ushort RecordSize => Converter.ToUInt16(_recordSize);

        public ushort EncryptionType => Converter.ToUInt16(_encryptionType);

        public string EncryptionTypeAsString
        {
            get
            {
                switch (EncryptionType)
                {
                    case 0:
                        return "None";

                    case 1:
                        return "Old Mobipocket";

                    case 2:
                        return "Mobipocket";

                    default:
                        return $"Unknown {EncryptionType}";
                }
            }
        }
    }
}