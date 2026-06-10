using System;
using System.Drawing;
using System.Windows.Forms;
using WindowForm.Api;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace WindowForm
{
    public partial class Form1 : Form, IMainView
    {
        private readonly IApiClient _apiClient;
        private MainViewPresenter _mainViewPresenter;

        private DataGridView dgv = new DataGridView();

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IList<Arve> DataSource 
        { 
            get 
            { 
                var bs = dgv.DataSource as BindingSource;
                return bs?.DataSource as IList<Arve>;
            }
            set { dgv.DataSource = value != null ? new BindingSource(value, null) : null; } 
        }

        public void SetPresenter(MainViewPresenter presenter)
        {
            _mainViewPresenter = presenter;
        }

        public int CurrentId { get; set; }
        public int CurrentArveOmanik { get; set; }
        public int CurrentRendiAeg { get; set; }
        public int CurrentSumma { get; set; }
        public string CurrentTitle { get; set; }

        public Form1(IApiClient apiClient)
        {
            _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));

            InitializeComponent();
            SetupUI();

            this.Load += Form1_Load;
        }

        private void SetupUI()
        {
            this.Controls.Clear(); // Clears all controls including the designer DataGridView 

            this.Size = new Size(1000, 550);
            this.Text = "KooliProjekt Form Andmebaas";

            this.Controls.Add(CreateMasterDetailSection("arve_", dgv, typeof(Arve)));
        }

        private Control CreateMasterDetailSection(string endpointName, DataGridView grid, Type modelType)
        {
            TableLayoutPanel split = new TableLayoutPanel 
            { 
                Dock = DockStyle.Fill, 
                ColumnCount = 2,
                RowCount = 1
            };
            split.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            split.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            split.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            // Left side
            grid.Dock = DockStyle.Fill;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.MultiSelect = false;
            grid.ReadOnly = true;
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            split.Controls.Add(grid, 0, 0); // Put grid in column 0

            // Right side
            Panel detailPanel = new Panel { Dock = DockStyle.Fill, Padding = new Padding(20) };

            int y = 40;
            var inputs = new Dictionary<string, Control>();

            foreach(var prop in modelType.GetProperties())
            {
                Label lbl = new Label { Text = prop.Name + ":", Left = 20, Top = y + 3, Width = 100 };
                Control input;
                if (prop.PropertyType == typeof(bool))
                {
                    input = new CheckBox { Left = 130, Top = y, Width = 200 };
                }
                else
                {
                    input = new TextBox { Left = 130, Top = y, Width = 200 };
                    if (prop.Name == "Id") input.Enabled = false; // Id is usually read-only
                }

                detailPanel.Controls.Add(lbl);
                detailPanel.Controls.Add(input);
                inputs[prop.Name] = input;

                y += 40;
            }

            y += 20;

            Button btnSalvesta = new Button { Text = "Salvesta", Left = 20, Top = y, Width = 80 };
            Button btnLisaUus = new Button { Text = "Lisa uus", Left = 110, Top = y, Width = 80 };
            Button btnKustuta = new Button { Text = "Kustuta", Left = 200, Top = y, Width = 80 };

            detailPanel.Controls.Add(btnSalvesta);
            detailPanel.Controls.Add(btnLisaUus);
            detailPanel.Controls.Add(btnKustuta);

            split.Controls.Add(detailPanel, 1, 0); // Put detail panel in column 1

            // Selection Logic
            grid.SelectionChanged += (s, e) => {
                if (grid.CurrentRow != null && grid.CurrentRow.DataBoundItem != null)
                {
                    var item = grid.CurrentRow.DataBoundItem;
                    foreach(var prop in modelType.GetProperties())
                    {
                        var val = prop.GetValue(item);
                        if (prop.PropertyType == typeof(bool))
                            ((CheckBox)inputs[prop.Name]).Checked = val != null && (bool)val;
                        else
                            inputs[prop.Name].Text = val?.ToString() ?? "";
                    }
                }
            };

            // Add new logic
            btnLisaUus.Click += (s, e) => {
                grid.ClearSelection();
                foreach(var prop in modelType.GetProperties())
                {
                    if (prop.Name == "Id")
                    {
                        inputs[prop.Name].Text = "0";
                    }
                    else
                    {
                        if (prop.PropertyType == typeof(bool))
                            ((CheckBox)inputs[prop.Name]).Checked = false;
                        else
                            inputs[prop.Name].Text = "";
                    }
                }
            };

            // Save logic
            btnSalvesta.Click += async (s, e) => {
                try {
                    var item = Activator.CreateInstance(modelType);
                    foreach(var prop in modelType.GetProperties())
                    {
                        if (prop.PropertyType == typeof(bool))
                        {
                            prop.SetValue(item, ((CheckBox)inputs[prop.Name]).Checked);
                        }
                        else if (prop.PropertyType == typeof(int))
                        {
                            int.TryParse(inputs[prop.Name].Text, out int parsed);
                            prop.SetValue(item, parsed);
                        }
                        else
                        {
                            prop.SetValue(item, inputs[prop.Name].Text);
                        }
                    }

                    if (_mainViewPresenter != null)
                    {
                        var arve = (Arve)item;
                        CurrentId = arve.Id;
                        CurrentArveOmanik = arve.arve_omanik;
                        CurrentRendiAeg = arve.rendi_aeg;
                        CurrentSumma = arve.summa;
                        await _mainViewPresenter.Save();
                    }
                } catch(Exception ex) {
                    MessageBox.Show($"Viga salvestamisel: {ex.Message}");
                }
            };

            // Delete logic
            btnKustuta.Click += async (s, e) => {
                var idText = inputs["Id"].Text;
                if (int.TryParse(idText, out int idValue) && idValue > 0)
                {
                    try
                    {
                        if (_mainViewPresenter != null)
                        {
                            CurrentId = idValue;
                            await _mainViewPresenter.Delete();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Viga kustutamisel: {ex.Message}");
                    }
                }
            };

            return split;
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            if (_mainViewPresenter != null)
            {
                await _mainViewPresenter.LoadData();
            }
        }

        public bool ConfirmDelete()
        {
            var message = "Oled kindel, et soovid kustutada?";
            var answer = MessageBox.Show(message, "Kustutamine", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            return (answer == DialogResult.Yes);
        }

        public void ShowError(string message, OperationResult result)
        {
            var error = message + "\r\n";
            var apiErrors = "";
            var propertyErrors = "";

            if (result.Errors != null)
            {
                foreach (var apiError in result.Errors)
                {
                    apiErrors += apiError + "\r\n";
                }
            }

            if (result.PropertyErrors != null)
            {
                foreach(var propertyError in result.PropertyErrors)
                {
                    propertyErrors += propertyError.Key + ": " + propertyError.Value;
                }
            }

            if(!string.IsNullOrEmpty(apiErrors))
            {
                error += "\r\n" + apiErrors + "\r\n";
            }

            if(!string.IsNullOrEmpty(propertyErrors))
            {
                error += "\r\n" + propertyErrors;
            }

            error = error.Trim();

            MessageBox.Show(error, "Viga!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}

