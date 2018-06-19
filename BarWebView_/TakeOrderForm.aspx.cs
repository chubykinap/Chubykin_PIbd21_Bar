using BarService.BindingModels;
using BarService.Interfaces;
using BarService.ViewModel;
using System;
using System.Collections.Generic;
using System.Web.UI;
using Unity;

namespace BarWebView
{
    public partial class TakeIndentInWork : System.Web.UI.Page
    {
        private readonly IExecutor serviceP = UnityConfig.Container.Resolve<IExecutor>();

        private readonly IMainService serviceM = UnityConfig.Container.Resolve<IMainService>();

        private int id;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    if (!Int32.TryParse((string)Session["id"], out id))
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Не указан заказ');</script>");
                        Server.Transfer("MainForm.aspx");
                    }
                    List<ExecutorViewModel> listI = serviceP.GetList();
                    if (listI != null)
                    {
                        DropDownListPerformer.DataSource = listI;
                        DropDownListPerformer.DataBind();
                        DropDownListPerformer.DataTextField = "ExecutorFIO";
                        DropDownListPerformer.DataValueField = "ID";
                        DropDownListPerformer.SelectedIndex = -1;
                    }
                    Page.DataBind();
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            if (DropDownListPerformer.SelectedValue == null)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Выберите исполнителя');</script>");
                return;
            }
            try
            {
                serviceM.TakeOrderInWork(new OrderBindModel
                {
                    ID = id + 1,
                    ExecutorID = Convert.ToInt32(DropDownListPerformer.SelectedValue)
                });
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Сохранение прошло успешно');</script>");
                Session["id"] = null;
                Server.Transfer("MainForm.aspx");
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            Session["id"] = null;
            Server.Transfer("MainForm.aspx");
        }
    }
}