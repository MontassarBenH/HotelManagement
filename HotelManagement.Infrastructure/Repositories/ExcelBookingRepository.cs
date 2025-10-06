using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManagement.Application.Interfaces;
using HotelManagement.Application.Models;
using Excel = Microsoft.Office.Interop.Excel;

namespace HotelManagement.Infrastructure.Repositories
{
    /// <summary>
    /// Speichert Buchungen zusätzlich in Excel (neben XML)
    /// </summary>
    public class ExcelBookingRepository : IBookingRepository
    {
        private readonly Excel.Application _excelApp;
        private readonly IBookingRepository _xmlRepository;

        public ExcelBookingRepository(Excel.Application excelApp, IBookingRepository xmlRepository)
        {
            _excelApp = excelApp;
            _xmlRepository = xmlRepository;
        }

        public async Task SaveBookingAsync(Booking booking)
        {
            // 1. Speichere in XML 
            await _xmlRepository.SaveBookingAsync(booking);

            // 2. Speichere auch in Excel
            await Task.Run(() =>
            {
                try
                {
                    var sheet = GetOrCreateBookingsWorksheet();

                    // Finde nächste freie Zeile
                    int nextRow = FindNextEmptyRow(sheet);

                    // Daten schreiben
                    sheet.Cells[nextRow, 1] = booking.BookingId;
                    sheet.Cells[nextRow, 2] = booking.Guest?.Name ?? "Unbekannt";
                    sheet.Cells[nextRow, 3] = booking.RoomNumber;
                    sheet.Cells[nextRow, 4] = booking.CheckInDate.ToString("dd.MM.yyyy");
                    sheet.Cells[nextRow, 5] = booking.CheckOutDate.ToString("dd.MM.yyyy");
                    sheet.Cells[nextRow, 6] = booking.NumberOfNights;
                    sheet.Cells[nextRow, 7] = booking.TotalPrice;
                    sheet.Cells[nextRow, 8] = DateTime.Now.ToString("dd.MM.yyyy HH:mm");

                    System.Diagnostics.Debug.WriteLine($"Buchung in Excel gespeichert: Zeile {nextRow}");
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Fehler beim Speichern in Excel: {ex.Message}");
                }
            });
        }

        public async Task<List<Booking>> GetAllBookingsAsync()
        {
            // Lese aus XML
            return await _xmlRepository.GetAllBookingsAsync();
        }

        public async Task<Booking> GetBookingByIdAsync(string bookingId)
        {
            return await _xmlRepository.GetBookingByIdAsync(bookingId);
        }

        private Excel.Worksheet GetOrCreateBookingsWorksheet()
        {
            string sheetName = "Buchungen";

            // Suche existierendes Sheet
            foreach (Excel.Worksheet sheet in _excelApp.Worksheets)
            {
                if (sheet.Name.Equals(sheetName, StringComparison.OrdinalIgnoreCase))
                {
                    return sheet;
                }
            }

            // Erstelle neues Sheet
            Excel.Worksheet newSheet = _excelApp.Worksheets.Add();
            newSheet.Name = sheetName;

            // Header erstellen
            newSheet.Cells[1, 1] = "Buchungs-ID";
            newSheet.Cells[1, 2] = "Gast";
            newSheet.Cells[1, 3] = "Zimmer";
            newSheet.Cells[1, 4] = "Check-in";
            newSheet.Cells[1, 5] = "Check-out";
            newSheet.Cells[1, 6] = "Nächte";
            newSheet.Cells[1, 7] = "Preis (EUR)";
            newSheet.Cells[1, 8] = "Erstellt am";

            // Header fett
            Excel.Range headerRange = newSheet.Range[newSheet.Cells[1, 1], newSheet.Cells[1, 8]];
            headerRange.Font.Bold = true;
            headerRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);

            // Spaltenbreiten
            newSheet.Columns[1].ColumnWidth = 35; // Buchungs-ID
            newSheet.Columns[2].ColumnWidth = 20; // Gast
            newSheet.Columns[3].ColumnWidth = 10; // Zimmer
            newSheet.Columns[4].ColumnWidth = 12; // Check-in
            newSheet.Columns[5].ColumnWidth = 12; // Check-out
            newSheet.Columns[6].ColumnWidth = 8;  // Nächte
            newSheet.Columns[7].ColumnWidth = 12; // Preis
            newSheet.Columns[8].ColumnWidth = 18; // Erstellt

            System.Diagnostics.Debug.WriteLine("Worksheet 'Buchungen' erstellt");

            return newSheet;
        }

        private int FindNextEmptyRow(Excel.Worksheet sheet)
        {
            int row = 2; 
            while (sheet.Cells[row, 1].Value != null)
            {
                row++;
            }
            return row;
        }
    }
}
