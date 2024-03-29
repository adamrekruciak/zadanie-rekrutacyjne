using System;
using System.IO;
using System.Linq;
using CsvHelper;
using zadanie_rekrutacyjne.Model;

namespace zadanie_rekrutacyjne.Services
{
    public class DataImporter
    {
        private readonly DatabaseContext _context;

        public DataImporter(DatabaseContext context)
        {
            _context = context;
        }

        public void ImportDataFromCsv(string documentsFilePath, string documentItemsFilePath)
        {
            var documents = ReadDocumentsFromCsv(documentsFilePath);
            var documentItems = ReadDocumentItemsFromCsv(documentItemsFilePath);

            _context.Documents.AddRange(documents);
            _context.DocumentItems.AddRange(documentItems);
            _context.SaveChanges();
        }

        private Document[] ReadDocumentsFromCsv(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, System.Globalization.CultureInfo.InvariantCulture))
            {
                return csv.GetRecords<Document>().ToArray();
            }
        }

        private DocumentItem[] ReadDocumentItemsFromCsv(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, System.Globalization.CultureInfo.InvariantCulture))
            {
                return csv.GetRecords<DocumentItem>().ToArray();
            }
        }
    }
}
