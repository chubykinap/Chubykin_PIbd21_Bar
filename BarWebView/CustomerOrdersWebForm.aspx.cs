using BarService.BDImplementation;
using BarService.BindingModels;
using BarService.Interfaces;
using Microsoft.Reporting.WebForms;
using System;
using System.Web.UI;
using Unity;

namespace BarWebView
{
    public partial class CustomerOrdersWebForm : System.Web.UI.Page
    {
        private readonly IReportService service = UnityConfig.Container.Resolve<ReportBD>();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ButtonMake_Click(object sender, EventArgs e)
        {
            if (Calendar1.SelectedDate >= Calendar2.SelectedDate)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ScriptAllertDate", "<script>alert('Дата начала должна быть меньше даты окончания');</script>");
                return;
            }
            try
            {
                ReportParameter parameter = new ReportParameter("ReportParameterPeriod",
                                            "c " + Calendar1.SelectedDate.ToShortDateString() +
                                            " по " + Calendar2.SelectedDate.ToShortDateString());
                ReportViewer1.LocalReport.SetParameters(parameter);

                var dataSource = service.GetCustomerOrders(new ReportBindModel
                {
                    DateFrom = Calendar1.SelectedDate,
                    DateTo = Calendar2.SelectedDate
                });
                ReportDataSource source = new ReportDataSource("DataSetOrders", dataSource);
                ReportViewer1.LocalReport.DataSources.Add(source);
                ReportViewer1.DataBind();
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ScriptAllert", "<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void ButtonToPdf_Click(object sender, EventArgs e)
        {
            Session["DateFrom"] = Calendar1.SelectedDate.ToString();
            Session["DateTo"] = Calendar2.SelectedDate.ToString();
            Server.Transfer("CustomerOrdersSaveWebForm.aspx");
        }

        protected void ButtonBack_Click(object sender, EventArgs e)
        {
            Server.Transfer("MainForm.aspx");
        }
    }
}