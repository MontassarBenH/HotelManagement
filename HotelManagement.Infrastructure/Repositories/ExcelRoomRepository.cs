using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManagement.Application.Interfaces;
using HotelManagement.Application.Models;
using Excel = Microsoft.Office.Interop.Excel;

namespace HotelManagement.Infrastructure.Repositories
{
    public class ExcelRoomRepository : IRoomRepository
    {
        private readonly Excel.Application _excelApp;
        private Excel.Workbook _workbook;
        private readonly string _workbookPath;
        private bool _isInitialized = false;

        public ExcelRoomRepository(Excel.Application excelApp, string workbookPath = null)
        {
            _excelApp = excelApp ?? throw new ArgumentNullException(nameof(excelApp));
            _workbookPath = workbookPath ?? System.IO.Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "HotelManagement.xlsx"
            );
        }

        public Task SetOccupiedTodayAsync(string roomNumber, bool isOccupiedToday)
        {
            EnsureWorkbookInitialized();

            var sheet = GetWorksheetByName("Zimmer") ?? CreateZimmerWorksheet();

            // ensure headers
            if (sheet.Cells[1, 1].Value == null) CreateHeader(sheet);
            if (sheet.Cells[1, 4].Value == null || sheet.Cells[1, 4].Value.ToString() != "BelegtHeute")
                sheet.Cells[1, 4].Value = "BelegtHeute";

            int row = FindRoomRow(sheet, roomNumber);
            if (row != -1)
            {
                sheet.Cells[row, 4].Value = isOccupiedToday ? "Ja" : "Nein";
                SaveWorkbook();
                System.Diagnostics.Debug.WriteLine($"BelegtHeute: {roomNumber} -> {(isOccupiedToday ? "Ja" : "Nein")}");
            }

            return Task.CompletedTask;
        }

        private void EnsureWorkbookInitialized()
        {
            if (_isInitialized && _workbook != null) return;

            try
            {
                if (System.IO.File.Exists(_workbookPath))
                {
                    _workbook = _excelApp.Workbooks.Open(_workbookPath);
                    System.Diagnostics.Debug.WriteLine($"Opened workbook: {_workbookPath}");
                }
                else
                {
                    _workbook = _excelApp.Workbooks.Add();
                    _workbook.SaveAs(_workbookPath);
                    System.Diagnostics.Debug.WriteLine($"Created workbook: {_workbookPath}");
                }
                _isInitialized = true;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Excel-Arbeitsmappe konnte nicht initialisiert werden", ex);
            }
        }


        public Task<List<Room>> GetAllRoomsAsync()
        {
            return Task.Run(() =>
            {
                var rooms = new List<Room>();

                try
                {
                    EnsureWorkbookInitialized(); 

                    Excel.Worksheet sheet = GetWorksheetByName("Zimmer");

                    if (sheet == null)
                    {
                        System.Diagnostics.Debug.WriteLine("Worksheet 'Zimmer' nicht gefunden - erstelle neu");
                        sheet = CreateZimmerWorksheet();
                    }

                    // Header prüfen
                    if (sheet.Cells[1, 1].Value == null)
                    {
                        CreateHeader(sheet);
                    }

                    // Daten lesen
                    int row = 2;
                    int emptyRowCount = 0;

                    while (emptyRowCount < 5)
                    {
                        var zimmerNummer = sheet.Cells[row, 1].Value;

                        if (zimmerNummer == null)
                        {
                            emptyRowCount++;
                            row++;
                            continue;
                        }

                        emptyRowCount = 0;

                        rooms.Add(new Room
                        {
                            RoomNumber = zimmerNummer?.ToString().Trim(),
                            RoomType = sheet.Cells[row, 2].Value?.ToString().Trim() ?? "Standard",
                            IsAvailable = IsRoomAvailable(sheet.Cells[row, 3].Value)
                        });

                        row++;
                    }

                    System.Diagnostics.Debug.WriteLine($"Geladen: {rooms.Count} Zimmer");
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Fehler in GetAllRoomsAsync: {ex.Message}");
                    throw;
                }

                return rooms;
            });
        }

