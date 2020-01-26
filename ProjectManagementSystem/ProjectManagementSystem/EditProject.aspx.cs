using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;

namespace ProjectManagementSystem
{
    public partial class EditProject : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["ProjectID"]))
                {
                    GetUsersWithAccessForProject(Request.QueryString["ProjectID"].ToString());
                }
                else
                {
                    Response.Redirect("Projects.aspx");
                }
            }
        }
        public void GetUsersWithAccessForProject(string projectId)
        {
            ProjectBusinessLogic obj = new ProjectBusinessLogic();
            var result = obj.GetUsersWithAccessForProject(projectId);
            hdnUsersWithAccess.Value = result;
        }
    }
}