using BarService.BindingModels;
using BarService.ViewModel;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BarView
{
    public partial class TakeOrderForm : Form
    {
        public int ID { set { id = value; } }

        private int? id;

        public TakeOrderForm()
        {
            InitializeComponent();
        }

        private void TakeOrderForm_Load(object sender, EventArgs e)
        {
            try
            {
                if (!id.HasValue)
                {
                    MessageBox.Show("Не указан заказ", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                }
                var response = APIClient.GetRequest("api/Executor/GetList");
                if (response.Result.IsSuccessStatusCode)
                {
                    List<ExecutorViewModel> listI = APIClient.GetElement<List<ExecutorViewModel>>(response);
                    if (listI != null)
                    {
                        ExecutorCB.DisplayMember = "ExecutorFIO";
                        ExecutorCB.ValueMember = "ID";
                        ExecutorCB.DataSource = listI;
                        ExecutorCB.SelectedItem = null;
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

        private void Save_Click(object sender, EventArgs e)
        {
            if (ExecutorCB.SelectedValue == null)
            {
                MessageBox.Show("Выберите исполнителя", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                var response = APIClient.PostRequest("api/Main/TakeOrderInWork", new OrderBindModel
                {
                    ID = id.Value,
                    ExecutorID = Convert.ToInt32(ExecutorCB.SelectedValue)
                });
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