        public async Task<Room> GetRoomByNumberAsync(string roomNumber)
        {
            var rooms = await GetAllRoomsAsync();
            return rooms.FirstOrDefault(r =>
                r.RoomNumber != null &&
                r.RoomNumber.Equals(roomNumber?.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        public Task AddRoomAsync(Room room)
        {
            return Task.Run(() =>
            {
                try
                {
                    EnsureWorkbookInitialized(); 

                    var sheet = GetWorksheetByName("Zimmer");
                    if (sheet == null)
                    {
                        sheet = CreateZimmerWorksheet();
                    }

                    int nextRow = FindNextEmptyRow(sheet);

                    sheet.Cells[nextRow, 1] = room.RoomNumber;
                    sheet.Cells[nextRow, 2] = room.RoomType;
                    sheet.Cells[nextRow, 3] = room.IsAvailable ? "Ja" : "Nein";

                    SaveWorkbook();

                    System.Diagnostics.Debug.WriteLine($"Zimmer hinzugefügt: {room.RoomNumber} in Zeile {nextRow}");
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Fehler in AddRoomAsync: {ex.Message}");
                    throw;
                }
            });
        }

        public Task UpdateRoomAsync(Room room)
        {
            return Task.Run(() =>
            {
                try
                {
                    EnsureWorkbookInitialized(); 

                    var sheet = GetWorksheetByName("Zimmer");
                    if (sheet == null)
                        throw new Exception("Worksheet 'Zimmer' nicht gefunden");

                    int row = FindRoomRow(sheet, room.RoomNumber);

                    if (row == -1)
                        throw new Exception($"Zimmer '{room.RoomNumber}' nicht gefunden");

                    sheet.Cells[row, 2] = room.RoomType;
                    sheet.Cells[row, 3] = room.IsAvailable ? "Ja" : "Nein";

                    SaveWorkbook();

                    System.Diagnostics.Debug.WriteLine($"Zimmer aktualisiert: {room.RoomNumber} in Zeile {row}");
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Fehler in UpdateRoomAsync: {ex.Message}");
                    throw;
                }
            });
        }

        public Task DeleteRoomAsync(string roomNumber)
        {
            return Task.Run(() =>
            {
                try
                {
                    EnsureWorkbookInitialized(); 

                    var sheet = GetWorksheetByName("Zimmer");
                    if (sheet == null)
                        throw new Exception("Worksheet 'Zimmer' nicht gefunden");

                    int row = FindRoomRow(sheet, roomNumber);

                    if (row == -1)
                        throw new Exception($"Zimmer '{roomNumber}' nicht gefunden");

                    Excel.Range rowRange = sheet.Rows[row];
                    rowRange.Delete(Excel.XlDeleteShiftDirection.xlShiftUp);

                    SaveWorkbook();

                    System.Diagnostics.Debug.WriteLine($"Zimmer gelöscht: {roomNumber} (war Zeile {row})");
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Fehler in DeleteRoomAsync: {ex.Message}");
                    throw;
                }
            });
        }

        // HELPER METHODS
        private void SaveWorkbook()
        {
            try
            {
                if (_workbook != null)
                {
                    if (string.IsNullOrEmpty(_workbook.Path))
                    {
                        _workbook.SaveAs(_workbookPath);
                        System.Diagnostics.Debug.WriteLine($"Arbeitsmappe gespeichert unter: {_workbookPath}");
                    }
                    else
                    {
                        _workbook.Save();
                        System.Diagnostics.Debug.WriteLine("Arbeitsmappe gespeichert");
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Fehler beim Speichern: {ex.Message}");
            }
        }

        private bool IsRoomAvailable(object cellValue)
        {
            if (cellValue == null)
                return false;

            var value = cellValue.ToString().Trim().ToLower();

            return value == "ja" ||
                   value == "yes" ||
                   value == "true" ||
                   value == "1" ||
                   value == "x";
        }

        private Excel.Worksheet GetWorksheetByName(string name)
        {
            try
            {
                if (_workbook == null)
                    return null;

                foreach (Excel.Worksheet sheet in _workbook.Worksheets)
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

        private Excel.Worksheet CreateZimmerWorksheet()
        {
            Excel.Worksheet newSheet = _workbook.Worksheets.Add();
            newSheet.Name = "Zimmer";

            CreateHeader(newSheet);

            // Beispieldaten
            newSheet.Cells[2, 1] = "101";
            newSheet.Cells[2, 2] = "Einzel";
            newSheet.Cells[2, 3] = "Ja";

            newSheet.Cells[3, 1] = "102";
            newSheet.Cells[3, 2] = "Doppel";
            newSheet.Cells[3, 3] = "Ja";

            newSheet.Cells[4, 1] = "103";
            newSheet.Cells[4, 2] = "Suite";
            newSheet.Cells[4, 3] = "Ja";

            SaveWorkbook();

            System.Diagnostics.Debug.WriteLine("Worksheet 'Zimmer' erstellt");

            return newSheet;
        }

        private void CreateHeader(Excel.Worksheet sheet)
        {
            sheet.Cells[1, 1] = "Zimmernummer";
            sheet.Cells[1, 2] = "Zimmertyp";
            sheet.Cells[1, 3] = "Verfügbar";
            sheet.Cells[1, 4] = "BelegtHeute";

            Excel.Range headerRange = sheet.Range[sheet.Cells[1, 1], sheet.Cells[1, 4]];
            headerRange.Font.Bold = true;
            headerRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
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

        private int FindRoomRow(Excel.Worksheet sheet, string roomNumber)
        {
            int row = 2;
            int maxRows = 1000; 

            for (int i = row; i < maxRows; i++)
            {
                var cellValue = sheet.Cells[i, 1].Value;

                if (cellValue == null)
                    break;

                if (cellValue.ToString().Trim().Equals(roomNumber.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    return i;
                }
            }

            return -1;
        }
    }
}