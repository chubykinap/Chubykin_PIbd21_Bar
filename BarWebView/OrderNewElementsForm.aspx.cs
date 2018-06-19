using BarService.BindingModels;
using BarService.Interfaces;
using BarService.ServicesList;
using BarService.ViewModel;
using System;
using System.Collections.Generic;
using System.Web.UI;

namespace PlumbingRepairWebView
{
    public partial class FormPutOnStorage : System.Web.UI.Page
    {
        private readonly IStorage serviceS = new StorageList();

        private readonly IElement serviceE = new ElementList();

        private readonly IMainService serviceM = new MainList();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                List<ElementViewModel> listE = serviceE.GetList();
                if (listE != null)
                {
                    DropDownListStorage.DataSource = listE;
                    DropDownListStorage.DataBind();
                    DropDownListStorage.DataTextField = "ElementName";
                    DropDownListStorage.DataValueField = "ID";
                }
                List<StorageViewModel> listS = serviceS.GetList();
                if (listS != null)
                {
                    DropDownListElement.DataSource = listS;
                    DropDownListElement.DataBind();
                    DropDownListElement.DataTextField = "StorageName";
                    DropDownListElement.DataValueField = "ID";
                }
                Page.DataBind();
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxCount.Text))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Заполните поле Количество');</script>");
                return;
            }
            if (DropDownListElement.SelectedValue == null)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Выберите компонент');</script>");
                return;
            }
            if (DropDownListStorage.SelectedValue == null)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Выберите склад');</script>");
                return;
            }
            try
            {
                serviceM.PutElementOnStorage(new ElementStorageBindModel
                {
                    ElementID = Convert.ToInt32(DropDownListElement.SelectedValue),
                    StorageID = Convert.ToInt32(DropDownListStorage.SelectedValue),
                    Count = Convert.ToInt32(TextBoxCount.Text)
                });
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Сохранение прошло успешно');</script>");
                Server.Transfer("MainForm.aspx");
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            Server.Transfer("MainForm.aspx");
        }

        protected void TextBoxCount_TextChanged(object sender, EventArgs e)
        {}
    }
}