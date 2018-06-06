using BarService.BindingModels;
using BarService.ViewModel;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BarView
{
    public partial class CocktailForm : Form
    {
        public int ID { set { id = value; } }
        private int? id;
        private List<ElementRequirementsViewModel> productElems;

        public CocktailForm()
        {
            InitializeComponent();
        }

        private void CocktailForm_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    var product = Task.Run(() => APIClient.GetRequestData<CocktailViewModel>("api/Cocktail/Get/" + id.Value)).Result;
                    Name.Text = product.CocktailName;
                    Price.Text = product.Price.ToString();
                    productElems = product.ElementRequirements;
                    LoadData();
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
            else
            {
                productElems = new List<ElementRequirementsViewModel>();
            }
        }

        private void LoadData()
        {
            try
            {
                if (productElems != null)
                {
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = productElems;
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns[1].Visible = false;
                    dataGridView1.Columns[2].Visible = false;
                    dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Add_Click(object sender, EventArgs e)
        {
            var form = new AddElementForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (form.Model != null)
                {
                    if (id.HasValue)
                    {
                        form.Model.CocktailID = id.Value;
                    }
                    productElems.Add(form.Model);
                }
                LoadData();
            }
        }

        private void Change_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                var form = new AddElementForm();
                form.Model = productElems[dataGridView1.SelectedRows[0].Cells[0].RowIndex];
                if (form.ShowDialog() == DialogResult.OK)
                {
                    productElems[dataGridView1.SelectedRows[0].Cells[0].RowIndex] = form.Model;
                    LoadData();
                }
            }
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        productElems.RemoveAt(dataGridView1.SelectedRows[0].Cells[0].RowIndex);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    LoadData();
                }
            }
        }

        private void Update_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void Save_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(NameTextBox.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(PriceTextBox.Text))
            {
                MessageBox.Show("Заполните цену", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (productElems == null || productElems.Count == 0)
            {
                MessageBox.Show("Заполните компоненты", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            List<ElementRequirementsBindModel> productComponentBM = new List<ElementRequirementsBindModel>();
            for (int i = 0; i < productElems.Count; ++i)
            {
                productComponentBM.Add(new ElementRequirementsBindModel
                {
                    ID = productElems[i].ID,
                    CocktailID = productElems[i].CocktailID,
                    ElementID = productElems[i].ElementID,
                    Count = productElems[i].Count
                });
            }
            string name = NameTextBox.Text;
            int price = Convert.ToInt32(PriceTextBox.Text);
            Task task;
            if (id.HasValue)
            {
                task = Task.Run(() => APIClient.PostRequestData("api/Cocktail/UpdElement", new CocktailBindModel
                {
                    ID = id.Value,
                    CocktailName = NameTextBox.Text,
                    Price = Convert.ToInt32(PriceTextBox.Text),
                    ElementRequirements = productComponentBM
                }));
            }
            else
            {
                task = Task.Run(() => APIClient.PostRequestData("api/Cocktail/UpdElement", new CocktailBindModel
                {
                    CocktailName = NameTextBox.Text,
                    Price = Convert.ToInt32(PriceTextBox.Text),
                    ElementRequirements = productComponentBM
                }));
            }
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

        private void Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
