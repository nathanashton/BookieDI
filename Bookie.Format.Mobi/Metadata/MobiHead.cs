using System;
using System.IO;
using System.Text;

namespace Bookie.Format.Mobi.Metadata
{
    public class MobiHead : BaseHeader
    {
        private readonly byte[] _exthFlags = new byte[4];
        private readonly byte[] _extraIndex0 = new byte[4];
        private readonly byte[] _extraIndex1 = new byte[4];
        private readonly byte[] _extraIndex2 = new byte[4];
        private readonly byte[] _extraIndex3 = new byte[4];
        private readonly byte[] _extraIndex4 = new byte[4];
        private readonly byte[] _extraIndex5 = new byte[4];
        private readonly byte[] _fileVersion = new byte[4];
        private readonly byte[] _firstImageIndex = new byte[4];
        private readonly byte[] _firstNonBookIndex = new byte[4];
        private readonly byte[] _fullNameLength = new byte[4];
        private readonly byte[] _fullNameOffset = new byte[4];
        private readonly byte[] _headerLength = new byte[4];
        private readonly byte[] _huffmanRecordCount = new byte[4];
        private readonly byte[] _huffmanRecordOffset = new byte[4];
        private readonly byte[] _huffmanTableLength = new byte[4];
        private readonly byte[] _huffmanTableOffset = new byte[4];
        private readonly byte[] _identifier = new byte[4];
        private readonly byte[] _indexKeys = new byte[4];
        private readonly byte[] _indexNames = new byte[4];
        private readonly byte[] _inflectionIndex = new byte[4];
        private readonly byte[] _inputLanguage = new byte[4];
        private readonly byte[] _locale = new byte[4];
        private readonly byte[] _minVersion = new byte[4];
        private readonly byte[] _mobiType = new byte[4];
        private readonly byte[] _orthographicIndex = new byte[4];
        private readonly byte[] _outputLanguage = new byte[4];

        private readonly byte[] _remainder;
        private readonly byte[] _textEncoding = new byte[4];
        private readonly byte[] _uniqueId = new byte[4];

        public MobiHead()
        {
            PopulateFieldList(true);
        }

