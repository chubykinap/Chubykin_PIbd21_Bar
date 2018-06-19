using BarService.BindingModels;
using BarService.Interfaces;
using BarService.ViewModel;
using System;
using System.Web.UI;
using Unity;

namespace BarWebView
{
    public partial class FormStorage : System.Web.UI.Page
    {
        private readonly IStorage service = UnityConfig.Container.Resolve<IStorage>();

        private int id;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Int32.TryParse((string)Session["id"], out id))
            {
                try
                {
                    StorageViewModel view = service.GetElement(id);
                    if (view != null)
                    {
                        if (!Page.IsPostBack)
                        {
                            textBoxName.Text = view.StorageName;
                        }
                        dataGridView.DataSource = view.StorageElements;
                        dataGridView.DataBind();
                    }
                    Page.DataBind();
                }
                catch (Exception ex)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
                }
            }
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Заполните название');</script>");
                return;
            }
            try
            {
                if (Int32.TryParse((string)Session["id"], out id))
                {
                    service.UpdElement(new StorageBindModel
                    {
                        ID = id,
                        StorageName = textBoxName.Text
                    });
                }
                else
                {
                    service.AddElement(new StorageBindModel
                    {
                        StorageName = textBoxName.Text
                    });
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
                Server.Transfer("StoragesForm.aspx");
            }
            Session["id"] = null;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Сохранение прошло успешно');</script>");
            Server.Transfer("StoragesForm.aspx");
        }

        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            Session["id"] = null;
            Server.Transfer("StorageForms.aspx");
        }
    }
}