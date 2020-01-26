using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAPI.Models;
using WebAPI.BusinessLayer;

namespace ProjectManagementSystem
{
    public partial class AddRole : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            UserModel objUser = new UserModel();

            if (Session["User"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                objUser = (UserModel)Session["User"];
            }


            RolesBusinessActions objRole = new RolesBusinessActions();
            bool isSuccess = false;

            isSuccess = objRole.CreateRole(txtName.Text, chkEnquiryView.Checked, chkEnquiryEdit.Checked, chkEnquiryDelete.Checked,
                chkCustomerView.Checked,chkCustomerEdit.Checked,chkCustomerDelete.Checked, chkProjectsView.Checked, chkProjectsEdit.Checked, chkProjectsDelete.Checked,
                chkOtherActivityView.Checked, chkOtherActivityEdit.Checked, chkOtherActivityDelete.Checked,
                chkTaskUpdateView.Checked, chkTaskUpdateEdit.Checked, chkTaskUpdateDelete.Checked,
                chkAdminView.Checked, chkAdminEdit.Checked, chkAdminDelete.Checked, chkExpenseView.Checked, chkExpenseEdit.Checked, chkExpenseDelete.Checked,
                chkRolesView.Checked, chkRolesEdit.Checked, chkRolesDelete.Checked, chkReportView.Checked, chkReportEdit.Checked, chkReportDelete.Checked,
                chkUserAccessProjectView.Checked, chkUserAccessProjectEdit.Checked, chkUserAccessProjectDelete.Checked,
                objUser.UserId);

            //isSuccess = objRole.CreateRole(txtName.Text, chkEnquiryView.Checked, chkEnquiryEdit.Checked, chkStudentView.Checked, chkStudentEdit.Checked,
            //                chkReportView.Checked, chkReportEdit.Checked, false, false, chkStaffView.Checked, chkStaffEdit.Checked, false, false, chkPaymentView.Checked,
            //                chkPaymentEdit.Checked);

            if (isSuccess)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "SuccessRedirect", "alert('Role Added Successsfully'); window.location.href = 'Admin.aspx';", true);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorRedirect", "alert('Something went wrong. Please try again later.');", true);
            }
        }
    }
}