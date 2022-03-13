using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminPanel_City_CityList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            #region Check Session UserID and Load Controls

            if (Session["UserID"] != null)
            {
                FillCityGridView(Convert.ToInt32(Session["UserID"].ToString()));
            }

            #endregion Check Session UserID and Load Controls
        }
    }
    private void FillCityGridView(Int32 UserID)
    {
        #region Get All Cities By UserID

        CityBAL balCity = new CityBAL();
        DataTable dtCity = new DataTable();

        dtCity = balCity.SelectAllByUserID(UserID);

        gvCityList.DataSource = dtCity;
        gvCityList.DataBind();

        #endregion Get All Cities By UserID
    }
    protected void gvCityList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        #region Handle Delete Action from GridView

        if (e.CommandName == "DeleteRecord" && e.CommandArgument != null)
        {
            DeleteRecord(Convert.ToInt32(e.CommandArgument));
            if (Session["UserID"] != null)
            {
                FillCityGridView(Convert.ToInt32(Session["UserID"].ToString()));
            }
        }

        #endregion Handle Delete Action from GridView
    }
    private void DeleteRecord(Int32 CityID)
    {
        #region Delete City By PK

        CityBAL balCity = new CityBAL();
        if (balCity.Delete(CityID))
        {
            lblErrorMessage.Text = "Deleted Successfully!";
        }
        else
        {
            lblErrorMessage.Text = balCity.Message;
        }

        #endregion Delete City By PK
    }
}