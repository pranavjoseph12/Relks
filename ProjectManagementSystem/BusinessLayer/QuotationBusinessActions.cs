using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.DatabaseLayer;
using WebAPI.Models;
namespace WebAPI.BusinessLayer
{
    public class QuotationBusinessActions
    {
        public bool AddItem(string name, string description, string unit, string specification, int unitRate, int labour, int addedBy, int margin, int tax, List<int> comboItems,string selectedItem)
        {
            QuotationDataAccess obj = new QuotationDataAccess();
            var result = obj.AddItem(name, description, unit, specification, unitRate, labour, addedBy, margin,tax,comboItems,selectedItem);
            return result;
        }
        #region Get All Items

        public List<Item> GetAllItems(string searchTerm)
        {
            QuotationDataAccess obj = new QuotationDataAccess();
            var result = obj.GetAllItems( searchTerm,10000,1);
            return MapItem(result);
        }
        public ItemDetails GetItemDetails(string itemId)
        {
            QuotationDataAccess obj = new QuotationDataAccess();
            var result = obj.GetItemDetails(itemId);
            ItemDetails data = new ItemDetails();
            if (result != null && result.Rows.Count > 0)
            {
                List<int> childs = new List<int>();
                foreach (DataRow dn in result.Rows)
                {
                    childs.Add(Convert.ToInt32(dn["ChildItemId"]));
                }
                data.ChildItems = childs;
                DataRow dr = result.Rows[0];
                data.Name = dr["Name"].ToString();
                data.Description = dr["Description"].ToString();
                data.Unit = dr["Unit"].ToString();
                data.Specification = dr["Specification"].ToString();
                data.UnitRate = Convert.ToInt32(dr["UnitRate"]);
                data.ItemId = Convert.ToInt32(dr["ItemId"]);
                data.Labour = Convert.ToInt32(dr["Labour"]);
                data.Margin = Convert.ToInt32(dr["Margin"]);
                data.Tax = Convert.ToInt32(dr["Tax"]);
                data.IsComboItem = Convert.ToBoolean(dr["IsComboItem"]);
            }
            return data;
        }
        public List<ItemDetails> GetItems(string searchTerm, int numberOfRecords, int pageNumber)
        {
            QuotationDataAccess obj = new QuotationDataAccess();
            var result = obj.GetAllItems(searchTerm, numberOfRecords, pageNumber);
            List<ItemDetails> data = new List<ItemDetails>();
            if (result != null && result.Rows.Count > 0)
            {
                foreach (DataRow dr in result.Rows)
                {
                    ItemDetails det = new ItemDetails();
                    det.ItemId = Convert.ToInt32(dr["ItemId"]);
                    det.Name = dr["Name"].ToString();
                    det.UnitRate = Convert.ToInt32(dr["UnitRate"]);
                    det.TotalCount = Convert.ToInt32(dr["TotalCount"]);
                    det.IsComboItem = Convert.ToBoolean(dr["IsComboItem"]);
                    det.Unit = dr["Unit"].ToString();
                    data.Add(det);
                }
            }
            return data;
        }

        #endregion
        #region Get  Items By ID

        public List<ItemDetails> GetItemsByID(List<int> ItemIds)
        {
            QuotationDataAccess obj = new QuotationDataAccess();
            var result = obj.GetItemsById(ItemIds);
            return MapItemDetails(result);
        }

        #endregion
        private List<Item> MapItem(DataTable dt)
        {
            List<Item> result = new List<Item>();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Item obj = new Item();
                    obj.ItemId = Convert.ToInt32(dr["ItemId"]);
                    obj.Name = dr["Name"].ToString();
                    obj.UnitRate = Convert.ToInt32(dr["UnitRate"]);
                    obj.Labour = Convert.ToInt32(dr["Labour"].ToString());
                    result.Add(obj);
                }
            }

