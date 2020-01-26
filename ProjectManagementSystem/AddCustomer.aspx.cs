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
    public partial class AddCustomer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["EnquiryID"] != null && Request.QueryString["EnquiryID"].ToString() != string.Empty)
            {
                BindEnquiryDetails(Request.QueryString["EnquiryID"].ToString());
            }
        }

        private void BindEnquiryDetails(string enquiryId)
        {
            EnquiryBusinessLogic obj = new EnquiryBusinessLogic();
            var result = obj.GetEnquiryAndFollowUpbYEnquiryId(Convert.ToInt32(enquiryId));
            txtName.Text = result.Name;
            txtContactNumber.Text = result.Phone;
            txtEmail.Text = result.Email;
            txtAddress.InnerText = result.Address;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            UserModel objUser = new UserModel();
            if (Session["User"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                objUser = (UserModel)Session["User"];
                if (!objUser.IsValidUser)
                {
                    Response.Redirect("Login.aspx");
                }
            }

            int enquiryId = 0;

            if (Request.QueryString["EnquiryID"] != null && Request.QueryString["EnquiryID"].ToString() != string.Empty)
            {
                try
                {
                    enquiryId = Convert.ToInt32(Request.QueryString["EnquiryID"]);
                }
                catch (Exception ex)
                {

                }
            }


            CustomerBusinessActions obj = new CustomerBusinessActions();
            var result = obj.AddCustomer(txtName.Text, txtContactNumber.Text, txtAddress.InnerText, txtEmail.Text, txtPinCode.Text, ddlRating.SelectedValue, objUser.UserId.ToString(), enquiryId);
            if (result == true)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "SuccessRedirect", "alert('Customer Added Successsfully'); window.location.href = 'Customer.aspx';", true);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorRedirect", "alert('Something Went wrong. Please try again later');", true);
            }
        }

    }
}