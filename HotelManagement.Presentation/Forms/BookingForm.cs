using HotelManagement.Application.Features.Bookings;
using HotelManagement.Infrastructure.Repositories;
using MediatR;
using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Linq;

namespace HotelManagement.Presentation.Forms
{
    
    public partial class BookingForm : Form
    {
        private Label lblPricePreview;
        private Button btnShowAvailableRooms;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel statusLabel;
        private ErrorProvider errorProvider;
        public BookingForm()
        {
            InitializeComponent();
            EnhanceBookingForm();

            // Standardwerte setzen
            dtpCheckIn.Value = DateTime.Today;
            dtpCheckOut.Value = DateTime.Today.AddDays(1);
            dtpCheckIn.MinDate = DateTime.Today;
            dtpCheckOut.MinDate = DateTime.Today.AddDays(1);

            this.AcceptButton = btnSave;      
            this.CancelButton = btnCancel;    

            // Ctrl+S = Speichern
            this.KeyPreview = true;
            this.KeyDown += (s, e) =>
            {
                if (e.Control && e.KeyCode == Keys.S)
                {
                    btnSave.PerformClick();
                    e.Handled = true;
                }
            };
        }

        private void txtRoomNumber_Validating(object sender, CancelEventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(txtRoomNumber.Text, @"^\d{3}$"))
            {
                errorProvider.SetError(txtRoomNumber, "Zimmernummer muss 3-stellig sein");
                e.Cancel = true;
            }
            else
            {
                errorProvider.SetError(txtRoomNumber, "");
            }
        }
        private async void InitializeGuestAutoComplete()
        {
            var guestRepo = new ExcelGuestRepository(Globals.ThisAddIn.Application);
            var guests = await guestRepo.GetAllGuestsAsync();

            var autoComplete = new AutoCompleteStringCollection();
            autoComplete.AddRange(guests.Select(g => g.Name).ToArray());

            txtGuestName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtGuestName.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtGuestName.AutoCompleteCustomSource = autoComplete;
        }

        private void EnhanceBookingForm()
        {
            // Modern Font
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);

            // Status Bar hinzufügen
            statusStrip = new System.Windows.Forms.StatusStrip();
            statusLabel = new System.Windows.Forms.ToolStripStatusLabel("Bereit");
            statusStrip.Items.Add(statusLabel);
            this.Controls.Add(statusStrip);

            // Preis-Preview Label 
            lblPricePreview = new System.Windows.Forms.Label
            {
                Location = new System.Drawing.Point(dtpCheckOut.Right + 10, dtpCheckOut.Top),
                Size = new System.Drawing.Size(200, 45),
                ForeColor = System.Drawing.Color.Green,
                Font = new System.Drawing.Font(this.Font, System.Drawing.FontStyle.Bold)
            };
            this.Controls.Add(lblPricePreview);

            // Button "Verfügbare Zimmer anzeigen"
            btnShowAvailableRooms = new System.Windows.Forms.Button
            {
                Text = "Verfügbare Zimmer",
                Location = new System.Drawing.Point(txtRoomNumber.Right + 10, txtRoomNumber.Top),
                Size = new System.Drawing.Size(200, 35),
                FlatStyle = System.Windows.Forms.FlatStyle.Flat,
                BackColor = System.Drawing.Color.LightBlue
            };
            btnShowAvailableRooms.Click += BtnShowAvailableRooms_Click;
            this.Controls.Add(btnShowAvailableRooms);

            // Event Handler für Live-Preisberechnung
            dtpCheckIn.ValueChanged += DatePickers_ValueChanged;
            dtpCheckOut.ValueChanged += DatePickers_ValueChanged;

            // Keyboard Shortcuts
            this.KeyPreview = true;
            this.KeyDown += BookingForm_KeyDown;

