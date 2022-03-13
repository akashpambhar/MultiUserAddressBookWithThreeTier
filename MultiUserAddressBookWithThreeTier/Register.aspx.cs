using MultiUserAddressBook.ENT;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Register : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            txtFullName.Focus();
        }
    }
    protected void btnRegister_Click(object sender, EventArgs e)
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
        String strUserPhotoPath = MakePhotoPath();
        if (strUserPhotoPath == "Please upload file of size less than 1 MB" || strUserPhotoPath == "Please select a jpg, jpeg or png file")
        {
            strErrorMessage += strUserPhotoPath + " <br/>";
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
        if (txtFullName.Text.Trim() != "")
        {
            entUserMaster.FullName = txtFullName.Text.Trim();
        }
        if (txtAddress.Text.Trim() != "")
        {
            entUserMaster.Address = txtAddress.Text.Trim();
        }
        if (txtMobileNo.Text.Trim() != "")
        {
            entUserMaster.MobileNo = txtMobileNo.Text.Trim();
        }
        if (txtEmail.Text.Trim() != "")
        {
            entUserMaster.EmailID = txtEmail.Text.Trim();
        }
        if (txtFacebookID.Text.Trim() != "")
        {
            entUserMaster.FacebookID = txtFacebookID.Text.Trim();
        }
        if (txtBirthDate.Text.Trim() != "")
        {
            entUserMaster.BirthDate = Convert.ToDateTime(txtBirthDate.Text.Trim());
        }
        if (strUserPhotoPath.Trim() != "")
        {
            entUserMaster.PhotoPath = strUserPhotoPath.Trim();
        }

        #endregion Gather Information

        #region Insert

        UserMasterBAL balUserMaster = new UserMasterBAL();

        if (balUserMaster.Insert(entUserMaster))
        {
            Response.Redirect("~/AB/AdminPanel/Login");
        }
        else
        {
            lblErrorMessage.Text = balUserMaster.Message;
        }

        #endregion Insert
    }
    private String MakePhotoPath()
    {
        #region Perform Photo Validation and Return PhotoPath

        if (fuUserPic.HasFile)
        {
            String fileExt = System.IO.Path.GetExtension(fuUserPic.FileName);
            if (fileExt == ".jpg" || fileExt == ".jpeg" || fileExt == ".png")
            {
                HttpPostedFile file = fuUserPic.PostedFile;
                int iFileSize = file.ContentLength;
                if (iFileSize > 1048576)
                {
                    return "Please upload file of size less than 1 MB";
                }
                else
                {
                    String strPhotoPath = "~/Content/Asset/Content/images/";
                    if (!Directory.Exists(Server.MapPath(strPhotoPath)))
                    {
                        Directory.CreateDirectory(Server.MapPath(strPhotoPath));
                    }

                    fuUserPic.SaveAs(Server.MapPath(strPhotoPath + fuUserPic.FileName));
                    return strPhotoPath + fuUserPic.FileName;
                }
            }
            else
            {
                return "Please select a jpg, jpeg or png file";
            }
        }
        return "";

        #endregion Perform Photo Validation and Return PhotoPath
    }
}