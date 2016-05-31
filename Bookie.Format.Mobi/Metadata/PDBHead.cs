using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bookie.Format.Mobi.Metadata
{
    public class PdbHead : BaseHeader
    {
        private readonly byte[] _name = new byte[32];

        private readonly byte[] _attributes = new byte[2];
        private readonly byte[] _version = new byte[2];
        private readonly byte[] _creationDate = new byte[4];
        private readonly byte[] _modificationDate = new byte[4];
        private readonly byte[] _lastBackupDate = new byte[4];
        private readonly byte[] _modificationNumber = new byte[4];
        private readonly byte[] _appInfoId = new byte[4];
        private readonly byte[] _sortInfoId = new byte[4];
        private readonly byte[] _type = new byte[4];
        private readonly byte[] _creator = new byte[4];
        private readonly byte[] _uniqueIdSeed = new byte[4];
        private readonly byte[] _nextRecordListId = new byte[4];
        private readonly byte[] _numRecords = new byte[2];
        private readonly List<RecordInfo> _recordInfoList = new List<RecordInfo>();
        private readonly byte[] _gapToData = new byte[2];

        public PdbHead()
        {
            PopulateFieldList(true);
        }

        public PdbHead(FileStream fs)
        {
            fs.Read(_name, 0, _name.Length);
            fs.Read(_attributes, 0, _attributes.Length);
            fs.Read(_version, 0, _version.Length);
            fs.Read(_creationDate, 0, _creationDate.Length);
            fs.Read(_modificationDate, 0, _modificationDate.Length);
            fs.Read(_lastBackupDate, 0, _lastBackupDate.Length);
            fs.Read(_modificationNumber, 0, _modificationNumber.Length);
            fs.Read(_appInfoId, 0, _appInfoId.Length);
            fs.Read(_sortInfoId, 0, _sortInfoId.Length);

            fs.Read(_type, 0, _type.Length);
            fs.Read(_creator, 0, _creator.Length);
            fs.Read(_uniqueIdSeed, 0, _uniqueIdSeed.Length);
            fs.Read(_nextRecordListId, 0, _nextRecordListId.Length);
            fs.Read(_numRecords, 0, _numRecords.Length);

            int recordCount = Converter.ToInt16(_numRecords);

            for (int i = 0; i < recordCount; i++)
            {
                _recordInfoList.Add(new RecordInfo(fs));
            }

            fs.Read(_gapToData, 0, _gapToData.Length);

            PopulateFieldList();
        }

        public string Name => Encoding.ASCII.GetString(_name).Replace("\0", String.Empty);

        public ushort Attributes => Converter.ToUInt16(_attributes);

        public ushort Version => Converter.ToUInt16(_version);

        public uint CreationDate => Converter.ToUInt32(_creationDate);

        public uint ModificationDate => Converter.ToUInt32(_creationDate);

        public uint LastBackupDate => Converter.ToUInt32(_lastBackupDate);

        public uint ModificationNumber => Converter.ToUInt32(_modificationNumber);

        public uint AppInfoId => Converter.ToUInt32(_appInfoId);

        public uint SortInfoId => Converter.ToUInt32(_sortInfoId);

        public uint Type => Converter.ToUInt32(_type);

        public uint Creator => Converter.ToUInt32(_creator);

        public uint UniqueIdSeed => Converter.ToUInt32(_uniqueIdSeed);

        public ushort NumRecords => Converter.ToUInt16(_numRecords);

        public ushort GapToData => Converter.ToUInt16(_gapToData);

        public uint MobiHeaderSize
        {
            get
            {
                if (_recordInfoList.Count > 1)
                {
                    return _recordInfoList[1].RecordDataOffset - _recordInfoList[0].RecordDataOffset;
                }
                else
                {
                    return 0;
                }
            }
        }

        public class RecordInfo
        {
            private readonly byte[] _recordDataOffset = new byte[4];
            private byte _recordAttributes;
            private readonly byte[] _uniqueId = new byte[3];

            public RecordInfo(FileStream fs)
            {
                fs.Read(_recordDataOffset, 0, _recordDataOffset.Length);
                _recordAttributes = (byte)fs.ReadByte();
                fs.Read(_uniqueId, 0, _uniqueId.Length);
            }

            public uint RecordDataOffset => Converter.ToUInt32(_recordDataOffset);
        }
    }
}