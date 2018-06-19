using BarService.BindingModels;
using BarService.Interfaces;
using BarService.ViewModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unity;

namespace BarWebView
{
    public partial class FormService : System.Web.UI.Page
    {
        private readonly ICocktail service = UnityConfig.Container.Resolve<ICocktail>();

        private int id;

        private List<ElementRequirementsViewModel> productComponents;

        private ElementRequirementsViewModel model;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Int32.TryParse((string)Session["id"], out id))
            {
                try
                {
                    CocktailViewModel view = service.GetElement(id);
                    if (view != null)
                    {
                        if (!Page.IsPostBack)
                        {
                            textBoxName.Text = view.CocktailName;
                            textBoxPrice.Text = view.Price.ToString();
                        }
                        productComponents = view.ElementRequirements;
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
                }
            }
            else
            {
                productComponents = new List<ElementRequirementsViewModel>();
            }
            if (Session["SEId"] != null)
            {
                if (Session["SEIs"] != null)
                {
                    model = new ElementRequirementsViewModel
                    {
                        ID = (int)Session["SEId"],
                        CocktailID = (int)Session["SEServiceId"],
                        ElementID = (int)Session["SEElementId"],
                        ElementName = (string)Session["SEElementName"],
                        Count = (int)Session["SECount"]
                    };
                    productComponents[(int)Session["SEIs"]] = model;
                }
                else
                {
                    model = new ElementRequirementsViewModel
                    {
                        CocktailID = (int)Session["SEServiceId"],
                        ElementID = (int)Session["SEElementId"],
                        ElementName = (string)Session["SEElementName"],
                        Count = (int)Session["SECount"]
                    };
                    productComponents.Add(model);
                }
                Session["SEId"] = null;
                Session["SEServiceId"] = null;
                Session["SEElementId"] = null;
                Session["SEElementName"] = null;
                Session["SECount"] = null;
                Session["SEIs"] = null;
            }
            List<ElementRequirementsBindModel> productComponentBM = new List<ElementRequirementsBindModel>();
            for (int i = 0; i < productComponents.Count; ++i)
            {
                productComponentBM.Add(new ElementRequirementsBindModel
                {
                    ID = productComponents[i].ID,
                    CocktailID = productComponents[i].CocktailID,
                    ElementID = productComponents[i].ElementID,
                    Count = productComponents[i].Count
                });
            }
            if (productComponentBM.Count != 0)
            {
                if (Int32.TryParse((string)Session["id"], out id))
                {
                    service.UpdElement(new CocktailBindModel
                    {
                        ID = id,
                        CocktailName = textBoxName.Text,
                        Price = Convert.ToInt32(textBoxPrice.Text),
                        ElementRequirements = productComponentBM
                    });
                }
                else
                {
                    service.AddElement(new CocktailBindModel
                    {
                        CocktailName = "-0",
                        Price = 0,
                        ElementRequirements = productComponentBM
                    });
                    Session["id"] = service.GetList().Last().ID.ToString();
                    Session["Change"] = "0";
                }
            }
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                if (productComponents != null)
                {
                    dataGridView.DataBind();
                    dataGridView.DataSource = productComponents;
                    dataGridView.DataBind();
                    dataGridView.ShowHeaderWhenEmpty = true;
                    dataGridView.SelectedRowStyle.BackColor = Color.Silver;
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            Server.Transfer("AddElementForm.aspx");
        }

        protected void ButtonChange_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedIndex >= 0)
            {
                model = service.GetElement(id).ElementRequirements[dataGridView.SelectedIndex];
                Session["SEId"] = model.ID;
                Session["SEServiceId"] = model.CocktailID;
                Session["SEElementId"] = model.ElementID;
                Session["SEElementName"] = model.ElementName;
                Session["SECount"] = model.Count;
                Session["SEIs"] = dataGridView.SelectedIndex;
                Session["Change"] = "0";
                Server.Transfer("AddElementForm.aspx");
            }
        }

        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedIndex >= 0)
            {
                try
                {
                    productComponents.RemoveAt(dataGridView.SelectedIndex);
                }
                catch (Exception ex)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
                }
                LoadData();
            }
        }

        protected void ButtonUpd_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Заполните название');</script>");
                return;
            }
            if (string.IsNullOrEmpty(textBoxPrice.Text))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Заполните цену');</script>");
                return;
            }
            if (productComponents == null || productComponents.Count == 0)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Заполните компоненты');</script>");
                return;
            }
            try
            {
                List<ElementRequirementsBindModel> productComponentBM = new List<ElementRequirementsBindModel>();
                for (int i = 0; i < productComponents.Count; ++i)
                {
                    productComponentBM.Add(new ElementRequirementsBindModel
                    {
                        ID = productComponents[i].ID,
                        CocktailID = productComponents[i].CocktailID,
                        ElementID = productComponents[i].ElementID,
                        Count = productComponents[i].Count
                    });
                }
                if (Int32.TryParse((string)Session["id"], out id))
                {
                    service.UpdElement(new CocktailBindModel
                    {
                        ID = id,
                        CocktailName = textBoxName.Text,
                        Price = Convert.ToInt32(textBoxPrice.Text),
                        ElementRequirements = productComponentBM
                    });
                }
                else
                {
                    service.AddElement(new CocktailBindModel
                    {
                        CocktailName = textBoxName.Text,
                        Price = Convert.ToInt32(textBoxPrice.Text),
                        ElementRequirements = productComponentBM
                    });
                }
                Session["id"] = null;
                Session["Change"] = null;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Сохранение прошло успешно');</script>");
                Server.Transfer("CocktailsForm.aspx");
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            if (service.GetList().Count != 0 && service.GetList().Last().CocktailName == null)
            {
                service.DelElement(service.GetList().Last().ID);
            }
            if (!String.Equals(Session["Change"], null))
            {
                service.DelElement(id);
            }
            Session["id"] = null;
            Session["Change"] = null;
            Server.Transfer("CocktailsForm.aspx");
        }

        protected void dataGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }
    }
}