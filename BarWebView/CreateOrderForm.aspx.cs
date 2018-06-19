using BarService.BindingModels;
using BarService.Interfaces;
using BarService.ServicesList;
using BarService.ViewModel;
using System;
using System.Collections.Generic;
using System.Web.UI;

namespace PlumbingRepairWebView
{
    public partial class FormIndent : System.Web.UI.Page
    {
        private readonly ICustomer serviceC=new CustomerList();

        private readonly ICocktail serviceS = new CocktailList();

        private readonly IMainService serviceM = new MainList();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                List<CustomerViewModel> listC = serviceC.GetList();
                if (listC != null)
                {
                    DropDownListCustomer.DataSource = listC;
                    DropDownListCustomer.DataBind();
                    DropDownListCustomer.DataTextField = "CustomerFIO";
                    DropDownListCustomer.DataValueField = "ID";
                }
                List<CocktailViewModel> listP = serviceS.GetList();
                if (listP != null)
                {
                    DropDownListService.DataSource = listP;
                    DropDownListService.DataBind();
                    DropDownListService.DataTextField = "CocktailName";
                    DropDownListService.DataValueField = "ID";
                }
                Page.DataBind();
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
            }
        }

        private void CalcSum()
        {
            
            if (DropDownListService.SelectedValue != null && !string.IsNullOrEmpty(TextBoxCount.Text))
            {
                try
                {
                    int id = Convert.ToInt32(DropDownListService.SelectedValue);
                    CocktailViewModel product = serviceS.GetElement(id);
                    int count = Convert.ToInt32(TextBoxCount.Text);
                    TextBoxSum.Text = (count * product.Price).ToString();
                }
                catch (Exception ex)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
                }
            }
        }

        protected void DropDownListService_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalcSum();
        }

        protected void TextBoxCount_TextChanged(object sender, EventArgs e)
        {
            CalcSum();
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxCount.Text))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Заполните поле Количество');</script>");
                return;
            }
            if (DropDownListCustomer.SelectedValue == null)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Выберите клиента');</script>");
                return;
            }
            if (DropDownListService.SelectedValue == null)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Выберите изделие');</script>");
                return;
            }
            try
            {
                serviceM.CreateOrder(new OrderBindModel
                {
                    CustomerID = Convert.ToInt32(DropDownListCustomer.SelectedValue),
                    CocktailID = Convert.ToInt32(DropDownListService.SelectedValue),
                    Count = Convert.ToInt32(TextBoxCount.Text),
                    Sum = Convert.ToInt32(TextBoxSum.Text)
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
    }
}