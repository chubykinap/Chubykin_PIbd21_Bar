using BarService.BindingModels;
using BarService.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BarView
{
    public partial class OrderNewElementsForm : Form
    {
        public OrderNewElementsForm()
        {
            InitializeComponent();
        }

        private void OrderNewElementsForm_Load(object sender, EventArgs e)
        {
            try
            {
                List<ElementViewModel> listC = Task.Run(() => APIClient.GetRequestData<List<ElementViewModel>>("api/Element/GetList")).Result;
                if (listC != null)
                {
                    ElementTB.DisplayMember = "ElementName";
                    ElementTB.ValueMember = "ID";
                    ElementTB.DataSource = listC;
                    ElementTB.SelectedItem = null;
                }

                List<StorageViewModel> listS = Task.Run(() => APIClient.GetRequestData<List<StorageViewModel>>("api/Storage/GetList")).Result;
                if (listS != null)
                {
                    StorageTB.DisplayMember = "StorageName";
                    StorageTB.ValueMember = "ID";
                    StorageTB.DataSource = listS;
                    StorageTB.SelectedItem = null;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Save_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Count.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (ElementTB.SelectedValue == null)
            {
                MessageBox.Show("Выберите компонент", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (StorageTB.SelectedValue == null)
            {
                MessageBox.Show("Выберите склад", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                int componentId = Convert.ToInt32(ElementTB.SelectedValue);
                int stockId = Convert.ToInt32(StorageTB.SelectedValue);
                int count = Convert.ToInt32(Count.Text);
                Task task = Task.Run(() => APIClient.PostRequestData("api/Main/PutComponentOnStock", new ElementStorageBindModel
                {
                    ElementID = componentId,
                    StorageID = stockId,
                    Count = count
                }));
                task.ContinueWith((prevTask) => MessageBox.Show("Склад пополнен", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information),
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
