using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UserMasterENT
/// </summary>
namespace MultiUserAddressBook.ENT
{
    public class UserMasterENT
    {
        #region Constructor

        public UserMasterENT()
        {
           
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

        #region UserName

        protected SqlString _UserName;

        public SqlString UserName
        {
            get
            {
                return _UserName;
            }
            set
            {
                _UserName = value;
            }
        }

        #endregion

        #region Password

        protected SqlString _Password;

        public SqlString Password
        {
            get
            {
                return _Password;
            }
            set
            {
                _Password = value;
            }
        }

        #endregion

        #region FullName

        protected SqlString _FullName;

        public SqlString FullName
        {
            get
            {
                return _FullName;
            }
            set
            {
                _FullName = value;
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

        #region EmailID

        protected SqlString _EmailID;

        public SqlString EmailID
        {
            get
            {
                return _EmailID;
            }
            set
            {
                _EmailID = value;
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

        #region BirthDate

        protected SqlDateTime _BirthDate;

        public SqlDateTime BirthDate
        {
            get
            {
                return _BirthDate;
            }
            set
            {
                _BirthDate = value;
            }
        }

        #endregion

        #region PhotoPath

        protected SqlString _PhotoPath;

        public SqlString PhotoPath
        {
            get
            {
                return _PhotoPath;
            }
            set
            {
                _PhotoPath = value;
            }
        }

        #endregion
    }
}