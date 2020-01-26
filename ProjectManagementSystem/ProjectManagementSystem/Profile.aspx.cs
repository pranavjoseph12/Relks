using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WebAPI.BusinessLayer;
using WebAPI.Models;

namespace ProjectManagementSystem
{
    public partial class Profile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RolesBusinessActions objRole = new RolesBusinessActions();
            UserModel objUser = new UserModel();
            if (Session["User"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            objUser = (UserModel)Session["User"];
            if (!objUser.IsValidUser)
            {
                Response.Redirect("Login.aspx");
            }

            if (!Page.IsPostBack)
            {
                UserBusinessActions obj = new UserBusinessActions();
                var result = obj.GetUserDetailsById(objUser.UserId);
                if (result != null && result.Rows.Count > 0)
                {
                    txtName.Text = result.Rows[0]["Name"].ToString();
                    txtEmail.Text = result.Rows[0]["Email"].ToString();
                    txtPhone.Text = result.Rows[0]["Mobile"].ToString();
                    txtPassword.Text = result.Rows[0]["Password"].ToString();
                    lblRole.Text = result.Rows[0]["RoleName"].ToString();
                }
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            UserModel objUser = new UserModel();
            objUser = (UserModel)Session["User"];
            if (!objUser.IsValidUser)
            {
                Response.Redirect("Login.aspx");
            }
            UserBusinessActions obj = new UserBusinessActions();

            var result = obj.UpdateUserDetailsById(objUser.UserId, txtName.Text, txtEmail.Text, txtPhone.Text, txtPassword.Text);

            if (result == true)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "SuccessRedirect", "alert('Profile Updated Successfully.');", true);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorRedirect", "alert('Something Went wrong. Please try again later');", true);
            }
        }
    }
}