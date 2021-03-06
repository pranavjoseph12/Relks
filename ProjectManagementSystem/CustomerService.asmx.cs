﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using WebAPI.Models;
using WebAPI.BusinessLayer;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.IO;

namespace ProjectManagementSystem
{
    /// <summary>
    /// Summary description for CustomerService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class CustomerService : System.Web.Services.WebService
    {
        [WebMethod]
        public string GetAllCustomers(int pageNumber, string searchTerm, int numberOfRecords, int rating = 0)
        {
            CustomerBusinessActions obj = new CustomerBusinessActions();
            var result = obj.GetAllCustomers(pageNumber, searchTerm, numberOfRecords, rating);
            var json = new JavaScriptSerializer().Serialize(result);
            return json;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public bool DeleteCustomer(string customerId)
        {
            UserModel objUser = new UserModel();
            int userID = 0;
            if (HttpContext.Current.Session["User"] != null)
            {
                objUser = (UserModel)Session["User"];
                userID = objUser.UserId;
            }


            CustomerBusinessActions obj = new CustomerBusinessActions();
            var result = obj.DeleteCustomer(customerId, userID);
            return result;
        }
        [WebMethod]
        public string GetCustomerExcelReport(int pageNumber, string searchTerm, int numberOfRecords, int rating = 0)
        {
            CustomerBusinessActions obj = new CustomerBusinessActions();
            var result = obj.GetCustomerExcel(pageNumber,searchTerm,numberOfRecords,rating);
            var directory = HttpContext.Current.Server.MapPath("~/");
            var filename = Path.Combine(directory, "Download", "Customer.xlsx");
            File.WriteAllBytes(filename, result);
            return new JavaScriptSerializer().Serialize(filename);
        }
    }
}
