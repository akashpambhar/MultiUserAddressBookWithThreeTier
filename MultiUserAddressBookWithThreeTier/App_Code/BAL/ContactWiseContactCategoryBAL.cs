using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlTypes;
using MultiUserAddressBook.ENT;
using MultiUserAddressBook.DAL;

/// <summary>
/// Summary description for ContactWiseContactCategoryBAL
/// </summary>
public class ContactWiseContactCategoryBAL 
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
    public Boolean Insert(ContactWiseContactCategoryENT entContactWiseContactCategory)
    {
        ContactWiseContactCategoryDAL dalContactWiseContactCategory = new ContactWiseContactCategoryDAL();

        if (dalContactWiseContactCategory.Insert(entContactWiseContactCategory))
        {
            return true;
        }
        else
        {
            Message = dalContactWiseContactCategory.Message;
            return false;
        }
    }
    #endregion Insert Operation

    #region Update Operation
    public Boolean Update(ContactWiseContactCategoryENT entContactWiseContactCategory)
    {
        ContactWiseContactCategoryDAL dalContactWiseContactCategory = new ContactWiseContactCategoryDAL();

        if (dalContactWiseContactCategory.Update(entContactWiseContactCategory))
        {
            return true;
        }
        else
        {
            Message = dalContactWiseContactCategory.Message;
            return false;
        }
    }
    #endregion Update Operation

    #region Delete Operation
    public Boolean DeleteByContactID(SqlInt32 ContactID)
    {
        ContactWiseContactCategoryDAL dalContactWiseContactCategory = new ContactWiseContactCategoryDAL();

        if (dalContactWiseContactCategory.DeleteByContactID(ContactID))
        {
            return true;
        }
        else
        {
            Message = dalContactWiseContactCategory.Message;
            return false;
        }
    }
    #endregion Delete Operation

    #region Select Operation

    #region SelectAllByContactID
    public DataTable SelectAllByContactID(SqlInt32 ContactID)
    {
        ContactWiseContactCategoryDAL dalContactWiseContactCategory = new ContactWiseContactCategoryDAL();
        return dalContactWiseContactCategory.SelectAllByContactID(ContactID);
    }
    #endregion SelectAllByContactID

    #region Select For Dropdown List
    public DataTable SelectForDropdownList()
    {
        ContactWiseContactCategoryDAL dalContactWiseContactCategory = new ContactWiseContactCategoryDAL();
        return dalContactWiseContactCategory.SelectForDropdownList();
    }
    #endregion Select For Dropdown List

    #region SelectByPK
    public ContactWiseContactCategoryENT SelectByPK(SqlInt32 ContactWiseContactCategoryID)
    {
        ContactWiseContactCategoryDAL dalContactWiseContactCategory = new ContactWiseContactCategoryDAL();
        return dalContactWiseContactCategory.SelectByPK(ContactWiseContactCategoryID);
    }
    #endregion SelectByPK

    #endregion Select Operation
}