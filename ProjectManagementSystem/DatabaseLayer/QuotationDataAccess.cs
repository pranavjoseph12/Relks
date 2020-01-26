using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.DatabaseLayer
{
    public class QuotationDataAccess
    {
        public bool AddItem(string name, string description, string unit, string specification, int  unitRate, int labour, int addedBy, int margin, int tax,List<int> comboItems,string selectedItem)
        {
            bool isSuccess = true;
            try
            {
                DataTable dtcomboItems = new DataTable();
                dtcomboItems.Columns.Add("ItemId", typeof(Int32));
                foreach(int item in comboItems)
                {
                    dtcomboItems.Rows.Add(item);
                }
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "AddItem";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@Description", description);
                        cmd.Parameters.AddWithValue("@Unit", unit);
                        cmd.Parameters.AddWithValue("@Specification", specification);
                        cmd.Parameters.AddWithValue("@UnitRate", unitRate);
                        cmd.Parameters.AddWithValue("@Labour", labour);
                        cmd.Parameters.AddWithValue("@AddedBy", addedBy);
                        cmd.Parameters.AddWithValue("@Margin", margin);
                        cmd.Parameters.AddWithValue("@Tax", tax);
                        cmd.Parameters.AddWithValue("@ComboItems", dtcomboItems);
                        cmd.Parameters.AddWithValue("@SelectedItem", selectedItem);
                        cmd.Connection = conn;
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                //LogError(ex);
                isSuccess = false;
            }

            return isSuccess;
        }
        public DataTable GetItemDetails(string itemId)
        {
            DataTable dtProjects = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "GetItemDetails";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@itemId", itemId);
                        cmd.Connection = conn;
                        conn.Open();
                        using (SqlDataAdapter ada = new SqlDataAdapter(cmd))
                        {
                            ada.Fill(dtProjects);
                        }

                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                //LogError(ex);
            }

            return dtProjects;
        }
        public DataTable GetAllItems(string searchTerm, int numberOfRecords, int pageNumber)
        {
            DataTable dtProjects = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "GetAllItems";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@searchTerm", searchTerm);
                        cmd.Parameters.AddWithValue("@PageNumber", pageNumber);
                        cmd.Parameters.AddWithValue("@NumberOfRecords", numberOfRecords);
                        cmd.Connection = conn;
                        conn.Open();
                        using (SqlDataAdapter ada = new SqlDataAdapter(cmd))
                        {
                            ada.Fill(dtProjects);
                        }

                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                //LogError(ex);
            }

            return dtProjects;
        }
        public DataTable GetItemsById(List<int> Items)
        {
            DataTable dtItems = new DataTable();
            try
            {
                DataTable dtcomboItems = new DataTable();
                dtcomboItems.Columns.Add("ItemId", typeof(Int32));
                foreach (int item in Items)
                {
                    dtcomboItems.Rows.Add(item);
                }
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "GetItemsById";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ItemIds", dtcomboItems);
                        cmd.Connection = conn;
                        conn.Open();
                        using (SqlDataAdapter ada = new SqlDataAdapter(cmd))
                        {
                            ada.Fill(dtItems);
                        }

                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                //LogError(ex);
            }

            return dtItems;
        }
        public DataTable GetAllQuotations(string CustName,int numberOfRecords,int pageNumber)
        {
            DataTable dtQuotations = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "GetAllQuotations";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CustomerName", CustName);
                        cmd.Parameters.AddWithValue("@PageNumber", pageNumber);
                        cmd.Parameters.AddWithValue("@NumberOfRecords", numberOfRecords);
                        cmd.Connection = conn;
                        conn.Open();
                        using (SqlDataAdapter ada = new SqlDataAdapter(cmd))
                        {
                            ada.Fill(dtQuotations);
                        }

                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                //LogError(ex);
            }

            return dtQuotations;
        }
        public DataTable GetQuotationUserVersions(string customerId, bool isCustomer)
        {
            DataTable dtQuotations = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "GetQuotationUserVersions";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@customerId", customerId);
                        cmd.Parameters.AddWithValue("@IsCustomer", isCustomer);
                        cmd.Connection = conn;
                        conn.Open();
                        using (SqlDataAdapter ada = new SqlDataAdapter(cmd))
                        {
                            ada.Fill(dtQuotations);
                        }

                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                //LogError(ex);
            }

            return dtQuotations;
        }
        public DataTable GetQuotationDetailsById(string quotationId)
        {
            DataTable dtQuotation = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "GetQuotationDetailsById";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@quotationId", quotationId);
                        cmd.Connection = conn;
                        conn.Open();
                        using (SqlDataAdapter ada = new SqlDataAdapter(cmd))
                        {
                            ada.Fill(dtQuotation);
                        }

                        conn.Close();
                    }
                }
            }
            catch(Exception ex)
            {

            }
            return dtQuotation;
        }
        public DataTable GetQuotationVersionDetails(string customerId, string version,bool isCustomer)
        {
            DataTable dtQuotation = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "GetQuotationVersionDetails";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@customerId", customerId);
                        cmd.Parameters.AddWithValue("@version", version);
                        cmd.Parameters.AddWithValue("@isCustomer", isCustomer);
                        cmd.Connection = conn;
                        conn.Open();
                        using (SqlDataAdapter ada = new SqlDataAdapter(cmd))
                        {
                            ada.Fill(dtQuotation);
                        }

                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return dtQuotation;
        }
        public bool SaveQuotation(WebAPI.Models.Quotation quotation)
        {
            var success = true;
            DataTable dtItems = new DataTable();
            try
            {
                dtItems.Columns.Add("ItemId", typeof(Int32));
                dtItems.Columns.Add("Name", typeof(string));
                dtItems.Columns.Add("Description", typeof(string));
                dtItems.Columns.Add("Unit", typeof(string));
                dtItems.Columns.Add("Quantity", typeof(Int32));
                dtItems.Columns.Add("UnitRate", typeof(float));
                dtItems.Columns.Add("Rate", typeof(float));
                dtItems.Columns.Add("ItemMargin", typeof(float));
                dtItems.Columns.Add("ItemTotal", typeof(float));
                dtItems.Columns.Add("IsItem", typeof(bool));
                DataRow dr;
                foreach (var item in quotation.Items)
                {
                    dr = dtItems.NewRow();
                    dr["ItemId"] = item.ItemId;
                    dr["Name"] = item.Name;
                    dr["Description"] = item.Description;
                    dr["Unit"] = item.Unit;
                    dr["Quantity"] = item.Quantity;
                    dr["UnitRate"] = item.UnitRate;
                    dr["Rate"] = item.Rate;
                    dr["ItemMargin"] = item.Margin;
                    dr["ItemTotal"] = item.Total;
                    dr["IsItem"] = item.IsItem;
                    dtItems.Rows.Add(dr);

                }
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "AddQuotation";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@GrossMargin", quotation.GrossMargin);
                        cmd.Parameters.AddWithValue("@CustomerId", quotation.CustomerId);
                        cmd.Parameters.AddWithValue("@Total", quotation.Total);
                        cmd.Parameters.AddWithValue("@Version", quotation.Version);
                        cmd.Parameters.AddWithValue("@IsCustomer", quotation.IsCustomer);
                        cmd.Parameters.AddWithValue("@QuotationItemdata", dtItems);
                        cmd.Connection = conn;
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                //LogError(ex);
                success = false;
            }

            return success;
        }
    }
}
