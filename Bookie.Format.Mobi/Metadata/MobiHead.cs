﻿using System;
using System.IO;
using System.Text;

namespace Bookie.Format.Mobi.Metadata
{
    public class MobiHead : BaseHeader
    {
        private byte[] identifier = new byte[4];
        private byte[] headerLength = new byte[4];
        private byte[] mobiType = new byte[4];
        private byte[] textEncoding = new byte[4];
        private byte[] uniqueID = new byte[4];
        private byte[] fileVersion = new byte[4];
        private byte[] orthographicIndex = new byte[4];
        private byte[] inflectionIndex = new byte[4];
        private byte[] indexNames = new byte[4];
        private byte[] indexKeys = new byte[4];
        private byte[] extraIndex0 = new byte[4];
        private byte[] extraIndex1 = new byte[4];
        private byte[] extraIndex2 = new byte[4];
        private byte[] extraIndex3 = new byte[4];
        private byte[] extraIndex4 = new byte[4];
        private byte[] extraIndex5 = new byte[4];
        private byte[] firstNonBookIndex = new byte[4];
        private byte[] fullNameOffset = new byte[4];
        private byte[] fullNameLength = new byte[4];
        private byte[] locale = new byte[4];
        private byte[] inputLanguage = new byte[4];
        private byte[] outputLanguage = new byte[4];
        private byte[] minVersion = new byte[4];
        private byte[] firstImageIndex = new byte[4];
        private byte[] huffmanRecordOffset = new byte[4];
        private byte[] huffmanRecordCount = new byte[4];
        private byte[] huffmanTableOffset = new byte[4];
        private byte[] huffmanTableLength = new byte[4];
        private byte[] exthFlags = new byte[4];
        private byte[] restOfMobiHeader = null;
        private EXTHHead exthHeader = null;

        private byte[] remainder = null;
        private byte[] fullName = null;

        public MobiHead()
        {
            PopulateFieldList(true);
        }

        public MobiHead(FileStream fs, uint mobiHeaderSize)
        {

            fs.Read(this.identifier, 0, this.identifier.Length);

            if (this.IdentifierAsString != "MOBI")
            {
                throw new IOException("Did not get expected MOBI identifier");
            }

            fs.Read(this.headerLength, 0, this.headerLength.Length);
            this.restOfMobiHeader = new byte[this.HeaderLength + 16 - 132];

            fs.Read(this.mobiType, 0, this.mobiType.Length);
            fs.Read(this.textEncoding, 0, this.textEncoding.Length);
            fs.Read(this.uniqueID, 0, this.uniqueID.Length);
            fs.Read(this.fileVersion, 0, this.fileVersion.Length);
            fs.Read(this.orthographicIndex, 0, this.orthographicIndex.Length);
            fs.Read(this.inflectionIndex, 0, this.inflectionIndex.Length);
            fs.Read(this.indexNames, 0, this.indexNames.Length);
            fs.Read(this.indexKeys, 0, this.indexKeys.Length);
            fs.Read(this.extraIndex0, 0, this.extraIndex0.Length);
            fs.Read(this.extraIndex1, 0, this.extraIndex1.Length);
            fs.Read(this.extraIndex2, 0, this.extraIndex2.Length);
            fs.Read(this.extraIndex3, 0, this.extraIndex3.Length);
            fs.Read(this.extraIndex4, 0, this.extraIndex4.Length);
            fs.Read(this.extraIndex5, 0, this.extraIndex5.Length);
            fs.Read(this.firstNonBookIndex, 0, this.firstNonBookIndex.Length);
            fs.Read(this.fullNameOffset, 0, this.fullNameOffset.Length);
            fs.Read(this.fullNameLength, 0, this.fullNameLength.Length);

            int fullNameLen = Converter.ToInt32(this.fullNameLength);
            fs.Read(this.locale, 0, this.locale.Length);
            fs.Read(this.inputLanguage, 0, this.inputLanguage.Length);
            fs.Read(this.outputLanguage, 0, this.outputLanguage.Length);
            fs.Read(this.minVersion, 0, this.minVersion.Length);
            fs.Read(this.firstImageIndex, 0, this.firstImageIndex.Length);
            fs.Read(this.huffmanRecordOffset, 0, this.huffmanRecordOffset.Length);
            fs.Read(this.huffmanRecordCount, 0, this.huffmanRecordCount.Length);
            fs.Read(this.huffmanTableOffset, 0, this.huffmanTableOffset.Length);
            fs.Read(this.huffmanTableLength, 0, this.huffmanTableLength.Length);
            fs.Read(this.exthFlags, 0, this.exthFlags.Length);

            //If bit 6 (0x40) is set, then there's an EXTH record 
            bool exthExists = (Converter.ToUInt32(this.exthFlags) & 0x40) != 0;

            fs.Read(this.restOfMobiHeader, 0, this.restOfMobiHeader.Length);

            if (exthExists)
            {
                this.exthHeader = new EXTHHead(fs);
            }

            int currentOffset = 132 + this.restOfMobiHeader.Length + ExthHeaderSize;
            this.remainder = new byte[(int)(mobiHeaderSize - currentOffset)];
            fs.Read(this.remainder, 0, this.remainder.Length);

            int fullNameIndexInRemainder = Converter.ToInt32(this.fullNameOffset) - currentOffset;

            this.fullName = new byte[fullNameLen];

            if (fullNameIndexInRemainder >= 0)
            {
              if (fullNameIndexInRemainder < this.remainder.Length)
              {
                if (fullNameIndexInRemainder + fullNameLen <= this.remainder.Length)
                {
                  if (fullNameLen > 0)
                  {
                      Array.Copy(this.remainder, 
                      fullNameIndexInRemainder, 
                      this.fullName, 
                      0, 
                      fullNameLen);
                  }
                }
              }
            }

            PopulateFieldList();
        }

