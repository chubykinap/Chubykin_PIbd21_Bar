using BarService.BDImplementation;
using BarService.BindingModels;
using BarService.Interfaces;
using System;
using System.Web.UI;
using Unity;

namespace BarWebView
{
    public partial class SavePriceListWebForm : System.Web.UI.Page
    {
        readonly IReportService reportService = UnityConfig.Container.Resolve<ReportBD>();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            string path = "D:\\PriceList.docx";
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("Content-Disposition", "filename=PriceList.docx");
            Response.ContentType = "application/vnd.ms-word";
            try
            {
                reportService.SaveCocktailPrice(new ReportBindModel
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