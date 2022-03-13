using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlTypes;
using MultiUserAddressBook.ENT;
using MultiUserAddressBook.DAL;

/// <summary>
/// Summary description for ContactCategoryBAL
/// </summary>
public class ContactCategoryBAL 
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
    public Boolean Insert(ContactCategoryENT entContactCategory)
    {
        ContactCategoryDAL dalContactCategory = new ContactCategoryDAL();

        if (dalContactCategory.Insert(entContactCategory))
        {
            return true;
        }
        else
        {
            Message = dalContactCategory.Message;
            return false;
        }
    }
    #endregion Insert Operation

    #region Update Operation
    public Boolean Update(ContactCategoryENT entContactCategory)
    {
        ContactCategoryDAL dalContactCategory = new ContactCategoryDAL();

        if (dalContactCategory.Update(entContactCategory))
        {
            return true;
        }
        else
        {
            Message = dalContactCategory.Message;
            return false;
        }
    }
    #endregion Update Operation

    #region Delete Operation
    public Boolean Delete(SqlInt32 ContactCategoryID)
    {
        ContactCategoryDAL dalContactCategory = new ContactCategoryDAL();

        if (dalContactCategory.Delete(ContactCategoryID))
        {
            return true;
        }
        else
        {
            Message = dalContactCategory.Message;
            return false;
        }
    }
    #endregion Delete Operation

    #region Select Operation

    #region SelectAllByUserID
    public DataTable SelectAllByUserID(SqlInt32 UserID)
    {
        ContactCategoryDAL dalContactCategory = new ContactCategoryDAL();
        return dalContactCategory.SelectAllByUserID(UserID);
    }
    #endregion SelectAllByUserID

    #region Select For Dropdown List
    public DataTable SelectForDropdownList()
    {
        ContactCategoryDAL dalContactCategory = new ContactCategoryDAL();
        return dalContactCategory.SelectForDropdownList();
    }
    #endregion Select For Dropdown List

    #region SelectByPK
    public ContactCategoryENT SelectByPK(SqlInt32 ContactCategoryID)
    {
        ContactCategoryDAL dalContactCategory = new ContactCategoryDAL();
        return dalContactCategory.SelectByPK(ContactCategoryID);
    }
    #endregion SelectByPK

    #endregion Select Operation
}