            // Tooltips
            var toolTip = new System.Windows.Forms.ToolTip();
            toolTip.SetToolTip(txtGuestName, "Geben Sie den vollständigen Namen des Gastes ein");
            toolTip.SetToolTip(txtRoomNumber, "3-stellige Zimmernummer (z.B. 101, 102)");
            toolTip.SetToolTip(dtpCheckIn, "Anreisedatum");
            toolTip.SetToolTip(dtpCheckOut, "Abreisedatum (muss nach Anreise liegen)");

            // Initial berechnen
            UpdatePricePreview();
        }

        private void DatePickers_ValueChanged(object sender, EventArgs e)
        {
            UpdatePricePreview();
            statusLabel.Text = "Preis aktualisiert";
        }

        private void UpdatePricePreview()
        {
            if (dtpCheckOut.Value > dtpCheckIn.Value)
            {
                var nights = (dtpCheckOut.Value - dtpCheckIn.Value).Days;
                var totalPrice = nights * 100m;
                lblPricePreview.Text = $"{nights} Nacht(e)={totalPrice:C}";
            }
            else
            {
                lblPricePreview.Text = "";
            }
        }

        private void BookingForm_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            // Ctrl+S = Speichern
            if (e.Control && e.KeyCode == System.Windows.Forms.Keys.S)
            {
                btnSave.PerformClick();
                e.Handled = true;
            }
            // ESC = Abbrechen
            else if (e.KeyCode == System.Windows.Forms.Keys.Escape)
            {
                btnCancel.PerformClick();
                e.Handled = true;
            }
        }

        private async void BtnShowAvailableRooms_Click(object sender, EventArgs e)
        {
            try
            {
                statusLabel.Text = "Lade verfügbare Zimmer...";
                btnShowAvailableRooms.Enabled = false;

                var roomRepo = new HotelManagement.Infrastructure.Repositories.ExcelRoomRepository(
                    Globals.ThisAddIn.Application);

                var allRooms = await roomRepo.GetAllRoomsAsync();
                var availableRooms = allRooms.Where(r => r.IsAvailable).ToList();

                // Zeige verfügbare Zimmer in einer ListBox
                var form = new System.Windows.Forms.Form
                {
                    Text = "Verfügbare Zimmer",
                    Size = new System.Drawing.Size(400, 300),
                    StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                };

                var listBox = new System.Windows.Forms.ListBox
                {
                    Dock = System.Windows.Forms.DockStyle.Fill,
                    Font = new System.Drawing.Font("Consolas", 10F)
                };

                foreach (var room in availableRooms)
                {
                    listBox.Items.Add($"Zimmer {room.RoomNumber} - {room.RoomType}");
                }

                listBox.DoubleClick += (s, ev) =>
                {
                    if (listBox.SelectedItem != null)
                    {
                        var selected = listBox.SelectedItem.ToString();
                        var roomNumber = selected.Split(' ')[1]; 
                        txtRoomNumber.Text = roomNumber;
                        form.Close();
                    }
                };

                form.Controls.Add(listBox);
                form.ShowDialog();

                statusLabel.Text = $"{availableRooms.Count} Zimmer verfügbar";
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Fehler: {ex.Message}", "Fehler");
            }
            finally
            {
                btnShowAvailableRooms.Enabled = true;
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            // Validierung
            if (string.IsNullOrWhiteSpace(txtGuestName.Text))
            {
                System.Windows.Forms.MessageBox.Show("Bitte Gast-Namen eingeben!", "Validierung",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                txtGuestName.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtRoomNumber.Text))
            {
                System.Windows.Forms.MessageBox.Show("Bitte Zimmernummer eingeben!", "Validierung",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                txtRoomNumber.Focus();
                return;
            }

            if (dtpCheckIn.Value >= dtpCheckOut.Value)
            {
                System.Windows.Forms.MessageBox.Show("Check-out muss nach Check-in liegen!", "Validierung",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                dtpCheckOut.Focus();
                return;
            }

            // Visual Feedback
            btnSave.Enabled = false;
            var originalText = btnSave.Text;
            btnSave.Text = "⏳ Wird gebucht...";
            btnSave.BackColor = System.Drawing.Color.Gray;
            statusLabel.Text = "Buchung wird erstellt...";

            // Progress Bar
            var progress = new System.Windows.Forms.ProgressBar
            {
                Style = System.Windows.Forms.ProgressBarStyle.Marquee,
                MarqueeAnimationSpeed = 30,
                Dock = System.Windows.Forms.DockStyle.Bottom,
                Height = 5
            };
            this.Controls.Add(progress);
            progress.BringToFront();

            try
            {
                var mediator = Globals.ThisAddIn.ServiceProvider?.GetService(typeof(MediatR.IMediator)) as MediatR.IMediator;

                if (mediator == null)
                {
                    System.Windows.Forms.MessageBox.Show("Service nicht verfügbar. Bitte Excel neu starten.",
                        "Fehler", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return;
                }

                var command = new HotelManagement.Application.Features.Bookings.CreateBookingCommand
                {
                    GuestId = System.Guid.NewGuid().ToString(),
                    GuestName = txtGuestName.Text.Trim(),
                    RoomNumber = txtRoomNumber.Text.Trim(),
                    CheckInDate = dtpCheckIn.Value.Date,
                    CheckOutDate = dtpCheckOut.Value.Date
                };

                var result = await mediator.Send(command);

                if (result.Success)
                {
                    // Success Animation
                    statusLabel.Text = "✓ Buchung erfolgreich!";
                    statusLabel.ForeColor = System.Drawing.Color.Green;

                    // Bessere Success-Message
                    var nights = result.Booking.NumberOfNights;
                    var total = result.Booking.TotalPrice;

                    var message = $"✅ Buchung erfolgreich erstellt!\n\n" +
                                 $"━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n" +
                                 $"📋 Buchungs-ID: {result.BookingId}\n" +
                                 $"👤 Gast: {result.Booking.Guest.Name}\n" +
                                 $"🏨 Zimmer: {result.Booking.RoomNumber}\n" +
                                 $"📅 Check-in: {result.Booking.CheckInDate:dd.MM.yyyy}\n" +
                                 $"📅 Check-out: {result.Booking.CheckOutDate:dd.MM.yyyy}\n" +
                                 $"🌙 Anzahl Nächte: {nights}\n" +
                                 $"💰 Gesamtpreis: {total:C}\n" +
                                 $"━━━━━━━━━━━━━━━━━━━━━━━━━━━━";

                    System.Windows.Forms.MessageBox.Show(message, "Buchung erfolgreich",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);

                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                }
                else
                {
                    statusLabel.Text = "❌ Buchung fehlgeschlagen";
                    statusLabel.ForeColor = System.Drawing.Color.Red;

                    System.Windows.Forms.MessageBox.Show(
                        $"❌ Buchung fehlgeschlagen\n\n{result.Message}",
                        "Fehler",
                        System.Windows.Forms.MessageBoxButtons.OK,
                        System.Windows.Forms.MessageBoxIcon.Error);
                }
            }
            catch (System.Exception ex)
            {
                statusLabel.Text = "❌ Fehler aufgetreten";
                statusLabel.ForeColor = System.Drawing.Color.Red;

                System.Windows.Forms.MessageBox.Show(
                    $"❌ Unerwarteter Fehler:\n\n{ex.Message}",
                    "Fehler",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                // Cleanup
                btnSave.Enabled = true;
                btnSave.Text = originalText;
                btnSave.BackColor = System.Drawing.SystemColors.Control;
                this.Controls.Remove(progress);

                if (statusLabel.ForeColor != System.Drawing.Color.Green &&
                    statusLabel.ForeColor != System.Drawing.Color.Red)
                {
                    statusLabel.Text = "Bereit";
                    statusLabel.ForeColor = System.Drawing.SystemColors.ControlText;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
