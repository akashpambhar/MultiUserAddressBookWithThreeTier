using MultiUserAddressBook;
using MultiUserAddressBook.ENT;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
/// <summary>
/// Summary description for UserMasterDAL
/// </summary>
namespace MultiUserAddressBook.DAL
{
    public class UserMasterDAL : DatabaseConfig
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
            using (SqlConnection objConn = new SqlConnection(ConnectionString))
            {
                objConn.Open();

                using (SqlCommand objCmd = objConn.CreateCommand())
                {
                    try
                    {
                        #region Prepare Command

                        objCmd.CommandType = CommandType.StoredProcedure;
                        objCmd.CommandText = "PR_UserMaster_Insert";
                        objCmd.Parameters.Add("@UserID", SqlDbType.Int).Direction = ParameterDirection.Output;
                        objCmd.Parameters.AddWithValue("@UserName", entUserMaster.UserName);
                        objCmd.Parameters.AddWithValue("@Password", entUserMaster.Password);
                        objCmd.Parameters.AddWithValue("@FullName", entUserMaster.FullName);
                        objCmd.Parameters.AddWithValue("@Address", entUserMaster.Address);
                        objCmd.Parameters.AddWithValue("@MobileNo", entUserMaster.MobileNo);
                        objCmd.Parameters.AddWithValue("@EmailID", entUserMaster.EmailID);
                        objCmd.Parameters.AddWithValue("@FacebookID", entUserMaster.FacebookID);
                        objCmd.Parameters.AddWithValue("@BirthDate", entUserMaster.BirthDate);
                        objCmd.Parameters.AddWithValue("@PhotoPath", entUserMaster.PhotoPath);

                        #endregion Prepare Command

                        objCmd.ExecuteNonQuery();

                        entUserMaster.UserID = Convert.ToInt32(objCmd.Parameters["@UserID"].Value);

                        return true;
                    }
                    catch (SqlException sqlex)
                    {
                        Message = sqlex.InnerException.Message;
                        return false;
                    }
                    catch (Exception ex)
                    {
                        Message = ex.InnerException.Message;
                        return false;
                    }
                    finally
                    {
                        if (objConn.State == ConnectionState.Open)
                            objConn.Close();
                    }
                }
            }
        }
        #endregion Insert Operation

        #region Update Operation
        public Boolean Update(UserMasterENT entUserMaster)
        {
            using (SqlConnection objConn = new SqlConnection(ConnectionString))
            {
                objConn.Open();

                using (SqlCommand objCmd = objConn.CreateCommand())
                {
                    try
                    {
                        #region Prepare Command

                        objCmd.CommandType = CommandType.StoredProcedure;
                        objCmd.CommandText = "PR_UserMaster_UpdateByPK";
                        objCmd.Parameters.AddWithValue("@UserID", entUserMaster.UserID);
                        objCmd.Parameters.AddWithValue("@UserName", entUserMaster.UserName);
                        objCmd.Parameters.AddWithValue("@Password", entUserMaster.Password);
                        objCmd.Parameters.AddWithValue("@FullName", entUserMaster.FullName);
                        objCmd.Parameters.AddWithValue("@Address", entUserMaster.Address);
                        objCmd.Parameters.AddWithValue("@MobileNo", entUserMaster.MobileNo);
                        objCmd.Parameters.AddWithValue("@EmailID", entUserMaster.EmailID);
                        objCmd.Parameters.AddWithValue("@FacebookID", entUserMaster.FacebookID);
                        objCmd.Parameters.AddWithValue("@BirthDate", entUserMaster.BirthDate);
                        objCmd.Parameters.AddWithValue("@PhotoPath", entUserMaster.PhotoPath);

                        #endregion

                        objCmd.ExecuteNonQuery();
                        return true;
                    }
                    catch (SqlException sqlex)
                    {
                        Message = sqlex.InnerException.Message;
                        return false;
                    }
                    catch (Exception ex)
                    {
                        Message = ex.InnerException.Message;
                        return false;
                    }
                    finally
                    {
                        if (objConn.State == ConnectionState.Open)
                            objConn.Close();
                    }
                }
            }
        }
        #endregion Update Operation

        #region Delete Operation
        public Boolean Delete(SqlInt32 UserID)
        {
            using (SqlConnection objConn = new SqlConnection(ConnectionString))
            {
                objConn.Open();

                using (SqlCommand objCmd = objConn.CreateCommand())
                {
                    try
                    {
                        #region Prepare Command

                        objCmd.CommandType = CommandType.StoredProcedure;
                        objCmd.CommandText = "PR_UserMaster_DeleteByPK";

                        objCmd.Parameters.AddWithValue("@UserID", UserID);

                        #endregion Prepare Command

                        objCmd.ExecuteNonQuery();

                        return true;
                    }
                    catch (SqlException sqlex)
                    {
                        Message = sqlex.InnerException.Message;
                        return false;
                    }
                    catch (Exception ex)
                    {
                        Message = ex.InnerException.Message;
                        return false;
                    }
                    finally
                    {
                        if (objConn.State == ConnectionState.Open)
                            objConn.Close();
                    }
                }
            }
        }
        #endregion Delete Operation

        #region Select Operation

        #region SelectAll
        public DataTable SelectAll()
        {
            using (SqlConnection objConn = new SqlConnection(ConnectionString))
            {
                objConn.Open();

                using (SqlCommand objCmd = objConn.CreateCommand())
                {
                    try
                    {
                        #region Prepare Command

                        objCmd.CommandType = CommandType.StoredProcedure;
                        objCmd.CommandText = "PR_UserMaster_SelectAll";

                        #endregion Prepare Command

                        #region Read Data and return DataTable

                        DataTable dt = new DataTable();

                        using (SqlDataReader objSDR = objCmd.ExecuteReader())
                        {
                            dt.Load(objSDR);
                        }
                        return dt;

                        #endregion Read Data and return DataTable
                    }
                    catch (SqlException sqlex)
                    {
                        Message = sqlex.InnerException.Message;
                        return null;
                    }
                    catch (Exception ex)
                    {
                        Message = ex.InnerException.Message;
                        return null;
                    }
                    finally
                    {
                        if (objConn.State == ConnectionState.Open)
                            objConn.Close();
                    }
                }
            }
        }
        #endregion SelectAll

        #region SelectForDropdownList
        public DataTable SelectForDropdownList()
        {
            using (SqlConnection objConn = new SqlConnection(ConnectionString))
            {
                objConn.Open();

                using (SqlCommand objCmd = objConn.CreateCommand())
                {
                    try
                    {

                        DataTable dt = new DataTable();

                        return dt;
                    }
                    catch (SqlException sqlex)
                    {
                        Message = sqlex.InnerException.Message;
                        return null;
                    }
                    catch (Exception ex)
                    {
                        Message = ex.InnerException.Message;
                        return null;
                    }
                    finally
                    {
                        if (objConn.State == ConnectionState.Open)
                            objConn.Close();
                    }
                }
            }
        }
        #endregion SelectForDropdownList

        #region SelectByPK
        public UserMasterENT SelectByPK(SqlInt32 UserID)
        {
            using (SqlConnection objConn = new SqlConnection(ConnectionString))
            {
                objConn.Open();

                using (SqlCommand objCmd = objConn.CreateCommand())
                {
                    try
                    {
                        #region Prepare Command

                        objCmd.CommandType = CommandType.StoredProcedure;
                        objCmd.CommandText = "PR_UserMaster_SelectByPK";
                        objCmd.Parameters.AddWithValue("@UserID", UserID);

                        #endregion Prepare Command

                        #region Read Data and return ENT

                        UserMasterENT entUserMaster = new UserMasterENT();

                        SqlDataReader objSDR = objCmd.ExecuteReader();

                        if (objSDR.HasRows)
                        {
                            while (objSDR.Read())
                            {
                                if (!objSDR["UserName"].Equals(DBNull.Value))
                                    entUserMaster.UserName = Convert.ToString(objSDR["UserName"]);

                                if (!objSDR["Password"].Equals(DBNull.Value))
                                    entUserMaster.Password = Convert.ToString(objSDR["Password"]);

                                if (!objSDR["FullName"].Equals(DBNull.Value))
                                    entUserMaster.FullName = Convert.ToString(objSDR["FullName"]);

                                if (!objSDR["Address"].Equals(DBNull.Value))
                                    entUserMaster.Address = Convert.ToString(objSDR["Address"]);

                                if (!objSDR["MobileNo"].Equals(DBNull.Value))
                                    entUserMaster.MobileNo = Convert.ToString(objSDR["MobileNo"]);

                                if (!objSDR["EmailID"].Equals(DBNull.Value))
                                    entUserMaster.EmailID = Convert.ToString(objSDR["EmailID"]);

                                if (!objSDR["FacebookID"].Equals(DBNull.Value))
                                    entUserMaster.FacebookID = Convert.ToString(objSDR["FacebookID"]);

                                if (!objSDR["BirthDate"].Equals(DBNull.Value))
                                    entUserMaster.BirthDate = Convert.ToDateTime(objSDR["BirthDate"]);

                                if (!objSDR["PhotoPath"].Equals(DBNull.Value))
                                    entUserMaster.PhotoPath = Convert.ToString(objSDR["PhotoPath"]);

                                break;
                            }
                        }

                        return entUserMaster;

                        #endregion Read Data and return ENT
                    }
                    catch (SqlException sqlex)
                    {
                        Message = sqlex.InnerException.Message;
                        return null;
                    }
                    catch (Exception ex)
                    {
                        Message = ex.InnerException.Message;
                        return null;
                    }
                    finally
                    {
                        if (objConn.State == ConnectionState.Open)
                            objConn.Close();
                    }
                }
            }
        }
        #endregion SelectByPK

        #region SelectByUserNamePassword
        public DataTable SelectByUserNamePassword(UserMasterENT entUserMaster)
        {
            using (SqlConnection objConn = new SqlConnection(ConnectionString))
            {
                objConn.Open();

                using (SqlCommand objCmd = objConn.CreateCommand())
                {
                    try
                    {
                        #region Prepare Command

                        objCmd.CommandType = CommandType.StoredProcedure;
                        objCmd.CommandText = "PR_UserMaster_SelectByUserNamePassword";

                        objCmd.Parameters.AddWithValue("@UserName", entUserMaster.UserName);
                        objCmd.Parameters.AddWithValue("@Password", entUserMaster.Password);

                        #endregion Prepare Command

                        #region Read Data and return DataTable

                        DataTable dt = new DataTable();

                        using (SqlDataReader objSDR = objCmd.ExecuteReader())
                        {
                            dt.Load(objSDR);
                        }
                        return dt;

                        #endregion Read Data and return DataTable
                    }
                    catch (SqlException sqlex)
                    {
                        Message = sqlex.InnerException.Message;
                        return null;
                    }
                    catch (Exception ex)
                    {
                        Message = ex.InnerException.Message;
                        return null;
                    }
                    finally
                    {
                        if (objConn.State == ConnectionState.Open)
                            objConn.Close();
                    }
                }
            }
        }
        #endregion SelectByUserNamePassword

        #endregion Select Operation
    }
}