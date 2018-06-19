using BarService.Interfaces;
using BarService.ServicesList;
using BarService.ViewModel;
using System;
using System.Collections.Generic;
using System.Web.UI;

namespace PlumbingRepairWebView
{
    public partial class FormStorages : System.Web.UI.Page
    {
        private readonly IStorage service = new StorageList();

        List<StorageViewModel> list;

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                list = service.GetList();
                dataGridView.Columns[0].Visible = false;
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            Server.Transfer("AddStorageForm.aspx");
        }

        protected void ButtonChange_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedIndex >= 0)
            {
                string index = list[dataGridView.SelectedIndex].ID.ToString();
                Session["id"] = index;
                Server.Transfer("AddStorageForm.aspx");
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
                Server.Transfer("StoragesForm.aspx");
            }
        }

        protected void ButtonUpd_Click(object sender, EventArgs e)
        {
            LoadData();
            Server.Transfer("StoragesForm.aspx");
        }

        protected void ButtonBack_Click(object sender, EventArgs e)
        {
            Server.Transfer("MainForm.aspx");
        }
    }
}