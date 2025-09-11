using ClosedXML.Excel;
using DataAccessLayer.Concrete;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TraversalCoreProject.Models;

//pdf icin
using iTextSharp.text;

namespace TraversalCoreProject.Controllers
{
    public class FileReportController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        // Veritabanından destinasyonları çekiyoruz
        public List<ExcelDestinationModel> DestinationList()
        {
            List<ExcelDestinationModel> excelDestinationModels = new List<ExcelDestinationModel>();

            using (Context c = new Context())
            {
                excelDestinationModels = c.Destinations.Select(x => new ExcelDestinationModel
                {
                    City = x.City,
                    Capacity = x.Capacity,
                    DayNight = x.DayNight,
                    Price = x.Price
                }).ToList();
            }
            return excelDestinationModels;
        }

        // Statik Excel (test amaçlı)
        public IActionResult StaticExcelReport()
        {
            using (var workBook = new XLWorkbook())
            {
                var workSheet = workBook.Worksheets.Add("Page1");
                workSheet.Cell(1, 1).Value = "Rota";
                workSheet.Cell(1, 2).Value = "Rehber";
                workSheet.Cell(1, 3).Value = "Kontenjan";

                workSheet.Cell(2, 1).Value = "Karadeniz - İç Anadolu - Batum (Doğa ve kültür turu)";
                workSheet.Cell(2, 2).Value = "Dilara Gümüş";
                workSheet.Cell(2, 3).Value = "28";

                using (var stream = new MemoryStream())
                {
                    workBook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "StaticFile.xlsx");
                }
            }
        }

        // Dinamik Excel (veritabanından gelen liste)
        public IActionResult DestinationExcelReport()
        {
            using (var workBook = new XLWorkbook())
            {
                var workSheet = workBook.Worksheets.Add("Tur Listesi");

                // Başlıklar
                workSheet.Cell(1, 1).Value = "Şehir";
                workSheet.Cell(1, 2).Value = "Konaklama Süresi";
                workSheet.Cell(1, 3).Value = "Fiyat";
                workSheet.Cell(1, 4).Value = "Kapasite";

                // Veriler
                var rowCount = 2;
                foreach (var item in DestinationList())
                {
                    workSheet.Cell(rowCount, 1).Value = item.City;
                    workSheet.Cell(rowCount, 2).Value = item.DayNight;
                    workSheet.Cell(rowCount, 3).Value = item.Price;
                    workSheet.Cell(rowCount, 4).Value = item.Capacity;

                    rowCount++;
                }

                using (var stream = new MemoryStream())
                {
                    workBook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Destinations.xlsx");
                }
            }
        }

        public IActionResult StaticPDFReport()
        {
            using (var stream = new MemoryStream())
            {
                Document document = new Document(PageSize.A4);
                PdfWriter.GetInstance(document, stream);

                document.Open();
                Paragraph elements = new Paragraph("Traversal Rezervasyon PDF Raporu");
                document.Add(elements);
                document.Close();

                var bytes = stream.ToArray();
                return File(bytes, "application/pdf", "StaticPDFReport.pdf");
            }
        }

        public IActionResult DestinationPDFReport()
        {
            using (var stream = new MemoryStream())
            {
                Document document = new Document(PageSize.A4);
                PdfWriter.GetInstance(document, stream);

                document.Open();
                Paragraph title = new Paragraph("Traversal Dinamik PDF Tur Raporu\n\n");
                document.Add(title);

                // Tablo
                PdfPTable table = new PdfPTable(4);
                table.AddCell("Şehir");
                table.AddCell("Konaklama Süresi");
                table.AddCell("Fiyat");
                table.AddCell("Kapasite");

                using (var c = new Context())
                {
                    var destinations = c.Destinations.ToList();
                    foreach (var item in destinations)
                    {
                        table.AddCell(item.City);
                        table.AddCell(item.DayNight);
                        table.AddCell(item.Price.ToString());
                        table.AddCell(item.Capacity.ToString());
                    }
                }

                document.Add(table);
                document.Close();

                var bytes = stream.ToArray();
                return File(bytes, "application/pdf", "DestinationPDFReport.pdf");
            }
        }
    }
}
