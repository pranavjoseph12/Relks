using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectManagementSystem
{
    public partial class Enquiries : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["type"]))
            {
                if (Request.QueryString["type"].ToString() == "today")
                {
                    hdnEnquiryTpe.Value = "today";
                }
                else if (Request.QueryString["type"].ToString() == "overdue")
                {
                    hdnEnquiryTpe.Value = "overdue";
                }
            }
        }
    }
}