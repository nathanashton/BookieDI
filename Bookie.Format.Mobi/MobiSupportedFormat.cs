using Bookie.Common.Entities;
using Bookie.Common.Exceptions;
using Bookie.Common.Plugin;
using Bookie.Format.Mobi.Metadata;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using static System.String;

namespace Bookie.Format.Mobi
{
    [DisplayName("MOBI Format Plugin")]
    [Description("Provides support for the MOBI Format.")]
    public class MobiSupportedFormat : ISupportedFormatPlugin
    {
        public MobiSupportedFormat()
        {
            Format = "MOBI Format";
            FileExtension = ".mobi";
        }

        public string Format { get; set; }
        public string FileExtension { get; set; }

        public Image ExtractCover(string inputPath)
        {
            try
            {
                using (var fs = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
                {
                    var s = CoverExtractor.ExtractCover(fs);
                    if (s == null) return null;
                    var image = Image.FromStream(s);
                    return image;
                }
            }
            catch (Exception ex)
            {
                throw new BookieException($"Error extracting cover image for {inputPath}", ex);
            }
        }

        public Common.Plugin.Metadata ExtractMetadata(string inputPath)
        {
            try
            {
                using (var stream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
                {
                    var meta = new MobiMetadata(stream);

                    var metadata = new Common.Plugin.Metadata
                    {
                        Abstract = Regex.Replace(meta.MobiHeader.ExthHeader.Description, "<.*?>", Empty),
                        Title =
                            IsNullOrEmpty(meta.MobiHeader.ExthHeader.UpdatedTitle)
                                ? Path.GetFileNameWithoutExtension(inputPath)
                                : meta.MobiHeader.ExthHeader.UpdatedTitle,
                        Asin = meta.MobiHeader.ExthHeader.Asin,
                        Isbn = meta.MobiHeader.ExthHeader.Ibsn,
                        Subject = meta.MobiHeader.ExthHeader.Subject
                    };

                    //  metadata.Title = Utils.CleanInput(metadata.Title);

                    DateTime publisheddate;
                    if (DateTime.TryParse(meta.MobiHeader.ExthHeader.PublishedDate, out publisheddate))
                    {
                        metadata.PublishedDate = publisheddate;
                    }

                    metadata.Authors = new List<Author>();
                    if (meta.MobiHeader.ExthHeader.Author.Contains(","))
                    {
                        var split = meta.MobiHeader.ExthHeader.Author.Split(',');
                        var author = new Author
                        {
                            FirstName = split[1].Replace(" ", Empty),
                            LastName = split[0].Replace(" ", Empty)
                        };
                        metadata.Authors.Add(author);
                    }
                    else
                    {
                        var split = meta.MobiHeader.ExthHeader.Author.Split(' ');
                        var author = new Author();
                        if (split.Length > 1)
                        {
                            author.FirstName = split[0];
                            author.LastName = split[1];
                            metadata.Authors.Add(author);
                        }
                    }

                    return metadata;
                }
            }
            catch (Exception ex)
            {
                throw new BookieException("Error with mobi file", ex);
            }
        }
    }
}