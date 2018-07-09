using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using QRCoder;
using SSBO5G__Szakdolgozat.Models;
using System.Drawing;
using System.IO;
using System.Text;

namespace SSBO5G__Szakdolgozat.Services
{
    public static class PdfGenerator
    {
        static byte[] ImageToByteArray(Bitmap imageIn)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                return ms.ToArray();
            }
        }

        public static byte[] GetDiploma(Registration registration, HikeCourse course)
        {
            bool isInLimitTime = registration.Passes[registration.Passes.Count - 1].NettoTime < course.LimitTime;
            PdfFont normalFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA, "CP1250");
            using (MemoryStream ms = new MemoryStream())
            {
                using (PdfWriter writer = new PdfWriter(ms))
                {
                    PdfDocument pdfDocument = new PdfDocument(writer);
                    Document document = new Document(pdfDocument, PageSize.A4.Rotate());
                    pdfDocument.SetCloseWriter(false);
                    Paragraph header = new Paragraph();
                    header.AddTabStops(new TabStop((PageSize.A4.GetHeight() - document.GetLeftMargin() - document.GetRightMargin()) / 2, iText.Layout.Properties.TabAlignment.CENTER));
                    header.SetFont(normalFont);
                    header.SetFontSize(80);
                    header.Add(new Tab());
                    header.Add(isInLimitTime ? "Oklevél" : "Emléklap");
                    document.Add(header);
                    Paragraph body = new Paragraph();
                    body.SetFont(normalFont);
                    body.SetFontSize(32);
                    body.Add($"{registration.Hiker.Name} számára, aki " +
                        $"{(isInLimitTime ? "szintidőn belül" : "")} teljesítette a " +
                        $"{course.Name} túrát! " +
                        $"(idő: {registration.Passes[registration.Passes.Count - 1].NettoTime.Value.ToString("hh\\:mm\\:ss")})");
                    document.Add(body);
                    document.Close();
                    pdfDocument.Close();
                    writer.Flush();
                    ms.Flush();
                    ms.Position = 0;
                    return ms.ToArray();

                }
            }
        }

        public static byte[] GetCourseInfoPdf(HikeCourse course)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            PdfFont normalFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA, "CP1250");
            using (MemoryStream ms = new MemoryStream())
            {
                using (PdfWriter writer = new PdfWriter(ms))
                {
                    PdfDocument pdfDocument = new PdfDocument(writer);
                    Document document = new Document(pdfDocument);
                    pdfDocument.SetCloseWriter(false);
                    Paragraph header = new Paragraph();
                    header.SetFont(normalFont);
                    header.SetFontSize(32);
                    header.Add($"{course.Name} nevezési lista");
                    document.Add(header);
                    foreach (Registration reg in course.Registrations)
                    {
                        Paragraph p = new Paragraph();
                        p.SetFont(normalFont);

                        QRCodeData qrCodeData = qrGenerator.CreateQrCode(reg.StartNumber, QRCodeGenerator.ECCLevel.Q);
                        QRCode qrCode = new QRCode(qrCodeData);
                        p.Add($"Név: {reg.Hiker.Name}\n");
                        p.Add($"Telefonszám: {reg.Hiker.PhoneNumber}\n");
                        p.Add($"Születési Dátum: {reg.Hiker.DateOfBirth.ToShortDateString()}\n");
                        p.Add($"Rajtszám: {reg.StartNumber}\n");
                        p.Add(
                           new iText.Layout.Element.Image(
                               ImageDataFactory.Create(ImageToByteArray(qrCode.GetGraphic(5)))));
                        document.Add(p);
                    }
                    document.Close();
                    pdfDocument.Close();
                    writer.Flush();
                    ms.Flush();
                    ms.Position = 0;
                    return ms.ToArray();

                }
            }
        }
    }
}
