using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminPanel_Country_CountryList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            #region Check Session UserID and Load Controls

            if (Session["UserID"] != null)
            {
                FillCountryGridView(Convert.ToInt32(Session["UserID"].ToString()));
            }

            #endregion Check Session UserID and Load Controls
        }
    }
    private void FillCountryGridView(Int32 UserID)
    {
        #region Get All Countries By UserID

        CountryBAL balCountry = new CountryBAL();
        DataTable dtCountry = new DataTable();

        dtCountry = balCountry.SelectAllByUserID(UserID);

        gvCountryList.DataSource = dtCountry;
        gvCountryList.DataBind();

        #endregion Get All Countries By UserID
    }

    protected void gvCountryList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        #region Handle Delete Action from GridView

        if (e.CommandName == "DeleteRecord" && e.CommandArgument != null)
        {
            DeleteRecord(Convert.ToInt32(e.CommandArgument));
            FillCountryGridView(Convert.ToInt32(Session["UserID"].ToString()));
        }

        #endregion Handle Delete Action from GridView
    }
    private void DeleteRecord(Int32 CountryID)
    {
        #region Delete Country By PK

        CountryBAL balCountry = new CountryBAL();
        if (balCountry.Delete(CountryID))
        {
            lblErrorMessage.Text = "Deleted Successfully!";
        }
        else
        {
            lblErrorMessage.Text = balCountry.Message;
        }

        #endregion Delete Country By PK
    }
}