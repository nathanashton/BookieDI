using System.IO;

namespace Bookie.Format.Mobi.Metadata
{
    public class ExthRecord
    {
        private readonly byte[] _recordType = new byte[4];
        private readonly byte[] _recordLength = new byte[4];
        private readonly byte[] _recordData;

        public ExthRecord(FileStream fs)
        {
            fs.Read(_recordType, 0, _recordType.Length);
            fs.Read(_recordLength, 0, _recordLength.Length);

            if (RecordLength < 8) throw new IOException("Invalid EXTH record length");
            _recordData = new byte[RecordLength - 8];
            fs.Read(_recordData, 0, _recordData.Length);
        }

        //Properties
        public int DataLength => _recordData.Length;

        public int Size => DataLength + 8;

        public uint RecordLength => Converter.ToUInt32(_recordLength);

        public uint RecordType => Converter.ToUInt32(_recordType);

        public byte[] RecordData => _recordData;
    }
}