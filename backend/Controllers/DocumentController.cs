using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.IO;
using zadanie_rekrutacyjne.Model;
using CsvHelper.Configuration.Attributes;
using Microsoft.EntityFrameworkCore;

namespace zadanie_rekrutacyjne.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DocumentController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public DocumentController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpPost("import")]
        public IActionResult ImportDocuments()
        {
            var filePath = "Assets/Documents.csv";
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                Delimiter = ";", // Make sure this is the delimiter used in your CSV file
                BadDataFound = null // Optionally ignore bad data
            };

            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, config))
            {
                // Adjust the date format according to your CSV file
                csv.Context.TypeConverterOptionsCache.GetOptions<DateTime>().Formats = new[] { "yyyy-MM-dd" };

                var records = csv.GetRecords<Document>().Select(doc => new Document
                {
                    Type = doc.Type,
                    Date = doc.Date,
                    FirstName = doc.FirstName,
                    LastName = doc.LastName,
                    City = doc.City
                    // Do not set the Id property here
                }).ToList();
                _context.Documents.AddRange(records);
                _context.SaveChanges();
            }

            return Ok();
        }

        [HttpPost("import-items")]
        public IActionResult ImportDocumentItems()
        {
            var filePath = "Assets/DocumentItems.csv";
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                Delimiter = ";", // Upewnij się, że to jest poprawny delimiter
                BadDataFound = null // Opcjonalnie ignoruj złe dane
            };

            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, config))
            {
                csv.Context.RegisterClassMap<DocumentItemMap>();
                var records = csv.GetRecords<DocumentItem>().ToList();
                _context.DocumentItems.AddRange(records);
                _context.SaveChanges();
            }


            return Ok();
        }

        [HttpGet("documents")]
        public IActionResult GetDocuments()
        {
            try
            {
                var documents = _context.Documents.ToList(); // Pobierz wszystkie dokumenty z bazy danych
                return Ok(documents); // Zwróć dokumenty w formacie JSON
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error"); // Obsłuż błąd w przypadku niepowodzenia pobrania danych
            }
        }

        [HttpGet("document-items")]
        public IActionResult GetDocumentItems()
        {
            try
            {
                var documentItems = _context.DocumentItems.ToList(); // Pobierz wszystkie pozycje dokumentów z bazy danych
                return Ok(documentItems); // Zwróć pozycje dokumentów w formacie JSON
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error"); // Obsłuż błąd w przypadku niepowodzenia pobrania danych
            }
        }

        [HttpGet("documents/{id}")]
        public IActionResult GetDocumentById(int id)
        {
            try
            {
                var document = _context.Documents.FirstOrDefault(d => d.Id == id); // Pobierz dokument na podstawie jego identyfikatora
                if (document == null)
                {
                    return NotFound(); // Jeśli dokument nie został znaleziony, zwróć kod 404
                }
                return Ok(document); // Zwróć dokument w formacie JSON
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error"); // Obsłuż błąd w przypadku niepowodzenia pobrania danych
            }
        }
        [HttpGet("documents/{id}/items")]
        public IActionResult GetDocumentItemsById(int id)
        {
            try
            {
                var documentItems = _context.DocumentItems.Where(item => item.DocumentId == id).ToList();
                if (documentItems.Count == 0)
                {
                    return NotFound(); // Jeśli nie znaleziono pozycji dokumentów dla danego ID, zwróć kod 404
                }
                return Ok(documentItems); // Zwróć pozycje dokumentów w formacie JSON
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error"); // Obsłuż błąd w przypadku niepowodzenia pobrania danych
            }
        }
        [HttpDelete("delete-all-documents")]
        public IActionResult DeleteAllDocuments()
        {
            try
            {
                _context.Database.ExecuteSqlRaw("TRUNCATE TABLE Documents");
                return Ok("All documents have been deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("delete-all-document-items")]
        public IActionResult DeleteAllDocumentItems()
        {
            try
            {
                _context.Database.ExecuteSqlRaw("TRUNCATE TABLE DocumentItems");
                return Ok("All document items have been deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }


    }

    public sealed class DocumentItemMap : ClassMap<DocumentItem>
    {
        public DocumentItemMap()
        {
            Map(m => m.DocumentId).Name("DocumentId"); // Mapowanie kolumny DocumentId z pliku CSV na właściwość DocumentId
            Map(m => m.Ordinal);
            Map(m => m.Product);
            Map(m => m.Quantity);
            Map(m => m.Price);
            Map(m => m.TaxRate);
        }
    }



}