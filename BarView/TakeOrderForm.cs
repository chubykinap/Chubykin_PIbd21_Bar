using BarService.BindingModels;
using BarService.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
                List<ExecutorViewModel> listI = Task.Run(() => APIClient.GetRequestData<List<ExecutorViewModel>>("api/Executor/GetList")).Result;
                if (listI != null)
                {
                    ExecutorCB.DisplayMember = "ExecutorFIO";
                    ExecutorCB.ValueMember = "ID";
                    ExecutorCB.DataSource = listI;
                    ExecutorCB.SelectedItem = null;
                }
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

        private void Save_Click(object sender, EventArgs e)
        {
            if (ExecutorCB.SelectedValue == null)
            {
                MessageBox.Show("Выберите исполнителя", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                int implementerId = Convert.ToInt32(ExecutorCB.SelectedValue);
                Task task = Task.Run(() => APIClient.PostRequestData("api/Main/TakeOrderInWork", new OrderBindModel
                {
                    ID = id.Value,
                    ExecutorID = Convert.ToInt32(ExecutorCB.SelectedValue)
                }));
                task.ContinueWith((prevTask) => MessageBox.Show("Заказ передан в работу. Обновите список", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information),
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
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