        public MobiHead(FileStream fs, uint mobiHeaderSize)
        {
            fs.Read(_identifier, 0, _identifier.Length);

            if (IdentifierAsString != "MOBI")
            {
                throw new IOException("Did not get expected MOBI identifier");
            }

            fs.Read(_headerLength, 0, _headerLength.Length);
            var restOfMobiHeader = new byte[HeaderLength + 16 - 132];

            fs.Read(_mobiType, 0, _mobiType.Length);
            fs.Read(_textEncoding, 0, _textEncoding.Length);
            fs.Read(_uniqueId, 0, _uniqueId.Length);
            fs.Read(_fileVersion, 0, _fileVersion.Length);
            fs.Read(_orthographicIndex, 0, _orthographicIndex.Length);
            fs.Read(_inflectionIndex, 0, _inflectionIndex.Length);
            fs.Read(_indexNames, 0, _indexNames.Length);
            fs.Read(_indexKeys, 0, _indexKeys.Length);
            fs.Read(_extraIndex0, 0, _extraIndex0.Length);
            fs.Read(_extraIndex1, 0, _extraIndex1.Length);
            fs.Read(_extraIndex2, 0, _extraIndex2.Length);
            fs.Read(_extraIndex3, 0, _extraIndex3.Length);
            fs.Read(_extraIndex4, 0, _extraIndex4.Length);
            fs.Read(_extraIndex5, 0, _extraIndex5.Length);
            fs.Read(_firstNonBookIndex, 0, _firstNonBookIndex.Length);
            fs.Read(_fullNameOffset, 0, _fullNameOffset.Length);
            fs.Read(_fullNameLength, 0, _fullNameLength.Length);

            var fullNameLen = Converter.ToInt32(_fullNameLength);
            fs.Read(_locale, 0, _locale.Length);
            fs.Read(_inputLanguage, 0, _inputLanguage.Length);
            fs.Read(_outputLanguage, 0, _outputLanguage.Length);
            fs.Read(_minVersion, 0, _minVersion.Length);
            fs.Read(_firstImageIndex, 0, _firstImageIndex.Length);
            fs.Read(_huffmanRecordOffset, 0, _huffmanRecordOffset.Length);
            fs.Read(_huffmanRecordCount, 0, _huffmanRecordCount.Length);
            fs.Read(_huffmanTableOffset, 0, _huffmanTableOffset.Length);
            fs.Read(_huffmanTableLength, 0, _huffmanTableLength.Length);
            fs.Read(_exthFlags, 0, _exthFlags.Length);

            //If bit 6 (0x40) is set, then there's an EXTH record
            var exthExists = (Converter.ToUInt32(_exthFlags) & 0x40) != 0;

            fs.Read(restOfMobiHeader, 0, restOfMobiHeader.Length);

            if (exthExists)
            {
                ExthHeader = new ExthHead(fs);
            }

            var currentOffset = 132 + restOfMobiHeader.Length + ExthHeaderSize;
            _remainder = new byte[(int)(mobiHeaderSize - currentOffset)];
            fs.Read(_remainder, 0, _remainder.Length);

            var fullNameIndexInRemainder = Converter.ToInt32(_fullNameOffset) - currentOffset;

            var fullName = new byte[fullNameLen];

            if (fullNameIndexInRemainder >= 0)
            {
                if (fullNameIndexInRemainder < _remainder.Length)
                {
                    if (fullNameIndexInRemainder + fullNameLen <= _remainder.Length)
                    {
                        if (fullNameLen > 0)
                        {
                            Array.Copy(_remainder,
                                fullNameIndexInRemainder,
                                fullName,
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
                if (ExthHeader == null)
                {
                    return 0;
                }
                return ExthHeader.Size;
            }
        }

        public string FullName => Encoding.ASCII.GetString(_remainder).Replace("\0", string.Empty);

        public string IdentifierAsString => Encoding.UTF8.GetString(_identifier).Replace("\0", string.Empty);

        public uint HeaderLength => Converter.ToUInt32(_headerLength);

        public uint MobiType => Converter.ToUInt32(_mobiType);

        public string MobiTypeAsString
        {
            get
            {
                switch (MobiType)
                {
                    case 2:
                        return "Mobipocket Book";

                    case 3:
                        return "PalmDoc Book";

                    case 4:
                        return "Audio";

                    case 257:
                        return "News";

                    case 258:
                        return "News Feed";

                    case 259:
                        return "News Magazine";

                    case 513:
                        return "PICS";

                    case 514:
                        return "WORD";

                    case 515:
                        return "XLS";

                    case 516:
                        return "PPT";

                    case 517:
                        return "TEXT";

                    case 518:
                        return "HTML";

                    default:
                        return $"Unknown {MobiType}";
                }
            }
        }

        public uint TextEncoding => Converter.ToUInt32(_textEncoding);

        public string TextEncodingAsString
        {
            get
            {
                switch (TextEncoding)
                {
                    case 1252:
                        return "Cp1252";

                    case 65001:
                        return "UTF-8";

                    default:
                        return null;
                }
            }
        }

        public uint UniqueId => Converter.ToUInt32(_uniqueId);

        public uint FileVersion => Converter.ToUInt32(_fileVersion);

        public uint OrthographicIndex => Converter.ToUInt32(_orthographicIndex);

        public uint InflectionIndex => Converter.ToUInt32(_inflectionIndex);

        public uint IndexNames => Converter.ToUInt32(_indexNames);

        public uint IndexKeys => Converter.ToUInt32(_indexKeys);

        public uint ExtraIndex0 => Converter.ToUInt32(_extraIndex0);

        public uint ExtraIndex1 => Converter.ToUInt32(_extraIndex1);

        public uint ExtraIndex2 => Converter.ToUInt32(_extraIndex2);

        public uint ExtraIndex3 => Converter.ToUInt32(_extraIndex3);

        public uint ExtraIndex4 => Converter.ToUInt32(_extraIndex4);

        public uint ExtraIndex5 => Converter.ToUInt32(_extraIndex5);

        public uint FirstNonBookIndex => Converter.ToUInt32(_firstNonBookIndex);

        public uint FullNameOffset => Converter.ToUInt32(_fullNameOffset);

        public uint FullNameLength => Converter.ToUInt32(_fullNameLength);

        public uint MinVersion => Converter.ToUInt32(_minVersion);

        public uint HuffmanRecordOffset => Converter.ToUInt32(_huffmanRecordOffset);

        public uint HuffmanRecordCount => Converter.ToUInt32(_huffmanRecordCount);

        public uint HuffmanTableOffset => Converter.ToUInt32(_huffmanTableOffset);

        public uint HuffmanTableLength => Converter.ToUInt32(_huffmanTableLength);

        public ExthHead ExthHeader { get; }
    }
}