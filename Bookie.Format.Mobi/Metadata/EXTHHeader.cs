using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bookie.Format.Mobi.Metadata
{
    public class EXTHHead : BaseHeader
    {
        private byte[] identifier = new byte[4];
        private byte[] headerLength = new byte[4];
        private byte[] recordCount = new byte[4];

        private List<EXTHRecord> recordList = new List<EXTHRecord>();

        public EXTHHead()
        {
            PopulateFieldList(true);
        }

        public EXTHHead(FileStream fs)
        {
            fs.Read(this.identifier, 0, this.identifier.Length);

            if (this.IdentifierAsString != "EXTH")
            {
                throw new IOException("Expected to find EXTH header identifier EXTH but got something else instead");
            }

            fs.Read(this.headerLength, 0, this.headerLength.Length);
            fs.Read(this.recordCount, 0, this.recordCount.Length);

            for (int i = 0; i < this.RecordCount; i++)
            {
              this.recordList.Add(new EXTHRecord(fs));
            }

            PopulateFieldList();

        }

        protected int DataSize
        {
            get
            {
                int size = 0;
                foreach (EXTHRecord rec in this.recordList)
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
                int dataSize = this.DataSize;
                return 12 + dataSize + GetPaddingSize(dataSize);
            }
        }

        protected int GetPaddingSize(int dataSize)
        {
            int paddingSize = dataSize % 4;
            if (paddingSize != 0) paddingSize = 4 - paddingSize;

            return paddingSize;
        }


        //Properties
        public string IdentifierAsString
        {
            get { return Encoding.UTF8.GetString(this.identifier).Replace("\0", String.Empty); }
        }

        public uint HeaderLength
        {
            get { return Converter.ToUInt32(this.headerLength); }
        }

        public uint RecordCount
        {
            get { return Converter.ToUInt32(this.recordCount); }
        }

        public string Author
        {
            get { return GetRecordByType(100); }
        }

        public string Publisher
        {
            get { return GetRecordByType(101); }
        }

        public string Imprint
        {
            get { return GetRecordByType(102); }
        }

        public string Description
        {
            get { return GetRecordByType(103); }
        }

        public string IBSN
        {
            get { return GetRecordByType(104); }
        }

        public string Subject
        {
            get { return GetRecordByType(105); }
        }

        public string PublishedDate
        {
            get { return GetRecordByType(106); }
        }

        public string Review
        {
            get { return GetRecordByType(107); }
        }

        public string Contributor
        {
            get { return GetRecordByType(108); }
        }

        public string Rights
        {
            get { return GetRecordByType(109); }
        }

        public string SubjectCode
        {
            get { return GetRecordByType(110); }
        }

        public string Type
        {
            get { return GetRecordByType(111); }
        }

        public string Source
        {
            get { return GetRecordByType(112); }
        }

        public string ASIN
        {
            get { return GetRecordByType(113); }
        }

        public string VersionNumber
        {
            get { return GetRecordByType(114); }
        }

        public string RetailPrice
        {
            get { return GetRecordByType(118); }
        }

        public string RetailPriceCurrency
        {
            get { return GetRecordByType(119); }
        }

        public string DictionaryShortName
        {
            get { return GetRecordByType(200); }
        }

        public string CDEType
        {
            get { return GetRecordByType(501); }
        }

        public string UpdatedTitle
        {
            get { return GetRecordByType(503); }
        }

        public string ASIN2
        {
            get { return GetRecordByType(504); }
        }


        private string GetRecordByType(int recType)
        {
            string record = String.Empty;
            foreach (EXTHRecord rec in this.recordList)
            {
                if (rec.RecordType == recType)
                {
                    record = System.Text.Encoding.UTF8.GetString(rec.RecordData);
                }
            }
            return record;
        }
    }
}
