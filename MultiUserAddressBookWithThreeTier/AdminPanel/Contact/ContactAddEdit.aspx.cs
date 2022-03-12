﻿using System;
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
                    LoadControls(Convert.ToInt32(Session["UserID"].ToString()));
                }
            }

            #endregion
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

        #region Local Variables

        SqlString strContactName = SqlString.Null;
        SqlString strAddress = SqlString.Null;
        SqlString strPincode = SqlString.Null;
        SqlInt32 strCityID = SqlInt32.Null;
        SqlInt32 strStateID = SqlInt32.Null;
        SqlInt32 strCountryID = SqlInt32.Null;
        SqlString strEmailAddress = SqlString.Null;
        SqlString strMobileNo = SqlString.Null;
        SqlString strFacebookID = SqlString.Null;
        SqlString strLinkedInID = SqlString.Null;

        #endregion Local Variables

        #region Gather Information

        if (txtContactName.Text.Trim() != "")
        {
            strContactName = txtContactName.Text.Trim();
        }
        if (txtAddress.Text.Trim() != "")
        {
            strAddress = txtAddress.Text.Trim();
        }
        if (txtPincode.Text.Trim() != "")
        {
            strPincode = txtPincode.Text.Trim();
        }
        if (ddlCityID.SelectedIndex > 0)
        {
            strCityID = Convert.ToInt32(ddlCityID.SelectedValue);
        }
        if (ddlStateID.SelectedIndex > 0)
        {
            strStateID = Convert.ToInt32(ddlStateID.SelectedValue);
        }
        if (ddlCountryID.SelectedIndex > 0)
        {
            strCountryID = Convert.ToInt32(ddlCountryID.SelectedValue);
        }
        if (txtEmail.Text.Trim() != "")
        {
            strEmailAddress = txtEmail.Text.Trim();
        }
        if (txtMobileNo.Text.Trim() != "")
        {
            strMobileNo = txtMobileNo.Text.Trim();
        }
        if (txtFacebookID.Text.Trim() != "")
        {
            strFacebookID = txtFacebookID.Text.Trim();
        }
        if (txtLinkedInID.Text.Trim() != "")
        {
            strLinkedInID = txtLinkedInID.Text.Trim();
        }

        #endregion Gather Information

        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
        try
        {
            #region Open Connection and Set up Command

            if (objConn.State != ConnectionState.Open)
                objConn.Open();

            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;

            #endregion

            #region Common Parameters to pass

            objCmd.Parameters.AddWithValue("@ContactName", strContactName);
            objCmd.Parameters.AddWithValue("@Address", strAddress);
            objCmd.Parameters.AddWithValue("@Pincode", strPincode);
            objCmd.Parameters.AddWithValue("@CityID", strCityID);
            objCmd.Parameters.AddWithValue("@StateID", strStateID);
            objCmd.Parameters.AddWithValue("@CountryID", strCountryID);
            objCmd.Parameters.AddWithValue("@EmailAddress", strEmailAddress);
            objCmd.Parameters.AddWithValue("@MobileNo", strMobileNo);
            objCmd.Parameters.AddWithValue("@FacebookID", strFacebookID);
            objCmd.Parameters.AddWithValue("@LinkedInID", strLinkedInID);

            #endregion

            SqlInt32 ContactID = 0;

            #region Check and Perform Insert or Update Contact

            if (Page.RouteData.Values["ContactID"] != null)
            {
                objCmd.CommandText = "PR_Contact_UpdateByPK";
                objCmd.Parameters.AddWithValue("@ContactID", Page.RouteData.Values["ContactID"]);
                ContactID = Convert.ToInt32(Page.RouteData.Values["ContactID"]);
            }
            else
            {
                objCmd.CommandText = "PR_Contact_Insert";
                if (Session["UserID"] != null)
                {
                    objCmd.Parameters.AddWithValue("@UserID", Convert.ToInt32(Session["UserID"].ToString().Trim()));
                }
                objCmd.Parameters.AddWithValue("@CreationDate", DateTime.Now);
                objCmd.Parameters.Add("@ContactID", SqlDbType.Int, 32).Direction = ParameterDirection.Output;
                ContactID = Convert.ToInt32(objCmd.Parameters["@ContactID"].Value);
            }

            objCmd.ExecuteNonQuery();

            #endregion

            #region Insert Selected ContactCategory

            foreach (ListItem liContactCategory in cblContactCategoryID.Items)
            {
                if (liContactCategory.Selected)
                {
                    try
                    {
                        SqlCommand objCmdContactCategory = objConn.CreateCommand();
                        objCmdContactCategory.CommandType = CommandType.StoredProcedure;
                        objCmdContactCategory.CommandText = "PR_ContactWiseContactCategory_Insert";
                        objCmdContactCategory.Parameters.AddWithValue("@ContactID", ContactID);
                        objCmdContactCategory.Parameters.AddWithValue("@ContactCategoryID", liContactCategory.Value);
                        objCmdContactCategory.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {

                    }
                }
                else
                {
                    try
                    {
                        SqlCommand objCmdContactCategory = objConn.CreateCommand();
                        objCmdContactCategory.CommandType = CommandType.StoredProcedure;
                        objCmdContactCategory.CommandText = "PR_ContactWiseContactCategory_DeleteByContactIDAndContactCategoryID";
                        objCmdContactCategory.Parameters.AddWithValue("@ContactID", ContactID);
                        objCmdContactCategory.Parameters.AddWithValue("@ContactCategoryID", liContactCategory.Value);
                        objCmdContactCategory.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }

            objConn.Close();

            #endregion

            lblErrorMessage.Text = "Data recorded successfully!";
        }
        catch (SqlException sqlEx)
        {
            #region Set Error Message

            if (sqlEx.Number == 2627)
            {
                lblErrorMessage.Text = "You have already created a Contact with same name and Mobile number";
                clearFields();
            }
            else
            {
                lblErrorMessage.Text = sqlEx.Message.ToString();
            }

            #endregion
        }
        finally
        {
            if (objConn.State != ConnectionState.Closed)
                objConn.Close();
        }

        #region Clear Fields or Redirect

        if (Page.RouteData.Values["ContactID"] == null)
        {
            clearFields();
        }
        else
        {
            Response.Redirect("~/AB/AdminPanel/Contact");
        }

        #endregion
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
    private void LoadControls(Int32 UserID)
    {
        string ContactID = EncryptDecrypt.Base64Decode(Page.RouteData.Values["ContactID"].ToString());

        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
        try
        {
            #region Get Contact By PK

            if (objConn.State != ConnectionState.Open)
                objConn.Open();

            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "PR_Contact_SelectByPK";

            objCmd.Parameters.AddWithValue("@ContactID", ContactID);

            SqlDataReader objSDR = objCmd.ExecuteReader();

            #endregion

            #region Set obtained values to controls

            if (objSDR.HasRows)
            {
                while (objSDR.Read())
                {
                    if (!objSDR["ContactName"].Equals(DBNull.Value))
                    {
                        txtContactName.Text = objSDR["ContactName"].ToString();
                    }
                    if (!objSDR["MobileNo"].Equals(DBNull.Value))
                    {
                        txtMobileNo.Text = objSDR["MobileNo"].ToString();
                    }
                    if (!objSDR["EmailAddress"].Equals(DBNull.Value))
                    {
                        txtEmail.Text = objSDR["EmailAddress"].ToString();
                    }
                    if (!objSDR["Address"].Equals(DBNull.Value))
                    {
                        txtAddress.Text = objSDR["Address"].ToString();
                    }
                    if (!objSDR["Pincode"].Equals(DBNull.Value))
                    {
                        txtPincode.Text = objSDR["Pincode"].ToString();
                    }
                    if (!objSDR["CountryID"].Equals(DBNull.Value))
                    {
                        ddlCountryID.SelectedValue = objSDR["CountryID"].ToString();
                    }
                    CommonFillDropDown.FillStateDropDownList(ddlStateID, lblErrorMessage, Convert.ToInt32(Session["UserID"].ToString()), Convert.ToInt32(ddlCountryID.SelectedValue));
                    if (!objSDR["StateID"].Equals(DBNull.Value))
                    {
                        ddlStateID.SelectedValue = objSDR["StateID"].ToString();
                    }
                    CommonFillDropDown.FillCityDropDownList(ddlCityID, lblErrorMessage, Convert.ToInt32(Session["UserID"].ToString()), Convert.ToInt32(ddlCountryID.SelectedValue), Convert.ToInt32(ddlStateID.SelectedValue));
                    if (!objSDR["CityID"].Equals(DBNull.Value))
                    {
                        ddlCityID.SelectedValue = objSDR["CityID"].ToString();
                    }
                    if (!objSDR["FacebookID"].Equals(DBNull.Value))
                    {
                        txtFacebookID.Text = objSDR["FacebookID"].ToString();
                    }
                    if (!objSDR["LinkedInID"].Equals(DBNull.Value))
                    {
                        txtLinkedInID.Text = objSDR["LinkedInID"].ToString();
                    }
                    break;
                }
            }

            objSDR.Close();

            #endregion

            #region Get ContactCategory By ContactID

            SqlCommand objCmdContactCategory = objConn.CreateCommand();
            objCmdContactCategory.CommandType = CommandType.StoredProcedure;
            objCmdContactCategory.CommandText = "PR_ContactWiseContactCategory_SelectByContactID";
            objCmdContactCategory.Parameters.AddWithValue("@ContactID", Page.RouteData.Values["ContactID"]);

            objSDR = objCmdContactCategory.ExecuteReader();

            if (objSDR.HasRows)
            {
                while (objSDR.Read())
                {
                    if (!objSDR["ContactCategoryID"].Equals(DBNull.Value))
                        cblContactCategoryID.Items.FindByValue(objSDR["ContactCategoryID"].ToString()).Selected = true;
                }
            }

            objConn.Close();

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
    }
}