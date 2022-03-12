using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlTypes;
using MultiUserAddressBook.ENT;
using MultiUserAddressBook.DAL;

/// <summary>
/// Summary description for CountryBAL
/// </summary>
public class CountryBAL 
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
    public Boolean Insert(CountryENT entCountry)
    {
        CountryDAL dalCountry = new CountryDAL();

        if (dalCountry.Insert(entCountry))
        {
            return true;
        }
        else
        {
            Message = dalCountry.Message;
            return false;
        }
    }
    #endregion Insert Operation

    #region Update Operation
    public Boolean Update(CountryENT entCountry)
    {
        CountryDAL dalCountry = new CountryDAL();

        if (dalCountry.Update(entCountry))
        {
            return true;
        }
        else
        {
            Message = dalCountry.Message;
            return false;
        }
    }
    #endregion Update Operation

    #region Delete Operation
    public Boolean Delete(SqlInt32 CountryID)
    {
        CountryDAL dalCountry = new CountryDAL();

        if (dalCountry.Delete(CountryID))
        {
            return true;
        }
        else
        {
            Message = dalCountry.Message;
            return false;
        }
    }
    #endregion Delete Operation

    #region Select Operation

    #region SelectAll
    public DataTable SelectAll()
    {
        CountryDAL dalCountry = new CountryDAL();
        return dalCountry.SelectAll();
    }
    #endregion SelectAll

    #region Select For Dropdown List
    public DataTable SelectForDropdownList()
    {
        CountryDAL dalCountry = new CountryDAL();
        return dalCountry.SelectForDropdownList();
    }
    #endregion Select For Dropdown List

    #region SelectByPK
    public CountryENT SelectByPK(SqlInt32 CountryID)
    {
        CountryDAL dalCountry = new CountryDAL();
        return dalCountry.SelectByPK(CountryID);
    }
    #endregion SelectByPK

    #endregion Select Operation
}