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
    public partial class AddExecutorForm : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        public int ID { set { id = value; } }

        private readonly IExecutor service;

        private int? id;

        public AddExecutorForm(IExecutor service)
        {
            InitializeComponent();
            this.service = service;
        }

        private void AddExecutorForm_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    ExecutorViewModel view = service.GetElement(id.Value);
                    if (view != null)
                    {
                        FIOTextBox.Text = view.ExecutorFIO;
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
            if (string.IsNullOrEmpty(FIOTextBox.Text))
            {
                MessageBox.Show("Заполните ФИО", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (id.HasValue)
                {
                    service.UpdElement(new ExecutorBindModel
                    {
                        ID = id.Value,
                        ExecutorFIO = FIOTextBox.Text
                    });
                }
                else
                {
                    service.AddElement(new ExecutorBindModel
                    {
                        ExecutorFIO = FIOTextBox.Text
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
