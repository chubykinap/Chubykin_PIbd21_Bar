using BarService.BindingModels;
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
    public partial class AddNewElementForm : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        public int ID { set { id = value; } }

        private readonly IElement service;

        private int? id;

        public AddNewElementForm(IElement service)
        {
            InitializeComponent();
            this.service = service;
        }

        private void AddNewElementForm_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    ElementViewModel view = service.GetElement(id.Value);
                    if (view != null)
                    {
                        NameTextBox.Text = view.ElementName;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Save_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(NameTextBox.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (id.HasValue)
                {
                    service.UpdElement(new ElementBindModel
                    {
                        ID = id.Value,
                        ElementName = NameTextBox.Text
                    });
                }
                else
                {
                    service.AddElement(new ElementBindModel
                    {
                        ElementName = NameTextBox.Text
                    });
                }
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
