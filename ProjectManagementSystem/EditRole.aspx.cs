using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAPI.BusinessLayer;
using WebAPI.Models;
using BusinessLayer;

namespace ProjectManagementSystem
{
    public partial class EditRole : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                Response.Redirect("Admin.aspx");
            }

            if (!Page.IsPostBack)
            {
                BindRoleData(Request.QueryString["id"].ToString());
            }
        }

        public void BindRoleData(string id)
        {
            RolesBusinessActions obj = new RolesBusinessActions();
            var result = obj.GetRoleDetailsByRoleId(id);
            if(result != null)
            {
                txtName.Text = result.RoleName;
                foreach (RolePermissionModel role in result.RolePermissions)
                {
                    if (role.ActionName == "Enquiry")
                    {
                        chkEnquiryDelete.Checked = Convert.ToBoolean(role.DeleteAccess);
                        chkEnquiryEdit.Checked = Convert.ToBoolean(role.EditAccess);
                        chkEnquiryView.Checked = Convert.ToBoolean(role.ViewAccess);
                    }
                    else if (role.ActionName == "Customer")
                    {
                        chkCustomerDelete.Checked = Convert.ToBoolean(role.DeleteAccess);
                        chkCustomerEdit.Checked = Convert.ToBoolean(role.EditAccess);
                        chkCustomerView.Checked = Convert.ToBoolean(role.ViewAccess);
                    }
                    else if (role.ActionName == "Projects")
                    {
                        chkProjectsDelete.Checked = Convert.ToBoolean(role.DeleteAccess);
                        chkProjectsEdit.Checked = Convert.ToBoolean(role.EditAccess);
                        chkProjectsView.Checked = Convert.ToBoolean(role.ViewAccess);
                    }
                    else if (role.ActionName == "OtherActivity")
                    {
                        chkOtherActivityDelete.Checked = Convert.ToBoolean(role.DeleteAccess);
                        chkOtherActivityEdit.Checked = Convert.ToBoolean(role.EditAccess);
                        chkOtherActivityView.Checked = Convert.ToBoolean(role.ViewAccess);
                    }
                    else if (role.ActionName == "TaskUpdate")
                    {
                        chkTaskUpdateDelete.Checked = Convert.ToBoolean(role.DeleteAccess);
                        chkTaskUpdateEdit.Checked = Convert.ToBoolean(role.EditAccess);
                        chkTaskUpdateView.Checked = Convert.ToBoolean(role.ViewAccess);
                    }
                    else if (role.ActionName == "Admin")
                    {
                        chkAdminDelete.Checked = Convert.ToBoolean(role.DeleteAccess);
                        chkAdminEdit.Checked = Convert.ToBoolean(role.EditAccess);
                        chkAdminView.Checked = Convert.ToBoolean(role.ViewAccess);
                    }
                    else if (role.ActionName == "ExpenseManager")
                    {
                        chkExpenseDelete.Checked = Convert.ToBoolean(role.DeleteAccess);
                        chkExpenseEdit.Checked = Convert.ToBoolean(role.EditAccess);
                        chkExpenseView.Checked = Convert.ToBoolean(role.ViewAccess);
                    }
                    else if (role.ActionName == "Roles")
                    {
                        chkRolesDelete.Checked = Convert.ToBoolean(role.DeleteAccess);
                        chkRolesEdit.Checked = Convert.ToBoolean(role.EditAccess);
                        chkRolesView.Checked = Convert.ToBoolean(role.ViewAccess);
                    }
                    else if (role.ActionName == "Reports")
                    {
                        chkReportDelete.Checked = Convert.ToBoolean(role.DeleteAccess);
                        chkReportEdit.Checked = Convert.ToBoolean(role.EditAccess);
                        chkReportView.Checked = Convert.ToBoolean(role.ViewAccess);
                    }
                    else if (role.ActionName == "AssignUserToProject")
                    {
                        chkUserAccessProjectDelete.Checked = Convert.ToBoolean(role.DeleteAccess);
                        chkUserAccessProjectView.Checked = Convert.ToBoolean(role.ViewAccess);
                        chkUserAccessProjectEdit.Checked = Convert.ToBoolean(role.EditAccess);
                    }
                    else if (role.ActionName.Equals("Income",StringComparison.OrdinalIgnoreCase))
                    {
                        chkIncomeDelete.Checked = Convert.ToBoolean(role.DeleteAccess);
                        chkIncomeView.Checked = Convert.ToBoolean(role.ViewAccess);
                        chkIncomeEdit.Checked = Convert.ToBoolean(role.EditAccess);
                    }
                    else if (role.ActionName.Equals("Quotation", StringComparison.OrdinalIgnoreCase))
                    {
                        chkQuotationDelete.Checked = Convert.ToBoolean(role.DeleteAccess);
                        chkQuotationView.Checked = Convert.ToBoolean(role.ViewAccess);
                        chkQuotationEdit.Checked = Convert.ToBoolean(role.EditAccess);
                    }
                }
            }
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

            isSuccess = objRole.UpdateRole(Request.QueryString["id"].ToString(), txtName.Text, chkEnquiryView.Checked, chkEnquiryEdit.Checked, chkEnquiryDelete.Checked,
                chkCustomerView.Checked, chkCustomerEdit.Checked, chkCustomerDelete.Checked, chkProjectsView.Checked, chkProjectsEdit.Checked, chkProjectsDelete.Checked,
                chkOtherActivityView.Checked, chkOtherActivityEdit.Checked, chkOtherActivityDelete.Checked,
                chkTaskUpdateView.Checked, chkTaskUpdateEdit.Checked, chkTaskUpdateDelete.Checked,
                chkAdminView.Checked, chkAdminEdit.Checked, chkAdminDelete.Checked, chkExpenseView.Checked, chkExpenseEdit.Checked, chkExpenseDelete.Checked,
                chkRolesView.Checked, chkRolesEdit.Checked, chkRolesDelete.Checked, chkReportView.Checked, chkReportEdit.Checked, chkReportDelete.Checked,
                chkUserAccessProjectView.Checked, chkUserAccessProjectEdit.Checked,chkUserAccessProjectDelete.Checked,chkIncomeView.Checked,chkIncomeEdit.Checked, chkIncomeDelete.Checked,chkQuotationView.Checked,chkQuotationEdit.Checked,chkQuotationDelete.Checked,
                objUser.UserId);

            //isSuccess = objRole.CreateRole(txtName.Text, chkEnquiryView.Checked, chkEnquiryEdit.Checked, chkStudentView.Checked, chkStudentEdit.Checked,
            //                chkReportView.Checked, chkReportEdit.Checked, false, false, chkStaffView.Checked, chkStaffEdit.Checked, false, false, chkPaymentView.Checked,
            //                chkPaymentEdit.Checked);

            if (isSuccess)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "SuccessRedirect", "alert('Role Updated Successsfully'); window.location.href = 'Admin.aspx';", true);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorRedirect", "alert('Something went wrong. Please try again later.');", true);
            }
        }
    }
}