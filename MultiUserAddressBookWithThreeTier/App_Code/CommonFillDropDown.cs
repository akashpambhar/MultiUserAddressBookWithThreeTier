using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for CommonFillDropDown
/// </summary>
public static class CommonFillDropDown
{
    public static void FillCountryDropDownList(DropDownList ddlCountryID, Label lblErrorMessage, Int32 UserID)
    {
        ddlCountryID.Items.Clear();

        #region Get All Countries By UserID

        CountryBAL balCountry = new CountryBAL();
        DataTable dtCountry = new DataTable();

        dtCountry = balCountry.SelectAllByUserID(UserID);

        if (dtCountry != null && dtCountry.Rows.Count > 0)
        {
            ddlCountryID.DataSource = dtCountry;
            ddlCountryID.DataTextField = "CountryName";
            ddlCountryID.DataValueField = "CountryID";
            ddlCountryID.DataBind();
        }

        #endregion Get All Countries By UserID

        ddlCountryID.Items.Insert(0, new ListItem("Select Country...", "-1"));
    }

    public static void FillStateDropDownList(DropDownList ddlStateID, Label lblErrorMessage, Int32 UserID, Int32 CountryID)
    {
        ddlStateID.Items.Clear();

        #region Get All States By UserID

        StateBAL balState = new StateBAL();
        DataTable dtState = new DataTable();

        dtState = balState.SelectForDropdownList(CountryID, UserID);

        if (dtState != null && dtState.Rows.Count > 0)
        {
            ddlStateID.DataSource = dtState;
            ddlStateID.DataTextField = "StateName";
            ddlStateID.DataValueField = "StateID";
            ddlStateID.DataBind();
        }

        #endregion Get All States By UserID

        ddlStateID.Items.Insert(0, new ListItem("Select State...", "-1"));
    }

    public static void FillCityDropDownList(DropDownList ddlCityID, Label lblErrorMessage, Int32 UserID, Int32 CountryID, Int32 StateID)
    {
        ddlCityID.Items.Clear();

        #region Get All Cities By StateID and UserID

        CityBAL balCity = new CityBAL();
        DataTable dtCity = new DataTable();

        dtCity = balCity.SelectForDropdownList(StateID, UserID);

        if (dtCity != null && dtCity.Rows.Count > 0)
        {
            ddlCityID.DataSource = dtCity;
            ddlCityID.DataTextField = "CityName";
            ddlCityID.DataValueField = "CityID";
            ddlCityID.DataBind();
        }

        #endregion Get All Cities By StateID and UserID

        ddlCityID.Items.Insert(0, new ListItem("Select City...", "-1"));
    }
}