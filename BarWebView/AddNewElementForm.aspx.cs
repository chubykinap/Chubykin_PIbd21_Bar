using BarService.BindingModels;
using BarService.Interfaces;
using BarService.ServicesList;
using BarService.ViewModel;
using System;
using System.Web.UI;

namespace PlumbingRepairWebView
{
    public partial class FormElement : System.Web.UI.Page
    {
        public int Id { set { id = value; } }

        private readonly IElement service=new ElementList();

        private int id;

        private string name;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Int32.TryParse((string)Session["id"], out id))
            {
                try
                {
                    ElementViewModel view = service.GetElement(id);
                    if (view != null)
                    {
                        name = view.ElementName;
                        service.UpdElement(new ElementBindModel
                        {
                            ID = id,
                            ElementName = ""
                        });
                        if (!string.IsNullOrEmpty(name) && string.IsNullOrEmpty(textBoxName.Text))
                        {
                            textBoxName.Text = name;
                        }
                        service.UpdElement(new ElementBindModel
                        {
                            ID = id,
                            ElementName = name
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
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Заполните ФИО');</script>");
                return;
            }
            try
            {
                if (Int32.TryParse((string)Session["id"], out id))
                {
                    service.UpdElement(new ElementBindModel
                    {
                        ID = id,
                        ElementName = textBoxName.Text
                    });
                }
                else
                {
                    service.AddElement(new ElementBindModel
                    {
                        ElementName = textBoxName.Text
                    });
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
                Server.Transfer("ElementsForm.aspx");
            }
            Session["id"] = null;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Сохранение прошло успешно');</script>");
            Server.Transfer("ElementsForm.aspx");
        }

        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            Session["id"] = null;
            Server.Transfer("ElementsForm.aspx");
        }
    }
}