using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAPI.Models;

namespace ProjectManagementSystem
{
    public partial class Roles : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Role role = new Role();
            role.RoleName = txtRoleName.Text;
            role.Deletable = false;
            role.Editable = false;
            role.Viewable = false;

            var result = new BusinessLayer.RoleBusiness().AddRole(role);
            if (result == true)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "SuccessRedirect", "alert('Role Added Successsfully'); window.location.href = 'Roles.aspx';", true);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorRedirect", "alert('Something Went wrong. Please try again later');", true);
            }
        }
    }
}