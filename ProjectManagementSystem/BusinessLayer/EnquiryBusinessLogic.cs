using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.DatabaseLayer;
using WebAPI.Models;
using System.Data;
using OfficeOpenXml.Style;
using OfficeOpenXml;

namespace BusinessLayer
{
    public class EnquiryBusinessLogic
    {
        public bool AddEnquiry(string name, string number, string address, string email, string response, string nextFollowDate, string comments, string userId, string customerId)
        {
            EnquiryDataAccess obj = new EnquiryDataAccess();
            var result = obj.AddEnquiry(name, number, address, email, response, nextFollowDate, comments, userId, customerId);
            return result;
        }

        public List<EnquiryModel> GetAllEnquiries(string enquiryType, int pageNumber, string searchTerm, int numberOfRecords, string fromDate, string toDate)
        {
            EnquiryDataAccess obj = new EnquiryDataAccess();
            var result = obj.GetAllEnquiries(enquiryType, pageNumber, searchTerm, numberOfRecords,fromDate,toDate);
            return ConvertEnquiries(result);
        }

        public EnquiryNotificationModel GetEnquiryNotifications()
        {
            EnquiryDataAccess obj = new EnquiryDataAccess();
            var result = obj.GetEnquiryNotifications();
            return ConvertEnquiryNotifications(result);
        }
        public byte[] GetEnquiryExcel(string enquiryType, int pageNumber, string searchTerm, int numberOfRecords, string fromDate, string toDate)
        {
            EnquiryDataAccess obj = new EnquiryDataAccess();
            var result = obj.GetAllEnquiries(enquiryType, 1, searchTerm, 10000, fromDate, toDate);
            string[] columns = { "Name", "Phone", "Email", "Response", "DateAdded" };
            return ExportExcel(MapDataTableForExport(result), "Enquiry",false,"");
        }
        private DataTable MapDataTableForExport(DataTable data)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Contact Number");
            dt.Columns.Add("Email");
            dt.Columns.Add("Response");
            dt.Columns.Add("Enquiry Date");
           // dt.Columns.Add("Paid Date");
            int i = 0;
            foreach (DataRow dr in data.Rows)
            {
                dt.Rows.Add();
                dt.Rows[i]["Name"] = data.Rows[i]["Name"]!=null ?data.Rows[i]["Name"].ToString():string.Empty;
                dt.Rows[i]["Contact Number"] = data.Rows[i]["Phone"]!=null? data.Rows[i]["Phone"].ToString():string.Empty;
                dt.Rows[i]["Email"] = data.Rows[i]["Email"]!=null? data.Rows[i]["Email"].ToString():string.Empty;
                dt.Rows[i]["Response"] = data.Rows[i]["Response"]!=null? data.Rows[i]["Response"].ToString():string.Empty;
                dt.Rows[i]["Enquiry Date"] = DateTime.Parse(data.Rows[i]["DateAdded"].ToString()).ToString("dd/MM/yyyy").Trim();
                // dt.Rows[i]["Paid Date"] = DateTime.Parse(result.Rows[i]["PaidDate"].ToString()).ToString("dd/MM/yyyy").Trim();
                i++;
            }
            return dt;
        }
        public static byte[] ExportExcel(DataTable dataTable, string heading = "", bool showSrNo = false, params string[] columnsToTake)
        {

            byte[] result = null;
            using (ExcelPackage package = new ExcelPackage())
            {
                int startRowFrom = 0;
                ExcelWorksheet workSheet = package.Workbook.Worksheets.Add(String.Format("{0} Data", heading));
                if (heading.ToUpper().Contains("EXPENSE") || heading.ToUpper().Contains("INCOME"))
                {
                    startRowFrom = String.IsNullOrEmpty(heading) ? 3 : 5;
                }
                else
                {
                    startRowFrom = String.IsNullOrEmpty(heading) ? 1 : 3;
                }

                //if (showSrNo)
                //{
                //    DataColumn dataColumn = dataTable.Columns.Add("#", typeof(int));
                //    dataColumn.SetOrdinal(0);
                //    int index = 1;
                //    foreach (DataRow item in dataTable.Rows)
                //    {
                //        item[0] = index;
                //        index++;
                //    }
                //}


                // add the content into the Excel file  
                workSheet.Cells["A" + startRowFrom].LoadFromDataTable(dataTable, true);

                // autofit width of cells with small content  
                int columnIndex = 1;
                foreach (DataColumn column in dataTable.Columns)
                {
                    ExcelRange columnCells = workSheet.Cells[workSheet.Dimension.Start.Row, columnIndex, workSheet.Dimension.End.Row, columnIndex];
                    int maxLength = columnCells.Max(cell => cell.Value==null?0: cell.Value.ToString().Count());
                    if (maxLength < 150)
                    {
                        workSheet.Column(columnIndex).AutoFit();
                    }


                    columnIndex++;
                }

                // format header - bold, yellow on black  
                using (ExcelRange r = workSheet.Cells[startRowFrom, 1, startRowFrom, dataTable.Columns.Count])
                {
                    r.Style.Font.Color.SetColor(System.Drawing.Color.White);
                    r.Style.Font.Bold = true;
                    r.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    r.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#1fb5ad"));
                }

                // format cells - add borders  
                using (ExcelRange r = workSheet.Cells[startRowFrom + 1, 1, startRowFrom + dataTable.Rows.Count, dataTable.Columns.Count])
                {
                    r.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    r.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    r.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    r.Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    r.Style.Border.Top.Color.SetColor(System.Drawing.Color.Black);
                    r.Style.Border.Bottom.Color.SetColor(System.Drawing.Color.Black);
                    r.Style.Border.Left.Color.SetColor(System.Drawing.Color.Black);
                    r.Style.Border.Right.Color.SetColor(System.Drawing.Color.Black);
                }

                // removed ignored columns  
                //for (int i = dataTable.Columns.Count - 1; i >= 0; i--)
                //{
                //    if (i == 0 && showSrNo)
                //    {
                //        continue;
                //    }
                //    if (!columnsToTake.Contains(dataTable.Columns[i].ColumnName))
                //    {
                //        workSheet.DeleteColumn(i + 1);
                //    }
                //}

                if (!String.IsNullOrEmpty(heading))
                {
                    workSheet.Cells["A1"].Value = heading;
                    workSheet.Cells["A1"].Style.Font.Size = 20;

                    workSheet.InsertColumn(1, 1);
                    workSheet.InsertRow(1, 1);
                    workSheet.Column(1).Width = 5;
                    if (heading.ToUpper().Contains("EXPENSE"))
                    {
                        workSheet.Cells["A4"].Value = "Total Expense : "+ columnsToTake[0];
                       // workSheet.Cells["B4"].Value = columnsToTake[0];
                        workSheet.Cells["A4"].Style.Font.Size = 15;

                    }
                    else if(heading.ToUpper().Contains("INCOME"))
                    {
                        workSheet.Cells["A4"].Value = "Total Income : " + columnsToTake[0];
                        workSheet.Cells["A4"].Style.Font.Size = 15;
                    }
                }
               
                result = package.GetAsByteArray();
            }

            return result;
        }
        #region Delete enquiry

