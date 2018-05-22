using BarService.ViewModel;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BarView
{
    public partial class AddElementForm : Form
    {
        private ElementRequirementsViewModel model;
        public ElementRequirementsViewModel Model { set { model = value; } get { return model; } }

        public AddElementForm()
        {
            InitializeComponent();
        }

        private void AddComponentForm_Load(object sender, EventArgs e)
        {
            try
            {
                var response = APIClient.GetRequest("api/Element/GetList");
                if (response.Result.IsSuccessStatusCode)
                {
                    comboBox1.DisplayMember = "ElementName";
                    comboBox1.ValueMember = "ID";
                    comboBox1.DataSource = APIClient.GetElement<List<ElementViewModel>>(response);
                    comboBox1.SelectedItem = null;
                }
                else
                {
                    throw new Exception(APIClient.GetError(response));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (model != null)
            {
                comboBox1.Enabled = false;
                comboBox1.SelectedValue = model.ElementID;
                Number.Text = model.Count.ToString();
            }
        }

        private void Save_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Number.Text))
            {
                MessageBox.Show("Введите количество", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBox1.SelectedValue == null)
            {
                MessageBox.Show("Выберите компонент", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (model == null)
                {
                    model = new ElementRequirementsViewModel
                    {
                        ElementID = Convert.ToInt32(comboBox1.SelectedValue),
                        ElementName = comboBox1.Text,
                        Count = Convert.ToInt32(Number.Text),
                    };
                }
                else
                {
                    model.Count = Convert.ToInt32(Number.Text);
                }
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

    }
}
