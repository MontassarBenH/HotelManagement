using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManagement.Application.Interfaces;
using HotelManagement.Application.Models;
using Excel = Microsoft.Office.Interop.Excel;

namespace HotelManagement.Infrastructure.Repositories
{
    public class ExcelGuestRepository : IGuestRepository
    {
        private readonly Excel.Application _excelApp;

        public ExcelGuestRepository(Excel.Application excelApp)
        {
            _excelApp = excelApp;
        }

        public Task<List<Guest>> GetAllGuestsAsync()
        {
            return Task.Run(() =>
            {
                var guests = new List<Guest>();

                try
                {
                    Excel.Worksheet sheet = GetWorksheetByName("Gäste");

                    if (sheet == null)
                    {
                        System.Diagnostics.Debug.WriteLine("WARNUNG: Worksheet 'Gäste' nicht gefunden!");
                        sheet = CreateGaesteWorksheet();
                    }

                    // Header prüfen/erstellen
                    if (sheet.Cells[1, 1].Value == null)
                    {
                        sheet.Cells[1, 1] = "Gast-ID";
                        sheet.Cells[1, 2] = "Name";
                        sheet.Cells[1, 3] = "Email";
                        sheet.Cells[1, 4] = "Telefon";
                    }

                    // Daten lesen
                    int row = 2;
                    int emptyRowCount = 0;

                    while (emptyRowCount < 5)
                    {
                        var gastId = sheet.Cells[row, 1].Value;

                        if (gastId == null)
                        {
                            emptyRowCount++;
                            row++;
                            continue;
                        }

                        emptyRowCount = 0;

                        guests.Add(new Guest
                        {
                            Id = gastId?.ToString().Trim(),
                            Name = sheet.Cells[row, 2].Value?.ToString().Trim(),
                            Email = sheet.Cells[row, 3].Value?.ToString().Trim(),
                            Phone = sheet.Cells[row, 4].Value?.ToString().Trim()
                        });

                        row++;
                    }

                    System.Diagnostics.Debug.WriteLine($"GESAMT: {guests.Count} Gäste geladen");
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"FEHLER in GetAllGuestsAsync: {ex.Message}");
                    throw;
                }

                return guests;
            });
        }

        public async Task<Guest> GetGuestByIdAsync(string guestId)
        {
            var guests = await GetAllGuestsAsync();
            return guests.FirstOrDefault(g =>
                g.Id != null &&
                g.Id.Trim().Equals(guestId?.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        private Excel.Worksheet GetWorksheetByName(string name)
        {
            try
            {
                foreach (Excel.Worksheet sheet in _excelApp.Worksheets)
                {
                    if (sheet.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                    {
                        return sheet;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Fehler in GetWorksheetByName: {ex.Message}");
            }

            return null;
        }

        private Excel.Worksheet CreateGaesteWorksheet()
        {
            Excel.Worksheet newSheet = _excelApp.Worksheets.Add();
            newSheet.Name = "Gäste";

            // Header erstellen
            newSheet.Cells[1, 1] = "Gast-ID";
            newSheet.Cells[1, 2] = "Name";
            newSheet.Cells[1, 3] = "Email";
            newSheet.Cells[1, 4] = "Telefon";

            // Beispieldaten
            newSheet.Cells[2, 1] = "G001";
            newSheet.Cells[2, 2] = "Max Mustermann";
            newSheet.Cells[2, 3] = "max@example.com";
            newSheet.Cells[2, 4] = "0123-456789";

            System.Diagnostics.Debug.WriteLine("Worksheet 'Gäste' wurde neu erstellt");

            return newSheet;
        }
    }
}
