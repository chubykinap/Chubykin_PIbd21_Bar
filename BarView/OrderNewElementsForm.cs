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
    public partial class OrderNewElementsForm : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IStorage serviceS;

        private readonly IElement serviceC;

        private readonly IMainService serviceM;

        public OrderNewElementsForm(IStorage serviceS, IElement serviceC, IMainService serviceM)
        {
            InitializeComponent();
            this.serviceS = serviceS;
            this.serviceC = serviceC;
            this.serviceM = serviceM;
        }

        private void OrderNewElementsForm_Load(object sender, EventArgs e)
        {
            try
            {
                List<ElementViewModel> listC = serviceC.GetList();
                if (listC != null)
                {
                    ElementTB.DisplayMember = "ElementName";
                    ElementTB.ValueMember = "ID";
                    ElementTB.DataSource = listC;
                    ElementTB.SelectedItem = null;
                }
                List<StorageViewModel> listS = serviceS.GetList();
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
                serviceM.PutElementOnStorage(new ElementStorageBindModel
                {
                    ElementID = Convert.ToInt32(ElementTB.SelectedValue),
                    StorageID = Convert.ToInt32(StorageTB.SelectedValue),
                    Count = Convert.ToInt32(Count.Text)
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
    }
}
