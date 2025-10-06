using HotelManagement.Application.Features.Bookings;
using MediatR;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;


namespace HotelManagement.Presentation.Forms
{
    public partial class BookingListForm : Form
    {
        private TextBox txtSearch;
        private ComboBox cboFilterRoom;
        private Button btnExport;
        private BindingSource bindingSource;
        private List<BookingRow> allBookings;
        public BookingListForm()
        {
            InitializeComponent();
            bindingSource = new BindingSource();
            EnhanceBookingListForm();
            LoadBookings();
        }

        private class BookingRow
        {
            public string BuchungsID { get; set; }
            public string Gast { get; set; }
            public string Zimmer { get; set; }
            public string CheckIn { get; set; }
            public string CheckOut { get; set; }
            public int Nächte { get; set; }
            public string Preis { get; set; }
        }

        private List<BookingRow> _allRows = new List<BookingRow>();
        private BindingSource _bs = new BindingSource();

        private async void LoadBookings()
        {
            try
            {
                if (btnRefresh != null)
                {
                    btnRefresh.Enabled = false;
                    btnRefresh.Text = "Lädt...";
                }

                var mediator = Globals.ThisAddIn.ServiceProvider?.GetService(typeof(IMediator)) as IMediator;

                if (mediator == null)
                {
                    MessageBox.Show("Service nicht verfügbar.", "Fehler",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var query = new GetAllBookingsQuery();
                var bookings = await mediator.Send(query);

                // Zu Display-Items konvertieren
                allBookings = bookings.Select(b => new BookingRow
                {
                    BuchungsID = b.BookingId,
                    Gast = b.Guest?.Name ?? "Unbekannt",
                    Zimmer = b.RoomNumber,
                    CheckIn = b.CheckInDate.ToString("dd.MM.yyyy"),
                    CheckOut = b.CheckOutDate.ToString("dd.MM.yyyy"),
                    Nächte = b.NumberOfNights,
                    Preis = b.TotalPrice.ToString("C")
                }).ToList();

                // BindingSource setzen
                bindingSource.DataSource = allBookings;
                dgvBookings.DataSource = bindingSource;

                if (lblCount != null)
                    lblCount.Text = $"{bookings.Count} Buchung(en) gefunden";

                // Spaltenbreiten
                if (dgvBookings.Columns.Count > 0)
                {
                    dgvBookings.Columns["BuchungsID"].Width = 200;
                    dgvBookings.Columns["Gast"].Width = 150;
                    dgvBookings.Columns["Zimmer"].Width = 80;
                    dgvBookings.Columns["CheckIn"].Width = 100;
                    dgvBookings.Columns["CheckOut"].Width = 100;
                    dgvBookings.Columns["Nächte"].Width = 80;
                    dgvBookings.Columns["Preis"].Width = 100;
                }

                // Zimmer-Filter Dropdown füllen
                UpdateRoomFilter();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler beim Laden:\n\n{ex.Message}", "Fehler",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (btnRefresh != null)
                {
                    btnRefresh.Enabled = true;
                    btnRefresh.Text = "Aktualisieren";
                }
            }
        }

        private void UpdateRoomFilter()
        {
            if (cboFilterRoom == null || allBookings == null) return;

            var rooms = allBookings.Select(b => b.Zimmer).Distinct().OrderBy(r => r).ToList();

            cboFilterRoom.Items.Clear();
            cboFilterRoom.Items.Add("Alle");
            foreach (var room in rooms)
            {
                cboFilterRoom.Items.Add(room);
            }
            cboFilterRoom.SelectedIndex = 0;
        }

        private void EnhanceBookingListForm()
        {
            panelTop.Height = 120;

            var searchPanel = new Panel
            {
                Dock = DockStyle.Bottom, 
                Height = 40,
                Padding = new Padding(10, 5, 10, 5)
            };

            var lblSearch = new Label { Text = "Suche:", Location = new Point(10, 10), AutoSize = true };

            txtSearch = new TextBox { Location = new Point(70, 8), Width = 200 };
            txtSearch.TextChanged += TxtSearch_TextChanged;

            var lblFilter = new Label { Text = "Zimmer:", Location = new Point(280, 10), AutoSize = true };

            cboFilterRoom = new ComboBox
            {
                Location = new Point(350, 8),
                Width = 100,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cboFilterRoom.Items.Add("Alle");
            cboFilterRoom.SelectedIndex = 0;
            cboFilterRoom.SelectedIndexChanged += CboFilterRoom_SelectedIndexChanged;

            btnExport = new Button { Text = "Export", Location = new Point(455, 6), Size = new Size(80, 30) };
            btnExport.Click += BtnExport_Click;

            searchPanel.Controls.AddRange(new Control[] { lblSearch, txtSearch, lblFilter, cboFilterRoom, btnExport });

            panelTop.Controls.Add(searchPanel);
        }


        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void CboFilterRoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void ApplyFilters()
        {
            if (allBookings == null || bindingSource == null) return;

            try
            {
                var searchText = txtSearch?.Text?.ToLower() ?? "";
                var selectedRoom = cboFilterRoom?.SelectedItem?.ToString() ?? "Alle";

                // Filter anwenden
                var filtered = allBookings.AsEnumerable();

                // Suchtext-Filter
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    filtered = filtered.Where(b =>
                        (b.BuchungsID?.ToLower().Contains(searchText) ?? false) ||
                        (b.Gast?.ToLower().Contains(searchText) ?? false) ||
                        (b.Zimmer?.ToLower().Contains(searchText) ?? false) ||
                        (b.CheckIn?.ToLower().Contains(searchText) ?? false) ||
                        (b.CheckOut?.ToLower().Contains(searchText) ?? false) ||
                        (b.Preis?.ToLower().Contains(searchText) ?? false)
                    );
                }

                // Zimmer-Filter
                if (selectedRoom != "Alle")
                {
                    filtered = filtered.Where(b => b.Zimmer == selectedRoom);
                }

                var result = filtered.ToList();

                // BindingSource aktualisieren
                bindingSource.DataSource = result;
                bindingSource.ResetBindings(false);

                if (lblCount != null)
                    lblCount.Text = $"{result.Count} von {allBookings.Count} Buchung(en)";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Filter Error: {ex.Message}");
            }
        }



        private void BtnExport_Click(object sender, EventArgs e)
        {
            try
            {
                var saveDialog = new SaveFileDialog
                {
                    Filter = "CSV Datei|*.csv",
                    Title = "Buchungen exportieren",
                    FileName = $"Buchungen_{DateTime.Now:yyyyMMdd}.csv"
                };

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    var csv = new System.Text.StringBuilder();

                    // Header
                    csv.AppendLine("Buchungs-ID;Gast;Zimmer;Check-in;Check-out;Nächte;Preis");

                    // Daten aus BindingSource
                    var items = bindingSource.List as List<BookingRow>;
                    if (items != null)
                    {
                        foreach (var item in items)
                        {
                            csv.AppendLine($"{item.BuchungsID};{item.Gast};{item.Zimmer};{item.CheckIn};{item.CheckOut};{item.Nächte};{item.Preis}");
                        }
                    }

                    System.IO.File.WriteAllText(saveDialog.FileName, csv.ToString(),
                        System.Text.Encoding.UTF8);

                    MessageBox.Show($"✓ Export erfolgreich!\n\n{saveDialog.FileName}",
                        "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler beim Export:\n\n{ex.Message}",
                    "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadBookings();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}