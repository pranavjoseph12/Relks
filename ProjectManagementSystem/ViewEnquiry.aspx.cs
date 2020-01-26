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
    public partial class ViewEnquiry : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Session["User"] == null)
            //{
            //    Response.Redirect("Login.aspx");
            //}
            //else
            //{
            //    UserModel objUser = new UserModel();
            //    objUser = (UserModel)Session["User"];
            //    //if (!objUser.IsEnquiryViewAccess && !objUser.IsEnquiryEditAccess)
            //    //{
            //    //    Response.Redirect("Dashboard.aspx");
            //    //}
            //}

            if (!IsPostBack)
            {
                if (Request.Params["EnquiryID"] == null || string.IsNullOrEmpty(Request.Params["EnquiryID"]))
                {
                    Response.Redirect("Enquiries.aspx");
                }
                else
                {
                    BindData(Convert.ToInt32(Request.Params["EnquiryID"]));
                    hdnEnqID.Value = Request.Params["EnquiryID"].ToString();
                }
            }
        }

        private void BindData(int enquiryId)
        {
            EnquiryBusinessLogic objStudent = new EnquiryBusinessLogic();
            var studentData = objStudent.GetEnquiryAndFollowUpbYEnquiryId(enquiryId);
            if (studentData != null)
            {
                lblName.Text = studentData.Name;
                lblAddress.Text = studentData.Address;
                lblDateOfEnquiry.Text = studentData.EnquiryDate;
                lblEmail.Text = studentData.Email;
                lblMobile.Text = studentData.Phone;
                lblResponse.Text = studentData.Response;
                lblNextFollowup.Text = studentData.NextFollowUpDate;
                txtComments.Text = studentData.Comments;
                if (studentData.FollowUps != null)
                {
                    grdFollowUp.DataSource = studentData.FollowUps;
                    grdFollowUp.DataBind();
                }
            }
        }
    }
}