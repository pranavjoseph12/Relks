using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.DatabaseLayer;
using WebAPI.Models;
using System.Data;
using BusinessLayer;
using Models;

namespace WebAPI.BusinessLayer
{
    public class CustomerBusinessActions
    {
        public List<CustomerModel> GetAllCustomers(int pageNumber, string searchTerm, int numberOfRecords, int rating,string centerId)
        {
            CustomerDataAccess obj = new CustomerDataAccess();
            var result = obj.GetAllCustomers(pageNumber, searchTerm, numberOfRecords, rating,centerId);
            return ConvertCustomerToList(result);
        }
        public byte[] GetCustomerExcel(int pageNumber, string searchTerm, int numberOfRecords, int rating)
        {
            CustomerDataAccess obj = new CustomerDataAccess();
            var result = obj.GetAllCustomers(pageNumber, searchTerm, numberOfRecords, rating,"0");
            return EnquiryBusinessLogic.ExportExcel(MapDataTableForCustomer(result), "Student", false, "");
        }
        private DataTable MapDataTableForCustomer(DataTable data)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Contact Number");
            dt.Columns.Add("Email");
            dt.Columns.Add("Rating");
            int i = 0;
            foreach (DataRow dr in data.Rows)
            {
                dt.Rows.Add();
                dt.Rows[i]["Name"] = data.Rows[i]["Name"] != null ? data.Rows[i]["Name"].ToString() : string.Empty;
                dt.Rows[i]["Contact Number"] = data.Rows[i]["Phone"] != null ? data.Rows[i]["Phone"].ToString() : string.Empty;
                dt.Rows[i]["Email"] = data.Rows[i]["Email"] != null ? data.Rows[i]["Email"].ToString() : string.Empty;
                dt.Rows[i]["Rating"] = data.Rows[i]["Rating"] != null ? data.Rows[i]["Rating"].ToString() : string.Empty;
                i++;
            }
            return dt;
        }
        public DataTable GetAllCustomerNameAndId()
        {
            CustomerDataAccess obj = new CustomerDataAccess();
            return obj.GetAllCustomerNameAndId();
        }
        public DataTable GetAllCourse(string centerId)
        {
            CustomerDataAccess obj = new CustomerDataAccess();
            return obj.GetAllCourse(centerId);
        }
        public List<Center> GetAllCenter(string centerId)
        {

            CustomerDataAccess obj = new CustomerDataAccess();
            var result= obj.GetAllCourse(centerId);
            List<Center> centers = new List<Center>();
            Center center;
            foreach (DataRow row in result.Rows)
            {
                center = new Center();
                center.Id = Convert.ToInt32(row["Id"].ToString());
                center.Name = row["Name"].ToString();
                centers.Add(center);
            }
            return centers;
        }

        public bool AddCustomer(string name, string number, string address, string email, string pinCode, string rating, string addedBy, int enqId,string courseId)
        {
            CustomerDataAccess obj = new CustomerDataAccess();
            var result = obj.AddCustomer(name, number, address, email, pinCode, rating, addedBy, enqId,courseId);
            return result;
        }

        #region Delete Customer

        public bool DeleteCustomer(string customerId, int updatedBy)
        {
            bool isSuccess = false;
            CustomerDataAccess objEnq = new CustomerDataAccess();
            isSuccess = objEnq.DeleteCustomer(customerId, updatedBy);
            return isSuccess;
        }

        #endregion

        #region Get Cutomer details by Customer Id

        public CustomerModel GetCustomerDetailsById(int id)
        {
            CustomerDataAccess obj = new CustomerDataAccess();
            var result = obj.GetCustomerDetailsById(id);
            return CreateCustomerModel(result);
        }

        #endregion

        private CustomerModel CreateCustomerModel(DataTable dt)
        {
            var customer = new CustomerModel();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    customer.Email = dr["Email"].ToString();
                    //customer.CustomerId = Convert.ToInt32(dr["CustomerId"]);
                    customer.Name = dr["Name"].ToString();
                    customer.Phone = dr["Phone"].ToString();
                    customer.Address = dr["Address"].ToString();
                    customer.Center = dr["CenterName"].ToString();
                    customer.PinCode = dr["PinCode"].ToString();
                    customer.Rating = Convert.ToInt32(dr["Rating"]);
                }
            }

            return customer;
        }


        private List<CustomerModel> ConvertCustomerToList(DataTable dt)
        {
            List<CustomerModel> result = new List<CustomerModel>();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    var customer = new CustomerModel();
                    customer.Email = dr["Email"].ToString();
                    customer.CustomerId = Convert.ToInt32(dr["CustomerId"]);
                    customer.Name = dr["Name"].ToString();
                    customer.Phone = dr["Phone"].ToString();
                    customer.TotalCount = Convert.ToInt32(dr["TotalCount"]);
                    customer.Rating = Convert.ToInt32(dr["Rating"]);
                    result.Add(customer);
                }
            }

            return result;
        }
    }
}
