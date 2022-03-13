using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminPanel_State_StateList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            #region Check Session UserID and Load Controls

            if (Session["UserID"] != null)
            {
                FillStateGridView(Convert.ToInt32(Session["UserID"].ToString()));
            }

            #endregion Check Session UserID and Load Controls
        }
    }
    private void FillStateGridView(Int32 UserID)
    {
        #region Get All States By UserID

        StateBAL balState = new StateBAL();
        DataTable dtCountry = new DataTable();

        dtCountry = balState.SelectAllByUserID(UserID);

        gvStateList.DataSource = dtCountry;
        gvStateList.DataBind();

        #endregion Get All States By UserID
    }
    protected void gvStateList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        #region Handle Delete Action from GridView

        if (e.CommandName == "DeleteRecord" && e.CommandArgument != null)
        {
            DeleteRecord(Convert.ToInt32(e.CommandArgument));
            if (Session["UserID"] != null)
            {
                FillStateGridView(Convert.ToInt32(Session["UserID"].ToString()));
            }
        }

        #endregion Handle Delete Action from GridView
    }
    private void DeleteRecord(Int32 StateID)
    {
        #region Delete State By PK

        StateBAL balState = new StateBAL();
        if (balState.Delete(StateID))
        {
            lblErrorMessage.Text = "Deleted Successfully!";
        }
        else
        {
            lblErrorMessage.Text = balState.Message;
        }

        #endregion Delete State By PK
    }
}