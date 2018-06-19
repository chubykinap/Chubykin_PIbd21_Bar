using BarService.BindingModels;
using BarService.Interfaces;
using BarService.ServicesList;
using BarService.ViewModel;
using System;
using System.Web.UI;

namespace PlumbingRepairWebView
{
    public partial class FormStorage : System.Web.UI.Page
    {
        private readonly IStorage service = new StorageList();

        private int id;

        private string name;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Int32.TryParse((string)Session["id"], out id))
            {
                try
                {
                    StorageViewModel view = service.GetElement(id);
                    if (view != null)
                    {
                        name = view.StorageName;
                        dataGridView.DataSource = view.StorageElements;
                        dataGridView.DataBind();
                        dataGridView.Columns[1].Visible = false;
                        dataGridView.Columns[2].Visible = false;
                        dataGridView.Columns[3].Visible = false;
                        service.UpdElement(new StorageBindModel
                        {
                            ID = id,
                            StorageName = ""
                        });
                        if (!string.IsNullOrEmpty(name) && string.IsNullOrEmpty(textBoxName.Text))
                        {
                            textBoxName.Text = name;
                        }
                        service.UpdElement(new StorageBindModel
                        {
                            ID = id,
                            StorageName = name
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