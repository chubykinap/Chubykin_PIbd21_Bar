using BarService.BindingModels;
using BarService.ViewModel;
using System;
using System.Collections.Generic;
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
                var responseC = APIClient.GetRequest("api/Element/GetList");
                if (responseC.Result.IsSuccessStatusCode)
                {
                    List<ElementViewModel> list = APIClient.GetElement<List<ElementViewModel>>(responseC);
                    if (list != null)
                    {
                        ElementTB.DisplayMember = "ElementName";
                        ElementTB.ValueMember = "ID";
                        ElementTB.DataSource = list;
                        ElementTB.SelectedItem = null;
                    }
                }
                else
                {
                    throw new Exception(APIClient.GetError(responseC));
                }
                var responseS = APIClient.GetRequest("api/Storage/GetList");
                if (responseS.Result.IsSuccessStatusCode)
                {
                    List<StorageViewModel> list = APIClient.GetElement<List<StorageViewModel>>(responseS);
                    if (list != null)
                    {
                        StorageTB.DisplayMember = "StorageName";
                        StorageTB.ValueMember = "ID";
                        StorageTB.DataSource = list;
                        StorageTB.SelectedItem = null;
                    }
                }
                else
                {
                    throw new Exception(APIClient.GetError(responseC));
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
                var response = APIClient.PostRequest("api/Main/PutElementOnStorage", new ElementStorageBindModel
                {
                    ElementID = Convert.ToInt32(ElementTB.SelectedValue),
                    StorageID = Convert.ToInt32(StorageTB.SelectedValue),
                    Count = Convert.ToInt32(Count.Text)
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
