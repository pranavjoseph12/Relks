using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectManagementSystem
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblError.Visible = false;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string userName = txtUserName.Text;
                string password = txtPassword.Text;
                LoginBusiness objCommon = new LoginBusiness();
                var user = objCommon.ValidateLogin(userName, password);

                if (user != null && user.IsValidUser)
                {
                    Session["User"] = user;

                    FormsAuthentication.RedirectFromLoginPage(txtUserName.Text, true);
                   
                    Response.Redirect("Enquiries.aspx");
                }
                else
                {
                    lblError.Visible = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
                lblError.Text = "Something went wrong. Please try again later";
                lblError.Visible = false;
            }
        }
    }
}