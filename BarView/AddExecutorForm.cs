using BarService.BindingModels;
using BarService.ViewModel;
using System;
using System.Net.Http;
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
                    var response = APIClient.GetRequest("api/Executor/Get/" + id.Value);
                    if (response.Result.IsSuccessStatusCode)
                    {
                        ExecutorViewModel view = APIClient.GetElement<ExecutorViewModel>(response);
                        if (view != null)
                        {
                            FIOTextBox.Text = view.ExecutorFIO;
                        }
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
                Task<HttpResponseMessage> response;
                if (id.HasValue)
                {
                    response = APIClient.PostRequest("api/Executor/UpdElement", new ExecutorBindModel
                    {
                        ID = id.Value,
                        ExecutorFIO = FIOTextBox.Text
                    });
                }
                else
                {
                    response = APIClient.PostRequest("api/Executor/UpdElement", new ExecutorBindModel
                    {
                        ExecutorFIO = FIOTextBox.Text
                    });
                }
                if (response.Result.IsSuccessStatusCode)
                {
                    MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    Close();
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
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
