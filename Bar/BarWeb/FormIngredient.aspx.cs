﻿using BarServiceDAL.BindingModels;
using BarServiceDAL.Interfaces;
using BarServiceDAL.ViewModels;
using BarServiceImplement.Implementations;
using BarServiceImplementDataBase.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unity;

namespace BarWeb
{
    public partial class FormIngredient : System.Web.UI.Page
    {
        private int id;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Int32.TryParse((string)Session["id"], out id))
            {
                    try
                    {
                    IngredientViewModel view = APIClient.GetRequest<IngredientViewModel>("api/Ingredient/Get/" + id);
                    if (view != null)
                        {
                            if (!Page.IsPostBack)
                            {
                                textBoxName.Text = view.IngredientName;
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
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Заполните ФИО');</script>");
                return;
            }
            try
            {
                if (Int32.TryParse((string)Session["id"], out id))
                {
                    APIClient.PostRequest<IngredientBindingModel, bool>("api/Ingredient/UpdElement", new IngredientBindingModel
                    {
                        Id = id,
                        IngredientName = textBoxName.Text
                    });
                }
                else
                {
                    APIClient.PostRequest<IngredientBindingModel, bool>("api/Ingredient/AddElement", new IngredientBindingModel
                    {
                        IngredientName = textBoxName.Text
                    });
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
                Server.Transfer("FormIngredients.aspx");
            }
            Session["id"] = null;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Сохранение прошло успешно');</script>");
            Server.Transfer("FormIngredients.aspx");
        }

        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            Session["id"] = null;
            Server.Transfer("FormIngredients.aspx");
        }
    }
}