        public bool DeleteEnquiry(string enquiryID, int updatedBy)
        {
            bool isSuccess = false;
            EnquiryDataAccess objEnq = new EnquiryDataAccess();
            isSuccess = objEnq.DeleteEnquiry(enquiryID, updatedBy);
            return isSuccess;
        }

        #endregion

        #region Add Category

        public bool AddCategory(string categoryName, int updatedBy)
        {
            bool isSuccess = false;
            EnquiryDataAccess objEnq = new EnquiryDataAccess();
            isSuccess = objEnq.AddCategory(categoryName, updatedBy);
            return isSuccess;
        }

        #endregion

        #region Get All Enquiry And Followups

        public EnquiryModel GetEnquiryAndFollowUpbYEnquiryId(int enquiryID, bool showTime = false)
        {
            EnquiryDataAccess objEnq = new EnquiryDataAccess();
            return GetEnquiryAndFollowUpAsList(objEnq.GetEnquiryAndFollowUpbYEnquiryId(enquiryID), showTime);
        }

        #endregion

        #region Add Follow up

        public bool AddFollowUp(string enqID, string comment, string nextFollowUpDate, string response, int addedBy)
        {
            bool isSuccess = false;
            EnquiryDataAccess objEnq = new EnquiryDataAccess();
            isSuccess = objEnq.AddFollowUp(enqID, comment, nextFollowUpDate, response, addedBy);
            return isSuccess;
        }

