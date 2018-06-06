using BarService.BindingModels;
using BarService.ViewModel;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BarView
{
    public partial class CustomerOrdersForm : Form
    {
        public CustomerOrdersForm()
        {
            InitializeComponent();
        }

        private void CustomerOrdersForm_Load(object sender, System.EventArgs e)
        {
            reportViewer1.RefreshReport();
        }

        private void Make_Click(object sender, System.EventArgs e)
        {
            if (dateTimePicker1.Value.Date >= dateTimePicker2.Value.Date)
            {
                MessageBox.Show("Дата начала должна быть меньше даты окончания", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                ReportParameter parameter = new ReportParameter("ReportParameterPeriod",
                                            "c " + dateTimePicker1.Value.ToShortDateString() +
                                            " по " + dateTimePicker2.Value.ToShortDateString());
                reportViewer1.LocalReport.SetParameters(parameter);
                var dataSource = Task.Run(() => APIClient.PostRequestData<ReportBindModel, List<CustomerOrderViewModel>>("api/Report/GetCustomerOrders",
                    new ReportBindModel
                    {
                        DateFrom = dateTimePicker1.Value,
                        DateTo = dateTimePicker2.Value
                    })).Result;
                ReportDataSource source = new ReportDataSource("DataSetOrders", dataSource);
                reportViewer1.LocalReport.DataSources.Add(source);
                reportViewer1.RefreshReport();
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

        private void ToPDF_Click(object sender, System.EventArgs e)
        {
            if (dateTimePicker1.Value.Date >= dateTimePicker2.Value.Date)
            {
                MessageBox.Show("Дата начала должна быть меньше даты окончания", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "pdf|*.pdf"
            };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string fileName = sfd.FileName;
                Task task = Task.Run(() => APIClient.PostRequestData("api/Report/SaveCustomerOrders", new ReportBindModel
                {
                    FileName = fileName,
                    DateFrom = dateTimePicker1.Value,
                    DateTo = dateTimePicker2.Value
                }));

                task.ContinueWith((prevTask) => MessageBox.Show("Список заказов сохранен", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information),
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
            }
        }
    }
}
