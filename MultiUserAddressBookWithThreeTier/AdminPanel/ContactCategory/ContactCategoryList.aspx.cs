using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminPanel_ContactCategory_ContactCategoryList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            #region Check Session UserID and Load Controls

            if (Session["UserID"] != null)
            {
                FillContactCategoryGridView(Convert.ToInt32(Session["UserID"].ToString()));
            }

            #endregion Check Session UserID and Load Controls
        }
    }
    private void FillContactCategoryGridView(Int32 UserID)
    {
        #region Get All Contact Categories By UserID

        ContactCategoryBAL balContactCategory = new ContactCategoryBAL();
        DataTable dtContactCategory = new DataTable();

        dtContactCategory = balContactCategory.SelectAllByUserID(UserID);

        if (dtContactCategory != null && dtContactCategory.Rows.Count > 0)
        {
            gvContactCategoryList.DataSource = dtContactCategory;
            gvContactCategoryList.DataBind();
        }

        #endregion Get All Contact Categories By UserID
    }
    protected void gvContactCategoryList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        #region Handle Delete Action from GridView

        if (e.CommandName == "DeleteRecord" && e.CommandArgument != null)
        {
            DeleteRecord(Convert.ToInt32(e.CommandArgument));
        }

        #endregion Handle Delete Action from GridView
    }
    private void DeleteRecord(Int32 ContactCategoryID)
    {
        #region Delete ContactCategory By PK

        ContactCategoryBAL balContactCategory = new ContactCategoryBAL();

        if (balContactCategory.Delete(ContactCategoryID))
        {
            if (Session["UserID"] != null)
            {
                FillContactCategoryGridView(Convert.ToInt32(Session["UserID"].ToString()));
            }
        }
        else
        {
            lblErrorMessage.Text = balContactCategory.Message;
        }

        #endregion Delete ContactCategory By PK
    }
}