using BarService.Interfaces;
using BarService.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;
using Unity.Attributes;

namespace BarView
{
    public partial class AddElementForm : Form
    {
        [Dependency]
        public new IUnityContainer container { set; get; }
        private readonly BarService.Interfaces.IElement service;
        private ElementRequirementsViewModel model;
        public ElementRequirementsViewModel Model { set { model = value; } get { return model; } }

        public AddElementForm(BarService.Interfaces.IElement service)
        {
            InitializeComponent();
            this.service = service;
        }

        private void AddComponentForm_Load(object sender, EventArgs e)
        {
            try
            {
                List<ElementViewModel> list = service.GetList();
                if (list != null)
                {
                    comboBox1.DisplayMember = "ElementName";
                    comboBox1.ValueMember = "ID";
                    comboBox1.DataSource = list;
                    comboBox1.SelectedItem = null;
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
