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

public partial class AdminPanel_Contact_ContactAddEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            #region Check Session UserID and Load Controls

            if (Session["UserID"] != null)
            {
                CommonFillDropDown.FillCountryDropDownList(ddlCountryID, lblErrorMessage, Convert.ToInt32(Session["UserID"].ToString()));
                FillContactCategoryDropDownList(Convert.ToInt32(Session["UserID"].ToString()));
                if (Page.RouteData.Values["ContactID"] != null)
                {
                    LoadControls(Convert.ToInt32(EncryptDecrypt.Base64Decode(Page.RouteData.Values["ContactID"].ToString())));
                }
            }

            #endregion Check Session UserID and Load Controls
        }
    }
    private void FillContactCategoryDropDownList(Int32 UserID)
    {
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
        try
        {
            #region Set up Connection and Command

            if (objConn.State != ConnectionState.Open)
                objConn.Open();

            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;

            #endregion

            #region Get All Contact Categories By UserID

            objCmd.CommandText = "PR_ContactCategory_SelectAllByUserID";

            objCmd.Parameters.AddWithValue("@UserID", UserID);

            SqlDataReader objSDR = objCmd.ExecuteReader();

            cblContactCategoryID.DataSource = objSDR;
            cblContactCategoryID.DataTextField = "ContactCategoryName";
            cblContactCategoryID.DataValueField = "ContactCategoryID";
            cblContactCategoryID.DataBind();

            #endregion
        }
        catch (Exception ex)
        {
            lblErrorMessage.Text = ex.Message.ToString();
        }
        finally
        {
            if (objConn.State != ConnectionState.Closed)
                objConn.Close();
        }

        if (cblContactCategoryID.Items.Count == 0)
        {
            lblContactCategoryEmptyMessage.Visible = true;
            lblContactCategoryEmptyMessage.Text = "No Categories Added. Please add one";
        }
    }
    protected void ddlCountryID_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Session["UserID"] != null)
        {
            CommonFillDropDown.FillStateDropDownList(ddlStateID, lblErrorMessage, Convert.ToInt32(Session["UserID"].ToString()), Convert.ToInt32(ddlCountryID.SelectedValue));
            CommonFillDropDown.FillCityDropDownList(ddlCityID, lblErrorMessage, Convert.ToInt32(Session["UserID"].ToString()), Convert.ToInt32(ddlCountryID.SelectedValue), Convert.ToInt32(ddlStateID.SelectedValue));
        }
        if (ddlCountryID.SelectedIndex == 0)
        {
            ddlStateID.Items.Clear();
            ddlCityID.Items.Clear();
            ddlStateID.Items.Insert(0, new ListItem("Select State...", "-1"));
            ddlCityID.Items.Insert(0, new ListItem("Select City...", "-1"));
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        #region Server Side Validation

        String strErrorMessage = "";

        if (txtContactName.Text.Trim() == "")
        {
            strErrorMessage += "Enter Contact Name<br/>";
        }
        if (txtMobileNo.Text.Trim() == "")
        {
            strErrorMessage += "Enter Mobile Number<br/>";
        }
        if (strErrorMessage.Trim() != "")
        {
            lblErrorMessage.Text = strErrorMessage;
            return;
        }

        #endregion Server Side Validation

        #region Gather Information

        ContactENT entContact = new ContactENT();

        if (txtContactName.Text.Trim() != "")
        {
            entContact.ContactName = txtContactName.Text.Trim();
        }
        if (txtAddress.Text.Trim() != "")
        {
            entContact.Address = txtAddress.Text.Trim();
        }
        if (txtPincode.Text.Trim() != "")
        {
            entContact.Pincode = txtPincode.Text.Trim();
        }
        if (ddlCityID.SelectedIndex > 0)
        {
            entContact.CityID = Convert.ToInt32(ddlCityID.SelectedValue);
        }
        if (ddlStateID.SelectedIndex > 0)
        {
            entContact.StateID = Convert.ToInt32(ddlStateID.SelectedValue);
        }
        if (ddlCountryID.SelectedIndex > 0)
        {
            entContact.CountryID = Convert.ToInt32(ddlCountryID.SelectedValue);
        }
        if (txtEmail.Text.Trim() != "")
        {
            entContact.EmailAddress = txtEmail.Text.Trim();
        }
        if (txtMobileNo.Text.Trim() != "")
        {
            entContact.MobileNo = txtMobileNo.Text.Trim();
        }
        if (txtFacebookID.Text.Trim() != "")
        {
            entContact.FacebookID = txtFacebookID.Text.Trim();
        }
        if (txtLinkedInID.Text.Trim() != "")
        {
            entContact.LinkedInID = txtLinkedInID.Text.Trim();
        }

        #endregion Gather Information

        #region Check and Perform Insert or Update Contact

        ContactBAL balContact = new ContactBAL();

        if (Page.RouteData.Values["ContactID"] != null)
        {
            entContact.ContactID = Convert.ToInt32(EncryptDecrypt.Base64Decode(Page.RouteData.Values["ContactID"].ToString()));

            if (balContact.Update(entContact))
            {
                lblErrorMessage.Text = "Updated Successfully!";
            }
            else
            {
                lblErrorMessage.Text = balContact.Message;
            }
        }
        else
        {
            if (Session["UserID"] != null)
            {
                entContact.UserID = Convert.ToInt32(Session["UserID"].ToString().Trim());
            }

            if (balContact.Insert(entContact))
            {
                lblErrorMessage.Text = "Insered Successfully!";
            }
            else
            {
                lblErrorMessage.Text = balContact.Message;
            }
        }

        #endregion Check and Perform Insert or Update Contact

        #region Insert Selected ContactCategory

        ContactWiseContactCategoryBAL balContactWiseContactCategory = new ContactWiseContactCategoryBAL();

        balContactWiseContactCategory.DeleteByContactID(entContact.ContactID);

        foreach (ListItem liContactCategory in cblContactCategoryID.Items)
        {
            ContactWiseContactCategoryENT entContactWiseContactCategory = new ContactWiseContactCategoryENT();
            entContactWiseContactCategory.ContactCategoryID = Convert.ToInt32(liContactCategory.Value);
            entContactWiseContactCategory.ContactID = entContact.ContactID;

            if (liContactCategory.Selected)
            {
                balContactWiseContactCategory.Insert(entContactWiseContactCategory);
            }
        }

        #endregion Insert Selected ContactCategory

        #region Clear Fields or Redirect

        if (Page.RouteData.Values["ContactID"] == null)
        {
            clearFields();
        }
        else
        {
            Response.Redirect("~/AB/AdminPanel/Contact");
        }

        #endregion Clear Fields or Redirect
    }
    private void clearFields()
    {
        txtContactName.Text = "";
        cblContactCategoryID.ClearSelection();
        txtMobileNo.Text = "";
        txtEmail.Text = "";
        txtAddress.Text = "";
        txtPincode.Text = "";
        ddlCountryID.SelectedIndex = 0;
        ddlStateID.SelectedIndex = 0;
        ddlCityID.SelectedIndex = 0;
        txtFacebookID.Text = "";
        txtLinkedInID.Text = "";
    }
    protected void ddlStateID_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Session["UserID"] != null)
        {
            CommonFillDropDown.FillCityDropDownList(ddlCityID, lblErrorMessage, Convert.ToInt32(Session["UserID"].ToString()), Convert.ToInt32(ddlCountryID.SelectedValue), Convert.ToInt32(ddlStateID.SelectedValue));
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/AB/AdminPanel/Contact");
    }
    private void LoadControls(Int32 ContactID)
    {
        #region Get Contact By PK

        ContactENT entContact = new ContactENT();
        ContactBAL balContact = new ContactBAL();

        entContact = balContact.SelectByPK(ContactID);

        #endregion Get Contact By PK

        #region Set obtained values to controls

        if (!entContact.ContactName.IsNull)
        {
            txtContactName.Text = entContact.ContactName.ToString();
        }
        if (!entContact.MobileNo.IsNull)
        {
            txtMobileNo.Text = entContact.MobileNo.ToString();
        }
        if (!entContact.EmailAddress.IsNull)
        {
            txtEmail.Text = entContact.EmailAddress.ToString();
        }
        if (!entContact.Address.IsNull)
        {
            txtAddress.Text = entContact.Address.ToString();
        }
        if (!entContact.Pincode.IsNull)
        {
            txtPincode.Text = entContact.Pincode.ToString();
        }
        if (!entContact.CountryID.IsNull)
        {
            ddlCountryID.SelectedValue = entContact.CountryID.ToString();
        }
        CommonFillDropDown.FillStateDropDownList(ddlStateID, lblErrorMessage, Convert.ToInt32(Session["UserID"].ToString()), Convert.ToInt32(ddlCountryID.SelectedValue));
        if (!entContact.StateID.IsNull)
        {
            ddlStateID.SelectedValue = entContact.StateID.ToString();
        }
        CommonFillDropDown.FillCityDropDownList(ddlCityID, lblErrorMessage, Convert.ToInt32(Session["UserID"].ToString()), Convert.ToInt32(ddlCountryID.SelectedValue), Convert.ToInt32(ddlStateID.SelectedValue));
        if (!entContact.CityID.IsNull)
        {
            ddlCityID.SelectedValue = entContact.CityID.ToString();
        }
        if (!entContact.FacebookID.IsNull)
        {
            txtFacebookID.Text = entContact.FacebookID.ToString();
        }
        if (!entContact.LinkedInID.IsNull)
        {
            txtLinkedInID.Text = entContact.LinkedInID.ToString();
        }

        #endregion Set obtained values to controls

        #region Get ContactCategory By ContactID

        ContactWiseContactCategoryBAL balContactWiseContactCategory = new ContactWiseContactCategoryBAL();
        DataTable dtContactWiseContactCategory = new DataTable();

        dtContactWiseContactCategory = balContactWiseContactCategory.SelectAllByContactID(ContactID);

        if (dtContactWiseContactCategory != null && dtContactWiseContactCategory.Rows.Count > 0)
        {
            foreach (DataRow drContactWiseContactCategory in dtContactWiseContactCategory.Rows)
            {
                if (!drContactWiseContactCategory["ContactCategoryID"].Equals(DBNull.Value))
                    cblContactCategoryID.Items.FindByValue(drContactWiseContactCategory["ContactCategoryID"].ToString()).Selected = true;
            }
        }

        #endregion Get ContactCategory By ContactID
    }
}