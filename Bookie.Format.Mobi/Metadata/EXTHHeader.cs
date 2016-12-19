using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bookie.Format.Mobi.Metadata
{
    public class ExthHead : BaseHeader
    {
        private readonly byte[] _headerLength = new byte[4];
        private readonly byte[] _identifier = new byte[4];
        private readonly byte[] _recordCount = new byte[4];

        private readonly List<ExthRecord> _recordList = new List<ExthRecord>();

        public ExthHead()
        {
            PopulateFieldList(true);
        }

        public ExthHead(FileStream fs)
        {
            fs.Read(_identifier, 0, _identifier.Length);

            if (IdentifierAsString != "EXTH")
            {
                throw new IOException("Expected to find EXTH header identifier EXTH but got something else instead");
            }

            fs.Read(_headerLength, 0, _headerLength.Length);
            fs.Read(_recordCount, 0, _recordCount.Length);

            for (var i = 0; i < RecordCount; i++)
            {
                _recordList.Add(new ExthRecord(fs));
            }

            PopulateFieldList();
        }

        protected int DataSize
        {
            get
            {
                var size = 0;
                foreach (var rec in _recordList)
                {
                    size += rec.Size;
                }

                return size;
            }
        }

        public int Size
        {
            get
            {
                var dataSize = DataSize;
                return 12 + dataSize + GetPaddingSize(dataSize);
            }
        }

        //Properties
        public string IdentifierAsString => Encoding.UTF8.GetString(_identifier).Replace("\0", string.Empty);

        public uint HeaderLength => Converter.ToUInt32(_headerLength);

        public uint RecordCount => Converter.ToUInt32(_recordCount);

        public string Author => GetRecordByType(100);

        public string Publisher => GetRecordByType(101);

        public string Imprint => GetRecordByType(102);

        public string Description => GetRecordByType(103);

        public string Ibsn => GetRecordByType(104);

        public string Subject => GetRecordByType(105);

        public string PublishedDate => GetRecordByType(106);

        public string Review => GetRecordByType(107);

        public string Contributor => GetRecordByType(108);

        public string Rights => GetRecordByType(109);

        public string SubjectCode => GetRecordByType(110);

        public string Type => GetRecordByType(111);

        public string Source => GetRecordByType(112);

        public string Asin => GetRecordByType(113);

        public string VersionNumber => GetRecordByType(114);

        public string RetailPrice => GetRecordByType(118);

        public string RetailPriceCurrency => GetRecordByType(119);

        public string DictionaryShortName => GetRecordByType(200);

        public string CdeType => GetRecordByType(501);

        public string UpdatedTitle => GetRecordByType(503);

        public string Asin2 => GetRecordByType(504);

        protected int GetPaddingSize(int dataSize)
        {
            var paddingSize = dataSize % 4;
            if (paddingSize != 0) paddingSize = 4 - paddingSize;

            return paddingSize;
        }

        private string GetRecordByType(int recType)
        {
            var record = string.Empty;
            foreach (var rec in _recordList)
            {
                if (rec.RecordType == recType)
                {
                    record = Encoding.UTF8.GetString(rec.RecordData);
                }
            }
            return record;
        }
    }
}