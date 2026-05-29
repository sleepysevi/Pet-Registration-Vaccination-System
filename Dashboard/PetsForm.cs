using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MiniFrames;
using AlagaTrack;

namespace AlagaTrackFrontEnd
{
    public partial class PetsForm : Form
    {
        private readonly PetManager _petManager = new PetManager();
        private List<PetRecord> _pets = new List<PetRecord>();

        public PetsForm()
        {
            InitializeComponent();
            btnAdd.Click += BtnAdd_Click;
            btnRefresh.Click += (_, _) => LoadPets();
            btnDelete.Click += BtnDelete_Click;
            txtSearch.TextChanged += TxtSearch_TextChanged;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        protected override void OnShown(System.EventArgs e)
        {
            base.OnShown(e);
            if (UiRoundHelper.IsInDesignMode(this)) return;
            UiRoundHelper.ApplyDataViewChrome(this);

            // Refresh summary cards so shadow paint renders after layout.
            card1.Invalidate();
            card2.Invalidate();
            card3.Invalidate();
            LoadPets();
        }

        private void card1_Paint(object sender, PaintEventArgs e)
        {
            UiRoundHelper.DrawCardShadow(sender as Panel, e);
        }

        private void card2_Paint(object sender, PaintEventArgs e)
        {
            UiRoundHelper.DrawCardShadow(sender as Panel, e);
        }

        private void card3_Paint(object sender, PaintEventArgs e)
        {
            UiRoundHelper.DrawCardShadow(sender as Panel, e);
        }

        private void label1_Click(object sender, System.EventArgs e)
        {

        }

        private void label4_Click(object sender, System.EventArgs e)
        {

        }

        private void label5_Click(object sender, System.EventArgs e)
        {

        }

        private void label9_Click(object sender, System.EventArgs e)
        {

        }

        private void label8_Click(object sender, System.EventArgs e)
        {

        }

        private void BtnAdd_Click(object sender, System.EventArgs e)
        {
            using (var dialog = new PetForm())
            {
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    var pet = new PetRecord
                    {
                        PetName = dialog.PetNameValue,
                        Species = dialog.SpeciesValue,
                        Breed = dialog.BreedValue,
                        Color = dialog.ColorValue,
                        Age = dialog.AgeValue,
                        OwnerName = dialog.OwnerNameValue,
                        OwnerContact = dialog.OwnerContactValue,
                        PetsPhoto = dialog.PhotoPathValue
                    };

                    if (_petManager.AddPet(pet, out var newId))
                    {
                        var qrForm = new QrForm($"PET-{newId}", pet.PetName, pet.OwnerContact, pet.OwnerName);
                        qrForm.ShowDialog(this);
                        LoadPets();
                    }
                }
            }
        }

        private void BtnDelete_Click(object? sender, EventArgs e)
        {
            if (grid.CurrentRow == null || grid.CurrentRow.Index < 0 || grid.CurrentRow.Index >= _pets.Count)
            {
                MessageBox.Show("Select a pet to delete.", "Delete Pet", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var pet = _pets[grid.CurrentRow.Index];
            var confirm = MessageBox.Show($"Delete pet '{pet.PetName}'?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes) return;

            if (_petManager.DeletePet(pet.PetID))
            {
                LoadPets();
            }
        }

        private void TxtSearch_TextChanged(object? sender, EventArgs e)
        {
            var search = txtSearch.Text?.Trim() ?? string.Empty;
            if (search.Equals("Search pets...", StringComparison.OrdinalIgnoreCase))
            {
                search = string.Empty;
            }

            LoadPets(search);
        }

        private void LoadPets(string? search = null)
        {
            _pets = _petManager.GetPets(search);
            grid.Rows.Clear();

            foreach (var pet in _pets)
            {
                var status = string.IsNullOrWhiteSpace(pet.Species) ? "Unknown" : "Active";
                grid.Rows.Add(
                    pet.PetID,
                    string.IsNullOrWhiteSpace(pet.PetsPhoto) ? "No Photo" : "Uploaded",
                    pet.PetName,
                    pet.OwnerName,
                    pet.Breed,
                    pet.Age,
                    status,
                    "Double-click to edit");
            }

            var total = _pets.Count;
            var unvaccinated = _pets.Count(p => string.IsNullOrWhiteSpace(p.Species));
            var due = 0;
            label2.Text = total.ToString("00");
            label5.Text = unvaccinated.ToString("00");
            label8.Text = due.ToString("00");
        }
    }
}

