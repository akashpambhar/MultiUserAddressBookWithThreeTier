using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminPanel_Contact_ContactList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            #region Check Session UserID and Load Controls

            if (Session["UserID"] != null)
            {
                FillContactGridView(Convert.ToInt32(Session["UserID"].ToString()));
            }

            #endregion Check Session UserID and Load Controls
        }
    }
    private void FillContactGridView(Int32 UserID)
    {

        #region Get All Contacts By UserID

        ContactBAL balContact = new ContactBAL();
        DataTable dtContact = new DataTable();

        dtContact = balContact.SelectAllByUserID(UserID);

        gvContactList.DataSource = dtContact;
        gvContactList.DataBind();

        #endregion Get All Contacts By UserID
    }
    protected void gvContactList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        #region Handle Delete Action from GridView

        if (e.CommandName == "DeleteRecord" && e.CommandArgument != null)
        {
            DeleteRecord(Convert.ToInt32(e.CommandArgument));
        }

        #endregion Handle Delete Action from GridView
    }
    private void DeleteRecord(Int32 ContactID)
    {
        #region Delete Contact By PK

        ContactBAL balContact = new ContactBAL();

        if (balContact.Delete(ContactID))
        {
            if (Session["UserID"] != null)
            {
                FillContactGridView(Convert.ToInt32(Session["UserID"].ToString()));
            }
        }
        else
        {
            lblErrorMessage.Text = balContact.Message;
        }

        #endregion Delete Contact By PK
    }
}