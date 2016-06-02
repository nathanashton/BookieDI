using Bookie.Common.Entities;
using Bookie.Common.Exceptions;
using Bookie.Common.Plugin;
using Ghostscript.NET;
using Ghostscript.NET.Rasterizer;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using static System.String;
using Path = System.IO.Path;

namespace Bookie.Format.Pdf
{
    [DisplayName("PDF Format Plugin")]
    [Description("Provides support for the Adobe PDF Format using embedded iTextSharp library.")]
    public class PdfSupportedFormat : ISupportedFormatPlugin
    {
        private readonly GhostscriptVersionInfo _gvi;
        private string _isbn = Empty;

        public PdfSupportedFormat()
        {
            var currentPath = Directory.GetCurrentDirectory();
            var ghostScriptDll = new DirectoryInfo(currentPath).GetFiles("gsdll32.dll", SearchOption.AllDirectories);
            var meuDll = ghostScriptDll[0].FullName;
            _gvi = new GhostscriptVersionInfo(new Version(0, 0, 0), meuDll, Empty, GhostscriptLicense.GPL);
            Format = "PDF Format";
            FileExtension = ".pdf";
        }

        public string Format { get; set; }
        public string FileExtension { get; set; }

        public string Activate()
        {
            return "Activate";
        }

        public Image ExtractCover(string inputPath)
        {
            try
            {
                var desired_x_dpi = 96;
                var desired_y_dpi = 96;
                using (var rasterizer = new GhostscriptRasterizer())
                {
                    rasterizer.Open(inputPath, _gvi, false);
                    var img = rasterizer.GetPage(desired_x_dpi, desired_y_dpi, 1);
                    return img;
                }
            }
            catch (Exception ex)
            {
                throw new BookieException($"Error extracting cover image for {inputPath}", ex);
            }
        }

        public Metadata ExtractMetadata(string inputPath)
        {
            try
            {
                var reader = new PdfReader(inputPath);
                var metadata = new Metadata();
                string author;
                if (reader.Info.TryGetValue("Author", out author))
                {
                    if (author.Contains(","))
                    {
                        var split = author.Split(',');
                        var author2 = new Author
                        {
                            FirstName = split[1].Replace(" ", Empty),
                            LastName = split[0].Replace(" ", Empty)
                        };
                        metadata.Authors.Add(author2);
                    }
                    else
                    {
                        var split = author.Split(' ');
                        var author2 = new Author();
                        if (split.Length > 1)
                        {
                            author2.FirstName = split[0];
                            author2.LastName = split[1];
                            metadata.Authors.Add(author2);
                        }
                    }
                }
                string title;
                if (reader.Info.TryGetValue("Title", out title))
                {
                    metadata.Title = !IsNullOrEmpty(title) ? title : Path.GetFileNameWithoutExtension(inputPath);
                }
                else
                {
                    metadata.Title = Path.GetFileNameWithoutExtension(inputPath);
                }
                metadata.PageCount = reader.NumberOfPages;
                metadata.Isbn = ExtractIsbn(inputPath);
                return metadata;
            }
            catch (Exception ex)
            {
                throw new BookieException($"Error extracting metadata for {inputPath}", ex);
            }
        }

        private string ExtractIsbn(string url)
        {
            var text = new StringBuilder();
            try
            {
                using (var pdfReader = new PdfReader(url))
                {
                    // Loop through each page of the document
                    for (var page = 1; page <= pdfReader.NumberOfPages; page++)
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
            _isbn = _isbn.Replace(".", Empty);
            _isbn = _isbn.Replace(" ", Empty);
            _isbn = _isbn.Replace("-", Empty);
            _isbn = _isbn.Replace("_", Empty);
            return _isbn;
        }
    }
}