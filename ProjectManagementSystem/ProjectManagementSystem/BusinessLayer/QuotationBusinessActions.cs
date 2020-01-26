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
        public bool AddItem(string name, string description, string unit, string specification, int unitRate, int labour, int addedBy, int margin, int tax, List<int> comboItems)
        {
            QuotationDataAccess obj = new QuotationDataAccess();
            var result = obj.AddItem(name, description, unit, specification, unitRate, labour, addedBy, margin,tax,comboItems);
            return result;
        }
        #region Get All Items

        public List<Item> GetAllItems(string searchTerm)
        {
            QuotationDataAccess obj = new QuotationDataAccess();
            var result = obj.GetAllItems( searchTerm);
            return MapItem(result);
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
                    obj.Tax = Convert.ToInt32(dr["UnitRate"]);
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
                quotation.CustomerName = dr["Name"].ToString();
                quotations.Add(quotation);
            }
            return quotations;
        }

        #endregion
        #region Get Quotation Versions for customer

        public List<QuotationView> GetQuotationVersion(string custId)
        {
            QuotationDataAccess obj = new QuotationDataAccess();
            var result = obj.GetQuotationUserVersions(custId);
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
                quotationItem.Unit = dr["Unit"].ToString();
                quotationItems.Add(quotationItem);
            }
            quotation.GrossMargin= float.Parse(result.Rows[0]["GrossMargin"].ToString());
            quotation.Total = float.Parse(result.Rows[0]["Total"].ToString());
            quotation.Version = Convert.ToInt32(result.Rows[0]["VersionNumber"].ToString());
            quotation.Items = quotationItems;
            return quotation;
        }
        public Quotation GetQuotationVersionDetails(string custId,string version)
        {
            QuotationDataAccess obj = new QuotationDataAccess();
            var result = obj.GetQuotationVersionDetails(custId, version);
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
                quotationItem.Total = float.Parse(dr["Rate"].ToString());
                quotationItem.Unit = dr["Unit"].ToString();
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
