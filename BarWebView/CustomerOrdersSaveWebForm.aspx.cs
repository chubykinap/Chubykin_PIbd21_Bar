using BarService.BDImplementation;
using BarService.BindingModels;
using BarService.Interfaces;
using System;
using System.Web.UI;
using Unity;

namespace BarWebView
{
    public partial class CustomerOrdersSaveWebForm : System.Web.UI.Page
    {
        readonly IReportService reportService = UnityConfig.Container.Resolve<ReportBD>();

        protected void Page_Load(object sender, EventArgs e)
        {
            string path = "D:\\Orders.pdf";
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("Content-Disposition", "filename=Orders.pdf");
            Response.ContentType = "text/vnd.ms-word";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            try
            {
                reportService.SaveCustomerOrders(new ReportBindModel
                {
                    FileName = path,
                    DateFrom = DateTime.Parse(Session["DateFrom"].ToString()),
                    DateTo = DateTime.Parse(Session["DateTo"].ToString())
                });
                Response.WriteFile(path);
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ScriptAllert", "<script>alert('" + ex.Message + "');</script>");
            }
            Response.End();
        }
    }
}