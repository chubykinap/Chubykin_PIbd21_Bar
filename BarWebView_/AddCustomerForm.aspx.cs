using BarService.BindingModels;
using BarService.Interfaces;
using BarService.ViewModel;
using System;
using System.Web.UI;
using Unity;

namespace BarWebView
{
    public partial class FormCustomer : System.Web.UI.Page
    {
        public int Id { set { id = value; } }

        private readonly ICustomer service = UnityConfig.Container.Resolve<ICustomer>();

        private int id;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Int32.TryParse((string)Session["id"], out id))
            {
                try
                {
                    CustomerViewModel view = service.GetElement(id);
                    if (view != null)
                    {
                        if (!Page.IsPostBack)
                        {
                            TextBox1.Text = view.CustomerFIO;
                        }
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
            if (string.IsNullOrEmpty(TextBox1.Text))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Заполните ФИО');</script>");
                return;
            }
            try
            {
                if (Int32.TryParse((string)Session["id"], out id))
                {
                    service.UpdElement(new CustomerBindModel
                    {
                        ID = id,
                        CustomerFIO = TextBox1.Text
                    });
                }
                else
                {
                    service.AddElement(new CustomerBindModel
                    {
                        CustomerFIO = TextBox1.Text
                    });
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
                Server.Transfer("CustomersForm.aspx");
            }
            Session["id"] = null;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Сохранение прошло успешно');</script>");
            Server.Transfer("CustomersForm.aspx");
        }

        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            Session["id"] = null;
            Server.Transfer("CustomersForm.aspx");
        }
    }
}