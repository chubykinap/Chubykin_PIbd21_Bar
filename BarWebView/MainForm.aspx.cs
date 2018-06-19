using BarService.Interfaces;
using BarService.ServicesList;
using BarService.ViewModel;
using System;
using System.Collections.Generic;
using System.Web.UI;

namespace PlumbingRepairWebView
{
    public partial class FormMain : System.Web.UI.Page
    {
        private readonly IMainService service = new MainList();

        List<OrderViewModel> list;

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                list = service.GetList();
                dataGridView1.Columns[0].Visible = false;
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void ButtonCreate_Click(object sender, EventArgs e)
        {
            Server.Transfer("CreateOrderForm.aspx");
        }

        protected void ButtonTakeInWork_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedIndex >= 0)
            {
                string index = list[dataGridView1.SelectedIndex].ID.ToString();
                Session["id"] = index;
                Server.Transfer("TakeOrderForm.aspx");
            }
        }

        protected void ButtonReady_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedIndex >= 0)
            {
                int id = list[dataGridView1.SelectedIndex].ID;
                try
                {
                    service.FinishOrder(id);
                    LoadData();
                    Server.Transfer("MainForm.aspx");
                }
                catch (Exception ex)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
                }
            }
        }

        protected void ButtonPayed_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedIndex >= 0)
            {
                int id = list[dataGridView1.SelectedIndex].ID;
                try
                {
                    service.PayOrder(id);
                    LoadData();
                    Server.Transfer("MainForm.aspx");
                }
                catch (Exception ex)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
                }
            }
        }

        protected void ButtonUpd_Click(object sender, EventArgs e)
        {
            LoadData();
            Server.Transfer("MainForm.aspx");
        }
    }
}