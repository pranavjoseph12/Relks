using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using WebAPI.BusinessLayer;
using WebAPI.Models;

namespace ProjectManagementSystem
{
    public partial class AddEnquiry : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindCustomer();
                BindCourse();
            }
        }

        private void BindCustomer()
        {
            CustomerBusinessActions obj = new CustomerBusinessActions();
            var result = obj.GetAllCustomerNameAndId();
            ddlCustomer.DataSource = result;
            ddlCustomer.DataTextField = "Name";
            ddlCustomer.DataValueField = "CustomerId";
            ddlCustomer.DataBind();
            ddlCustomer.Items.Insert(0, new ListItem("New Student", "0"));
        }
        private void BindCourse()
        {
            CustomerBusinessActions obj = new CustomerBusinessActions();
            UserModel objUser = new UserModel();
            string centerId = "0";
            if (HttpContext.Current.Session["User"] != null)
            {
                objUser = (UserModel)Session["User"];
                if (objUser.RoleId != 1)
                    centerId = objUser.CenterId.ToString();
            }
            var result = obj.GetAllCourse(centerId);
            ddlCourse.DataSource = result;
            ddlCourse.DataTextField = "Name";
            ddlCourse.DataValueField = "Id";
            ddlCourse.DataBind();
            ddlCourse.Items.Insert(0, new ListItem("Select", "0"));
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            EnquiryBusinessLogic obj = new EnquiryBusinessLogic();
            var result = obj.AddEnquiry(txtName.Text, txtContactNumber.Text, txtAddress.InnerText, txtEmail.Text, ddlResponse.Value, txtNextFollowUp.Text, txtComments.InnerText, "1",ddlCustomer.SelectedValue,ddlCourse.SelectedValue);
            if (result == true)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "SuccessRedirect", "alert('Enquiry Added Successsfully'); window.location.href = 'Enquiries.aspx';", true);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorRedirect", "alert('Something Went wrong. Please try again later');", true);
            }
        }



        protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCustomer.SelectedValue == "0")
            {
                txtAddress.InnerText = "";
                txtContactNumber.Text = "";
                txtEmail.Text = "";
                txtName.Text = "";
            }
            else
            {
                CustomerBusinessActions obj = new CustomerBusinessActions();
                var result = obj.GetCustomerDetailsById(Convert.ToInt32(ddlCustomer.SelectedValue));
                txtAddress.InnerText = result.Address;
                txtContactNumber.Text = result.Phone;
                txtEmail.Text = result.Email;
                txtName.Text = result.Name;
            }
        }
    }
}