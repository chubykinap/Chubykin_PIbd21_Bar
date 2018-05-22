using BarService.BindingModels;
using BarService.ViewModel;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BarView
{
    public partial class AddCustomerForm : Form
    {
        private int? id;
        public int ID { set { id = value; } }

        public AddCustomerForm()
        {
            InitializeComponent();
        }

        private void AddCustomerForm_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    var response = APIClient.GetRequest("api/Customer/Get/" + id.Value);
                    if (response.Result.IsSuccessStatusCode)
                    {
                        var Customer = APIClient.GetElement<CustomerViewModel>(response);
                        FIO.Text = Customer.CustomerFIO;
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
            if (string.IsNullOrEmpty(FIO.Text))
            {
                MessageBox.Show("Введите ФИО", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                Task<HttpResponseMessage> response;
                if (id.HasValue)
                {
                    response = APIClient.PostRequest("api/Customer/UpdElement", new CustomerBindModel
                    {
                        ID = id.Value,
                        CustomerFIO = FIO.Text
                    });
                }
                else
                {
                    response = APIClient.PostRequest("api/Customer/AddElement", new CustomerBindModel
                    {
                        CustomerFIO = FIO.Text
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
