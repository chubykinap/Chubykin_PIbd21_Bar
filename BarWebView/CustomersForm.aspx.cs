using BarService.Interfaces;
using BarService.ViewModel;
using System;
using System.Collections.Generic;
using System.Web.UI;
using Unity;

namespace BarWebView
{
    public partial class FormCustomers : System.Web.UI.Page
    {
        private ICustomer service = UnityConfig.Container.Resolve<ICustomer>();

        List<CustomerViewModel> list;

        protected void Page_Load(object sender, EventArgs e)
        {
            service = UnityConfig.Container.Resolve<ICustomer>();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                list = service.GetList();
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            Server.Transfer("AddCustomerForm.aspx");
        }

        protected void ButtonChange_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedIndex >= 0)
            {
                string index = list[dataGridView.SelectedIndex].ID.ToString();
                Session["id"] = index;
                Server.Transfer("AddCustomerForm.aspx");
            }
        }

        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedIndex >= 0)
            {
                int id = list[dataGridView.SelectedIndex].ID;
                try
                {
                    service.DelElement(id);
                }
                catch (Exception ex)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
                }
                LoadData();
                Server.Transfer("CustomersForm.aspx");
            }
        }

        protected void ButtonUpd_Click(object sender, EventArgs e)
        {
            LoadData();
            Server.Transfer("CustomersForm.aspx");
        }

        protected void ButtonBack_Click(object sender, EventArgs e)
        {
            Server.Transfer("MainForm.aspx");
        }
    }
}