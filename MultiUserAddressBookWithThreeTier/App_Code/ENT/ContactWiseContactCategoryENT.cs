using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ContactWiseContactCategoryENT
/// </summary>
namespace MultiUserAddressBook.ENT
{
    public class ContactWiseContactCategoryENT
    {
        #region Constructor

        public ContactWiseContactCategoryENT()
        {
           
        }

        #endregion

        #region ContactWiseContactCategoryID

        protected SqlInt32 _ContactWiseContactCategoryID;

        public SqlInt32 ContactWiseContactCategoryID
        {
            get
            {
                return _ContactWiseContactCategoryID;
            }
            set
            {
                _ContactWiseContactCategoryID = value;
            }
        }

        #endregion

        #region ContactID

        protected SqlInt32 _ContactID;

        public SqlInt32 ContactID
        {
            get
            {
                return _ContactID;
            }
            set
            {
                _ContactID = value;
            }
        }

        #endregion

        #region ContactCategoryID

        protected SqlInt32 _ContactCategoryID;

        public SqlInt32 ContactCategoryID
        {
            get
            {
                return _ContactCategoryID;
            }
            set
            {
                _ContactCategoryID = value;
            }
        }

        #endregion
    }
}