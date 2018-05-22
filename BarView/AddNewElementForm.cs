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
                    var response = APIClient.GetRequest("api/Element/Get/" + id.Value);
                    if (response.Result.IsSuccessStatusCode)
                    {
                        var component = APIClient.GetElement<ElementViewModel>(response);
                        NameTextBox.Text = component.ElementName;
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
            if (string.IsNullOrEmpty(NameTextBox.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                Task<HttpResponseMessage> response;
                if (id.HasValue)
                {
                    response = APIClient.PostRequest("api/Element/UpdElement", new ElementBindModel
                    {
                        ID = id.Value,
                        ElementName = NameTextBox.Text
                    });
                }
                else
                {
                    response = APIClient.PostRequest("api/Element/UpdElement", new ElementBindModel
                    {
                        ElementName = NameTextBox.Text
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
