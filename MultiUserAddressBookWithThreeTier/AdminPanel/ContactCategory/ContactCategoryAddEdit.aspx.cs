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

public partial class AdminPanel_ContactCategory_ContactCategoryAddEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            #region Check Session UserID and Load Controls

            if (Session["UserID"] != null)
            {
                if (Page.RouteData.Values["ContactCategoryID"] != null)
                {
                    LoadControls(Convert.ToInt32(EncryptDecrypt.Base64Decode(Page.RouteData.Values["ContactCategoryID"].ToString())));
                }
            }

            #endregion Check Session UserID and Load Controls
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        #region Server Side Validation

        String strErrorMessage = "";

        if (txtContactCategoryName.Text.Trim() == "")
        {
            strErrorMessage += "Enter Contact Category<br/>";
        }
        if (strErrorMessage != "")
        {
            lblErrorMessage.Text = strErrorMessage;
            return;
        }

        #endregion Server Side Validation

        #region Gather Information

        ContactCategoryENT entContactCategory = new ContactCategoryENT();

        if (txtContactCategoryName.Text.Trim() != "")
        {
            entContactCategory.ContactCategoryName = txtContactCategoryName.Text.Trim();
        }

        #endregion Gather Information

        #region Check and Perform Insert or Update ContactCategory

        ContactCategoryBAL balContactCategory = new ContactCategoryBAL();

        if (Page.RouteData.Values["ContactCategoryID"] != null)
        {
            entContactCategory.ContactCategoryID = Convert.ToInt32(EncryptDecrypt.Base64Decode(Page.RouteData.Values["ContactCategoryID"].ToString()));

            if (balContactCategory.Update(entContactCategory))
            {
                lblErrorMessage.Text = "Updated Successfully!";
            }
            else
            {
                lblErrorMessage.Text = balContactCategory.Message;
            }
        }
        else
        {
            if (Session["UserID"] != null)
            {
                entContactCategory.UserID = Convert.ToInt32(Session["UserID"].ToString().Trim());
            }

            if (balContactCategory.Insert(entContactCategory))
            {
                lblErrorMessage.Text = "Inserted Successfully!";
            }
            else
            {
                lblErrorMessage.Text = balContactCategory.Message;
            }
        }

        #endregion Check and Perform Insert or Update ContactCategory

        #region Clear Fields or Redirect

        if (Page.RouteData.Values["ContactCategoryID"] == null)
        {
            clearFields();
        }
        else
        {
            Response.Redirect("~/AB/AdminPanel/ContactCategory");
        }

        #endregion
    }
    private void clearFields()
    {
        txtContactCategoryName.Text = "";
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/AB/AdminPanel/ContactCategory");
    }
    private void LoadControls(Int32 ContactCategoryID)
    {
        #region Get ContactCategory By PK

        ContactCategoryENT entContactCategory = new ContactCategoryENT();
        ContactCategoryBAL balContactCategory = new ContactCategoryBAL();

        entContactCategory = balContactCategory.SelectByPK(ContactCategoryID);

        #endregion Get ContactCategory By PK

        #region Set obtained values to controls

        if (!entContactCategory.ContactCategoryName.IsNull)
        {
            txtContactCategoryName.Text = entContactCategory.ContactCategoryName.ToString();
        }

        #endregion Set obtained values to controls
    }
}