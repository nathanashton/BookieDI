using Bookie.Common.Exceptions;
using Bookie.Common.Plugin;
using Ghostscript.NET;
using Ghostscript.NET.Rasterizer;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Bookie.Format.Pdf
{
    [DisplayName("PDF Format Plugin")]
    [Description("Provides support for the Adobe PDF Format using embedded iTextSharp library.")]
    public class PdfSupportedFormat : ISupportedFormatPlugin
    {
        private readonly GhostscriptVersionInfo _gvi;
        private string _isbn = string.Empty;

        public PdfSupportedFormat()
        {
            var currentPath = Directory.GetCurrentDirectory();
            var ghostScriptDll = new DirectoryInfo(currentPath).GetFiles("gsdll32.dll", SearchOption.AllDirectories);
            var meuDll = ghostScriptDll[0].FullName;
            _gvi = new GhostscriptVersionInfo(new Version(0, 0, 0), meuDll, string.Empty, GhostscriptLicense.GPL);
            Format = "PDF Format";
        }

        public string Format { get; set; }

        public string Activate()
        {
            return "Activate";
        }

        public void ExtractCover(string filePath)
        {
            var desired_x_dpi = 96;
            var desired_y_dpi = 96;

            var outputPath = @"C:\temp\books\test.png";

            using (var rasterizer = new GhostscriptRasterizer())
            {
                rasterizer.Open(filePath, _gvi, false);
                var img = rasterizer.GetPage(desired_x_dpi, desired_y_dpi, 1);
                img.Save(outputPath, ImageFormat.Png);
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