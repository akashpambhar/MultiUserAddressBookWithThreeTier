using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ContactENT
/// </summary>
namespace MultiUserAddressBook.ENT
{
    public class ContactENT
    {
        #region Constructor

        public ContactENT()
        {
            _ContactID = SqlInt32.Null;
            _ContactName = SqlString.Null;
            _Address = SqlString.Null;
            _Pincode = SqlString.Null;
            _CityID = SqlInt32.Null;
            _StateID = SqlInt32.Null;
            _CountryID = SqlInt32.Null;
            _EmailAddress = SqlString.Null;
            _MobileNo = SqlString.Null;
            _FacebookID = SqlString.Null;
            _LinkedInID = SqlString.Null;
            _CreationDate = SqlDateTime.Null;
            _UserID = SqlInt32.Null;
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

        #region ContactName

        protected SqlString _ContactName;

        public SqlString ContactName
        {
            get
            {
                return _ContactName;
            }
            set
            {
                _ContactName = value;
            }
        }

        #endregion

        #region Address

        protected SqlString _Address;

        public SqlString Address
        {
            get
            {
                return _Address;
            }
            set
            {
                _Address = value;
            }
        }

        #endregion

        #region Pincode

        protected SqlString _Pincode;

        public SqlString Pincode
        {
            get
            {
                return _Pincode;
            }
            set
            {
                _Pincode = value;
            }
        }

        #endregion

        #region CityID

        protected SqlInt32 _CityID;

        public SqlInt32 CityID
        {
            get
            {
                return _CityID;
            }
            set
            {
                _CityID = value;
            }
        }

        #endregion

        #region StateID

        protected SqlInt32 _StateID;

        public SqlInt32 StateID
        {
            get
            {
                return _StateID;
            }
            set
            {
                _StateID = value;
            }
        }

        #endregion

        #region CountryID

        protected SqlInt32 _CountryID;

        public SqlInt32 CountryID
        {
            get
            {
                return _CountryID;
            }
            set
            {
                _CountryID = value;
            }
        }

        #endregion

        #region EmailAddress

        protected SqlString _EmailAddress;

        public SqlString EmailAddress
        {
            get
            {
                return _EmailAddress;
            }
            set
            {
                _EmailAddress = value;
            }
        }

        #endregion

        #region MobileNo

        protected SqlString _MobileNo;

        public SqlString MobileNo
        {
            get
            {
                return _MobileNo;
            }
            set
            {
                _MobileNo = value;
            }
        }

        #endregion

        #region FacebookID

        protected SqlString _FacebookID;

        public SqlString FacebookID
        {
            get
            {
                return _FacebookID;
            }
            set
            {
                _FacebookID = value;
            }
        }

        #endregion

        #region LinkedInID

        protected SqlString _LinkedInID;

        public SqlString LinkedInID
        {
            get
            {
                return _LinkedInID;
            }
            set
            {
                _LinkedInID = value;
            }
        }

        #endregion

        #region UserID

        protected SqlInt32 _UserID;

        public SqlInt32 UserID
        {
            get
            {
                return _UserID;
            }
            set
            {
                _UserID = value;
            }
        }

        #endregion
    }
}