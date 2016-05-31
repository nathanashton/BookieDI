using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bookie.Format.Mobi.Metadata
{
    public class PDBHead : BaseHeader
    {
        private byte[] name = new byte[32];

        private byte[] attributes = new byte[2];
        private byte[] version = new byte[2];
        private byte[] creationDate = new byte[4];
        private byte[] modificationDate = new byte[4];
        private byte[] lastBackupDate = new byte[4];
        private byte[] modificationNumber = new byte[4];
        private byte[] appInfoID = new byte[4];
        private byte[] sortInfoID = new byte[4];
        private byte[] type = new byte[4];
        private byte[] creator = new byte[4];
        private byte[] uniqueIDSeed = new byte[4];
        private byte[] nextRecordListID = new byte[4];
        private byte[] numRecords = new byte[2];
        private List<RecordInfo> recordInfoList = new List<RecordInfo>();
        private byte[] gapToData = new byte[2];

        public PDBHead()
        {
            PopulateFieldList(true);
        }

        public PDBHead(FileStream fs)
        {
            fs.Read(this.name, 0, this.name.Length);
            fs.Read(this.attributes, 0, this.attributes.Length);
            fs.Read(this.version, 0, this.version.Length);
            fs.Read(this.creationDate, 0, this.creationDate.Length);
            fs.Read(this.modificationDate, 0, this.modificationDate.Length);
            fs.Read(this.lastBackupDate, 0, this.lastBackupDate.Length);
            fs.Read(this.modificationNumber, 0, this.modificationNumber.Length);
            fs.Read(this.appInfoID, 0, this.appInfoID.Length);
            fs.Read(this.sortInfoID, 0, this.sortInfoID.Length);

            fs.Read(this.type, 0, this.type.Length);
            fs.Read(this.creator, 0, this.creator.Length);
            fs.Read(this.uniqueIDSeed, 0, this.uniqueIDSeed.Length);
            fs.Read(this.nextRecordListID, 0, this.nextRecordListID.Length);
            fs.Read(this.numRecords, 0, this.numRecords.Length);

            int recordCount = Converter.ToInt16(this.numRecords);

            for (int i = 0; i < recordCount; i++)
            {
                this.recordInfoList.Add(new RecordInfo(fs));
            }

            fs.Read(this.gapToData, 0, this.gapToData.Length);

            PopulateFieldList();
        }

        public string Name
        {
            get { return Encoding.ASCII.GetString(this.name).Replace("\0", String.Empty); }
        }

        public ushort Attributes
        {
            get { return Converter.ToUInt16(this.attributes); }
        }

        public ushort Version
        {
            get { return Converter.ToUInt16(this.version); }
        }

        public uint CreationDate
        {
            get { return Converter.ToUInt32(this.creationDate); }
        }

        public uint ModificationDate
        {
            get { return Converter.ToUInt32(this.creationDate); }
        }

        public uint LastBackupDate
        {
            get { return Converter.ToUInt32(this.lastBackupDate); }
        }

        public uint ModificationNumber
        {
            get { return Converter.ToUInt32(this.modificationNumber); }
        }

        public uint AppInfoID
        {
            get { return Converter.ToUInt32(this.appInfoID); }
        }

        public uint SortInfoID
        {
            get { return Converter.ToUInt32(this.sortInfoID); }
        }

        public uint Type
        {
            get { return Converter.ToUInt32(this.type); }
        }

        public uint Creator
        {
            get { return Converter.ToUInt32(this.creator); }
        }

        public uint UniqueIDSeed
        {
            get { return Converter.ToUInt32(this.uniqueIDSeed); }
        }

        public ushort NumRecords
        {
            get { return Converter.ToUInt16(this.numRecords); }
        }

        public ushort GapToData
        {
            get { return Converter.ToUInt16(this.gapToData); }
        }

        public uint MobiHeaderSize
        {
            get
            {
                if (this.recordInfoList.Count > 1)
                {
                    return ((RecordInfo)this.recordInfoList[1]).RecordDataOffset - ((RecordInfo)this.recordInfoList[0]).RecordDataOffset;
                }
                else
                {
                    return 0;
                }

            }
        }

        public class RecordInfo
        {
            private byte[] recordDataOffset = new byte[4];
            private byte recordAttributes = 0;
            private byte[] uniqueID = new byte[3];

            public RecordInfo(FileStream fs)
            {
                fs.Read(this.recordDataOffset, 0, this.recordDataOffset.Length);
                recordAttributes = (byte)fs.ReadByte();
                fs.Read(this.uniqueID, 0, this.uniqueID.Length);
            }

            public uint RecordDataOffset
            {
                get { return Converter.ToUInt32(this.recordDataOffset); }
            }
        }
    }
}
