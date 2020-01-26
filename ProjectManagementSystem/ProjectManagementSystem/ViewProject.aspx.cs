using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAPI.Models;


namespace ProjectManagementSystem
{
    public partial class ViewProject : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Params["ProjectID"] == null || string.IsNullOrEmpty(Request.Params["ProjectID"]))
                {
                    Response.Redirect("Projects.aspx");
                }
                else
                {
                    BindData(Request.Params["ProjectID"].ToString());
                    hdnProjectID.Value = Request.Params["ProjectID"].ToString();
                }
            }
        }

        private void BindData(string projectID)
        {
            ProjectBusinessLogic obj = new ProjectBusinessLogic();
            var result = obj.GetProjectDetailsById(projectID);
            if (result != null)
            {
                lbalActualEnd.Text = result.ActualEndDate;
                lblActualStart.Text = result.ActualStartDate;
                lblAddress.Text = result.Address;
                lblEstimate.Text = result.ProjectEstimate;
                lblExpectedEnd.Text = result.EstimatedEndDate;
                lblExpectedStart.Text = result.EstimatedStartDate;
                lblName.Text = result.ProjectName;
                lblOtherContact1Name.Text = result.Contact1Name;
                lblOtherContact1Number.Text = result.Contact1number;
                lblOtherContact2Name.Text = result.Contact2Name;
                lblOtherContact2Number.Text = result.Contact2Number;
                lblOtherContact3Name.Text = result.Contact3Name;
                lblOtherContact3Number.Text = result.Contact3Number;
                lblPinCode.Text = result.PinCode;
                lblPrimaryContactName.Text = result.PrimaryContactName;
                lblPrimaryContactNumber.Text = result.PrimaryNumber;
            }

            //var tasks = obj.GetAllProjectTasksByProjectd(projectID);
            //grdFollowUp.DataSource = tasks;
            //grdFollowUp.DataBind();


        }
    }
}