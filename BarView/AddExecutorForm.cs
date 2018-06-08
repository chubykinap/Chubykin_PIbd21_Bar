using BarService.BindingModels;
using BarService.ViewModel;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BarView
{
    public partial class AddExecutorForm : Form
    {
        public int ID { set { id = value; } }

        private int? id;

        public AddExecutorForm()
        {
            InitializeComponent();
        }

        private void AddExecutorForm_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    var implementer = Task.Run(() => APIClient.GetRequestData<ExecutorViewModel>("api/Executor/Get/" + id.Value)).Result;
                    FIOTextBox.Text = implementer.ExecutorFIO;
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
            if (string.IsNullOrEmpty(FIOTextBox.Text))
            {
                MessageBox.Show("Заполните ФИО", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string fio = FIOTextBox.Text;
            Task task;
            if (id.HasValue)
            {
                task = Task.Run(() => APIClient.PostRequestData("api/Executor/UpdElement", new ExecutorBindModel
                {
                    ID = id.Value,
                    ExecutorFIO = fio
                }));
            }
            else
            {
                task = Task.Run(() => APIClient.PostRequestData("api/Executor/AddElement", new ExecutorBindModel
                {
                    ExecutorFIO = fio
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
