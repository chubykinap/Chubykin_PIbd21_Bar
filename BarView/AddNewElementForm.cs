using BarService.BindingModels;
using BarService.ViewModel;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BarView
{
    public partial class AddNewElementForm : Form
    {
        public int ID { set { id = value; } }

        private int? id;

        public AddNewElementForm()
        {
            InitializeComponent();
        }

        private void AddNewElementForm_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    var component = Task.Run(() => APIClient.GetRequestData<ElementViewModel>("api/Element/Get/" + id.Value)).Result;
                    NameTextBox.Text = component.ElementName;
                }
                catch (Exception ex)
                {
                    while (ex.InnerException != null)
                    {
                        ex = ex.InnerException;
                    }
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
            string name = NameTextBox.Text;
            Task task;
            if (id.HasValue)
            {
                task = Task.Run(() => APIClient.PostRequestData("api/Element/UpdElement", new ElementBindModel
                {
                    ID = id.Value,
                    ElementName = name
                }));
            }
            else
            {
                task = Task.Run(() => APIClient.PostRequestData("api/Element/AddElement", new ElementBindModel
                {
                    ElementName = name
                }));
            }
            task.ContinueWith((prevTask) => MessageBox.Show("Сохранение прошло успешно. Обновите список", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information),
                TaskContinuationOptions.OnlyOnRanToCompletion);
            task.ContinueWith((prevTask) =>
            {
                var ex = (Exception)prevTask.Exception;
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }, TaskContinuationOptions.OnlyOnFaulted);
            Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
