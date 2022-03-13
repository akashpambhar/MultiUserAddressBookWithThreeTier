using MultiUserAddressBook.ENT;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminPanel_State_StateAddEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            #region Check Session UserID and Load Controls

            if (Session["UserID"] != null)
            {
                CommonFillDropDown.FillCountryDropDownList(ddlCountryID, lblErrorMessage, Convert.ToInt32(Session["UserID"].ToString()));
                if (Page.RouteData.Values["StateID"] != null)
                {
                    LoadControls(Convert.ToInt32(EncryptDecrypt.Base64Decode(Page.RouteData.Values["StateID"].ToString())));
                }
            }

            #endregion Check Session UserID and Load Controls
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        #region Server Side Validation

        String strErrorMessage = "";

        if (txtStateName.Text.Trim() == "")
        {
            strErrorMessage += "Enter State Name<br/>";
        }
        if (ddlCountryID.SelectedIndex == 0)
        {
            strErrorMessage += "Select Country<br/>";
        }
        if (strErrorMessage.Trim() != "")
        {
            lblErrorMessage.Text = strErrorMessage;
            return;
        }

        #endregion Server Side Validation

        #region Gather Information

        StateENT entState = new StateENT();

        if (ddlCountryID.SelectedIndex > 0)
        {
            entState.CountryID = Convert.ToInt32(ddlCountryID.SelectedValue);
        }
        if (txtStateName.Text.Trim() != "")
        {
            entState.StateName = txtStateName.Text.Trim();
        }

        #endregion Gather Information

        #region Check and Perform Insert or Update State

        StateBAL balState = new StateBAL();

        if (Page.RouteData.Values["StateID"] != null)
        {
            entState.StateID = Convert.ToInt32(EncryptDecrypt.Base64Decode(Page.RouteData.Values["StateID"].ToString()));
            
            if (balState.Update(entState))
            {
                lblErrorMessage.Text = "Updated Successfully!";
            }
            else
            {
                lblErrorMessage.Text = balState.Message;
            }
        }
        else
        {
            if (Session["UserID"] != null)
            {
                entState.UserID = Convert.ToInt32(Session["UserID"].ToString().Trim());
            }

            if (balState.Insert(entState))
            {
                lblErrorMessage.Text = "Inserted Successfully!";
            }
            else
            {
                lblErrorMessage.Text = balState.Message;
            }
        }

        #endregion Check and Perform Insert or Update State

        #region Clear Fields or Redirect

        if (Page.RouteData.Values["StateID"] == null)
        {
            clearFields();
        }
        else
        {
            Response.Redirect("~/AB/AdminPanel/State");
        }

        #endregion Clear Fields or Redirect
    }
    private void clearFields()
    {
        ddlCountryID.SelectedIndex = 0;
        txtStateName.Text = "";
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/AB/AdminPanel/State");
    }
    private void LoadControls(Int32 StateID)
    {
        #region Get State By PK

        StateENT entState = new StateENT();
        StateBAL balState = new StateBAL();

        entState = balState.SelectByPK(StateID);

        #endregion Get State By PK

        #region Set obtained values to controls

        if (!entState.StateName.IsNull)
        {
            txtStateName.Text = entState.StateName.ToString();
        }
        if (!entState.CountryID.IsNull)
        {
            ddlCountryID.SelectedValue = entState.CountryID.ToString();
        }

        #endregion Set obtained values to controls
    }
}