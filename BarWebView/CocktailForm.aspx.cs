using BarService.BindingModels;
using BarService.Interfaces;
using BarService.ServicesList;
using BarService.ViewModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PlumbingRepairWebView
{
    public partial class FormService : System.Web.UI.Page
    {
        private readonly ICocktail service = new CocktailList();

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
                        textBoxName.Text = view.CocktailName;
                        textBoxPrice.Text = view.Price.ToString();
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
                if (service.GetList().Count == 0 || service.GetList().Last().CocktailName != null)
                {
                    productComponents = new List<ElementRequirementsViewModel>();
                    LoadData();
                }
                else
                {
                    productComponents = service.GetList().Last().ElementRequirements;
                    LoadData();
                }
            }
            if (Session["SEId"] != null)
            {
                model = new ElementRequirementsViewModel
                {
                    ID = (int)Session["SEId"],
                    CocktailID = (int)Session["SEServiceId"],
                    ElementID = (int)Session["SEElementId"],
                    ElementName = (string)Session["SEElementName"],
                    Count = (int)Session["SECount"]
                };
                if (Session["SEIs"] != null)
                {
                    productComponents[(int)Session["SEIs"]] = model;
                }
                else
                {
                    productComponents.Add(model);
                }
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
                if (service.GetList().Count == 0 || service.GetList().Last().CocktailName != null)
                {
                    service.AddElement(new CocktailBindModel
                    {
                        CocktailName = null,
                        Price = -1,
                        ElementRequirements = productComponentBM
                    });
                }
                else
                {
                    service.UpdElement(new CocktailBindModel
                    {
                        ID = service.GetList().Last().ID,
                        CocktailName = null,
                        Price = -1,
                        ElementRequirements = productComponentBM
                    });
                }

            }
            try
            {
                if (productComponents != null)
                {
                    dataGridView.DataBind();
                    dataGridView.DataSource = productComponents;
                    dataGridView.DataBind();
                    dataGridView.ShowHeaderWhenEmpty = true;
                    dataGridView.SelectedRowStyle.BackColor = Color.Silver;
                    dataGridView.Columns[1].Visible = false;
                    dataGridView.Columns[2].Visible = false;
                    dataGridView.Columns[3].Visible = false;
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
            }
            Session["SEId"] = null;
            Session["SEServiceId"] = null;
            Session["SEElementId"] = null;
            Session["SEElementName"] = null;
            Session["SECount"] = null;
            Session["SEIs"] = null;
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
                    dataGridView.Columns[1].Visible = false;
                    dataGridView.Columns[2].Visible = false;
                    dataGridView.Columns[3].Visible = false;
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
                Session["SEId"] = model.ID;
                Session["SEServiceId"] = model.CocktailID;
                Session["SEElementId"] = model.ElementID;
                Session["SEElementName"] = model.ElementName;
                Session["SECount"] = model.Count;
                Session["SEIs"] = dataGridView.SelectedIndex;
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
                service.DelElement(service.GetList().Last().ID);
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
            Session["id"] = null;
            Server.Transfer("CocktailsForm.aspx");
        }

        protected void dataGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[3].Visible = false;
        }
    }
}