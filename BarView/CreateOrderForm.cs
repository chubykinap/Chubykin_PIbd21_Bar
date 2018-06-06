using BarService.BindingModels;
using BarService.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BarView
{
    public partial class CreateOrderForm : Form
    {
        public CreateOrderForm()
        {
            InitializeComponent();
        }

        private void CreateOrderForm_Load(object sender, EventArgs e)
        {
            try
            {
                List<CustomerViewModel> listC = Task.Run(() => APIClient.GetRequestData<List<CustomerViewModel>>("api/Customer/GetList")).Result;
                if (listC != null)
                {
                    CustomerCB.DisplayMember = "CustomerFIO";
                    CustomerCB.ValueMember = "ID";
                    CustomerCB.DataSource = listC;
                    CustomerCB.SelectedItem = null;
                }
                List<CocktailViewModel> listP = Task.Run(() => APIClient.GetRequestData<List<CocktailViewModel>>("api/Cocktail/GetList")).Result;
                if (listP != null)
                {
                    CocktailCB.DisplayMember = "CocktailName";
                    CocktailCB.ValueMember = "ID";
                    CocktailCB.DataSource = listP;
                    CocktailCB.SelectedItem = null;
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

        private void Calculate()
        {
            if (CocktailCB.SelectedValue != null && !string.IsNullOrEmpty(CountTB.Text))
            {
                try
                {
                    int id = Convert.ToInt32(CocktailCB.SelectedValue);
                    CocktailViewModel product = Task.Run(() => APIClient.GetRequestData<CocktailViewModel>("api/Cocktail/Get/" + id)).Result;
                    int count = Convert.ToInt32(CountTB.Text);
                    SumTB.Text = (count * (int)product.Price).ToString();
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
            if (string.IsNullOrEmpty(CountTB.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (CustomerCB.SelectedValue == null)
            {
                MessageBox.Show("Выберите клиента", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (CocktailCB.SelectedValue == null)
            {
                MessageBox.Show("Выберите изделие", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int clientId = Convert.ToInt32(CustomerCB.SelectedValue);
            int productId = Convert.ToInt32(CocktailCB.SelectedValue);
            int count = Convert.ToInt32(CountTB.Text);
            int sum = Convert.ToInt32(SumTB.Text);
            Task task = Task.Run(() => APIClient.PostRequestData("api/Main/CreateOrder", new OrderBindModel
            {
                CustomerID = clientId,
                CocktailID = productId,
                Count = count,
                Sum = sum
            }));
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

        private void CountTB_TextChanged(object sender, EventArgs e)
        {
            Calculate();
        }

        private void CocktailCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            Calculate();
        }
    }
}