            return result;

        }
        private List<ItemDetails> MapItemDetails(DataTable dt)
        {
            List<ItemDetails> result = new List<ItemDetails>();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    ItemDetails obj = new ItemDetails();
                    obj.ItemId = Convert.ToInt32(dr["ItemId"]);
                    obj.Name = dr["Name"].ToString();
                    obj.Description = dr["Description"].ToString();
                    obj.Unit = dr["Unit"].ToString();
                    obj.Specification = dr["Specification"].ToString();
                    obj.UnitRate = Convert.ToInt32(dr["UnitRate"]);
                    obj.Labour = Convert.ToInt32(dr["Labour"]);
                    obj.Margin = Convert.ToInt32(dr["Margin"]);
                    obj.Tax = Convert.ToInt32(dr["Tax"]);
                    result.Add(obj);
                }
            }

            return result;

        }
        #region Save Quotation

        public bool SaveQuotation(WebAPI.Models.Quotation quotation)
        {
            QuotationDataAccess obj = new QuotationDataAccess();
            var result = obj.SaveQuotation(quotation);
            return result;
            //return true;
        }

        #endregion
        #region Get Quotations

        public List<QuotationView> GetAllQuotations(string CustName, int numberOfRecords, int pageNumber)
        {
            QuotationDataAccess obj = new QuotationDataAccess();
            var result = obj.GetAllQuotations(CustName,numberOfRecords,pageNumber);
            List<QuotationView> quotations = new List<QuotationView>();
            foreach (DataRow dr in result.Rows)
            {
                QuotationView quotation = new QuotationView();
                quotation.QuotationId = Convert.ToInt64(dr["QuotationId"]);
                quotation.CustomerId = Convert.ToInt64(dr["CustomerId"]);
                quotation.Version = Convert.ToInt32(dr["VersionNumber"]);
                quotation.TotalCount = Convert.ToInt32(dr["TotalCount"]);
                quotation.IsCustomer = Convert.ToBoolean(dr["IsCustomer"]);
                quotation.CustomerName = dr["Name"].ToString();
                quotations.Add(quotation);
            }
            return quotations;
        }

        #endregion
        #region Get Quotation Versions for customer

        public List<QuotationView> GetQuotationVersion(string custId, bool isCustomer)
        {
            QuotationDataAccess obj = new QuotationDataAccess();
            var result = obj.GetQuotationUserVersions(custId,isCustomer);
            List<QuotationView> quotations = new List<QuotationView>();
            foreach (DataRow dr in result.Rows)
            {
                QuotationView quotation = new QuotationView();
                quotation.QuotationId = Convert.ToInt64(dr["QuotationId"]);
                quotation.Version = Convert.ToInt32(dr["VersionNumber"]);
                quotations.Add(quotation);
            }
            return quotations;
        }

        #endregion
        #region Get Quotation Details By ID

        public Quotation GetQuotationDetailsById(string quotId)
        {
            QuotationDataAccess obj = new QuotationDataAccess();
            var result = obj.GetQuotationDetailsById(quotId);
            Quotation quotation = new Quotation();
            List<QuotationItem> quotationItems = new List<QuotationItem>();
            foreach (DataRow dr in result.Rows)
            {
                //quotationItems = new List<QuotationItem>();
                QuotationItem quotationItem = new QuotationItem();
                quotationItem.Name = dr["QuotationId"].ToString();
                quotationItem.IsItem = Convert.ToBoolean(dr["IsItem"].ToString());
                quotationItem.ItemId = Convert.ToInt32(dr["ItemId"]);
                quotationItem.Margin = float.Parse(dr["ItemMargin"].ToString());
                quotationItem.Name = dr["Name"].ToString();
                quotationItem.Quantity= dr["Quantity"].ToString();
                quotationItem.Description = dr["Description"].ToString();
                quotationItem.UnitRate= float.Parse(dr["UnitRate"].ToString());
                quotationItem.Total= float.Parse(dr["Rate"].ToString());
                quotationItem.Tax = float.Parse(dr["Tax"].ToString());
                quotationItem.Unit = dr["Unit"].ToString();
                quotationItems.Add(quotationItem);
            }
            quotation.GrossMargin= float.Parse(result.Rows[0]["GrossMargin"].ToString());
            quotation.Total = float.Parse(result.Rows[0]["Total"].ToString());
            quotation.Version = Convert.ToInt32(result.Rows[0]["VersionNumber"].ToString());
            quotation.Items = quotationItems;
            return quotation;
        }
        public byte[] GetQuotationExport(string custId, string version, bool isCustomer, bool isSupplyLabourTogether,string customerName)
        {
            QuotationDataAccess obj = new QuotationDataAccess();
            var result = obj.GetQuotationVersionDetails(custId, version, isCustomer);
            byte[] resultArray = null;
            using (ExcelPackage package = new ExcelPackage())
            {
                int startRowFrom = 2;
                string unitName = string.Empty;
                ExcelWorksheet workSheet = package.Workbook.Worksheets.Add("Electrical");
                int itemCOunt = 0;
                workSheet.Cells["A2"].Value = "SL. No";
                workSheet.Cells["B2"].Value = "Description";
                workSheet.Cells["C2"].Value = "Unit";
                workSheet.Cells["D2"].Value = "QTY";
                workSheet.Cells["E2"].Value = "Rate";
                workSheet.Cells["F2"].Value = "Amount";
                //workSheet.Cells["A2"].Style.Font.Size = 15;
                //workSheet.Cells["B2"].Style.Font.Size = 15;
                //workSheet.Cells["C2"].Style.Font.Size = 15;
                //workSheet.Cells["D2"].Style.Font.Size = 15;
                //workSheet.Cells["E2"].Style.Font.Size = 15;
                //workSheet.Cells["F2"].Style.Font.Size = 15;
                workSheet.Cells["A2"].Style.Font.Bold = true;
                workSheet.Cells["B2"].Style.Font.Bold = true;
                workSheet.Cells["C2"].Style.Font.Bold = true;
                workSheet.Cells["D2"].Style.Font.Bold = true;
                workSheet.Cells["E2"].Style.Font.Bold = true;
                workSheet.Cells["F2"].Style.Font.Bold = true;
                int itemProp = 0;
                if(!isSupplyLabourTogether)
                {
                    foreach (DataRow dr in result.Rows)
                    {
                        startRowFrom++;
                        itemProp++;
                        if (Convert.ToBoolean(dr["IsItem"]))
                        {
                            itemCOunt++;
                            itemProp = 0;
                            workSheet.Cells["A" + startRowFrom].Value = itemCOunt.ToString();
                            workSheet.Cells["B" + startRowFrom].Value = dr["Name"].ToString();
                            unitName = dr["Unit"].ToString();
                            workSheet.Cells["C" + startRowFrom].Value = string.Empty;
                            workSheet.Cells["D" + startRowFrom].Value = float.Parse(dr["Quantity"].ToString());
                            var rate = float.Parse(dr["ItemTotal"].ToString());
                            var qty= float.Parse(dr["Quantity"].ToString());
                            rate = rate / qty;
                            workSheet.Cells["E" + startRowFrom].Value = rate;
                            workSheet.Cells["F" + startRowFrom].Value = float.Parse(dr["ItemTotal"].ToString());
                            startRowFrom++;
                            workSheet.Cells["B" + startRowFrom].Value = dr["Description"].ToString();
                            startRowFrom++;
                            workSheet.Cells["B" + startRowFrom].Value = dr["Specification"].ToString();
                        }
                        else
                        {
                            // itemCOunt++;

                            if (itemProp == 1 || itemProp == 2)
                            {
                                if (itemProp == 1)
                                    workSheet.Cells["B" + startRowFrom].Value = "Supply";
                                else
                                    workSheet.Cells["B" + startRowFrom].Value = "Labour";
                                workSheet.Cells["C" + startRowFrom].Value = unitName;
                                workSheet.Cells["D" + startRowFrom].Value = float.Parse(dr["Quantity"].ToString());
                                var rate = float.Parse(dr["ItemTotal"].ToString());
                                var qty = float.Parse(dr["Quantity"].ToString());
                                rate = rate / qty;
                                workSheet.Cells["E" + startRowFrom].Value = rate;
                                workSheet.Cells["F" + startRowFrom].Value = float.Parse(dr["ItemTotal"].ToString());
                            }

                        }


                    }
                    startRowFrom++;
                    workSheet.Cells["B" + startRowFrom].Value = "TOTAL : ";
                    workSheet.Cells["C" + startRowFrom].Value = float.Parse(result.Rows[0]["Total"].ToString());

                    // add the content into the Excel file  
                    //workSheet.Cells["A" + startRowFrom].LoadFromDataTable(result, true);

                    // autofit width of cells with small content  
                    int columnIndex = 1;
                    foreach (DataColumn column in result.Columns)
                    {
                        ExcelRange columnCells = workSheet.Cells[1, columnIndex, workSheet.Dimension.End.Row, columnIndex];
                        int maxLength = columnCells.Max(cell => cell.Value == null ? 0 : cell.Value.ToString().Count());
                        if (maxLength < 30)
                        {
                            workSheet.Column(columnIndex).AutoFit();
                        }


                        if (columnIndex == 6)
                            break;
                    }
                    workSheet.Column(2).Width = 50;
                    // format header - bold, yellow on black  
                    using (ExcelRange r = workSheet.Cells[startRowFrom, 1, startRowFrom, 6])
                    {
                        r.Style.Font.Color.SetColor(System.Drawing.Color.White);
                        r.Style.Font.Bold = true;
                        r.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        r.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#1fb5ad"));
                    }

                    // format cells - add borders  
                    using (ExcelRange r = workSheet.Cells[2, 1, 2 + (result.Rows.Count + (result.Rows.Count * 2 / 3)), 6])
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
                }
                else
                {
                    float prevRate = 0;
                    float prevtotal = 0;
                    int prevIndex = 2;
                    foreach (DataRow dr in result.Rows)
                    {
                        
                        itemProp++;
                        if (Convert.ToBoolean(dr["IsItem"]))
                        {
                            startRowFrom++;
                            itemCOunt++;
                            itemProp = 0;
                            prevtotal = 0;
                            prevRate = 0;
                            workSheet.Cells["A" + startRowFrom].Value = itemCOunt.ToString();
                            workSheet.Cells["B" + startRowFrom].Value = dr["Name"].ToString();
                            unitName = dr["Unit"].ToString();
                            workSheet.Cells["C" + startRowFrom].Value = dr["Unit"].ToString();
                            workSheet.Cells["D" + startRowFrom].Value = float.Parse(dr["Quantity"].ToString());
                            
                            var rate = float.Parse(dr["ItemTotal"].ToString());
                            var qty= float.Parse(dr["Quantity"].ToString());
                            rate = rate / qty;
                            workSheet.Cells["E" + startRowFrom].Value = rate;
                            workSheet.Cells["F" + startRowFrom].Value = float.Parse(dr["ItemTotal"].ToString());
                            startRowFrom++;
                            workSheet.Cells["B" + startRowFrom].Value = dr["Description"].ToString();
                            startRowFrom++;
                            workSheet.Cells["B" + startRowFrom].Value = dr["Specification"].ToString();
                            
                        }
                    }
                   
                    startRowFrom++;
                    workSheet.Cells["B" + startRowFrom].Value = "TOTAL : ";
                    workSheet.Cells["C" + startRowFrom].Value = float.Parse(result.Rows[0]["Total"].ToString());

                    // add the content into the Excel file  
                    //workSheet.Cells["A" + startRowFrom].LoadFromDataTable(result, true);

                    // autofit width of cells with small content  
                    int columnIndex = 1;
                    foreach (DataColumn column in result.Columns)
                    {
                        ExcelRange columnCells = workSheet.Cells[1, columnIndex, workSheet.Dimension.End.Row, columnIndex];
                        int maxLength = columnCells.Max(cell => cell.Value == null ? 0 : cell.Value.ToString().Count());
                        if (maxLength < 30)
                        {
                            workSheet.Column(columnIndex).AutoFit();
                        }


                        if (columnIndex == 6)
                            break;
                    }
                    workSheet.Column(2).Width = 50;
                    // format header - bold, yellow on black  
                    using (ExcelRange r = workSheet.Cells[startRowFrom, 1, startRowFrom, 6])
                    {
                        r.Style.Font.Color.SetColor(System.Drawing.Color.White);
                        r.Style.Font.Bold = true;
                        r.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        r.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#1fb5ad"));
                    }

                    // format cells - add borders  
                    using (ExcelRange r = workSheet.Cells[2, 1,  result.Rows.Count+2, 6])
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

                if (!String.IsNullOrEmpty(customerName))
                {
                    workSheet.Cells["A1"].Value = customerName;
                    workSheet.Cells["A1"].Style.Font.Size = 20;

                    workSheet.InsertColumn(1, 1);
                    workSheet.InsertRow(1, 1);
                    workSheet.Column(1).Width = 5;
                   
                }

                resultArray = package.GetAsByteArray();
            }
            return resultArray;
        }
        public Quotation GetQuotationVersionDetails(string custId,string version, bool isCustomer)
        {
            QuotationDataAccess obj = new QuotationDataAccess();
            var result = obj.GetQuotationVersionDetails(custId, version, isCustomer);
            Quotation quotation = new Quotation();
            List<QuotationItem> quotationItems = new List<QuotationItem>();
            foreach (DataRow dr in result.Rows)
            {
                //quotationItems = new List<QuotationItem>();
                QuotationItem quotationItem = new QuotationItem();
                quotationItem.Name = dr["QuotationId"].ToString();
                quotationItem.IsItem = Convert.ToBoolean(dr["IsItem"].ToString());
                quotationItem.ItemId = Convert.ToInt32(dr["ItemId"]);
                quotationItem.Margin = float.Parse(dr["ItemMargin"].ToString());
                quotationItem.Name = dr["Name"].ToString();
                quotationItem.Quantity = dr["Quantity"].ToString();
                quotationItem.Description = dr["Description"].ToString();
                quotationItem.UnitRate = float.Parse(dr["UnitRate"].ToString());
                quotationItem.Total = float.Parse(dr["Total"].ToString());
                quotationItem.Rate = float.Parse(dr["Rate"].ToString());
                quotationItem.ItemTotal = float.Parse(dr["ItemTotal"].ToString());
                quotationItem.Unit = dr["Unit"].ToString();
                //quotationItem.Unit = dr["Unit"].ToString();
                quotationItems.Add(quotationItem);
            }
            quotation.GrossMargin = float.Parse(result.Rows[0]["GrossMargin"].ToString());
            quotation.Total = float.Parse(result.Rows[0]["Total"].ToString());
            quotation.Version = Convert.ToInt32(result.Rows[0]["VersionNumber"].ToString());
            quotation.Items = quotationItems;
            return quotation;
        }

        #endregion
    }
}
