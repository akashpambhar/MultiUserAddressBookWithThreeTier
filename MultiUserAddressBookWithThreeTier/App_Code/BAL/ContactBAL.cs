using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlTypes;
using MultiUserAddressBook.ENT;
using MultiUserAddressBook.DAL;

/// <summary>
/// Summary description for ContactBAL
/// </summary>
public class ContactBAL 
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
    public Boolean Insert(ContactENT entContact)
    {
        ContactDAL dalContact = new ContactDAL();

        if (dalContact.Insert(entContact))
        {
            return true;
        }
        else
        {
            Message = dalContact.Message;
            return false;
        }
    }
    #endregion Insert Operation

    #region Update Operation
    public Boolean Update(ContactENT entContact)
    {
        ContactDAL dalContact = new ContactDAL();

        if (dalContact.Update(entContact))
        {
            return true;
        }
        else
        {
            Message = dalContact.Message;
            return false;
        }
    }
    #endregion Update Operation

    #region Delete Operation
    public Boolean Delete(SqlInt32 ContactID)
    {
        ContactDAL dalContact = new ContactDAL();

        if (dalContact.Delete(ContactID))
        {
            return true;
        }
        else
        {
            Message = dalContact.Message;
            return false;
        }
    }
    #endregion Delete Operation

    #region Select Operation

    #region SelectAll
    public DataTable SelectAll()
    {
        ContactDAL dalContact = new ContactDAL();
        return dalContact.SelectAll();
    }
    #endregion SelectAll

    #region Select For Dropdown List
    public DataTable SelectForDropdownList()
    {
        ContactDAL dalContact = new ContactDAL();
        return dalContact.SelectForDropdownList();
    }
    #endregion Select For Dropdown List

    #region SelectByPK
    public ContactENT SelectByPK(SqlInt32 ContactID)
    {
        ContactDAL dalContact = new ContactDAL();
        return dalContact.SelectByPK(ContactID);
    }
    #endregion SelectByPK

    #endregion Select Operation
}