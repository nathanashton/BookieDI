using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Bookie.Common.Plugin;
using Bookie.Format.Mobi.Metadata;

namespace Bookie.Format.Mobi
{
    [DisplayName("MOBI Format Plugin")]
    [Description("Provides support for the MOBI Format.")]
    public class MobiSupportedFormat : ISupportedFormatPlugin
    {
        public string Format { get; set; }

        public MobiSupportedFormat()
        {
            Format = "MOBI Format";
        }

        public string Activate()
        {
            var testfile = @"C:\Users\nathana\Downloads\18 Never Go Back.mobi";
         


            using (var fs2 = new FileStream(testfile, FileMode.Open, FileAccess.Read))
            {
                MobiMetadata meta = new MobiMetadata(fs2);
                return meta.MobiHeader.EXTHHeader.UpdatedTitle;
            }


        }

        public void ExtractCover(string filePath)
        {
            using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                var s = CoverExtractor.ExtractCover(fs);
                var image = Image.FromStream(s);
              //  return image;
            }
        }
    }
}
