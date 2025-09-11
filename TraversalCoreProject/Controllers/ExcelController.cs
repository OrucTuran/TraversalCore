using ClosedXML.Excel;
using DataAccessLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TraversalCoreProject.Models;

namespace TraversalCoreProject.Controllers
{
    public class ExcelController : Controller
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
    }
}
