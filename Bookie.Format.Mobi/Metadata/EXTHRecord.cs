using System.IO;

namespace Bookie.Format.Mobi.Metadata
{
    public class EXTHRecord
    {
        private byte[] recordType = new byte[4];
        private byte[] recordLength = new byte[4];
        private byte[] recordData = null;

        public EXTHRecord(FileStream fs)
        {
            fs.Read(this.recordType, 0, this.recordType.Length);
            fs.Read(this.recordLength, 0, this.recordLength.Length);

            if (this.RecordLength < 8) throw new IOException("Invalid EXTH record length");
            this.recordData = new byte[this.RecordLength - 8];
            fs.Read(this.recordData, 0, this.recordData.Length);
        }

        //Properties
        public int DataLength
        {
            get { return this.recordData.Length; }
        }

        public int Size
        {
            get { return DataLength + 8; }
        }

        public uint RecordLength
        {
            get { return Converter.ToUInt32(this.recordLength); }
        }

        public uint RecordType
        {
            get { return Converter.ToUInt32(this.recordType); }
        }

        public byte[] RecordData
        {
            get { return this.recordData; }
        }
    }
}