        #endregion

        private List<EnquiryModel> ConvertEnquiries(DataTable dt)
        {
            List<EnquiryModel> result = new List<EnquiryModel>();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    var enquiry = new EnquiryModel();
                    enquiry.Email = dr["Email"].ToString();
                    enquiry.EnquiryDate = Convert.ToDateTime(dr["DateAdded"]).ToString("MM/dd/yyyy");
                    enquiry.EnquiryId = Convert.ToInt32(dr["enquiryid"]);
                    enquiry.QuotationVersion = Convert.ToInt32(dr["VersionNumber"]);
                    enquiry.Name = dr["Name"].ToString();
                    enquiry.NextFollowUpDate = Convert.ToDateTime(dr["NextFollowUpDate"]).ToString("MM/dd/yyyy");
                    enquiry.Phone = dr["Phone"].ToString();
                    enquiry.Response = dr["Response"].ToString();
                    enquiry.Comments = dr["Comments"].ToString();
                    enquiry.TotalEnquiries = Convert.ToInt32(dr["TotalCount"]);
                    enquiry.ExistingCustomerId = Convert.ToInt32(dr["CustomerId"]);
                    result.Add(enquiry);
                }
            }

            return result;
        }

        private EnquiryModel GetEnquiryAndFollowUpAsList(DataSet ds, bool showTime)
        {
            EnquiryModel obj = new EnquiryModel();
            DataTable dtEnquiry = new DataTable();

            if (ds != null && ds.Tables.Count > 0)
            {
                dtEnquiry = ds.Tables[0];

                if (dtEnquiry != null && dtEnquiry.Rows.Count > 0)
                {
                    obj.EnquiryId = Convert.ToInt32(dtEnquiry.Rows[0]["EnquiryId"]);
                    obj.Name = dtEnquiry.Rows[0]["Name"].ToString();
                    obj.Phone = dtEnquiry.Rows[0]["Phone"].ToString();
                    obj.EnquiryDate = Convert.ToDateTime(dtEnquiry.Rows[0]["DateAdded"]).ToString("dd/MM/yyyy");
                    obj.Address = dtEnquiry.Rows[0]["Address"].ToString();
                    obj.Response = dtEnquiry.Rows[0]["Response"].ToString();
                    obj.Comments = dtEnquiry.Rows[0]["Comments"].ToString();
                    obj.Email = dtEnquiry.Rows[0]["Email"].ToString();
                    obj.NextFollowUpDate = Convert.ToDateTime(dtEnquiry.Rows[0]["NextFollowUpDate"]).ToString("dd/MM/yyyy");
                }

                DataTable dtFollowUp = new DataTable();
                if (ds.Tables.Count > 1)
                {
                    List<FollowUpModel> followUps = new List<FollowUpModel>();
                    obj.FollowUps = new List<FollowUpModel>();
                    dtFollowUp = ds.Tables[1];
                    if (dtFollowUp.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtFollowUp.Rows.Count; i++)
                        {
                            FollowUpModel objFollow = new FollowUpModel();
                            objFollow.Comments = dtFollowUp.Rows[i]["Comment"].ToString();
                            //objFollow.FollowUpBy = dtFollowUp.Rows[i]["StaffName"].ToString();

                            objFollow.DateAdded = Convert.ToDateTime(dtFollowUp.Rows[i]["DateAdded"]).ToString("dd/MM/yyyy");
                            objFollow.NextFollowUpDate = Convert.ToDateTime(dtFollowUp.Rows[i]["NextFollowUp"]).ToString("dd/MM/yyyy");
                            objFollow.Response = dtFollowUp.Rows[i]["Response"].ToString();
                            followUps.Add(objFollow);
                        }
                    }
                    obj.FollowUps = followUps;
                }
            }

            return obj;
        }

        private EnquiryNotificationModel ConvertEnquiryNotifications(DataTable dt)
        {
            EnquiryNotificationModel result = new EnquiryNotificationModel();
            if (dt != null && dt.Rows.Count > 0)
            {
                result.DueTodayCount = Convert.ToInt32(dt.Rows[0]["DueTodayCount"]);
                result.OverDueCount = Convert.ToInt32(dt.Rows[0]["OverDueCount"]);
            }

            return result;
        }
    }
}
