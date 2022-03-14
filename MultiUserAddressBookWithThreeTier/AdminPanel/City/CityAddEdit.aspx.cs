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

public partial class AdminPanel_City_CityAddEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            #region Check Session UserID and Load Controls

            if (Session["UserID"] != null)
            {
                CommonFillDropDown.FillCountryDropDownList(ddlCountryID, lblErrorMessage, Convert.ToInt32(Session["UserID"].ToString()));
                if (Page.RouteData.Values["CityID"] != null)
                {
                    LoadControls(Convert.ToInt32(EncryptDecrypt.Base64Decode(Page.RouteData.Values["CityID"].ToString())));
                    CommonFillDropDown.FillStateDropDownList(ddlStateID, lblErrorMessage, Convert.ToInt32(Session["UserID"].ToString()), Convert.ToInt32(ddlCountryID.SelectedValue));
                }
            }

            #endregion Check Session UserID and Load Controls
        }
    }
    protected void ddlCountryID_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Session["UserID"] != null)
        {
            CommonFillDropDown.FillStateDropDownList(ddlStateID, lblErrorMessage, Convert.ToInt32(Session["UserID"].ToString()), Convert.ToInt32(ddlCountryID.SelectedValue));
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        #region Server Side Validation

        String strErrorMessage = "";

        if (txtCityName.Text.Trim() == "")
        {
            strErrorMessage += "Enter City Name<br/>";
        }
        if (ddlCountryID.SelectedIndex == 0)
        {
            strErrorMessage += "Select Country<br/>";
        }
        if (ddlStateID.SelectedIndex == 0)
        {
            strErrorMessage += "Select State<br/>";
        }
        if (strErrorMessage.Trim() != "")
        {
            lblErrorMessage.Text = strErrorMessage;
            return;
        }

        #endregion Server Side Validation

        #region Gather Information

        CityENT entCity = new CityENT();

        if (txtCityName.Text.Trim() != "")
        {
            entCity.CityName = txtCityName.Text.Trim();
        }
        if (txtPincode.Text.Trim() != "")
        {
            entCity.Pincode = txtPincode.Text.Trim();
        }
        if (txtSTDCode.Text.Trim() != "")
        {
            entCity.STDCode = txtSTDCode.Text.Trim();
        }
        if (ddlStateID.SelectedIndex > 0)
        {
            entCity.StateID = Convert.ToInt32(ddlStateID.SelectedValue);
        }

        #endregion Gather Information

        #region Check and Perform Insert or Update City

        CityBAL balCity = new CityBAL();

        if (Page.RouteData.Values["CityID"] != null)
        {
            entCity.CityID = Convert.ToInt32(EncryptDecrypt.Base64Decode(Page.RouteData.Values["CityID"].ToString()));

            if (balCity.Update(entCity))
            {
                lblErrorMessage.Text = "Updated Successfully!";
            }
            else
            {
                lblErrorMessage.Text = balCity.Message;
            }

            Response.Redirect("~/AB/AdminPanel/City");
        }
        else
        {
            if (Session["UserID"] != null)
            {
                entCity.UserID = Convert.ToInt32(Session["UserID"].ToString().Trim());
            }

            if (balCity.Insert(entCity))
            {
                lblErrorMessage.Text = "Inserted Successfully!";
            }
            else
            {
                lblErrorMessage.Text = balCity.Message;
            }

            clearFields();
        }

        #endregion Check and Perform Insert or Update City
    }
    private void clearFields()
    {
        txtCityName.Text = "";
        txtPincode.Text = "";
        txtSTDCode.Text = "";
        ddlCountryID.SelectedIndex = 0;
        ddlStateID.SelectedIndex = 0;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/AB/AdminPanel/City");
    }
    private void LoadControls(Int32 CityID)
    {
        #region Get City By PK

        CityENT entCity = new CityENT();
        StateENT entState = new StateENT();

        CityBAL balCity = new CityBAL();
        StateBAL balState = new StateBAL();

        entCity = balCity.SelectByPK(CityID);

        #endregion Get City By PK

        #region Set obtained values to controls

        if (!entCity.CityName.IsNull)
        {
            txtCityName.Text = entCity.CityName.ToString();
        }
        if (!entCity.Pincode.IsNull)
        {
            txtPincode.Text = entCity.Pincode.ToString();
        }
        if (!entCity.STDCode.IsNull)
        {
            txtSTDCode.Text = entCity.STDCode.ToString();
        }
        if (!entCity.StateID.IsNull)
        {
            entState = balState.SelectByPK(entCity.StateID);
            ddlStateID.SelectedValue = entState.StateID.ToString();
        }
        if (!entState.CountryID.IsNull)
        {
            ddlCountryID.SelectedValue = entState.CountryID.ToString();
        }

        #endregion Set obtained values to controls
    }
}