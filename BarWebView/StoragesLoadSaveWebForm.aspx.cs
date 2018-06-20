using BarService.BDImplementation;
using BarService.BindingModels;
using BarService.Interfaces;
using System;
using System.Web.UI;
using Unity;

namespace BarWebView
{
    public partial class StoragesLoadSaveWebForm : Page
    {
        readonly IReportService reportService = UnityConfig.Container.Resolve<ReportBD>();

        protected void Page_Load(object sender, EventArgs e)
        {
            string path = "D:\\Load.xls";
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("Content-Disposition", "attachment; filename=Load.xls");
            Response.ContentType = "application/vnd.ms-excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            try
            {
                reportService.SaveStoragesLoad(new ReportBindModel
                {
                    FileName = path
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