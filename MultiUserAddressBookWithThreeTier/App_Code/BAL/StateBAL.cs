using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlTypes;
using MultiUserAddressBook.ENT;
using MultiUserAddressBook.DAL;

/// <summary>
/// Summary description for StateBAL
/// </summary>
public class StateBAL 
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
    public Boolean Insert(StateENT entState)
    {
        StateDAL dalState = new StateDAL();

        if (dalState.Insert(entState))
        {
            return true;
        }
        else
        {
            Message = dalState.Message;
            return false;
        }
    }
    #endregion Insert Operation

    #region Update Operation
    public Boolean Update(StateENT entState)
    {
        StateDAL dalState = new StateDAL();

        if (dalState.Update(entState))
        {
            return true;
        }
        else
        {
            Message = dalState.Message;
            return false;
        }
    }
    #endregion Update Operation

    #region Delete Operation
    public Boolean Delete(SqlInt32 StateID)
    {
        StateDAL dalState = new StateDAL();

        if (dalState.Delete(StateID))
        {
            return true;
        }
        else
        {
            Message = dalState.Message;
            return false;
        }
    }
    #endregion Delete Operation

    #region Select Operation

    #region SelectAllByUserID
    public DataTable SelectAllByUserID(SqlInt32 UserID)
    {
        StateDAL dalState = new StateDAL();
        return dalState.SelectAllByUserID(UserID);
    }
    #endregion SelectAllByUserID

    #region Select For Dropdown List
    public DataTable SelectForDropdownList(SqlInt32 CountryID, SqlInt32 UserID)
    {
        StateDAL dalState = new StateDAL();
        return dalState.SelectForDropdownList(CountryID, UserID);
    }
    #endregion Select For Dropdown List

    #region SelectByPK
    public StateENT SelectByPK(SqlInt32 StateID)
    {
        StateDAL dalState = new StateDAL();
        return dalState.SelectByPK(StateID);
    }
    #endregion SelectByPK

    #endregion Select Operation
}