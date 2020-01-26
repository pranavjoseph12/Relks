using WebAPI.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAPI.Models;


namespace ProjectManagementSystem
{
    public partial class ViewCustomer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Params["CustomerID"] == null || string.IsNullOrEmpty(Request.Params["CustomerID"]))
                {
                    Response.Redirect("Customer.aspx");
                }
                else
                {
                    BindData(Convert.ToInt32(Request.Params["CustomerID"]));
                }
            }
        }

        private void BindData(int customerId)
        {
            CustomerBusinessActions obj = new CustomerBusinessActions();
            var result = obj.GetCustomerDetailsById(customerId);
            if (result != null)
            {
                lblName.Text = result.Name;
                lblAddress.Text = result.Address;
                lblEmail.Text = result.Email;
                lblMobile.Text = result.Phone;
                lblPIN.Text = result.PinCode;
                lblRating.Text = result.Rating.ToString();
            }
        }
    }
}