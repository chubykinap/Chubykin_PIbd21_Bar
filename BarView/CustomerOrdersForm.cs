using BarService.BindingModels;
using BarService.ViewModel;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
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
            this.reportViewer1.RefreshReport();
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

                var response = APIClient.PostRequest("api/Report/GetCustomerOrders", new ReportBindModel
                {
                    DateFrom = dateTimePicker1.Value,
                    DateTo = dateTimePicker2.Value
                });
                if (response.Result.IsSuccessStatusCode)
                {
                    var dataSource = APIClient.GetElement<List<CustomerOrderViewModel>>(response);
                    ReportDataSource source = new ReportDataSource("DataSetOrders", dataSource);
                    reportViewer1.LocalReport.DataSources.Add(source);
                }
                else
                {
                    throw new Exception(APIClient.GetError(response));
                }
                reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
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
                try
                {
                    var response = APIClient.PostRequest("api/Report/SaveCustomerOrders", new ReportBindModel
                    {
                        FileName = sfd.FileName,
                        DateFrom = dateTimePicker1.Value,
                        DateTo = dateTimePicker2.Value
                    });
                    if (response.Result.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Выполнено", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
    }
}
