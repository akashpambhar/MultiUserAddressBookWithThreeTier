using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlTypes;
using MultiUserAddressBook.ENT;
using MultiUserAddressBook.DAL;

/// <summary>
/// Summary description for CityBAL
/// </summary>
public class CityBAL
{
    #region Local Variables

    private string _Message;

    public string Message
    {
        get
        {
            return _Message;
        }
        set
        {
            _Message = value;
        }
    }

    #endregion Local Variables

    #region Insert Operation
    public Boolean Insert(CityENT entCity)
    {
        CityDAL dalCity = new CityDAL();

        if (dalCity.Insert(entCity))
        {
            return true;
        }
        else
        {
            Message = dalCity.Message;
            return false;
        }
    }
    #endregion Insert Operation

    #region Update Operation
    public Boolean Update(CityENT entCity)
    {
        CityDAL dalCity = new CityDAL();

        if (dalCity.Update(entCity))
        {
            return true;
        }
        else
        {
            Message = dalCity.Message;
            return false;
        }
    }
    #endregion Update Operation

    #region Delete Operation
    public Boolean Delete(SqlInt32 CityID)
    {
        CityDAL dalCity = new CityDAL();

        if (dalCity.Delete(CityID))
        {
            return true;
        }
        else
        {
            Message = dalCity.Message;
            return false;
        }
    }
    #endregion Delete Operation

    #region Select Operation

    #region SelectAllByUserID
    public DataTable SelectAllByUserID(SqlInt32 UserID)
    {
        CityDAL dalCity = new CityDAL();
        return dalCity.SelectAllByUserID(UserID);
    }
    #endregion SelectAllByUserID

    #region Select For Dropdown List
    public DataTable SelectForDropdownList(SqlInt32 StateID, SqlInt32 UserID)
    {
        CityDAL dalCity = new CityDAL();
        return dalCity.SelectForDropdownList(StateID, UserID);
    }
    #endregion Select For Dropdown List

    #region SelectByPK
    public CityENT SelectByPK(SqlInt32 CityID)
    {
        CityDAL dalCity = new CityDAL();
        return dalCity.SelectByPK(CityID);
    }
    #endregion SelectByPK

    #endregion Select Operation
}