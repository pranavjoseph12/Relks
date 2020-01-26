using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAPI.Models;
using BusinessLayer;
using WebAPI.BusinessLayer;

namespace ProjectManagementSystem
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] == null)
            {
                Response.Redirect("Login.aspx");
                
            }
            else
            {
                RolesBusinessActions objRole = new RolesBusinessActions();
                //var roleResult = objRole.GetRoles();

                UserModel objUser = new UserModel();
                objUser = (UserModel)Session["User"];
                if (!objUser.IsValidUser)
                {
                    Response.Redirect("Login.aspx");
                }

                //if (objUser.UserId== 1)
                //{
                //    liPersonalExpense.Visible = true;
                //}

                pUserName.InnerText = objUser.UserName;
                pUserName2.InnerText = objUser.UserName;
                //if (!objUser.IsEnquiryViewAccess && !objUser.IsEnquiryEditAccess)
                //{
                //    Response.Redirect("Dashboard.aspx");
                //}
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Session["User"] = null;
            Response.Redirect("Login.aspx");
        }
    }
}