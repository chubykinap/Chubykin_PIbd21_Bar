using BarService.Interfaces;
using BarService.ViewModel;
using System;
using System.Collections.Generic;
using System.Web.UI;
using Unity;

namespace BarWebView
{
    public partial class FormServiceElement : System.Web.UI.Page
    {
        private readonly IElement service = UnityConfig.Container.Resolve<IElement>();

        private ElementRequirementsViewModel model;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                List<ElementViewModel> list = service.GetList();
                if (list != null)
                {
                    if (!Page.IsPostBack)
                    {
                        DropDownListElement.DataSource = list;
                        DropDownListElement.DataValueField = "ID";
                        DropDownListElement.DataTextField = "ElementName";
                        Page.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
            }
            if (Session["SEId"] != null)
            {
                DropDownListElement.Enabled = false;
                DropDownListElement.SelectedValue = (string)Session["SEElementId"];
                TextBoxCount.Text = (string)Session["SECount"];
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
            try
            {
                if (Session["SEId"] == null)
                {
                    model = new ElementRequirementsViewModel
                    {
                        ElementID = Convert.ToInt32(DropDownListElement.SelectedValue),
                        ElementName = DropDownListElement.SelectedItem.Text,
                        Count = Convert.ToInt32(TextBoxCount.Text)
                    };
                    Session["SEId"] = model.ID;
                    Session["SEServiceId"] = model.CocktailID;
                    Session["SEElementId"] = model.ElementID;
                    Session["SEElementName"] = model.ElementName;
                    Session["SECount"] = model.Count;
                }
                else
                {
                    model.Count = Convert.ToInt32(TextBoxCount.Text);
                    Session["SEId"] = model.ID;
                    Session["SEServiceId"] = model.CocktailID;
                    Session["SEElementId"] = model.ElementID;
                    Session["SEElementName"] = model.ElementName;
                    Session["SECount"] = model.Count;
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Сохранение прошло успешно');</script>");
                Server.Transfer("CocktailForm.aspx");
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            Server.Transfer("CocktailForm.aspx");
        }
    }
}