using MultiUserAddressBook.ENT;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            txtUserName.Focus();
        }
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        #region Server Side Validation

        String strErrorMessage = "";

        if (txtUserName.Text.Trim() == "")
        {
            strErrorMessage += "Enter User Name <br/>";
        }
        if (txtPassword.Text.Trim() == "")
        {
            strErrorMessage += "Enter Password <br/>";
        }
        if (strErrorMessage != "")
        {
            lblErrorMessage.Text = strErrorMessage;
            return;
        }

        #endregion Server Side Validation

        #region Gather Information

        UserMasterENT entUserMaster = new UserMasterENT();

        if (txtUserName.Text.Trim() != "")
        {
            entUserMaster.UserName = txtUserName.Text.Trim();
        }
        if (txtPassword.Text.Trim() != "")
        {
            entUserMaster.Password = txtPassword.Text.Trim();
        }

        #endregion Gather Information

        #region Get User By UserName and Password

        UserMasterBAL balUserMaster = new UserMasterBAL();
        DataTable dtUserMaster = new DataTable();

        dtUserMaster = balUserMaster.SelectByUserNamePassword(entUserMaster);

        if (dtUserMaster != null && dtUserMaster.Rows.Count > 0)
        {
            foreach (DataRow drUserMaster in dtUserMaster.Rows)
            {
                if (!drUserMaster["UserID"].Equals(DBNull.Value))
                {
                    Session["UserID"] = drUserMaster["UserID"].ToString();
                }
                if (!drUserMaster["UserName"].Equals(DBNull.Value))
                {
                    Session["UserName"] = drUserMaster["UserName"].ToString();
                }
                if (!drUserMaster["PhotoPath"].Equals(DBNull.Value))
                {
                    Session["PhotoPath"] = drUserMaster["PhotoPath"].ToString();
                }
                break;
            }
            Response.Redirect("~/AB/AdminPanel/Contact");
        }
        else
        {
            if (balUserMaster.Message == null)
            {
                lblErrorMessage.Text = "Either Username or Password is not wrong, please try again";
            }
            else
            {
                lblErrorMessage.Text = balUserMaster.Message;
            }
            txtUserName.Text = "";
            txtPassword.Text = "";
            txtUserName.Focus();
        }

        #endregion Get User By UserName and Password
    }
}