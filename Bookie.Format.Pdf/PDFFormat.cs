using Bookie.Common.Plugin;
using System.Windows;

namespace Bookie.Format.Pdf
{
    [DisplayName("PDF Format Plugin")]
    [Description("Provides support for the Adobe PDF Format.")]
    public class PdfFormat : IFormatPlugin
    {
        public string Format { get; set; }

        public PdfFormat()
        {
            Format = "PDF Format";
        }

        public void Activate()
        {
            Window w = new Window();
            w.Show();
        }
    }
}