using BarService.BindingModels;
using BarService.Interfaces;
using BarService.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;
using Unity.Attributes;

namespace BarView
{
    public partial class CreateOrderForm : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly ICustomer serviceC;

        private readonly ICocktail serviceP;

        private readonly IMainService serviceM;

        public CreateOrderForm(ICustomer serviceC, ICocktail serviceP, IMainService serviceM)
        {
            InitializeComponent();
            this.serviceC = serviceC;
            this.serviceP = serviceP;
            this.serviceM = serviceM;
        }

        private void CreateOrderForm_Load(object sender, EventArgs e)
        {
            try
            {
                List<CustomerViewModel> listC = serviceC.GetList();
                if (listC != null)
                {
                    CustomerCB.DisplayMember = "CustomerFIO";
                    CustomerCB.ValueMember = "ID";
                    CustomerCB.DataSource = listC;
                    CustomerCB.SelectedItem = null;
                }
                List<CocktailViewModel> listP = serviceP.GetList();
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
                    CocktailViewModel product = serviceP.GetElement(id);
                    int count = Convert.ToInt32(CountTB.Text);
                    SumTB.Text = (count * product.Price).ToString();
                }
                catch (Exception ex)
                {
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
            try
            {
                serviceM.CreateOrder(new OrderBindModel
                {
                    CustomerID = Convert.ToInt32(CustomerCB.SelectedValue),
                    CocktailID = Convert.ToInt32(CocktailCB.SelectedValue),
                    Count = Convert.ToInt32(CountTB.Text),
                    Sum = Convert.ToInt32(SumTB.Text)
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
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
