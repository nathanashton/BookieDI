using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Media.Imaging;
using Bookie.Common.Exceptions;
using Bookie.Common.Plugin;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Drawing;
using System.IO;
using libpdf;

namespace Bookie.Format.Pdf
{
    [DisplayName("PDF Format Plugin")]
    [Description("Provides support for the Adobe PDF Format using embedded iTextSharp library.")]
    public class PdfSupportedFormat : ISupportedFormatPlugin
    {
        private string _isbn = string.Empty;

        public PdfSupportedFormat()
        {
            Format = "PDF Format";
        }

        public string Format { get; set; }

        public string Activate()
        {
            return "Pdf";
        }

        public void ExtractCover(string filePath)
        {
            using (FileStream file = File.OpenRead(@"C:\temp\php.pdf")) // in file
            {
                var bytes = new byte[file.Length];
                file.Read(bytes, 0, bytes.Length);
                using (var pdf = new LibPdf(bytes))
                {
                    byte[] pngBytes = pdf.GetImage(0, ImageType.BMP); // image type
                    using (var outFile = File.Create(@"c:\temp\file.bmp")) // out file
                    {
                        outFile.Write(pngBytes, 0, pngBytes.Length);
                   }
                }
            }
        }


        public string Go(string url)
        {
            var text = new StringBuilder();
            try
            {
                using (var pdfReader = new PdfReader(url))
                {
                    // Loop through each page of the document
                    for (var page = 1; page <= 10; page++)
                    {
                        ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
                        try
                        {
                            var currentText = PdfTextExtractor.GetTextFromPage(pdfReader, page, strategy);
                            currentText =
                                Encoding.UTF8.GetString(
                                    Encoding.Convert(
                                        Encoding.Default,
                                        Encoding.UTF8,
                                        Encoding.Default.GetBytes(currentText)));
                            text.Append(currentText);
                        }
                        catch (ArgumentException)
                        {
                            // Logger.Log.Error(string.Format("Can't parse PDF {0}, only images and no text.", url));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new BookieException(ex.Message, ex);
            }

            var rFileIsbn = Regex.Match(text.ToString(), @"ISBN.*?([X\d\-_ .]{10,20})");
            if (!rFileIsbn.Success)
            {
                return null;
            }
            _isbn = rFileIsbn.Groups[1].ToString();
            _isbn = _isbn.Replace(".", string.Empty);
            _isbn = _isbn.Replace(" ", string.Empty);
            _isbn = _isbn.Replace("-", string.Empty);
            _isbn = _isbn.Replace("_", string.Empty);
            return _isbn;
        }
    }
}