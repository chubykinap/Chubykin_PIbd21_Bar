using BarService.BindingModels;
using BarService.ViewModel;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BarView
{
    public partial class AddStorageForm : Form
    {
        public int Id { set { id = value; } }

        private int? id;

        public AddStorageForm()
        {
            InitializeComponent();
        }

        private void AddStorageForm_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    var stock = Task.Run(() => APIClient.GetRequestData<StorageViewModel>("api/Storage/Get/" + id.Value)).Result;
                    textBoxName.Text = stock.StorageName;
                    dataGridView.DataSource = stock.StorageElements;
                    dataGridView.Columns[0].Visible = false;
                    dataGridView.Columns[1].Visible = false;
                    dataGridView.Columns[2].Visible = false;
                    dataGridView.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
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
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string name = textBoxName.Text;
            Task task;
            if (id.HasValue)
            {
                task = Task.Run(() => APIClient.PostRequestData("api/Storage/UpdElement", new StorageBindModel
                {
                    ID = id.Value,
                    StorageName = name
                }));
            }
            else
            {
                task = Task.Run(() => APIClient.PostRequestData("api/Storage/AddElement", new StorageBindModel
                {
                    StorageName = name
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
            Close();
        }
    }
}