        //Properties
        public int ExthHeaderSize
        {
            get
            {
                if (this.exthHeader == null)
                {
                    return 0;
                }
                else
                {
                    return this.exthHeader.Size;
                }
            }

        }

        public string FullName
        {
            get { return Encoding.ASCII.GetString(this.remainder).Replace("\0", String.Empty); }
        }

        public string IdentifierAsString
        {
            get { return Encoding.UTF8.GetString(this.identifier).Replace("\0", String.Empty); }
        }

        public uint HeaderLength
        {
            get { return Converter.ToUInt32(this.headerLength); }
        }

        public uint MobiType
        {
            get { return Converter.ToUInt32(this.mobiType); }
        }

        public string MobiTypeAsString
        {
            get
            {
                switch (this.MobiType)
                {
                    case 2: return "Mobipocket Book";
                    case 3: return "PalmDoc Book";
                    case 4: return "Audio";
                    case 257: return "News";
                    case 258: return "News Feed";
                    case 259: return "News Magazine";
                    case 513: return "PICS";
                    case 514: return "WORD";
                    case 515: return "XLS";
                    case 516: return "PPT";
                    case 517: return "TEXT";
                    case 518: return "HTML";
                    default:
                        return String.Format("Unknown (0)", this.MobiType);
                }
            }
        }

        public uint TextEncoding
        {
            get { return Converter.ToUInt32(this.textEncoding); }
        }

        public string TextEncodingAsString
        {
            get
            {
                switch (this.TextEncoding)
                {
                    case 1252: return "Cp1252";
                    case 65001: return "UTF-8";
                    default:
                        return null;
                }
            }
        }

        public uint UniqueID
        {
            get { return Converter.ToUInt32(this.uniqueID); }
        }

        public uint FileVersion
        {
            get { return Converter.ToUInt32(this.fileVersion); }
        }

        public uint OrthographicIndex
        {
            get { return Converter.ToUInt32(this.orthographicIndex); }
        }

        public uint InflectionIndex
        {
            get { return Converter.ToUInt32(this.inflectionIndex); }
        }

        public uint IndexNames
        {
            get { return Converter.ToUInt32(this.indexNames); }
        }

        public uint IndexKeys
        {
            get { return Converter.ToUInt32(this.indexKeys); }
        }

        public uint ExtraIndex0
        {
            get { return Converter.ToUInt32(this.extraIndex0); }
        }

        public uint ExtraIndex1
        {
            get { return Converter.ToUInt32(this.extraIndex1); }
        }

        public uint ExtraIndex2
        {
            get { return Converter.ToUInt32(this.extraIndex2); }
        }

        public uint ExtraIndex3
        {
            get { return Converter.ToUInt32(this.extraIndex3); }
        }

        public uint ExtraIndex4
        {
            get { return Converter.ToUInt32(this.extraIndex4); }
        }

        public uint ExtraIndex5
        {
            get { return Converter.ToUInt32(this.extraIndex5); }
        }

        public uint FirstNonBookIndex
        {
            get { return Converter.ToUInt32(this.firstNonBookIndex); }
        }

        public uint FullNameOffset
        {
            get { return Converter.ToUInt32(this.fullNameOffset); }
        }

        public uint FullNameLength
        {
            get { return Converter.ToUInt32(this.fullNameLength); }
        }

        public uint MinVersion
        {
            get { return Converter.ToUInt32(this.minVersion); }
        }

        public uint HuffmanRecordOffset
        {
            get { return Converter.ToUInt32(this.huffmanRecordOffset); }
        }

        public uint HuffmanRecordCount
        {
            get { return Converter.ToUInt32(this.huffmanRecordCount); }
        }

        public uint HuffmanTableOffset
        {
            get { return Converter.ToUInt32(this.huffmanTableOffset); }
        }

        public uint HuffmanTableLength
        {
            get { return Converter.ToUInt32(this.huffmanTableLength); }
        }

        public EXTHHead EXTHHeader
        {
            get { return this.exthHeader; }
        }
    }
}
