using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlTypes;
using MultiUserAddressBook.ENT;
using MultiUserAddressBook.DAL;

/// <summary>
/// Summary description for UserMasterBAL
/// </summary>
public class UserMasterBAL
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
    public Boolean Insert(UserMasterENT entUserMaster)
    {
        UserMasterDAL dalUserMaster = new UserMasterDAL();

        if (dalUserMaster.Insert(entUserMaster))
        {
            return true;
        }
        else
        {
            Message = dalUserMaster.Message;
            return false;
        }
    }
    #endregion Insert Operation

    #region Update Operation
    public Boolean Update(UserMasterENT entUserMaster)
    {
        UserMasterDAL dalUserMaster = new UserMasterDAL();

        if (dalUserMaster.Update(entUserMaster))
        {
            return true;
        }
        else
        {
            Message = dalUserMaster.Message;
            return false;
        }
    }
    #endregion Update Operation

    #region Delete Operation
    public Boolean Delete(SqlInt32 UserMasterID)
    {
        UserMasterDAL dalUserMaster = new UserMasterDAL();

        if (dalUserMaster.Delete(UserMasterID))
        {
            return true;
        }
        else
        {
            Message = dalUserMaster.Message;
            return false;
        }
    }
    #endregion Delete Operation

    #region Select Operation

    #region SelectAll
    public DataTable SelectAll()
    {
        UserMasterDAL dalUserMaster = new UserMasterDAL();
        return dalUserMaster.SelectAll();
    }
    #endregion SelectAll

    #region Select For Dropdown List
    public DataTable SelectForDropdownList()
    {
        UserMasterDAL dalUserMaster = new UserMasterDAL();
        return dalUserMaster.SelectForDropdownList();
    }
    #endregion Select For Dropdown List

    #region SelectByPK
    public UserMasterENT SelectByPK(SqlInt32 UserMasterID)
    {
        UserMasterDAL dalUserMaster = new UserMasterDAL();
        return dalUserMaster.SelectByPK(UserMasterID);
    }
    #endregion SelectByPK

    #endregion Select Operation
}