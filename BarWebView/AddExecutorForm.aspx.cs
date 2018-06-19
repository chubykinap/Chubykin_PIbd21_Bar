using BarService.BindingModels;
using BarService.Interfaces;
using BarService.ServicesList;
using BarService.ViewModel;
using System;
using System.Web.UI;

namespace PlumbingRepairWebView
{
    public partial class FormPerformer : System.Web.UI.Page
    {
        private readonly IExecutor service = new ExecutorList();

        private int id;

        private string name;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Int32.TryParse((string)Session["id"], out id))
            {
                try
                {
                    ExecutorViewModel view = service.GetElement(id);
                    if (view != null)
                    {
                        name = view.ExecutorFIO;
                        service.UpdElement(new ExecutorBindModel
                        {
                            ID = id,
                            ExecutorFIO = ""
                        });
                        if (!string.IsNullOrEmpty(name) && string.IsNullOrEmpty(TextBoxFIO.Text))
                        {
                            TextBoxFIO.Text = name;
                        }
                        service.UpdElement(new ExecutorBindModel
                        {
                            ID = id,
                            ExecutorFIO = name
                        });
                    }
                }
                catch (Exception ex)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
                }
            }
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxFIO.Text))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Заполните ФИО');</script>");
                return;
            }
            try
            {
                if (Int32.TryParse((string)Session["id"], out id))
                {
                    service.UpdElement(new ExecutorBindModel
                    {
                        ID = id,
                        ExecutorFIO = TextBoxFIO.Text
                    });
                }
                else
                {
                    service.AddElement(new ExecutorBindModel
                    {
                        ExecutorFIO = TextBoxFIO.Text
                    });
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
                Server.Transfer("ExecutorsForm.aspx");
            }
            Session["id"] = null;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Сохранение прошло успешно');</script>");
            Server.Transfer("ExecutorsForm.aspx");
        }

        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            Session["id"] = null;
            Server.Transfer("ExecutorsForm.aspx");
        }
    }
}