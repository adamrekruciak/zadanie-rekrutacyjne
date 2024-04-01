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
                Delimiter = ";", 
                BadDataFound = null 
            };

            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, config))
            {
                csv.Context.TypeConverterOptionsCache.GetOptions<DateTime>().Formats = new[] { "yyyy-MM-dd" };

                var records = csv.GetRecords<Document>().Select(doc => new Document
                {
                    Type = doc.Type,
                    Date = doc.Date,
                    FirstName = doc.FirstName,
                    LastName = doc.LastName,
                    City = doc.City
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
                Delimiter = ";", 
                BadDataFound = null 
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
                var documents = _context.Documents.ToList();
                return Ok(documents); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error"); 
            }
        }

        [HttpGet("document-items")]
        public IActionResult GetDocumentItems()
        {
            try
            {
                var documentItems = _context.DocumentItems.ToList();
                return Ok(documentItems);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("documents/{id}")]
        public IActionResult GetDocumentById(int id)
        {
            try
            {
                var document = _context.Documents.FirstOrDefault(d => d.Id == id); 
                if (document == null)
                {
                    return NotFound();
                }
                return Ok(document); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
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
                    return NotFound();
                }
                return Ok(documentItems); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
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
            Map(m => m.DocumentId).Name("DocumentId"); // Mapowanie
            Map(m => m.Ordinal);
            Map(m => m.Product);
            Map(m => m.Quantity);
            Map(m => m.Price);
            Map(m => m.TaxRate);
        }
    }
}