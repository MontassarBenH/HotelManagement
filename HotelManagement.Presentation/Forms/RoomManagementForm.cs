using HotelManagement.Application.Models;
using HotelManagement.Infrastructure.Repositories;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace HotelManagement.Presentation.Forms
{
    public partial class RoomManagementForm : Form
    {
        private readonly ExcelRoomRepository _roomRepository;
        private Room _selectedRoom;

        public RoomManagementForm()
        {
            InitializeComponent();

            // Repository initialisieren
            var excelApp = Globals.ThisAddIn.Application;
            _roomRepository = new ExcelRoomRepository(excelApp);

            // Standardwerte setzen
            cboRoomType.SelectedIndex = 0;

            // Daten laden
            LoadRooms();
        }

        private void ApplyRowColors()
        {
            foreach (DataGridViewRow row in dgvRooms.Rows)
            {
                var verfuegbar = row.Cells["Verfügbar"]?.Value?.ToString();

                if (verfuegbar == "Ja")
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(230, 255, 230); // Hell-Grün
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(255, 230, 230); // Hell-Rot
                }
            }
        }

        private async void LoadRooms()
        {
            try
            {
                btnRefresh.Enabled = false;
                btnRefresh.Text = "Lädt...";

                var rooms = await _roomRepository.GetAllRoomsAsync();

                // Für DataGridView formatieren
                var displayData = rooms.Select(r => new
                {
                    Zimmernummer = r.RoomNumber,
                    Zimmertyp = r.RoomType,
                    Verfügbar = r.IsAvailable ? "Ja" : "Nein",
                    Status = r.IsAvailable ? "✓ Verfügbar" : "✗ Nicht verfügbar"
                }).ToList();

                dgvRooms.DataSource = displayData;
                lblCount.Text = $"{rooms.Count} Zimmer";

                ApplyRowColors();

                // Spalten formatieren
                if (dgvRooms.Columns.Count > 0)
                {
                    dgvRooms.Columns["Zimmernummer"].Width = 120;
                    dgvRooms.Columns["Zimmertyp"].Width = 150;
                    dgvRooms.Columns["Verfügbar"].Width = 100;
                    dgvRooms.Columns["Status"].Width = 150;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler beim Laden:\n{ex.Message}", "Fehler",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnRefresh.Enabled = true;
                btnRefresh.Text = "Aktualisieren";
            }
        }

        private void dgvRooms_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvRooms.SelectedRows.Count > 0)
            {
                var row = dgvRooms.SelectedRows[0];

                txtRoomNumber.Text = row.Cells["Zimmernummer"].Value?.ToString();
                cboRoomType.Text = row.Cells["Zimmertyp"].Value?.ToString();
                chkAvailable.Checked = row.Cells["Verfügbar"].Value?.ToString() == "Ja";

                // Zimmernummer nicht editierbar bei Update
                txtRoomNumber.ReadOnly = true;
                btnAdd.Enabled = false;
                btnUpdate.Enabled = true;
                btnDelete.Enabled = true;
            }
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            // Validierung
            if (string.IsNullOrWhiteSpace(txtRoomNumber.Text))
            {
                MessageBox.Show("Bitte Zimmernummer eingeben!", "Validierung",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRoomNumber.Focus();
                return;
            }

            if (cboRoomType.SelectedIndex == -1)
            {
                MessageBox.Show("Bitte Zimmertyp wählen!", "Validierung",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboRoomType.Focus();
                return;
            }

            try
            {
                btnAdd.Enabled = false;
                btnAdd.Text = "Wird hinzugefügt...";

                // Prüfe ob Zimmer bereits existiert
                var existingRoom = await _roomRepository.GetRoomByNumberAsync(txtRoomNumber.Text.Trim());
                if (existingRoom != null)
                {
                    MessageBox.Show($"Zimmer '{txtRoomNumber.Text}' existiert bereits!",
                        "Duplikat", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var newRoom = new Room
                {
                    RoomNumber = txtRoomNumber.Text.Trim(),
                    RoomType = cboRoomType.Text,
                    IsAvailable = chkAvailable.Checked
                };

                await _roomRepository.AddRoomAsync(newRoom);

                MessageBox.Show($"Zimmer '{newRoom.RoomNumber}' wurde erfolgreich hinzugefügt!",
                    "Erfolg", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ClearForm();
                LoadRooms();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler beim Hinzufügen:\n{ex.Message}", "Fehler",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnAdd.Enabled = true;
                btnAdd.Text = "Hinzufügen";
            }
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRoomNumber.Text))
            {
                MessageBox.Show("Bitte erst ein Zimmer auswählen!", "Hinweis",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                btnUpdate.Enabled = false;
                btnUpdate.Text = "Wird aktualisiert...";

                var updatedRoom = new Room
                {
                    RoomNumber = txtRoomNumber.Text.Trim(),
                    RoomType = cboRoomType.Text,
                    IsAvailable = chkAvailable.Checked
                };

                await _roomRepository.UpdateRoomAsync(updatedRoom);

                MessageBox.Show($"Zimmer '{updatedRoom.RoomNumber}' wurde erfolgreich aktualisiert!",
                    "Erfolg", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ClearForm();
                LoadRooms();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler beim Aktualisieren:\n{ex.Message}", "Fehler",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnUpdate.Enabled = true;
                btnUpdate.Text = "Aktualisieren";
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRoomNumber.Text))
            {
                MessageBox.Show("Bitte erst ein Zimmer auswählen!", "Hinweis",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var result = MessageBox.Show(
                $"Möchten Sie Zimmer '{txtRoomNumber.Text}' wirklich löschen?\n\n" +
                "ACHTUNG: Bestehende Buchungen für dieses Zimmer bleiben erhalten!",
                "Löschen bestätigen",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    btnDelete.Enabled = false;
                    btnDelete.Text = "Wird gelöscht...";

                    await _roomRepository.DeleteRoomAsync(txtRoomNumber.Text.Trim());

                    MessageBox.Show($"Zimmer '{txtRoomNumber.Text}' wurde gelöscht!",
                        "Erfolg", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ClearForm();
                    LoadRooms();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Fehler beim Löschen:\n{ex.Message}", "Fehler",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    btnDelete.Enabled = true;
                    btnDelete.Text = "Löschen";
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadRooms();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ClearForm()
        {
            txtRoomNumber.Clear();
            txtRoomNumber.ReadOnly = false;
            cboRoomType.SelectedIndex = 0;
            chkAvailable.Checked = true;

            btnAdd.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;

            dgvRooms.ClearSelection();
            txtRoomNumber.Focus();
        }
    }
}
