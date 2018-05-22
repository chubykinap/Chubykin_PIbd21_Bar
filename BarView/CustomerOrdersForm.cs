using BarService.BindingModels;
using BarService.Interfaces;
using Microsoft.Reporting.WinForms;
using System;
using System.Windows.Forms;
using Unity;
using Unity.Attributes;

namespace BarView
{
    public partial class CustomerOrdersForm : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IReportService service;

        public CustomerOrdersForm(IReportService service)
        {
            this.service = service;
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

                var dataSource = service.GetCustomerOrders(new ReportBindModel
                {
                    DateFrom = dateTimePicker1.Value,
                    DateTo = dateTimePicker2.Value
                });
                CustomerOrderViewModelBindingSource.DataSource = dataSource;
                ReportDataSource source = new ReportDataSource("DataSetOrders", dataSource);
                reportViewer1.LocalReport.DataSources.Add(source);

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
                    service.SaveCustomerOrders(new ReportBindModel
                    {
                        FileName = sfd.FileName,
                        DateFrom = dateTimePicker1.Value,
                        DateTo = dateTimePicker2.Value
                    });
                    MessageBox.Show("Выполнено", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
