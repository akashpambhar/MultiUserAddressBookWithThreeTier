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

public partial class AdminPanel_Country_CountryAddEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            #region Check Session UserID and Load Controls

            if (Session["UserID"] != null)
            {
                if (Page.RouteData.Values["CountryID"] != null)
                {
                    LoadControls(Convert.ToInt32(EncryptDecrypt.Base64Decode(Page.RouteData.Values["CountryID"].ToString())));
                }
            }

            #endregion Check Session UserID and Load Controls
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        #region Server Side Validation

        String strErrorMessage = "";

        if (txtCountryName.Text.Trim() == "")
        {
            strErrorMessage += "Enter Country Name <br/>";
        }

        if (strErrorMessage != "")
        {
            lblErrorMessage.Text = strErrorMessage;
            return;
        }

        #endregion Server Side Validation

        #region Gather Information

        CountryENT entCountry = new CountryENT();

        if (txtCountryName.Text.Trim() != "")
        {
            entCountry.CountryName = txtCountryName.Text.Trim();
        }
        if (txtCountryCode.Text.Trim() != "")
        {
            entCountry.CountryCode = txtCountryCode.Text.Trim();
        }

        #endregion Gather Information

        #region Check and Perform Insert or Update Country

        CountryBAL balCountry = new CountryBAL();

        if (Page.RouteData.Values["CountryID"] != null)
        {
            entCountry.CountryID = Convert.ToInt32(EncryptDecrypt.Base64Decode(Page.RouteData.Values["CountryID"].ToString()));

            if (balCountry.Update(entCountry))
            {
                lblErrorMessage.Text = "Updated Successfully!";
            }
            else
            {
                lblErrorMessage.Text = balCountry.Message;
            }
        }
        else
        {
            if (Session["UserID"] != null)
            {
                entCountry.UserID = Convert.ToInt32(Session["UserID"].ToString().Trim());
            }

            if (balCountry.Insert(entCountry))
            {
                lblErrorMessage.Text = "Inserted Successfully!";
            }
            else
            {
                lblErrorMessage.Text = balCountry.Message;
            }
        }

        #endregion Check and Perform Insert or Update Country

        #region Clear Fields or Redirect

        if (Page.RouteData.Values["CountryID"] == null)
        {
            clearFields();
        }
        else
        {
            Response.Redirect("~/AB/AdminPanel/Country");
        }

        #endregion Clear Fields or Redirect
    }
    private void clearFields()
    {
        txtCountryName.Text = "";
        txtCountryCode.Text = "";
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/AB/AdminPanel/Country");
    }
    private void LoadControls(Int32 CountryID)
    {
        #region Get Country By PK

        CountryENT entCountry = new CountryENT();
        CountryBAL balCountry = new CountryBAL();

        entCountry = balCountry.SelectByPK(CountryID);

        #endregion Get Country By PK

        #region Set obtained values to controls

        if (!entCountry.CountryName.IsNull)
        {
            txtCountryName.Text = entCountry.CountryName.ToString();
        }
        if (!entCountry.CountryCode.IsNull)
        {
            txtCountryCode.Text = entCountry.CountryCode.ToString();
        }

        #endregion Set obtained values to controls
    }
}